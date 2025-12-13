//#nullable enable
//using cYo.Projects.ComicRack.Viewer.Properties;
//using cYo.Common.Windows;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Reflection;
//using Icon = cYo.Projects.ComicRack.Viewer.Controllers.CommandIcons;
//using Key = cYo.Projects.ComicRack.Viewer.Controllers.CommandKeys;
//using System.Windows.Forms;
//using Microsoft.Scripting.Utils;

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


//public interface ICommand
//{
//    string Name { get; }

//    Menu Menu { get; }

//    SubMenu SubMenu { get; }

//    string Text { get; }

//    string ToolTipText { get; }

//    Image Image { get; }

//    Keys[] Keys { get; }

//    Action Action { get; }

//    Func<bool> CanExecute { get; }
//}

//public sealed class Command : ICommand, IEquatable<Command>
//{

//    #region Public Properties
//    /// <value>Command Name. Statically populated from <see cref="FieldInfo"/>.Name.</value>
//    public string Name { get; private set; } = string.Empty;

//    /// <value>Menu that this Command belongs to.</value>
//    public Menu Menu { get; private set; }

//    /// <value>SubMenu that this Command belongs to.</value>
//    public SubMenu SubMenu { get; private set; }

//    /// <value>Command <see cref="MenuItem.Text"/></value>
//    public string Text { get; private set; }

//    /// <value>Command <see cref="MenuItem"/>.ToolTipText</value>
//    public string ToolTipText { get; private set; } = string.Empty;

//    /// <value>Command <see cref="MenuItem"/>.Image Image</value>
//    public Image Image { get; private set; } = Resources.NotValidated;

//    /// <value>Command <see cref="MenuItem"/>.ShortcutKeys</value>
//    public Keys[] Keys { get; private set; }

//    /// <value>Action that will be executed when this command is executed</value>
//    public Action Action { get; set; } = () => { }; // no-op

//    /// <value>Indicates whether <see cref="Command.Action"/> can currently be executed.</value>
//    public Func<bool> CanExecute { get; set; } = () => true;
//    #endregion

//    /// <summary>Statically populated <see cref="Dictionary{TKey, TValue}"/> of <see cref="Command.Name"/> and <see cref="Command"/></summary>
//    private static readonly Dictionary<string, Command> commands;

//    // --- Internal constructor ---
//    private Command() { }

//    #region Overloads
//    private Command(Menu menu, string itemText)
//        => new Command(menu, SubMenu.None, default, null, itemText);

//    private Command(Menu menu, Image image, string itemText)
//        => new Command(menu, SubMenu.None, image, null, itemText);

//    private Command(Menu menu, Keys[] shortcut, string itemText)
//        => new Command(menu, SubMenu.None, null, shortcut, itemText);

//    private Command(Menu menu, SubMenu subMenu, string itemText)
//        => new Command(menu, subMenu, null, null, itemText);

//    private Command(Menu menu, SubMenu subMenu, Image image, string itemText)
//        => new Command(menu, subMenu, image, null, itemText);

//    //private Command(Menu menu, SubMenu subMenu, Keys[] shortcut, string itemText)
//    //    => new Command(menu, subMenu, null, shortcut, itemText);

//    private Command(Menu menu, Image image, Keys[] shortcut, string itemText)
//        => new Command(menu, SubMenu.None, image, shortcut, itemText);
//    #endregion

//    private Command(Menu menu, SubMenu subMenu, Image image, Keys[] shortcut, string itemText)
//    {
//        Menu = menu;
//        SubMenu = subMenu;
//        Text = itemText;
//        Image = image;
//        Keys = shortcut;
//    }

//    public static readonly Command Separator = new();
//    // --- All Commands ---
//    #region File Menu

//    public static readonly Command Open = new(Menu.File, Icon.Open, Key.Open, "&Open...");
//    public static readonly Command Close = new(Menu.File, Key.Close, "&Close");
//    public static readonly Command CloseAll = new(Menu.File, Key.CloseAll, "Close A&ll");

