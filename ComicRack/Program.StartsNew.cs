using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using cYo.Common.Collections;
using cYo.Common.Compression;
using cYo.Common.Drawing;
using cYo.Common.Localize;
using cYo.Common.Mathematics;
using cYo.Common.Presentation.Tao;
using cYo.Common.Runtime;
using cYo.Common.Text;
using cYo.Common.Threading;
using cYo.Common.Win32;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display.Forms;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Properties;
using Microsoft.Win32;

namespace cYo.Projects.ComicRack.Viewer;

/// <summary>
/// <see cref="Program.StartNew(string[])"/> method, and any helpers.
/// </summary>
public static partial class Program
{
    // Program.SplitIconKeysWithYearAndMonth
    private static readonly Regex dateRangeRegex = new(
        @"\((?<startYear>\d{4})(?:_(?<startMonth>\d{2}))?-(?<endYear>\d{4})(?:_(?<endMonth>\d{2}))?\)",
        RegexOptions.Compiled
    );

    /// <summary>
    /// Effectively Run(). StartUp logic, concluding in displaying <see cref="MainForm"/>.
    /// </summary>
    /// <param name="args">Command line arguments</param>
    private static void StartNew(string[] args)
    {
        Thread.CurrentThread.Name = "GUI Thread";
        Diagnostic.StartWatchDog(CrashDialog.OnBark);
        ComicBookValueMatcher.RegisterMatcherType(typeof(ComicBookPluginMatcher));
        ComicBookValueMatcher.RegisterMatcherType(typeof(ComicBookExpressionMatcher));
        Settings = Settings.Load(defaultSettingsFile);
        Settings.RunCount++;
        CommandLineParser.Parse(ImageDisplayControl.HardwareSettings);
        CommandLineParser.Parse(EngineConfiguration.Default);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(defaultValue: false);
        ShellFile.DeleteAPI = ExtendedSettings.DeleteAPI;
        DatabaseManager.FirstDatabaseAccess += delegate
        {
            StartupProgress(TR.Messages["OpenDatabase", "Opening Database"], -1);
        };
        DatabaseManager.BackgroundSaveInterval = ExtendedSettings.DatabaseBackgroundSaving;
        WirelessSyncProvider.StartListen();
        WirelessSyncProvider.ClientSyncRequest += (object s, WirelessSyncProvider.ClientSyncRequestArgs e) =>
        {
            if (MainForm != null)
            {
                IPAddress address = s as IPAddress;
                e.IsPaired = QueueManager.Devices.Any((DeviceSyncSettings d) => d.DeviceKey == e.Key);
                if (e.IsPaired && address != null)
                {
                    MainForm.BeginInvoke(delegate
                    {
                        MainForm.StoreWorkspace(); // save workspace before sync, so sorted lists key are up to date
                        QueueManager.SynchronizeDevice(e.Key, address);
                    });
                }
            }
        };
        if (!ExtendedSettings.DisableAutoTuneSystem)
        {
            AutoTuneSystem();
        }
        ListExtensions.ParallelEnabled = EngineConfiguration.Default.EnableParallelQueries;
        if (EngineConfiguration.Default.IgnoredArticles != null)
        {
            StringUtility.Articles = EngineConfiguration.Default.IgnoredArticles;
        }
        ComicLibrary.QueryCacheMode = ExtendedSettings.QueryCacheMode;
        ComicLibrary.BackgroundQueryCacheUpdate = !ExtendedSettings.DisableBackgroundQueryCacheUpdate;
        ComicBook.EnableGroupNameCompression = ExtendedSettings.EnableGroupNameCompression;
        try
        {
            string text = ExtendedSettings.Language ?? Settings.CultureName;
            if (!string.IsNullOrEmpty(text))
            {
                SetUICulture(text);
                TR.DefaultCulture = new CultureInfo(text);
            }
        }
        catch (Exception)
        {
        }
        if (!ExtendedSettings.LoadDatabaseInForeground)
        {
            ThreadUtility.RunInBackground("Loading Database", delegate
            {
                InitializeDatabase(0, null);
            });
        }
        if (!ExtendedSettings.StartHidden && Settings.ShowSplash)
        {
            ManualResetEvent mre = new ManualResetEvent(initialState: false);
            ThreadUtility.RunInBackground("Splash Thread", delegate
            {
                splash = new Splash
                {
                    Fade = true
                };
                splash.Location = splash.Bounds.Align(Screen.FromPoint(Settings.CurrentWorkspace.FormBounds.Location).Bounds, ContentAlignment.MiddleCenter).Location;
                splash.VisibleChanged += delegate
                {
                    mre.Set();
                };
                splash.Closed += delegate
                {
                    splash = null;
                };
                splash.ShowDialog();
            });
            mre.WaitOne(5000, exitContext: false);
        }
        try
        {
            if (ExtendedSettings.LoadDatabaseInForeground)
            {
                string msgOpenDb = TR.Messages["OpenDatabase", "Opening Database"];
                StartupProgress(msgOpenDb, 0);
                InitializeDatabase(0, msgOpenDb);
            }
            StartupProgress(TR.Messages["LoadCustomSettings", "Loading custom settings"], 20);
            IEnumerable<string> defaultLocations = IniFile.GetDefaultLocations(DefaultIconPackagesPath);
            ComicBook.PublisherIcons.AddRange(ZipFileFolder.CreateFromFiles(defaultLocations, "Publishers*.zip"), SplitIconKeysWithYearAndMonth);
            ComicBook.AgeRatingIcons.AddRange(ZipFileFolder.CreateFromFiles(defaultLocations, "AgeRatings*.zip"), SplitIconKeys);
            ComicBook.FormatIcons.AddRange(ZipFileFolder.CreateFromFiles(defaultLocations, "Formats*.zip"), SplitIconKeys);
            ComicBook.SpecialIcons.AddRange(ZipFileFolder.CreateFromFiles(defaultLocations, "Special*.zip"), SplitIconKeys);
            ComicBook.GenericIcons = CreateGenericsIcons(defaultLocations, "*.zip", "_", SplitIconKeys);
            ToolStripRenderer renderer;
            if (ExtendedSettings.SystemToolBars)
            {
                renderer = new ToolStripSystemRenderer();
            }
            else
            {
                bool flag = Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major == 5;
                ProfessionalColorTable professionalColorTable = ((!(ExtendedSettings.ForceTanColorSchema || flag)) ? ((ProfessionalColorTable)new OptimizedProfessionalColorTable()) : ((ProfessionalColorTable)new OptimizedTanColorTable()));
                renderer = new ToolStripProfessionalRenderer(professionalColorTable)
                {
                    RoundedEdges = false
                };
            }
            ToolStripManager.Renderer = renderer;
            if (ExtendedSettings.DisableHardware)
            {
                ImageDisplayControl.HardwareAcceleration = ImageDisplayControl.HardwareAccelerationType.Disabled;
            }
            else
            {
                ImageDisplayControl.HardwareAcceleration = ((!ExtendedSettings.ForceHardware) ? ImageDisplayControl.HardwareAccelerationType.Enabled : ImageDisplayControl.HardwareAccelerationType.Forced);
            }
            if (ExtendedSettings.DisableMipMapping)
            {
                ImageDisplayControl.HardwareSettings.MipMapping = false;
            }
            Lists = new DefaultLists(() => Database.Books, IniFile.GetDefaultLocations(DefaultListsFile));
            StartupProgress(TR.Messages["InitCache", "Initialize Disk Caches"], 30);
            CacheManager = new CacheManager(DatabaseManager, Paths, Settings, Resources.ResourceManager);
            QueueManager = new QueueManager(DatabaseManager, CacheManager, Settings, Settings.Devices);
            QueueManager.ComicScanned += ScannerCheckFileIgnore;
            Settings.IgnoredCoverImagesChanged += IgnoredCoverImagesChanged;
            IgnoredCoverImagesChanged(null, EventArgs.Empty);
            SystemEvents.PowerModeChanged += SystemEventsPowerModeChanged;
        }
        catch (Exception ex2)
        {
            MessageBox.Show(StringUtility.Format(TR.Messages["FailedToInitialize", "Failed to initialize ComicRack: {0}"], ex2.Message));
            return;
        }
        StartupProgress(TR.Messages["ReadNewsFeed", "Reading News Feed"], 40);
        News = NewsStorage.Load(defaultNewsFile);
        if (News.Subscriptions.Count == 0)
        {
            News.Subscriptions.Add(new NewsStorage.Subscription(DefaultNewsFeed, "ComicRack News"));
        }
        else
        {
            //Because the default NewsFeeds.xml in the Data already as the old url inside it
            var oldSub = News.Subscriptions.FirstOrDefault(x => x.Url != DefaultNewsFeed);
            if (oldSub != null)
            {
                //Since the feeds doesn't match with the default url, we remove it, and add or new one.
                News.Subscriptions.Remove(oldSub);
                News.Subscriptions.Add(new NewsStorage.Subscription(DefaultNewsFeed, "ComicRack News"));
            }
        }
        StartupProgress(TR.Messages["CreateMainWindow", "Creating Main Window"], 50);
        if (ExtendedSettings.DisableScriptOptimization)
        {
            PythonCommand.Optimized = false;
        }
        if (ExtendedSettings.ShowScriptConsole)
        {
            ScriptConsole = new ScriptOutputForm();
            TextBoxStream logOutput = (TextBoxStream)(PythonCommand.Output = new TextBoxStream(ScriptConsole.Log));
            PythonCommand.EnableLog = true;
            WebComic.SetLogOutput(logOutput);
            ScriptConsole.Show();
        }
        NetworkManager = new NetworkManager(DatabaseManager, CacheManager, Settings, ExtendedSettings.PrivateServerPort, ExtendedSettings.InternetServerPort, ExtendedSettings.DisableBroadcast);
        MainForm = new MainForm();
        MainForm.FormClosed += MainFormFormClosed;
        MainForm.FormClosing += MainFormFormClosing;
        Application.AddMessageFilter(new MouseWheelDelegater());
        MainForm.Show();
        MainForm.Update();
        MainForm.Activate();
        if (splash != null)
        {
            splash.Invoke(splash.Close);
        }
        ThreadUtility.RunInBackground("Starting Network", NetworkManager.Start);
        ThreadUtility.RunInBackground("Generate Language Pack Info", delegate
        {
            int num = InstalledLanguages.Length;
        });
        if (!string.IsNullOrEmpty(DatabaseManager.OpenMessage))
        {
            MessageBox.Show(MainForm, DatabaseManager.OpenMessage, TR.Messages["Attention", "Attention"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        if (Settings.NewsStartup)
        {
            MainForm.ShowNews(always: false);
        }
        Application.Run(MainForm);
    }

    #region Helpers
    private class MouseWheelDelegater : IMessageFilter
    {
        private IntPtr lastHandle;

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case 512:
                    lastHandle = m.HWnd;
                    break;
                case 522:
                case 526:
                    if (!(m.HWnd == lastHandle))
                    {
                        SendMessage(lastHandle, m.Msg, m.WParam, m.LParam);
                        return true;
                    }
                    break;
            }
            return false;
        }
    }

    public static Dictionary<string, ImagePackage> CreateGenericsIcons(IEnumerable<string> folders, string searchPattern, string trigger, Func<string, IEnumerable<string>> mapKeys = null)
    {
        Dictionary<string, ImagePackage> dictionary = new Dictionary<string, ImagePackage>();
        foreach (var generic in ZipFileFolder.CreateDictionaryFromFiles(folders, searchPattern, trigger))
        {
            var icons = new ImagePackage { EnableWidthCropping = true };
            icons.Add(generic.Value, mapKeys);
            dictionary.Add(generic.Key, icons);
        }
        return dictionary;
    }

    private static void SetUICulture(string culture)
    {
        if (!string.IsNullOrEmpty(culture))
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }
            catch (Exception)
            {
            }
        }
    }

    private static IEnumerable<string> SplitIconKeys(string value)
    {
        return value.Split(',', '#');
    }

    private static IEnumerable<string> SplitIconKeysWithYearAndMonth(string value)
    {
        foreach (string key in SplitIconKeys(value))
        {
            Match dateMatch = dateRangeRegex.Match(key);

            if (!dateMatch.Success)
            {
                yield return key;
                continue;
            }

            Group startYearGroup = dateMatch.Groups["startYear"];
            Group endYearGroup = dateMatch.Groups["endYear"];
            Group startMonthGroup = dateMatch.Groups["startMonth"];
            Group endMonthGroup = dateMatch.Groups["endMonth"];
            string baseKey = dateRangeRegex.Replace(key, string.Empty);

            int startYear = int.Parse(startYearGroup.Value);
            int endYear = int.Parse(endYearGroup.Value);
            int startMonth = startMonthGroup.Success ? int.Parse(startMonthGroup.Value) : 1;
            int endMonth = endMonthGroup.Success ? int.Parse(endMonthGroup.Value) : 12;

            for (int year = startYear; year <= endYear; year++)
            {
                // Output the Year only for the files that don't have a month
                yield return $"{baseKey}({year})";

                int startMonthValue = (year == startYear) ? startMonth : 1;
                int endMonthValue = (year == endYear) ? endMonth : 12;

                for (int month = startMonthValue; month <= endMonthValue; month++)
                {
                    yield return $"{baseKey}({year}_{month:00})";
                }
            }
        }
    }

    private static void AutoTuneSystem()
    {
        if (ExtendedSettings.IsQueryCacheModeDefault && EngineConfiguration.Default.IsEnableParallelQueriesDefault && ImageDisplayControl.HardwareSettings.IsMaxTextureMemoryMBDefault && ImageDisplayControl.HardwareSettings.IsTextureManagerOptionsDefault)
        {
            int processorCount = Environment.ProcessorCount;
            // TODO: Query the video memory instead of physical memory
            int num = (int)(MemoryInfo.InstalledPhysicalMemory / 1024 / 1024);
            int cpuSpeedInHz = MemoryInfo.CpuSpeedInHz;
            if (num <= 512)
                ExtendedSettings.QueryCacheMode = QueryCacheMode.Disabled;

            EngineConfiguration.Default.EnableParallelQueries = processorCount > 1;
            if (cpuSpeedInHz < 2000)
                ExtendedSettings.OptimizedListScrolling = true;

            ImageDisplayControl.HardwareSettings.MaxTextureMemoryMB = (num / 8).Clamp(32, 2048);
            if (ImageDisplayControl.HardwareSettings.MaxTextureMemoryMB <= 64)
            {
                ImageDisplayControl.HardwareSettings.TextureManagerOptions |= TextureManagerOptions.BigTexturesAs16Bit;
                ImageDisplayControl.HardwareSettings.TextureManagerOptions &= ~TextureManagerOptions.MipMapFilter;
            }
        }
    }

    private static bool InitializeDatabase(int startPercent, string readDbMessage)
    {
        return DatabaseManager.Open(Paths.DatabasePath, ExtendedSettings.DataSource, ExtendedSettings.DoNotLoadQueryCaches, string.IsNullOrEmpty(readDbMessage) ? null : ((Action<int>)((int percent) =>
        {
            StartupProgress(readDbMessage, startPercent + percent / 5);
        })));
    }
    #endregion
}
