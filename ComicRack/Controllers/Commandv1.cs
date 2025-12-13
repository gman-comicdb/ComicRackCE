//using cYo.Projects.ComicRack.Plugins.Automation;
//using cYo.Projects.ComicRack.Viewer.Properties;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Reflection;
//using System.Windows.Forms;

//namespace cYo.Projects.ComicRack.Viewer.Controllers;

//public enum Menu
//{
//    None,
//    File,
//    Edit,
//    Browse,
//    Read,
//    Display,
//    //Library,
//    //View,
//    //Settings,
//    Help
//}

//public enum SubMenu
//{
//    None,
//    // File
//    Automation,
//    OpenBooks,
//    RecentBooks,
//    Read,
//    Display,
//    // Edit
//    MyRating,
//    PageType,
//    PageRotation,
//    Bookmarks,
//    // Browse
//    Workspaces,
//    ListLayout,
//    // Display
//    PageLayout,
//    Zoom,
//    Rotation,
//    // Help
//    Help,
//    Plugins,
//    ChooseHelpSystem
//}

//public enum Group
//{
//    None,
//    Library,
//    Browse,
//    AutoScroll,
//    Scroll,
//    DisplayOptions,
//    PageDisplay,
//    ZoomAndRotate,
//    Edit,
//    Other
//}

//public sealed class Command
//{
//    // --- Public API ---
//    public string Name { get; private set; }
//    public Menu Menu { get; private set; }
//    public SubMenu SubMenu { get; private set; }
//    public string Text { get; private set; }
//    public string ToolTipText { get; private set; }
//    public Image Image { get; private set; }
//    public EventHandler Handler { get; private set; }
//    public ToolStripMenuItem MenuItem { get; private set; }
//    public Keys ShortcutKeys { get; private set; }
//    public Action Action { get; private set; }
//    public Func<bool> CanExecute { get; private set; }

//    // --- Internal constructor ---
//    private Command() { }

//    private Command(string itemText, Keys shortcut = default)
//        => new Command(Menu.None, itemText, shortcut);

//    private Command(Menu menu, string name, string itemText, Keys shortcut = default)
//        => new Command(menu, SubMenu.None, name, itemText, shortcut);

//    private Command(Menu menu, string itemText, Keys shortcut = default)
//        => new Command(menu, SubMenu.None, ItemTextToName(itemText), itemText, shortcut);

//    private Command(Menu menu, SubMenu subMenu, string itemText, Keys shortcut = default)
//        => new Command(menu, subMenu, ItemTextToName(itemText), itemText, shortcut);

//    private Command(Menu menu, SubMenu subMenu, string name, string itemText, Keys shortcut = default)
//    {
//        Name = name;
//        Menu = menu;
//        SubMenu = subMenu;
//        Text = itemText;
//        Image = Registry.Image.TryGetValue(Name, out var image) ? image : default;
//        ShortcutKeys = shortcut;
//    }

//    // TODO : replace with regex
//    private string ItemTextToName(string itemText)
//        => itemText
//            .Replace("&", "")
//            .Replace(" ", "")
//            .Replace("...", "")
//            .Replace("(", "")
//            .Replace(")", "")
//            .Replace("%", "")
//            .Replace("°", "");

//    // --- All Commands ---
//    #region File Menu

//    public static readonly Command Open = new(Menu.File, "&Open...");
//    public static readonly Command Close = new(Menu.File, "&Close");
//    public static readonly Command CloseAll = new(Menu.File, "Close A&ll");

//    public static readonly Command AddTab = new(Menu.File, "AddTab", "New &Tab");

//    public static readonly Command AddFolder = new(Menu.File, "AddFolder", "&Add Folder to Library...");
//    public static readonly Command ScanFolders = new(Menu.File, "ScanFolders", "Scan Book &Folders");
//    public static readonly Command UpdateAllBooks = new(Menu.File, "UpdateAllBooks", "Update all Book Files");
//    public static readonly Command UpdateWebComics = new(Menu.File, "Update Web Comics");
//    public static readonly Command SynchronizeDevices = new(Menu.File, "Synchronize Devices");
//    public static readonly Command GenerateCovers = new(Menu.File, "CacheThumbnails", "Generate Cover Thumbnails");
//    public static readonly Command ShowTasks = new(Menu.File, "ShowTasks", " & Tasks...");
//    public static readonly Command Automation = new(Menu.File, "A&utomation");

