using cYo.Common.Localize;
using cYo.Common.Text;
using cYo.Common.Threading;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Backup;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Engine.IO.Cache;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Properties;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

/// <summary>Services, and methods related to those services</summary>
public static class AppServices
{
    /// <summary>The GUI. All of it.</summary>
    public static MainForm MainForm { get; private set; }
    public static ScriptOutputForm ScriptConsole { get; set; }
    public static NewsStorage News { get; private set; }

    // NetWorkManager
    public static NetworkManager NetworkManager { get; private set; }

    // CacheManager
    public static CacheManager CacheManager { get; private set; }
    public static ImagePool ImagePool => CacheManager.ImagePool;
    public static FileCache InternetCache => CacheManager.InternetCache;

    // DatabaseManager
    public static readonly DatabaseManager DatabaseManager = new(); // field
    public static ComicBookFactory BookFactory => DatabaseManager.BookFactory;
    public static ComicDatabase Database => DatabaseManager.Database;

    // QueueManager
    public static QueueManager QueueManager { get; private set; }
    public static ComicScanner Scanner => QueueManager.Scanner;

    public static BackupManager BackupManager { get; private set; }

    public static void RunMainForm() => Application.Run(MainForm);

    #region Bootstrap    
    /// <summary>Run the stuff that was ran first, first. Until proven it doesn't need special treatment.</summary>
    public static void RunPreInitialization()
    {
        DatabaseManager.FirstDatabaseAccess += BootstrapEventHandlers.OnFirstDatabaseAccess;
        
        WirelessSyncProvider.StartListen();
        WirelessSyncProvider.ClientSyncRequest += BootstrapEventHandlers.OnClientSyncRequest;
    }

    /// <summary>
    /// Initializes <see cref="Engine.CacheManager"/> and <see cref="Engine.QueueManager"/>.
    /// Applies initial database settings and event event subscriptions.
    /// </summary>
    /// <param name="dbBackgroundSaving">The database background saving interval.</param>
    /// <remarks><see cref="Engine.DatabaseManager"/> and <see cref="Engine.NetworkManager"/> are initialized later.</remarks>
    public static void Initialize(int dbBackgroundSaving)
    {
        Bootstrap.UpdateStartupProgress(TR.Messages["InitCache", "Initialize Disk Caches"], 30);

        CacheManager = new CacheManager(DatabaseManager, AppConfig.Paths, AppConfig.Settings, Resources.ResourceManager);
        QueueManager = new QueueManager(DatabaseManager, CacheManager, AppConfig.Settings, AppConfig.Settings.Devices);
        QueueManager.ComicScanned += BootstrapEventHandlers.OnComicScannedIgnoreFile;
        BackupManager = new BackupManager(
            AppConfig.Settings.BackupManager,
            AppConfig.Paths,
            AppConstants.DefaultSettingsFile,
            AppConstants.DefaultListsFile,
            AppConstants.DefaultIconPackagesPath);
        if (AppConfig.Settings.BackupManager.OnStartup) BackupManager.RunBackup();
        DatabaseManager.BackgroundSaveInterval = dbBackgroundSaving;

        AppConfig.Settings.IgnoredCoverImagesChanged += BootstrapEventHandlers.OnIgnoredCoverImagesChanged;
        BootstrapEventHandlers.OnIgnoredCoverImagesChanged(null, EventArgs.Empty);

        SystemEvents.PowerModeChanged += BootstrapEventHandlers.OnPowerModeChanged;
    }

    public static bool InitializeDatabase(int startPercent, string readDbMessage, string dataSource, bool dontLoadQueryCache)
    {
        return DatabaseManager.Open(
            AppConfig.Paths.DatabasePath,
            dataSource,
            dontLoadQueryCache,
            string.IsNullOrEmpty(readDbMessage)
                ? null
                : (int percent) => Bootstrap.UpdateStartupProgress(readDbMessage, startPercent + percent / 5)
            );
    }

    public static void InitializeNetworkManager(int privatePort, int internetPort, bool disableBroadcast)
    {
        NetworkManager = new NetworkManager(
            DatabaseManager,
            CacheManager,
            AppConfig.Settings,
            privatePort,
            internetPort,
            disableBroadcast
        );
        ThreadUtility.RunInBackground("Starting Network", NetworkManager.Start);
    }

    public static void InitializeMainForm()
    {
        MainForm = new MainForm();
        MainForm.FormClosed += BootstrapEventHandlers.OnMainFormFormClosed;
        MainForm.FormClosing += BootstrapEventHandlers.OnMainFormFormClosing;
        MainForm.Show();
        MainForm.Update();
        MainForm.Activate();
    }

    public static void LoadNews(string newsFilePath, string newsFeedPath)
    {
        Bootstrap.UpdateStartupProgress(TR.Messages["ReadNewsFeed", "Reading News Feed"], 40);
        News = NewsStorage.Load(newsFilePath);
        if (News.Subscriptions.Count == 0)
        {
            News.Subscriptions.Add(new NewsStorage.Subscription(newsFeedPath, "ComicRack News"));
        }
        else
        {
            //Because the default NewsFeeds.xml in the Data already as the old url inside it
            var oldSub = News.Subscriptions.FirstOrDefault(x => x.Url != newsFeedPath);
            if (oldSub != null)
            {
                //Since the feeds doesn't match with the default url, we remove it, and add or new one.
                News.Subscriptions.Remove(oldSub);
                News.Subscriptions.Add(new NewsStorage.Subscription(newsFeedPath, "ComicRack News"));
            }
        }
    }

    public static void ShowScriptConsole()
    {
        ScriptConsole = new ScriptOutputForm();
        TextBoxStream logOutput = (TextBoxStream)(PythonCommand.Output = new TextBoxStream(ScriptConsole.Log));
        PythonCommand.EnableLog = true;
        WebComic.SetLogOutput(logOutput);
        ScriptConsole.Show();
    }

    public static void CleanUp()
    {
        try
        {
            NetworkManager.Dispose();
            SystemEvents.PowerModeChanged -= BootstrapEventHandlers.OnPowerModeChanged;
            QueueManager.Dispose();
            News.Save(AppConstants.DefaultNewsFile);
            AppConfig.Settings.Save(AppConstants.DefaultSettingsFile);
            ImagePool.Dispose();
            DatabaseManager.Dispose();
            if (AppConfig.Settings.BackupManager.OnExit) BackupManager.RunBackup(false);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                StringUtility.Format(
                    TR.Messages["ErrorShutDown", "There was an error shutting down the application:\r\n{0}"],
                    ex.Message
                ),
                TR.Messages["Error", "Error"],
                MessageBoxButtons.OK,
                MessageBoxIcon.Hand
            );
        }
    }
    #endregion
}
