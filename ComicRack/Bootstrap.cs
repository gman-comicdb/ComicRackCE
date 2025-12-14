using cYo.Common.Collections;
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
using cYo.Projects.ComicRack.Engine.IO.Provider;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

/// <summary>Responsible for getting ComicRackCE up and running.</summary>
internal static class Bootstrap
{
    private static Splash splash;

    public static ExtendedSettings ExtendedSettings => AppConfig.ExtendedSettings;

    private static class Native
    {
        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();
    }

    /// <summary>
    /// Process command line arguments that should be processed prior to anything else.<br/>
    /// - Wait for previous instance to terminate if there is one.<br/>
    /// - Attempt to Register formats if they have been provided and populate <paramref name="exitCode"/>.
    /// </summary>
    /// <param name="exitCode">The exit code.</param>
    /// <returns><paramref name="true"/> if Bootstrap should continue, otherwise <paramref name="false"/></returns>
    public static bool PreBootstrap(out int exitCode)
    {
        exitCode = 0;
        if (ExtendedSettings.WaitPid != 0)
            try
            {
                Process.GetProcessById(ExtendedSettings.WaitPid).WaitForExit(30000);
            }
            catch { }

        if (!string.IsNullOrEmpty(ExtendedSettings.RegisterFormats))
        {
            if (!FileFormat.RegisterFormats(
                ExtendedSettings.RegisterFormats,
                AppConstants.ComicRackTypeId,
                AppConstants.ComicRackDocumentName))
                exitCode = 1;
            return false; // don't continue to BootStrap
        }
        return true; // continue to BootStrap
    }

    /// <summary>
    /// Runs remaining pre-startup that was in <see cref="Program.Main"/>, until proven it doesn't need to be here specifically.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static void RunBootstrap(string[] args)
    {
        Native.SetProcessDPIAware();
        ServicePointManager.Expect100Continue = false;

        TR.ResourceFolder = new PackedLocalize(TR.ResourceFolder);
        NativeLibraryHelper.RegisterDirectory(); //Add the resources directory to the search path for natives dll's
        Control.CheckForIllegalCrossThreadCalls = false;
        ItemMonitor.CatchThreadInterruptException = true;

        SingleInstance singleInstance = new SingleInstance("ComicRackSingleInstance", InitializeNewInstance, StartLast);
        singleInstance.Run(args);

        // simplistic alternate approach to Single Instance, using mutex + named pipes instead of WCF
        #region SingleInstanceManager: Mutex + Named Pipe
        //using var singleInstance = new SingleInstanceManager("ComicRackCommunityEdition");
        //if (!singleInstance.IsFirstInstance)
        //{
        //    singleInstance.SendToRunningInstance(args);
        //    return;
        //}
        //singleInstance.StartServer(receivedArgs => StartLast(receivedArgs));
        //InitializeNewInstance(args);
        #endregion
    }

    private static void InitializeNewInstance(string[] args)
    {
        // makes sense for this to be the first thing
        Thread.CurrentThread.Name = "GUI Thread";
        Diagnostic.StartWatchDog(CrashDialog.OnBark);
        AppConfig.LoadSettings(AppConstants.DefaultSettingsFile);

        // UI Culture + Theme
        AppEnvironment.Initialize(ExtendedSettings.Language ?? AppConfig.Settings.CultureName, ExtendedSettings.Theme);

        // it seems unnecessary to have to this here; doing it to keep order (roughly) the same
        AppServices.RunPreInitialization();
        if (!ExtendedSettings.DisableAutoTuneSystem)
            AutoTuneSystem(); // this is quite intensive. Maybe disable after first run unless environment changes? (Host, CPU, GPU, RAM)
        AppConfig.ApplyLibrarySettings();

        if (!ExtendedSettings.LoadDatabaseInForeground) // (if set to background) load database
            ThreadUtility.RunInBackground(
                "Loading Database",
                () => AppServices.InitializeDatabase(0, null, ExtendedSettings.DataSource, ExtendedSettings.DoNotLoadQueryCaches));

        if (!ExtendedSettings.StartHidden && AppConfig.Settings.ShowSplash) // show splash screen
        {
            ManualResetEvent mre = new ManualResetEvent(initialState: false);
            ThreadUtility.RunInBackground("Splash Thread", () => AppUtility.NewSplashDialog(splash, mre));
            mre.WaitOne(5000, exitContext: false);
        }

        try
        {
            RunInitialization();
        }
        catch (Exception ex)
        {
            MessageBox.Show(StringUtility.Format(
                TR.Messages["FailedToInitialize", "Failed to initialize ComicRack: {0}"],
                ex.Message
            ));
            return;
        }

        UpdateStartupProgress(TR.Messages["CreateMainWindow", "Creating Main Window"], 50);

        if (ExtendedSettings.ShowScriptConsole)
            AppServices.ShowScriptConsole();
        AppServices.InitializeNetworkManager(
            privatePort: ExtendedSettings.PrivateServerPort,
            internetPort: ExtendedSettings.InternetServerPort,
            disableBroadcast: ExtendedSettings.DisableBroadcast);
        AppServices.InitializeMainForm();
        Application.AddMessageFilter(new Mouse.MouseWheelDelegator());

        splash?.Invoke(splash.Close);

        ThreadUtility.RunInBackground(
            "Generate Language Pack Info",
            () => { int num = AppConfig.InstalledLanguages.Length; }); // ... what is the point of this?

        if (!string.IsNullOrEmpty(AppServices.DatabaseManager.OpenMessage))
            MessageBox.Show(
                AppServices.MainForm,
                AppServices.DatabaseManager.OpenMessage,
                TR.Messages["Attention", "Attention"],
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation
            );

        AppServices.RunMainForm();
    }

