#nullable enable
using cYo.Common.Localize;
using cYo.Common.Text;
using cYo.Common.Windows;
using cYo.Projects.ComicRack.Viewer.Properties;
using Microsoft.Scripting.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Icon = cYo.Projects.ComicRack.Viewer.Controllers.CommandIcons;
using Key = cYo.Projects.ComicRack.Viewer.Controllers.CommandKeys;

namespace cYo.Projects.ComicRack.Viewer.Controllers;

public enum Menu
{
    None,
    File,
    Edit,
    Browse,
    Read,
    Display,
    //Library,
    //View,
    //Settings,
    Help
}

public enum SubMenu
{
    None,
    // File
    Automation,
    OpenBooks,
    RecentBooks,
    Read,
    Display,
    // Edit
    MyRating,
    PageType,
    PageRotation,
    Bookmarks,
    // Browse
    Workspaces,
    ListLayout,
    // Display
    PageLayout,
    Zoom,
    Rotation,
    // Help
    Help,
    Plugins,
    ChooseHelpSystem
}

public enum Group
{
    None,
    Library,
    Browse,
    AutoScroll,
    Scroll,
    DisplayOptions,
    PageDisplay,
    ZoomAndRotate,
    Edit,
    Other
}


public interface ICommand
{
    string Name { get; }

    Menu Menu { get; }

    SubMenu SubMenu { get; }

    string Text { get; }

    string ToolTipText { get; }

    Image Image { get; }

    Keys[] Keys { get; }

    Action Action { get; }

    Func<bool> CanExecute { get; }

    Func<bool> Show { get; }
}

public sealed class Command : ICommand, IEquatable<Command>
{
    #region Public Properties
    /// <value>Command Name. Statically populated from <see cref="FieldInfo"/>.Name.</value>
    public string Name { get; private set; }

    /// <value>Menu that this Command belongs to.</value>
    public Menu Menu { get; set; }

    /// <value>SubMenu that this Command belongs to.</value>
    public SubMenu SubMenu { get;  set; }

    /// <value>Command <see cref="MenuItem.Text"/></value>
    public string Text { get; private set; } = string.Empty;

    /// <value>Command <see cref="MenuItem"/>.ToolTipText</value>
    public string ToolTipText { get; private set; } = string.Empty;

    /// <value>Command <see cref="MenuItem"/>.Image Image</value>
    public Image Image { get; private set; } = Resources.NotValidated;

    /// <value>Command <see cref="MenuItem"/>.ShortcutKeys</value>
    public Keys[] Keys { get; private set; } = [/*empty*/];