//    public static readonly Command NewComic = new(Menu.File, "NewComic", " & New fileless Book Entry...");
//    public static readonly Command OpenRemoteLibrary = new(Menu.File, "Open Remote Library...");

//    public static readonly Command OpenBooks = new(Menu.File, "Open Books");
//    public static readonly Command RecentBooks = new(Menu.File, "&Recent Books");

//    public static readonly Command Restart = new(Menu.File, "Rest&art");
//    public static readonly Command Exit = new(Menu.File, "&Exit");
//    #endregion

//    #region Edit Menu
//    public static readonly Command ShowInfo = new(Menu.Edit, "Info...");

//    public static readonly Command Undo = new(Menu.Edit, "&Undo");
//    public static readonly Command Redo = new(Menu.Edit, "&Redo");

//    public static readonly Command MyRating = new(Menu.Edit, "My R&ating");
//    public static readonly Command Rate0 = new(Menu.Edit, SubMenu.MyRating, "None");
//    public static readonly Command Rate1 = new(Menu.Edit, SubMenu.MyRating, "* (1 Star)");
//    public static readonly Command Rate2 = new(Menu.Edit, SubMenu.MyRating, "** (2 Stars)");
//    public static readonly Command Rate3 = new(Menu.Edit, SubMenu.MyRating, "*** (3 Stars)");
//    public static readonly Command Rate4 = new(Menu.Edit, SubMenu.MyRating, "**** (4 Stars)");
//    public static readonly Command Rate5 = new(Menu.Edit, SubMenu.MyRating, "***** (5 Stars)");
//    public static readonly Command QuickRating = new(Menu.Edit, SubMenu.MyRating, "Quick Rating and Review...");

//    public static readonly Command PageType = new(Menu.Edit, "&Page Type");

//    public static readonly Command PageRotation = new(Menu.Edit, "Page Rotation");

//    public static readonly Command Bookmarks = new(Menu.Edit, "&Bookmarks");
//    public static readonly Command SetBookmark = new(Menu.Edit, SubMenu.Bookmarks, "Set Bookmark...");
//    public static readonly Command RemoveBookmark = new(Menu.Edit, SubMenu.Bookmarks, "Remove Bookmark");
//    public static readonly Command PreviousBookmark = new(Menu.Edit, SubMenu.Bookmarks, "Previous Bookmark");
//    public static readonly Command NextBookmark = new(Menu.Edit, SubMenu.Bookmarks, "Next Bookmark");
//    public static readonly Command LastPageRead = new(Menu.Edit, SubMenu.Bookmarks, "L&ast Page Read");

//    public static readonly Command CopyPage = new(Menu.Edit, "&Copy Page");
//    public static readonly Command ExportPage = new(Menu.Edit, "&Export Page...");

//    public static readonly Command RefreshView = new(Menu.Edit, "RefreshView", "&Refresh");

//    public static readonly Command ShowDevices = new(Menu.Edit, "ShowDevices", "Devices...");
//    public static readonly Command ShowPreferences = new(Menu.Edit, "ShowPreferences", " & Preferences...");
//    #endregion

//    #region Browse Menu
//    public static readonly Command ToggleBrowser = new(Menu.Browse, "ToggleBrowser", "&Browser");

//    public static readonly Command ViewLibrary = new(Menu.Browse, "ViewLibrary", "Li&brary");
//    public static readonly Command ViewFolders = new(Menu.Browse, "ViewFolders", "&Folders");
//    public static readonly Command ViewPages = new(Menu.Browse, "ViewPages", "&Pages");

//    public static readonly Command ToggleSidebar = new(Menu.Browse, "ToggleSidebar", "&Sidebar");
//    public static readonly Command TogglePreview = new(Menu.Browse, "TogglePreview", "S&mall Preview");
//    public static readonly Command ToggleFilters = new(Menu.Browse, "ToggleFilters", "S&earch Browser");

//    public static readonly Command ToggleInfoPanel = new(Menu.Browse, "ToggleInfoPanel", "Info Panel");

