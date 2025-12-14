using cYo.Common;
using cYo.Common.Collections;
using cYo.Common.Localize;
using cYo.Common.Runtime;
using cYo.Common.Text;
using cYo.Common.Threading;
using cYo.Common.Win32;
using cYo.Common.Windows;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display.Forms;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

/// <summary><see cref="Config.Settings"/> and <see cref="Config.ExtendedSettings"/></summary>
public static class AppConfig
{
    // Program.Main
    // MainForm
    /// <summary>
    /// Set by <see cref="MainForm.MenuRestart"/> when a restart is required.<br/>
    /// </summary>
    /// <remarks>
    /// When <paramref name="true"/>, <see cref="Program.Main(string[])"/> calls <see cref="Process.Start()"/> to re-launch the application instead of exiting.
    /// </remarks>
    public static bool Restart { get; set; }

    public static DefaultLists Lists { get; private set; }

    public static Settings Settings { get; private set; }

    #region ExtendedSettings
    private static ExtendedSettings extendedSettings;

    public static bool UseLocalSettings => ExtendedSettings.UseLocalSettings || IniFile.Default.GetValue("UseLocalSettings", def: false);

    public static IEnumerable<string> CommandLineFiles => ExtendedSettings.Files ?? [];

    public static ExtendedSettings ExtendedSettings
    {
        get
        {
            if (extendedSettings == null)
            {
                extendedSettings = new ExtendedSettings();
                CommandLineParser.Parse(extendedSettings, CommandLineParserOptions.None);
                if (!string.IsNullOrEmpty(extendedSettings.AlternateConfig) || UseLocalSettings)
                {
                    IniFile.AddDefaultLocation(SystemPaths.GetApplicationDataPath(UseLocalSettings, extendedSettings.AlternateConfig));
                }
                extendedSettings = new ExtendedSettings();
                CommandLineParser.Parse(extendedSettings);
                if (extendedSettings.Restart)
                {
                    extendedSettings.InstallPlugin = null;
                    extendedSettings.ImportList = null;
                    extendedSettings.Files = Enumerable.Empty<string>();
                }
            }
            return extendedSettings;
        }
    }
    #endregion

    #region InstalledLanguages
    private static List<TRInfo> installedLanguages;

    private static readonly object installedLanguagesLock = new object();

    public static TRInfo[] InstalledLanguages
    {
        get
        {
            using (ItemMonitor.Lock(installedLanguagesLock))
            {
                if (installedLanguages == null)
                {
                    installedLanguages = new List<TRInfo>();
                    TRDictionary tRDictionary = null;
                    try
                    {
                        tRDictionary = new TRDictionary(TR.ResourceFolder, "de");
                    }
                    catch (Exception)
                    {
                    }
                    foreach (TRInfo languageInfo in TR.GetLanguageInfos())
                    {
                        TRDictionary tRDictionary2 = new TRDictionary(TR.ResourceFolder, languageInfo.CultureName);
                        if (tRDictionary != null)
                        {
                            languageInfo.CompletionPercent = tRDictionary2.CompletionPercent(tRDictionary);
                        }
                        installedLanguages.Add(languageInfo);
                    }
                    installedLanguages.Sort((TRInfo a, TRInfo b) =>
                    {
                        int num = b.CompletionPercent.CompareTo(a.CompletionPercent);
                        return (num == 0) ? string.Compare(a.CultureName, b.CultureName) : num;
                    });
                }
                return installedLanguages.ToArray();
            }
        }
    }
    #endregion

    #region HelpSystem
    public static readonly ContextHelp Help = new ContextHelp(Path.Combine(Application.StartupPath, "Help"));

    // replaced single references with Help.HelpSystems
    //public static IEnumerable<string> HelpSystems => Help.HelpSystems;

    public static string HelpSystem
    {
        get
        {
            return Help.HelpName;
        }
        set
        {
            if (!(Help.HelpName == value))
            {
                if (!Help.Load(value))
                {
                    Help.Load("ComicRack Wiki");
                }
                Help.Variables["APPEXE"] = Application.ExecutablePath;
                Help.Variables["APPPATH"] = Path.GetDirectoryName(Application.ExecutablePath);
                Help.Variables["APPDATA"] = AppConstants.Paths.ApplicationDataPath;
                Help.Variables["USERPATH"] = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                Help.ShowKey = ExtendedSettings.ShowContextHelpKey;
                Help.Initialize();
            }
        }
    }
    #endregion

    #region Paths
    public static readonly SystemPaths Paths = new(
        UseLocalSettings,
        ExtendedSettings.AlternateConfig,
        ExtendedSettings.DatabasePath,
        ExtendedSettings.CachePath
    );

    // MainForm
    // PreferencesDialog
    // Roslyn my foot. Leave this under "SystemPaths Paths ="
    public static readonly PackageManager ScriptPackages = new(
        Paths.ScriptPathSecondary,
        Paths.PendingScriptsPath,
        commit: true
    );
    #endregion

    public static IEnumerable<StringPair> DefaultKeyboardMapping { get; set; }

    public static ExportSettingCollection ExportComicRackPresets => new ExportSettingCollection
        {
            ExportSetting.ConvertToCBZ,
            ExportSetting.ConvertToCB7
        };

    public static void LoadSettings(string filePath)
    {
        Settings = Settings.Load(filePath);
        Settings.RunCount++;
        CommandLineParser.Parse(ImageDisplayControl.HardwareSettings);
        CommandLineParser.Parse(EngineConfiguration.Default);
    }

    public static void ApplyLibrarySettings()
    {
        ComicBookValueMatcher.RegisterMatcherType(typeof(ComicBookPluginMatcher));
        ComicBookValueMatcher.RegisterMatcherType(typeof(ComicBookExpressionMatcher));

        ListExtensions.ParallelEnabled = EngineConfiguration.Default.EnableParallelQueries;

        ComicLibrary.QueryCacheMode = ExtendedSettings.QueryCacheMode;
        ComicLibrary.BackgroundQueryCacheUpdate = !ExtendedSettings.DisableBackgroundQueryCacheUpdate;
        ComicBook.EnableGroupNameCompression = ExtendedSettings.EnableGroupNameCompression;

        ShellFile.DeleteAPI = ExtendedSettings.DeleteAPI;

        if (EngineConfiguration.Default.IgnoredArticles != null)
            StringUtility.Articles = EngineConfiguration.Default.IgnoredArticles;
    }

    public static void LoadLists(IEnumerable<string> locations)
    {
        Lists = new DefaultLists(() => AppServices.Database.Books, locations);
    }
}