    /// <value>Action that will be executed when this command is executed</value>
    public Action Action { get; set; } = () => {
        MessageBox.Show(
            Program.MainForm,
            "Command is not mapped.",
            "Unmapped Command",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    };

    /// <value>Indicates whether <see cref="Command.Action"/> can currently be executed.</value>
    public Func<bool> CanExecute { get; set; } = () => true;

    public Func<bool> Show { get; set; } = () => true;

    public EventHandler EventHandler { get; set; }

    public Action<ToolStripItem> UpdateHandler { get; set; }
    #endregion

    /// <summary>Statically populated <see cref="Dictionary{TKey, TValue}"/> of <see cref="Command.Name"/> and <see cref="Command"/></summary>
    private static readonly Dictionary<string, Command> commands;

    // --- Internal constructor ---
    #region Overloads
    // Go home Roslyn, you're drunk
    // Non-nullable field must contain a non-null value when exiting constructor.
#pragma warning disable CS8618

    private Command() => new Command(Image, Keys, Text);

    private Command(string itemText) => new Command(Image, Keys, itemText);

    private Command(Keys[] shortcut, string itemText) => new Command(Image, shortcut, itemText);

    private Command(Image image, string itemText) => new Command(image, Keys, itemText);
    // Non-nullable field must contain a non-null value when exiting constructor.
#pragma warning restore CS8618 
    #endregion

    private Command(Image image, Keys[] shortcut, string itemText)
    {
        Image = image;
        Keys = shortcut;
        Text = itemText;
    }

    public static readonly Command None = new();

    public static readonly Command Separator = new();
    // --- All Commands ---
    #region File Menu

    public static readonly Command Open = new(Icon.Open, Key.Open, "&Open...");
    public static readonly Command Close = new(Key.Close, "&Close");
    public static readonly Command CloseAll = new(Key.CloseAll, "Close A&ll");

    public static readonly Command AddTab = new(Icon.AddTab, Key.AddTab, "New &Tab");

    public static readonly Command AddFolder = new(Icon.AddFolder, Key.AddFolder, "&Add Folder to Library...");
    public static readonly Command ScanFolders = new(Icon.ScanFolders, Key.ScanFolders, "Scan Book &Folders");
    public static readonly Command UpdateAllBooks = new(Icon.UpdateAllBooks, Key.UpdateAllBooks, "Update all Book Files");
    public static readonly Command UpdateWebComics = new(Icon.UpdateWebComics, Key.UpdateWebComics, "Update Web Comics");
    public static readonly Command SynchronizeDevices = new(Icon.SynchronizeDevices, "Synchronize Devices");
    public static readonly Command GenerateCovers = new(Icon.GenerateCovers, "Generate Cover Thumbnails");
    public static readonly Command ShowTasks = new(Icon.ShowTasks, Key.ShowTasks, " & Tasks...");
    public static readonly Command Automation = new("A&utomation");

    public static readonly Command NewComic = new(Key.NewComic, " & New fileless Book Entry...");
    public static readonly Command OpenRemoteLibrary = new(Icon.OpenRemoteLibrary, "Open Remote Library...");

    public static readonly Command OpenBooks = new("Open Books");
    public static readonly Command RecentBooks = new("&Recent Books");

    public static readonly Command Restart = new(Icon.Restart, Key.Restart, "Rest&art");
    public static readonly Command Exit = new(Key.Exit, "&Exit");
    #endregion

    #region Edit Menu
    public static readonly Command ShowInfo = new(Icon.ShowInfo, "Info...");

    public static readonly Command Undo = new(Icon.Undo, "&Undo");
    public static readonly Command Redo = new(Icon.Redo, "&Redo");

    public static readonly Command MyRating = new("My R&ating");
    public static readonly Command Rate0 = new("None");
    public static readonly Command Rate1 = new("* (1 Star)");
    public static readonly Command Rate2 = new("** (2 Stars)");
    public static readonly Command Rate3 = new("*** (3 Stars)");
    public static readonly Command Rate4 = new("**** (4 Stars)");
    public static readonly Command Rate5 = new("***** (5 Stars)");
    public static readonly Command QuickRating = new("Quick Rating and Review...");

    public static readonly Command PageType = new("&Page Type");

    public static readonly Command PageRotation = new("Page Rotation");
    public static readonly Command RotatePage0 = new(Icon.RotatePage0, Key.RotatePage0, "&No Rotation");
    public static readonly Command RotatePage90 = new(Icon.RotatePage90, "Rotate90");
    public static readonly Command RotatePage180 = new(Icon.RotatePage180, "Rotate180");
    public static readonly Command RotatePage270 = new(Icon.RotatePage270, Key.RotatePage270, "Rotate270");

    public static readonly Command Bookmarks = new(Icon.Bookmarks, "&Bookmarks");
    public static readonly Command SetBookmark = new(Icon.SetBookmark, "Set Bookmark...");
    public static readonly Command RemoveBookmark = new(Icon.RemoveBookmark, "Remove Bookmark");
    public static readonly Command PreviousBookmark = new(Icon.PreviousBookmark, "Previous Bookmark");
    public static readonly Command NextBookmark = new(Icon.NextBookmark, "Next Bookmark");
    public static readonly Command LastPageRead = new("L&ast Page Read");

    public static readonly Command CopyPage = new(Icon.CopyPage, "&Copy Page");
    public static readonly Command ExportPage = new("&Export Page...");

    public static readonly Command RefreshView = new(Icon.RefreshView, "&Refresh");

    public static readonly Command ShowDevices = new(Icon.ShowDevices, "Devices...");
    public static readonly Command ShowPreferences = new(Icon.ShowPreferences, " & Preferences...");
    #endregion

    #region Browse Menu
    public static readonly Command ToggleBrowser = new(Icon.ToggleBrowser, "&Browser");

    public static readonly Command ViewLibrary = new(Icon.ViewLibrary, "Li&brary");
    public static readonly Command ViewFolders = new(Icon.ViewFolders, "&Folders");
    public static readonly Command ViewPages = new(Icon.ViewPages, "&Pages");

    public static readonly Command ToggleSidebar = new(Icon.ToggleSidebar, "&Sidebar");
    public static readonly Command TogglePreview = new(Icon.TogglePreview, "S&mall Preview");
    public static readonly Command ToggleSearchFilter = new(Icon.ToggleSearchFilter, "S&earch Browser");

    public static readonly Command ToggleInfoPanel = new(Icon.ToggleInfoPanel, "Info Panel");

    public static readonly Command PreviousList = new(Icon.PreviousList, "Previous List");
    public static readonly Command NextList = new(Icon.NextList, "Next List");

    public static readonly Command Workspaces = new(Icon.Workspaces, "&Workspaces");
    public static readonly Command SaveWorkspace = new("&Save Workspace...");
    public static readonly Command EditWorkspaces = new("&Edit Workspaces...");

    public static readonly Command ListLayout = new(Icon.ListLayout, "List Layout");
    public static readonly Command EditListLayout = new("&Edit List Layout...");
    public static readonly Command SaveListLayout = new("&Save List Layout...");
    public static readonly Command EditAllListLayout = new("&Edit Layouts...");
    public static readonly Command SetAllListLayout = new("Set all Lists to current Layout");
    #endregion

    #region Read Menu
    public static readonly Command GoFirstPage = new(Icon.GoFirstPage, "&First Page");
    public static readonly Command GoPreviousPage = new(Icon.GoPreviousPage, "&Previous Page"); //PrevPage
    public static readonly Command GoNextPage = new(Icon.GoNextPage, "&Next Page");
    public static readonly Command GoLastPage = new(Icon.GoLastPage, "&Last Page");

    public static readonly Command PreviousBook = new(Icon.PreviousBook, "Pre&vious Book"); // PrevFromList
    public static readonly Command NextBook = new(Icon.NextBook, "Ne&xt Book"); // NextFromList
    public static readonly Command RandomBook = new(Icon.RandomBook, "Random Book"); // RandomFromList
    public static readonly Command BrowseToBook = new(Icon.BrowseToBook, "Show in &Browser"); // SyncBrowser

    public static readonly Command PreviousTab = new(Icon.PreviousTab, "&Previous Tab"); // PrevTab
    public static readonly Command NextTab = new(Icon.NextTab, "Next &Tab");

    public static readonly Command ToggleAutoScroll = new(Icon.AutoScroll, "&Auto Scrolling");
    public static readonly Command ToggleDoublePageAutoScroll = new(Icon.DoublePageAutoScroll, "Double Page Auto Scrolling");

    public static readonly Command ToggleTrackCurrentPage = new("Track current Page");
    #endregion

    #region Display Menu
    public static readonly Command ShowDisplaySettings = new(Icon.ShowDisplaySettings, "Book Display Settings...");

    public static readonly Command PageLayout = new("&Page Layout");
    public static readonly Command OriginalSize = new("Original Size");
    public static readonly Command FitAll = new("Fit &All");
    public static readonly Command FitWidth = new("Fit &Width");
    public static readonly Command FitWidthAdaptive = new("Fit Width (Adaptive)");
    public static readonly Command FitHeight = new("Fit &Height");
    public static readonly Command FitBest = new("Fit &Best");
    public static readonly Command SinglePage = new("Single Page");
    public static readonly Command TwoPages = new("Two Pages");
    public static readonly Command TwoPagesAdaptive = new("Two Pages (Adaptive)");
    public static readonly Command ToggleRtL = new("Right to Left");
    public static readonly Command ToggleOversizeFit = new("&Only fit if oversized");

    public static readonly Command Zoom = new("Zoom");
    public static readonly Command ZoomIn = new("Zoom &In");
    public static readonly Command ZoomOut = new("Zoom &Out");
    public static readonly Command ToggleZoom = new("Toggle Zoom");
    public static readonly Command Zoom100 = new("Zoom 100%");
    public static readonly Command Zoom125 = new("Zoom 125%");
    public static readonly Command Zoom150 = new("Zoom 150%");
    public static readonly Command Zoom200 = new("Zoom 200%");
    public static readonly Command Zoom400 = new("Zoom 400%");
    public static readonly Command ShowZoomCustom = new("&Custom...");

    public static readonly Command Rotation = new("&Rotation");
    public static readonly Command RotateDisplayLeft = new(Icon.RotateDisplayLeft, "Rotate Left");
    public static readonly Command RotateDisplayRight = new(Icon.RotateDisplayRight, "Rotate Right");
    public static readonly Command RotateDisplay0 = new(Icon.RotateDisplay0, "&No Rotation");
    public static readonly Command RotateDisplay90 = new(Icon.RotateDisplay90, "Rotate90");
    public static readonly Command RotateDisplay180 = new(Icon.RotateDisplay180, "Rotate180");
    public static readonly Command RotateDisplay270 = new(Icon.RotateDisplay270, "Rotate270");
    public static readonly Command ToggleAutoRotate = new(Icon.ToggleAutoRotate, "Autorotate Double Pages");

    public static readonly Command ToggleMinimalGui = new("Minimal User Interface");
    public static readonly Command ToggleFullScreen = new(Icon.ToggleFullScreen, "&Full Screen");
    public static readonly Command ToggleReaderWindow = new(Icon.ToggleReaderWindow, "Reader in &own Window");
    public static readonly Command ToggleMagnifier = new(Icon.ToggleMagnifier, "&Magnifier");
    #endregion

    #region Help Menu
    public static readonly Command Help = new("Help");
    public static readonly Command WebHelp = new(Icon.WebHelp, "ComicRack Documentation...");

    public static readonly Command Plugins = new("Plugins");

    public static readonly Command ChooseHelp = new("Choose Help System");

    public static readonly Command QuickIntro = new("Quick Introduction");

    public static readonly Command WebHomepage = new(Icon.WebHomepage, "ComicRack Homepage...");
    public static readonly Command WebUserForum = new(Icon.WebUserForum, "ComicRack User Forum...");

    public static readonly Command ShowNews = new(Icon.ShowNews, "&News...");
    public static readonly Command CheckUpdate = new("Check For Update...");

    public static readonly Command ShowAbout = new(Icon.ShowAbout, "&About...");
    #endregion

    //public static readonly Command AddFolder = new("&Add Folder...");
    //public static readonly Command ScanFolders = new("Scan &Folders");
    //public static readonly Command UpdateBooks = new("Update Books");
    //public static readonly Command GenerateCovers = new("Generate Covers");

    // static initialization
    static Command()
    {
        commands = typeof(Command)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(field => field.FieldType == typeof(Command))
            .ToDictionary(
                field => field.Name,
                field => {
                    Command command = (Command)field.GetValue(null)!;
                    command.Name ??= field.Name; // Assign Name if not set

                    // We could add Action, CanExecute, EventHandler etc here, but in the interest of
                    // sticking to some industry standard, this class should could contain only static definitions.
                    // Static in the development sense, not the c# language sense

                    return command;
                }
            );
    }

    public static Command? FromName(string name) => commands.TryGetValue(name, out var cmd) ? cmd : null;

    /// <summary>Statically populated <see cref="IEnumerable{T}"/> of every registered <see cref="Command"/></summary>
    public static IEnumerable<Command> All => commands.Values;

    public override string ToString() => Name;

    public static implicit operator Command(string cmdName) => FromName(cmdName) ?? None;

    public bool Equals(Command? otherCommand)
    {
        if (ReferenceEquals(this, otherCommand))
            return true;

        if (otherCommand is null)
            return false;

        return Name == otherCommand.Name;
    }

    public override bool Equals(object? obj) => obj is Command otherCommand && Equals(otherCommand);

    public override int GetHashCode() => Name.GetHashCode();

    public static bool operator ==(Command? leftCommand, Command? rightCommand)
    {
        if (leftCommand is null)
            return rightCommand is null;

        return leftCommand.Equals(rightCommand);
    }

    public static bool operator !=(Command? leftCommand, Command? rightCommand) =>
        !(leftCommand == rightCommand);
}