//    public static readonly Command AddTab = new(Menu.File, Icon.AddTab, Key.AddTab, "New &Tab");

//    public static readonly Command AddFolder = new(Menu.File, Icon.AddFolder, Key.AddFolder, "&Add Folder to Library...");
//    public static readonly Command ScanFolders = new(Menu.File, Icon.ScanFolders, Key.ScanFolders, "Scan Book &Folders");
//    public static readonly Command UpdateAllBooks = new(Menu.File, Icon.UpdateAllBooks, Key.UpdateAllBooks, "Update all Book Files");
//    public static readonly Command UpdateWebComics = new(Menu.File, Icon.UpdateWebComics, Key.UpdateWebComics, "Update Web Comics");
//    public static readonly Command SynchronizeDevices = new(Menu.File, Icon.SynchronizeDevices, "Synchronize Devices");
//    public static readonly Command GenerateCovers = new(Menu.File, Icon.GenerateCovers, "Generate Cover Thumbnails");
//    public static readonly Command ShowTasks = new(Menu.File, Icon.ShowTasks, Key.ShowTasks, " & Tasks...");
//    public static readonly Command Automation = new(Menu.File, "A&utomation");

//    public static readonly Command NewComic = new(Menu.File, Key.NewComic, " & New fileless Book Entry...");
//    public static readonly Command OpenRemoteLibrary = new(Menu.File, Icon.OpenRemoteLibrary, "Open Remote Library...");

//    public static readonly Command OpenBooks = new(Menu.File, "Open Books");
//    public static readonly Command RecentBooks = new(Menu.File, "&Recent Books");

//    public static readonly Command Restart = new(Menu.File, Icon.Restart, Key.Restart, "Rest&art");
//    public static readonly Command Exit = new(Menu.File, Key.Exit, "&Exit");
//    #endregion

//    #region Edit Menu
//    public static readonly Command ShowInfo = new(Menu.Edit, Icon.ShowInfo, "Info...");

//    public static readonly Command Undo = new(Menu.Edit, Icon.Undo, "&Undo");
//    public static readonly Command Redo = new(Menu.Edit, Icon.Redo, "&Redo");

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

//    public static readonly Command Bookmarks = new(Menu.Edit, Icon.Bookmarks, "&Bookmarks");
//    public static readonly Command SetBookmark = new(Menu.Edit, SubMenu.Bookmarks, Icon.SetBookmark, "Set Bookmark...");
//    public static readonly Command RemoveBookmark = new(Menu.Edit, SubMenu.Bookmarks, Icon.RemoveBookmark, "Remove Bookmark");
//    public static readonly Command PreviousBookmark = new(Menu.Edit, SubMenu.Bookmarks, Icon.PreviousBookmark, "Previous Bookmark");
//    public static readonly Command NextBookmark = new(Menu.Edit, SubMenu.Bookmarks, Icon.NextBookmark, "Next Bookmark");
//    public static readonly Command LastPageRead = new(Menu.Edit, SubMenu.Bookmarks, "L&ast Page Read");

//    public static readonly Command CopyPage = new(Menu.Edit, Icon.CopyPage, "&Copy Page");
//    public static readonly Command ExportPage = new(Menu.Edit, "&Export Page...");

//    public static readonly Command RefreshView = new(Menu.Edit, Icon.RefreshView, "&Refresh");

//    public static readonly Command ShowDevices = new(Menu.Edit, Icon.ShowDevices, "Devices...");
//    public static readonly Command ShowPreferences = new(Menu.Edit, Icon.ShowPreferences, " & Preferences...");
//    #endregion

//    #region Browse Menu
//    public static readonly Command ToggleBrowser = new(Menu.Browse, Icon.ToggleBrowser, "&Browser");

//    public static readonly Command ViewLibrary = new(Menu.Browse, Icon.ViewLibrary, "Li&brary");
//    public static readonly Command ViewFolders = new(Menu.Browse, Icon.ViewFolders, "&Folders");
//    public static readonly Command ViewPages = new(Menu.Browse, Icon.ViewPages, "&Pages");