//    public static readonly Command PreviousList = new(Menu.Browse, "Previous List");
//    public static readonly Command NextList = new(Menu.Browse, "Next List");

//    public static readonly Command Workspaces = new(Menu.Browse, "&Workspaces");
//    public static readonly Command SaveWorkspace = new(Menu.Browse, SubMenu.Workspaces, "&Save Workspace...");
//    public static readonly Command EditWorkspaces = new(Menu.Browse, SubMenu.Workspaces, "&Edit Workspaces...");

//    public static readonly Command ListLayout = new(Menu.Browse, "List Layout");
//    public static readonly Command EditListLayout = new(Menu.Browse, SubMenu.ListLayout, "&Edit List Layout...");
//    public static readonly Command SaveListLayout = new(Menu.Browse, SubMenu.ListLayout, "&Save List Layout...");
//    public static readonly Command EditAllListLayout = new(Menu.Browse, SubMenu.ListLayout, "EditAllListLayout", " & Edit Layouts...");
//    public static readonly Command SetAllListLayout = new(Menu.Browse, SubMenu.ListLayout, "SetAllListLayout", "Set all Lists to current Layout");
//    #endregion

//    #region Read Menu
//    public static readonly Command FirstPage = new(Menu.Read, "&First Page");
//    public static readonly Command PreviousPage = new(Menu.Read, "&Previous Page"); //PrevPage
//    public static readonly Command NextPage = new(Menu.Read, "&Next Page");
//    public static readonly Command LastPage = new(Menu.Read, "&Last Page");

//    public static readonly Command PreviousBook = new(Menu.Read, "Pre&vious Book"); // PrevFromList
//    public static readonly Command NextBook = new(Menu.Read, "Ne&xt Book"); // NextFromList
//    public static readonly Command RandomBook = new(Menu.Read, "Random Book"); // RandomFromList
//    public static readonly Command SyncBrowser = new(Menu.Read, "SyncBrowser", "Show in &Browser"); // SyncBrowser

//    public static readonly Command PreviousTab = new(Menu.Read, "&Previous Tab"); // PrevTab
//    public static readonly Command NextTab = new(Menu.Read, "Next &Tab");

//    public static readonly Command AutoScroll = new(Menu.Read, "AutoScroll", "&Auto Scrolling");
//    public static readonly Command DoublePageAutoScroll = new(Menu.Read, "DoublePageAutoScroll", "Double Page Auto Scrolling");
//    public static readonly Command TrackCurrentPage = new(Menu.Read, "TrackCurrentPage", "Track current Page");
//    #endregion

//    #region Display Menu
//    public static readonly Command ShowDisplaySettings = new(Menu.Display, "ShowDisplaySettings", "Book Display Settings...");

//    public static readonly Command PageLayout = new(Menu.Display, "&Page Layout");
//    public static readonly Command OriginalSize = new(Menu.Display, SubMenu.PageLayout, "Original Size");
//    public static readonly Command FitAll = new(Menu.Display, SubMenu.PageLayout, "Fit &All");
//    public static readonly Command FitWidth = new(Menu.Display, SubMenu.PageLayout, "Fit &Width");
//    public static readonly Command FitWidthAdaptive = new(Menu.Display, SubMenu.PageLayout, "Fit Width (Adaptive)");
//    public static readonly Command FitHeight = new(Menu.Display, SubMenu.PageLayout, "Fit &Height");
//    public static readonly Command FitBest = new(Menu.Display, SubMenu.PageLayout, "Fit &Best");
//    public static readonly Command SinglePage = new(Menu.Display, SubMenu.PageLayout, "Single Page");
//    public static readonly Command TwoPages = new(Menu.Display, SubMenu.PageLayout, "Two Pages");
//    public static readonly Command TwoPagesAdaptive = new(Menu.Display, SubMenu.PageLayout, "Two Pages (Adaptive)");
//    public static readonly Command ToggleRtL = new(Menu.Display, SubMenu.PageLayout, "ToggleRtL", "Right to Left");
//    public static readonly Command ToggleOversizeFit = new(Menu.Display, SubMenu.PageLayout, "ToggleOversizeFit", " & Only fit if oversized");

