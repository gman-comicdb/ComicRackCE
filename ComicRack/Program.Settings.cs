using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cYo.Common;
using cYo.Common.Localize;
using cYo.Common.Runtime;
using cYo.Common.Threading;
using cYo.Common.Windows;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Viewer.Config;

namespace cYo.Projects.ComicRack.Viewer;

public static partial class Program
{
    public static DefaultLists Lists { get; private set; }
    public static Settings Settings { get; private set; }

    // MainForm
    // PreferencesDialog
    public static IEnumerable<StringPair> DefaultKeyboardMapping { get; set; }

    // MainForm
    // ComicBrowserControl
    public static ExportSettingCollection ExportComicRackPresets => new ExportSettingCollection
        {
            ExportSetting.ConvertToCBZ,
            ExportSetting.ConvertToCB7
        };

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

    public static readonly string QuickHelpManualFile = Path.Combine(Application.StartupPath, "Help\\ComicRack Introduction.djvu");

    // Private
    private const string DefaultListsFile = "DefaultLists.txt";

    private const string DefaultBackgroundTexturesPath = "Resources\\Textures\\Backgrounds";

    private const string DefaultPaperTexturesPath = "Resources\\Textures\\Papers";

    private const string DefaultIconPackagesPath = "Resources\\Icons";

    private static readonly string defaultSettingsFile = Path.Combine(Paths.ApplicationDataPath, "Config.xml");

    private static readonly string defaultNewsFile = Path.Combine(Paths.ApplicationDataPath, "NewsFeeds.xml");
    #endregion

    #region ExtendedSettings
    private static ExtendedSettings extendedSettings;

    public static bool UseLocalSettings => ExtendedSettings.UseLocalSettings || IniFile.Default.GetValue("UseLocalSettings", def: false);

    public static IEnumerable<string> CommandLineFiles => ExtendedSettings.Files ?? Enumerable.Empty<string>();

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

    public static IEnumerable<string> HelpSystems => Help.HelpSystems;

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
                Help.Variables["APPDATA"] = Paths.ApplicationDataPath;
                Help.Variables["USERPATH"] = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                Help.ShowKey = ExtendedSettings.ShowContextHelpKey;
                Help.Initialize();
            }
        }
    }
    #endregion

    #region Constants

    // [seems to be unused]
    public const int DeadLockTestTime = 0; 

    // Thumb Dimensions
    public const int MinThumbHeight = 96;
    
    public const int MaxThumbHeight = 512;

    // Tile Dimensions
    public const int MinTileHeight = 64;
    
    public const int MaxTileHeight = 256;
    
    // Row Dimensions
    public const int MinRowHeight = 12;
    
    public const int MaxRowHeight = 48;

    // Default URLs
    public const string DefaultWiki = "https://web.archive.org/web/20161013095840fw_/http://comicrack.cyolito.com:80/documentation/wiki";
    
    public const string DefaultWebSite = "https://github.com/maforget/ComicRackCE";
    
    public const string DefaultNewsFeed = "https://github.com/maforget/ComicRackCE/commits/master.atom";
    
    public const string DefaultUserForm = "https://github.com/maforget/ComicRackCE/discussions";
    
    public const string DefaultLocalizePage = "https://web.archive.org/web/20170528182733fw_/http://comicrack.cyolito.com/faqs/12-how-to-create-language-packs";

    // [idk where this should go]
    public const int NewsIntervalMinutes = 60;

    // Program.RegisterFormats
    // PreferencesDialog
    /// <summary>
    /// Win32 Shell Programmatic Identifier, or <see href="https://learn.microsoft.com/en-us/windows/win32/shell/fa-progids">ProgID</see>. (I think)
    /// </summary>
    public const string ComicRackTypeId = "cYo.ComicRack";

    // Program.RegisterFormats
    // PreferencesDialog
    /// <summary>
    /// Win32 Shell <see href="https://learn.microsoft.com/en-us/windows/win32/shell/fa-file-types">File Type</see>. (I think)
    /// </summary>
    public const string ComicRackDocumentName = "eComic";
    #endregion
}