//    public static readonly Command ToggleSidebar = new(Menu.Browse, Icon.ToggleSidebar, "&Sidebar");
//    public static readonly Command TogglePreview = new(Menu.Browse, Icon.TogglePreview, "S&mall Preview");
//    public static readonly Command ToggleSearchFilter = new(Menu.Browse, Icon.ToggleSearchFilter, "S&earch Browser");

//    public static readonly Command ToggleInfoPanel = new(Menu.Browse, Icon.ToggleInfoPanel, "Info Panel");

//    public static readonly Command PreviousList = new(Menu.Browse, Icon.PreviousList, "Previous List");
//    public static readonly Command NextList = new(Menu.Browse, Icon.NextList, "Next List");

//    public static readonly Command Workspaces = new(Menu.Browse, Icon.Workspaces, "&Workspaces");
//    public static readonly Command SaveWorkspace = new(Menu.Browse, SubMenu.Workspaces, "&Save Workspace...");
//    public static readonly Command EditWorkspaces = new(Menu.Browse, SubMenu.Workspaces, "&Edit Workspaces...");

//    public static readonly Command ListLayout = new(Menu.Browse, Icon.ListLayout, "List Layout");
//    public static readonly Command EditListLayout = new(Menu.Browse, SubMenu.ListLayout, "&Edit List Layout...");
//    public static readonly Command SaveListLayout = new(Menu.Browse, SubMenu.ListLayout, "&Save List Layout...");
//    public static readonly Command EditAllListLayout = new(Menu.Browse, SubMenu.ListLayout, "&Edit Layouts...");
//    public static readonly Command SetAllListLayout = new(Menu.Browse, SubMenu.ListLayout, "Set all Lists to current Layout");
//    #endregion

//    #region Read Menu
//    public static readonly Command GoFirstPage = new(Menu.Read, Icon.GoFirstPage, "&First Page");
//    public static readonly Command GoPreviousPage = new(Menu.Read, Icon.GoPreviousPage, "&Previous Page"); //PrevPage
//    public static readonly Command GoNextPage = new(Menu.Read, Icon.GoNextPage, "&Next Page");
//    public static readonly Command GoLastPage = new(Menu.Read, Icon.GoLastPage, "&Last Page");

//    public static readonly Command PreviousBook = new(Menu.Read, Icon.PreviousBook, "Pre&vious Book"); // PrevFromList
//    public static readonly Command NextBook = new(Menu.Read, Icon.NextBook, "Ne&xt Book"); // NextFromList
//    public static readonly Command RandomBook = new(Menu.Read, Icon.RandomBook, "Random Book"); // RandomFromList
//    public static readonly Command BrowseToBook = new(Menu.Read, Icon.BrowseToBook, "Show in &Browser"); // SyncBrowser

//    public static readonly Command PreviousTab = new(Menu.Read, Icon.PreviousTab, "&Previous Tab"); // PrevTab
//    public static readonly Command NextTab = new(Menu.Read, Icon.NextTab, "Next &Tab");

//    public static readonly Command ToggleAutoScroll = new(Menu.Read, Icon.AutoScroll, "&Auto Scrolling");
//    public static readonly Command ToggleDoublePageAutoScroll = new(Menu.Read, Icon.DoublePageAutoScroll, "Double Page Auto Scrolling");
//    public static readonly Command ToggleTrackCurrentPage = new(Menu.Read, "Track current Page");
//    #endregion

//    #region Display Menu
//    public static readonly Command ShowDisplaySettings = new(Menu.Display, Icon.ShowDisplaySettings, "Book Display Settings...");

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
//    public static readonly Command ToggleRtL = new(Menu.Display, SubMenu.PageLayout, "Right to Left");
//    public static readonly Command ToggleOversizeFit = new(Menu.Display, SubMenu.PageLayout, "&Only fit if oversized");

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
//    public static readonly Command Rotate0 = new(Menu.Display, "&No Rotation");
//    public static readonly Command Rotate90 = new(Menu.Display, "Rotate90");
//    public static readonly Command Rotate180 = new(Menu.Display, "Rotate180");
//    public static readonly Command Rotate270 = new(Menu.Display, "Rotate270");
//    public static readonly Command ToggleAutoRotate = new(Menu.Display, Icon.ToggleAutoRotate, "Autorotate Double Pages");