//    public static readonly Command Zoom = new(Menu.Display, "Zoom");
//    public static readonly Command ZoomIn = new(Menu.Display, "Zoom &In");
//    public static readonly Command ZoomOut = new(Menu.Display, "Zoom &Out");
//    public static readonly Command ToggleZoom = new(Menu.Display, SubMenu.Zoom, "Toggle Zoom");
//    public static readonly Command Zoom100 = new(Menu.Display, "Zoom 100%");
//    public static readonly Command Zoom125 = new(Menu.Display, "Zoom 125%");
//    public static readonly Command Zoom150 = new(Menu.Display, "Zoom 150%");
//    public static readonly Command Zoom200 = new(Menu.Display, "Zoom 200%");
//    public static readonly Command Zoom400 = new(Menu.Display, "Zoom 400%");
//    public static readonly Command ShowZoomCustom = new(Menu.Display, "&Custom...");

//    public static readonly Command Rotation = new(Menu.Display, "&Rotation");
//    public static readonly Command RotateLeft = new(Menu.Display, "Rotate Left");
//    public static readonly Command RotateRight = new(Menu.Display, "Rotate Right");
//    public static readonly Command Rotate0 = new(Menu.Display, "Rotate0", "&No Rotation");
//    public static readonly Command Rotate90 = new(Menu.Display, "Rotate90");
//    public static readonly Command Rotate180 = new(Menu.Display, "Rotate180");
//    public static readonly Command Rotate270 = new(Menu.Display, "Rotate270");
//    public static readonly Command ToggleAutoRotate = new(Menu.Display, "ToggleAutoRotate", "Autorotate Double Pages");

//    public static readonly Command ToggleMinimalGui = new(Menu.Display, "ToggleMinimalGui", "Minimal User Interface");
//    public static readonly Command ToggleFullScreen = new(Menu.Display, "ToggleFullScreen", "&Full Screen");
//    public static readonly Command ToggleReaderWindow = new(Menu.Display, "ToggleReaderWindow", "Reader in &own Window");
//    public static readonly Command ToggleMagnifier = new(Menu.Display, "ToggleMagnifier", "&Magnifier");
//    #endregion

//    #region Help Menu
//    public static readonly Command Help = new(Menu.Help, "Help");
//    public static readonly Command WebHelp = new(Menu.Help, SubMenu.Help, "ComicRack Documentation...");

//    public static readonly Command Plugins = new(Menu.Help, "Plugins");

//    public static readonly Command ChooseHelp = new(Menu.Help, "ChooseHelp", "Choose Help System");

//    public static readonly Command QuickIntro = new(Menu.Help, "QuickIntro", "Quick Introduction");

//    public static readonly Command WebHomepage = new(Menu.Help, "WebHomepage", "ComicRack Homepage...");
//    public static readonly Command WebUserForum = new(Menu.Help, "WebUserForum", "ComicRack User Forum...");

//    public static readonly Command ShowNews = new(Menu.Help, "&News...");
//    public static readonly Command CheckUpdate = new(Menu.Help, "CheckUpdate", "Check For Update...");

//    public static readonly Command ShowAbout = new(Menu.Help, "&About...");
//    #endregion

//    //public static readonly Command AddFolder = new(Menu.Library, "&Add Folder...");
//    //public static readonly Command ScanFolders = new(Menu.Library, "Scan &Folders");
//    //public static readonly Command UpdateBooks = new(Menu.Library, "Update Books");
//    //public static readonly Command GenerateCovers = new(Menu.Library, "Generate Covers");

//    private static class Registry
//    {
//        private static NavigatorManager OpenBooks => Program.MainForm.OpenBooks;

//        public static readonly Dictionary<string, Image> Image = new()
//        {
//            #region File Menu
//            ["Open"] = Resources.Open,
//            //["Close"] = Resources.
//            //["CloseAll"] = Resources.

//            ["AddTab"] = Resources.NewTab,

//            ["AddFolder"] = Resources.AddFolder,
//            ["ScanFolders"] = Resources.Scan,
//            ["UpdateAllBooks"] = Resources.UpdateSmall,
//            ["UpdateWebComics"] = Resources.UpdateWeb,
//            ["SynchronizeDevices"] = Resources.DeviceSync,
//            ["GenerateCovers"] = Resources.Screenshot,
//            ["ShowTasks"] = Resources.BackgroundJob,
//            //["Automation"] = Resources.

