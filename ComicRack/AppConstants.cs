using cYo.Projects.ComicRack.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

/// <summary>Magic numbers/strings</summary>
internal class AppConstants
{
    public static SystemPaths Paths => AppConfig.Paths;

    public static readonly string QuickHelpManualFile = Path.Combine(Application.StartupPath, "Help\\ComicRack Introduction.djvu");

    // public
    public const string DefaultListsFile = "DefaultLists.txt";

    public const string DefaultBackgroundTexturesPath = "Resources\\Textures\\Backgrounds";

    public const string DefaultPaperTexturesPath = "Resources\\Textures\\Papers";

    public const string DefaultIconPackagesPath = "Resources\\Icons";

    public static readonly string DefaultSettingsFile = Path.Combine(Paths.ApplicationDataPath, "Config.xml");

    public static readonly string DefaultNewsFile = Path.Combine(Paths.ApplicationDataPath, "NewsFeeds.xml");

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