//    public static readonly Command ToggleMinimalGui = new(Menu.Display, "Minimal User Interface");
//    public static readonly Command ToggleFullScreen = new(Menu.Display, Icon.ToggleFullScreen, "&Full Screen");
//    public static readonly Command ToggleReaderWindow = new(Menu.Display, Icon.ToggleReaderWindow, "Reader in &own Window");
//    public static readonly Command ToggleMagnifier = new(Menu.Display, Icon.ToggleMagnifier, "&Magnifier");
//    #endregion

//    #region Help Menu
//    public static readonly Command Help = new(Menu.Help, "Help");
//    public static readonly Command WebHelp = new(Menu.Help, SubMenu.Help, Icon.WebHelp, "ComicRack Documentation...");

//    public static readonly Command Plugins = new(Menu.Help, "Plugins");

//    public static readonly Command ChooseHelp = new(Menu.Help, "Choose Help System");

//    public static readonly Command QuickIntro = new(Menu.Help, "Quick Introduction");

//    public static readonly Command WebHomepage = new(Menu.Help, Icon.WebHomepage, "ComicRack Homepage...");
//    public static readonly Command WebUserForum = new(Menu.Help, Icon.WebUserForum, "ComicRack User Forum...");

//    public static readonly Command ShowNews = new(Menu.Help, Icon.ShowNews, "&News...");
//    public static readonly Command CheckUpdate = new(Menu.Help, "Check For Update...");

//    public static readonly Command ShowAbout = new(Menu.Help, Icon.ShowAbout, "&About...");
//    #endregion

//    //public static readonly Command AddFolder = new(Menu.Library, "&Add Folder...");
//    //public static readonly Command ScanFolders = new(Menu.Library, "Scan &Folders");
//    //public static readonly Command UpdateBooks = new(Menu.Library, "Update Books");
//    //public static readonly Command GenerateCovers = new(Menu.Library, "Generate Covers");

//    // static initialization
//    static Command()
//    {
//        commands = typeof(Command)
//            .GetFields(BindingFlags.Public | BindingFlags.Static)
//            .Where(field => field.FieldType == typeof(Command))
//            .ToDictionary(
//                field => field.Name,
//                field => {
//                    Command command = (Command)field.GetValue(null)!;
//                    command.Name ??= field.Name; // Assign Name if not set

//                    // We could add Action, CanExecute, EventHandler etc here, but in the interest of
//                    // sticking to some industry standard, this class should could contain only static definitions.
//                    // Static in the development sense, not the c# language sense

//                    return command;
//                }
//            );
//    }

//    public static Command? FromName(string name) => commands.TryGetValue(name, out var cmd) ? cmd : null;

//    /// <summary>Statically populated <see cref="IEnumerable{T}"/> of every registered <see cref="Command"/></summary>
//    public static IEnumerable<Command> All => commands.Values;

//    public override string ToString() => Name;

//    public static implicit operator Command(string cmdName) => FromName(cmdName);

//    public bool Equals(Command? otherCommand)
//    {
//        if (ReferenceEquals(this, otherCommand))
//            return true;

//        if (otherCommand is null)
//            return false;

//        return Name == otherCommand.Name;
//    }

//    public override bool Equals(object? obj) => obj is Command otherCommand && Equals(otherCommand);

//    public override int GetHashCode() => Name.GetHashCode();

//    public static bool operator ==(Command? leftCommand, Command? rightCommand)
//    {
//        if (leftCommand is null)
//            return rightCommand is null;

//        return leftCommand.Equals(rightCommand);
//    }

//    public static bool operator !=(Command? leftCommand, Command? rightCommand) =>
//        !(leftCommand == rightCommand);
//}