//            //["NewComic"] = Resources.
//            ["OpenRemoteLibrary"] = Resources.RemoteDatabase,

//            //["OpenBooks"] = Resources.
//            //["RecentBooks"] = Resources.

//            ["Restart"] = Resources.Restart,
//            //["Exit"] = Resources.
//            #endregion

//            #region Edit Menu
//            ["ShowInfo"] = Resources.GetInfo,

//            ["Undo"] = Resources.Undo,
//            ["Redo"] = Resources.Redo,

//            //["MyRating"] = Resources.
//            //["PageType"] = Resources.
//            //["PageRotation"] = Resources.
//            ["Rotate0"] = Resources.Rotate0Permanent,
//            ["Rotate90"] = Resources.Rotate90Permanent,
//            ["Rotate180"] = Resources.Rotate180Permanent,
//            ["Rotate270"] = Resources.Rotate270Permanent,

//            ["Bookmarks"] = Resources.Bookmark,
//            ["SetBookmark"] = Resources.NewBookmark,
//            ["RemoveBookmark"] = Resources.RemoveBookmark,
//            ["PreviousBookmark"] = Resources.PreviousBookmark,
//            ["NextBookmark"] = Resources.NextBookmark,
//            //["LastPageRead"] = Resources.

//            ["CopyPage"] = Resources.Copy,
//            //["ExportPage"] = Resources.

//            ["RefreshView"] = Resources.Refresh,

//            ["ShowDevices"] = Resources.EditDevices,
//            ["ShowPreferences"] = Resources.Preferences,
//            #endregion

//            #region Browse Menu
//            ["ToggleBrowser"] = Resources.Browser,

//            ["ViewLibrary"] = Resources.Database,
//            ["ViewFolders"] = Resources.FileBrowser,
//            ["ViewPages"] = Resources.ComicPage,

//            ["ToggleSidebar"] = Resources.Sidebar,
//            ["TogglePreview"] = Resources.SmallPreview,
//            ["ToggleFilters"] = Resources.Search,

//            ["ToggleInfoPanel"] = Resources.InfoPanel,

//            ["PreviousList"] = Resources.BrowsePrevious,
//            ["NextList"] = Resources.BrowseNext,

//            ["Workspaces"] = Resources.Workspace,
//            //["SaveWorkspace"] = Resources.
//            //["EditWorkspaces"] = Resources.

//            ["ListLayout"] = Resources.ListLayout,
//            //["EditListLayout"] = Resources.
//            //["SaveListLayout"] = Resources.

//            //["EditAllListLayout"] = Resources.
//            //["SetAllListLayout"] = Resources.
//            #endregion

//            #region Read Menu
//            ["FirstPage"] = Resources.GoFirst,
//            ["PreviousPage"] = Resources.GoPrevious,
//            ["NextPage"] = Resources.GoNext,
//            ["LastPage"] = Resources.GoLast,

//            ["PreviousBook"] = Resources.PrevFromList,
//            ["NextBook"] = Resources.NextFromList,
//            ["RandomBook"] = Resources.RandomComic,
//            ["SyncBrowser"] = Resources.SyncBrowser,

//            ["PreviousTab"] = Resources.Previous,
//            ["NextTab"] = Resources.Next,

//            ["AutoScroll"] = Resources.CursorScroll,
//            ["DoublePageAutoScroll"] = Resources.TwoPageAutoscroll,
//            //["TrackCurrentPage"] = Resources.
//            #endregion

//            #region Display Menu
//            ["ShowDisplaySettings"] = Resources.DisplaySettings,

//            //["PageLayout"] = Resources.
//            ["ToggleOversizeFit"] = Resources.FileBrowser,

//            //["Zoom"] = Resources.
//            ["ZoomIn"] = Resources.ZoomIn,
//            ["ZoomOut"] = Resources.ZoomOut,
//            //["ToggleZoom"] = Resources.