    // this needs to be moved to ImageDisplayControl
    public static void AutoTuneSystem()
    {
        bool shouldRunAutoTune = ExtendedSettings.IsQueryCacheModeDefault
            && EngineConfiguration.Default.IsEnableParallelQueriesDefault
            && ImageDisplayControl.HardwareSettings.IsMaxTextureMemoryMBDefault
            && ImageDisplayControl.HardwareSettings.IsTextureManagerOptionsDefault;

        if (shouldRunAutoTune)
        {
            int processorCount = Environment.ProcessorCount;
            // TODO: Query the video memory instead of physical memory
            int mbPhysMemory = (int)(MemoryInfo.InstalledPhysicalMemory / 1024 / 1024);
            int cpuSpeedInHz = MemoryInfo.CpuSpeedInHz;
            if (mbPhysMemory <= 512)
                ExtendedSettings.QueryCacheMode = QueryCacheMode.Disabled;

            EngineConfiguration.Default.EnableParallelQueries = processorCount > 1;
            if (cpuSpeedInHz < 2000)
                ExtendedSettings.OptimizedListScrolling = true;

            ImageDisplayControl.HardwareSettings.MaxTextureMemoryMB = (mbPhysMemory / 8).Clamp(32, 2048);
            if (ImageDisplayControl.HardwareSettings.MaxTextureMemoryMB <= 64)
            {
                ImageDisplayControl.HardwareSettings.TextureManagerOptions |= TextureManagerOptions.BigTexturesAs16Bit;
                ImageDisplayControl.HardwareSettings.TextureManagerOptions &= ~TextureManagerOptions.MipMapFilter;
            }
        }
    }

    public static void UpdateStartupProgress(string message, int progress)
    {
        //Splash splash = Program.splash;
        if (splash != null)
            splash.Message = splash.Message.AppendWithSeparator("\n", message);
        if (progress >= 0)
            splash.Progress = progress;
    }

    #region Initialization
    private static void RunInitialization()
    {
        if (ExtendedSettings.LoadDatabaseInForeground)
            AppServices.InitializeDatabase(
                0,
                TR.Messages["OpenDatabase", "Opening Database"],
                ExtendedSettings.DataSource,
                ExtendedSettings.DoNotLoadQueryCaches);

        UpdateStartupProgress(TR.Messages["LoadCustomSettings", "Loading custom settings"], 20);

        IconManager.AddIcons(AppConstants.DefaultIconPackagesPath);

        AppEnvironment.SetToolStripRenderer(
            useDarkMode: ExtendedSettings.UseDarkMode,
            systemToolBars: ExtendedSettings.SystemToolBars,
            forceTanColorSchema: ExtendedSettings.ForceTanColorSchema);

        ImageDisplayControl.InitializeHardwareSettings(
            disableHardware: ExtendedSettings.DisableHardware,
            forceHardware: ExtendedSettings.ForceHardware,
            disableMipMapping: ExtendedSettings.DisableMipMapping);

        AppConfig.LoadLists(IniFile.GetDefaultLocations(AppConstants.DefaultListsFile));
        AppServices.Initialize(ExtendedSettings.DatabaseBackgroundSaving);
        AppServices.LoadNews(AppConstants.DefaultNewsFile, AppConstants.DefaultNewsFeed);

        if (ExtendedSettings.DisableScriptOptimization)
            PythonCommand.Optimized = false; // ... otherwise true?
    }
    #endregion

    /// <summary>
    /// Executed when an instance of ComicRackCE is already running.<br/>
    /// For example, when double-clicking a Comic or Plugin in File Explorer. (Assuming extension is associated with ComicRackCE)
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static void StartLast(string[] args)
    {
        ExtendedSettings sw = default(ExtendedSettings);
        AppServices.MainForm.BeginInvoke(delegate
        {
            AppServices.MainForm.RestoreToFront();
            try
            {
                sw = new ExtendedSettings();
                IEnumerable<string> enumerable = CommandLineParser.Parse(sw, args);
                if (!string.IsNullOrEmpty(sw.ImportList))
                {
                    AppServices.MainForm.ImportComicList(sw.ImportList);
                }
                if (!string.IsNullOrEmpty(sw.InstallPlugin))
                {
                    AppServices.MainForm.ShowPreferences(sw.InstallPlugin);
                }
                if (enumerable.Any())
                {
                    enumerable.ForEach((string file) =>
                    {
                        AppServices.MainForm.OpenSupportedFile(file, newSlot: true, sw.Page, fromShell: true);
                    });
                }
            }
            catch (Exception)
            {
            }
        });
    }
}