//            //["Rotation"] = Resources.
//            ["RotateLeft"] = Resources.RotateLeft,
//            ["RotateRight"] = Resources.RotateRight,
//            ["Rotate0"] = Resources.Rotate0,
//            ["Rotate90"] = Resources.Rotate90,
//            ["Rotate180"] = Resources.Rotate180,
//            ["Rotate270"] = Resources.Rotate270,
//            ["ToggleAutoRotate"] = Resources.SmallPreview,

//            ["ToggleUI"] = Resources.MenuToggle,
//            ["ToggleFullScreen"] = Resources.FullScreen,
//            ["ToggleReaderWindow"] = Resources.UndockReader,
//            ["ToggleMagnifier"] = Resources.Zoom,
//            #endregion

//            #region Help Menu
//            //["Help"] = Resources.
//            ["WebHelp"] = Resources.Help,

//            //["Plugins"] = Resources.

//            //["ChooseHelp"] = Resources.
//            //["QuickIntro"] = Resources.

//            ["WebHomepage"] = Resources.WebBlog,
//            ["WebUserForum"] = Resources.WebForum,

//            ["ShowNews"] = Resources.News,
//            //["CheckUpdate"] = Resources.

//            ["ShowAbout"] = Resources.About,
//            #endregion
//        };

//        public static readonly Dictionary<string, string> ToolTipText = new()
//        {
//            ["Open"] = "Open File",
//            ["Close"] = "Close Tab",
//            ["CloseAll"] = "Close all Tabs",

//            ["NewTab"] = "Add a New Tab",

//            //["OpenBooks"] = "",
//            //["RecentBooks"] = "",

//            //["Restart"] = "Restart ComicRackCE",
//            //["Exit"] = "Shutdown ComicRackCE",

//            ["AddFolder"] = "Add a Folder to Library",
//            ["ScanFolders"] = "Scan Library Folders for Books",
//            ["UpdateBooks"] = "Update Books with pending Metadata",
//            ["GenerateCovers"] = "Generate Book Cover Thumbnails",
//            //["CloseAll"] = Resources.
//            // Exit intentionally has no image
//        };

//        public static Dictionary<string, Action> Action = [];

//        public static Dictionary<string, Func<bool>> CanExecute = new()
//        {
//            //["Open"] =
//            ["Close"] = () => OpenBooks.Slots.Count > 0,
//            ["CloseAll"] = () => OpenBooks.Slots.Count > 0,
//        };
//    }

//    // --- Registry  ---
//    private static readonly Dictionary<string, Command> commands;

//    // --- Static initialization, reflection performed only ONCE ---
//    static Command()
//    {
//        commands = typeof(Command)
//            .GetFields(BindingFlags.Public | BindingFlags.Static)
//            .Where(f => f.FieldType == typeof(Command))
//            .ToDictionary(
//                f => f.Name,
//                f => {
//                    Command c = (Command)f.GetValue(null)!;

//                    // Assign Name if not set
//                    c.Name ??= f.Name;

//                    // Assign image from registry
//                    if (Registry.Image.TryGetValue(c.Name, out var img))
//                        c.Image = img;

//                    // Create menu item, optional: skip if no image
//                    c.MenuItem = CreateMenuItem(c);

//                    // Assign default handler (optional)
//                    c.Handler ??= (_, __) => { Console.WriteLine($"Command triggered: {c.Name}"); };

//                    return c;
//                }
//            );
//    }

//    // ToolStripMenuItem
//    private static ToolStripMenuItem CreateMenuItem(Command c)
//    {
//        var item = new ToolStripMenuItem(c.Name.Replace('_', ' '));
//        if (c.Image != null)
//            item.Image = c.Image;

//        // Bind the event handler automatically
//        item.Click += c.Handler!;
//        return item;
//    }

//    //
//    public static Command? FromName(string name) =>
//        commands.TryGetValue(name, out var c) ? c : null;

//    // --- Implicit Conversions ---
//    public static implicit operator ToolStripMenuItem(Command c) => c.MenuItem!;
//    public static implicit operator Image(Command c) => c.Image;
//    public static implicit operator EventHandler(Command c) => c.Handler;

//    public static IEnumerable<Command> All => commands.Values;

//    public override string ToString() => Name;
//}