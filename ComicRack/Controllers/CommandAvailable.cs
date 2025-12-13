using cYo.Common.Collections;
using cYo.Common.Drawing;
using cYo.Common.Localize;
using cYo.Common.Mathematics;
using cYo.Common.Text;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Manager;
using cYo.Projects.ComicRack.Viewer.Views;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controllers;

internal class CommandAvailable
{

    #region Properties
    #region Main
    public static MainForm MainForm => Program.MainForm;

    //public static NavigatorManager OpenBooks => Program.MainForm.OpenBooks;

    public static ComicDisplay ComicDisplay => Program.MainForm.ComicDisplay;

    public static ComicBookNavigator CurrentBook => ComicDisplay.Book;
    #endregion

    #region ActiveService
    public static IComicBrowser ComicBrowser => MainForm.FindActiveService<IComicBrowser>();

    public static IEditPage GetPageEditor() => MainForm.GetPageEditor();

    public static IEditRating GetRatingEditor() => MainForm.GetRatingEditor();

    private static IEditBookmark GetBookmarkEditor() => MainForm.GetBookmarkEditor();

    private static ILibraryBrowser LibraryBrowser => MainForm.FindActiveService<ILibraryBrowser>();

    private static ISidebar SideBar => MainForm.FindActiveService<ISidebar>();

    private static ISearchOptions SearchFilter => MainForm.FindActiveService<ISearchOptions>();
    #endregion

    #region Boolean Helpers
    private static bool BookIsOpen => CurrentBook != null;

    private static bool HasComicBrowser => ComicBrowser != null;

    private static bool HasOpenTab => Program.MainForm.OpenBooks.Slots.Count > 0;

    private static bool HasMultipleOpenTabs => Program.MainForm.OpenBooks.Slots.Count > 1;

    private static bool HasSideBar => SideBar != null;

    private static bool HasSearchFilter => SearchFilter != null;
    #endregion
    #endregion

    #region File Menu
    //public static bool Open() => true;

    public static bool Close() => HasOpenTab;

    public static bool CloseAll() => HasOpenTab;

    //public static bool AddTab() => true;

    //public static bool AddFolder() => true;

    //public static bool ScanFolders() => true;

    //public static bool UpdateAllBooks() => true;

    //public static bool UpdateWebComics() => true;

    //public static bool GenerateCovers() => true;

    //public static bool ShowTasks() => true;

    //public static bool Automation() => true;

    //public static bool SynchronizeDevices() => true;

    //public static bool NewComic => true;

    //public static bool OpenRemoteLibrary() => true;

    public static bool OpenBooks() => Program.MainForm.OpenBooks.Slots.Count > 1;

    public static bool RecentBooks() => Program.Database.GetRecentFiles(Config.Settings.RecentFileCount).ToArray().Length > 0;

    //public static bool Restart() => true;

    //public static bool Exit() => true;
    #endregion

    #region Edit Menu
    public static bool ShowInfo() => MainForm.InvokeActiveService((IGetBookList bl) => !bl.GetBookList(ComicBookFilterType.Selected).IsEmpty(), defaultReturn: false);

    public static bool Undo() => Program.Database.Undo.CanUndo;

    public static bool Redo() => Program.Database.Undo.CanRedo;

    //public static readonly Command MyRating = new(Menu.Edit, "My R&ating");
    #region Rating
    public static bool Rate0() => GetRatingEditor().IsValid();
    public static bool Rate1() => GetRatingEditor().IsValid();
    public static bool Rate2() => GetRatingEditor().IsValid();
    public static bool Rate3() => GetRatingEditor().IsValid();
    public static bool Rate4() => GetRatingEditor().IsValid();
    public static bool Rate5() => GetRatingEditor().IsValid();
    public static bool QuickRating() => GetRatingEditor().IsValid();
    #endregion

    public static bool PageType = GetPageEditor().IsValid;

    public static bool PageRotation = GetPageEditor().IsValid;

    //public static bool Bookmarks => true;

    public static bool SetBookmark() => GetBookmarkEditor().CanBookmark;

    public static bool RemoveBookmark() => !string.IsNullOrEmpty(GetBookmarkEditor().Bookmark);

    public static bool PreviousBookmark() => CurrentBook?.CanNavigateBookmark(-1) ?? false;

    public static bool NextBookmark() => CurrentBook?.CanNavigateBookmark(1) ?? false;

    public static bool LastPageRead() => BookIsOpen && CurrentBook.CurrentPage != CurrentBook.Comic.LastPageRead;

    public static bool CopyPage() => BookIsOpen;

    public static bool ExportPage() => BookIsOpen;

    // TODO: Add
    //public static bool RefreshView()

    //public static bool ShowDevices() => true;

    //public static bool ShowPreferences() => true;
    #endregion

    #region Browse Menu
    //public static bool ToggleBrowser() => true;

    //public static bool ViewLibrary() => true;

    //public static bool ViewFolders() => true;

    //public static bool ViewPages() => true;

    public static bool ToggleSidebar() => HasSideBar;

    public static bool ToggleSmallPreview() => HasSideBar;

    public static bool ToggleSearchFilter() => HasSearchFilter;

    public static bool ToggleInfoPanel() => SideBar?.HasInfoPanels ?? false;

    public static bool PreviousList() => LibraryBrowser?.CanBrowsePrevious() ?? false;

    public static bool NextList() => LibraryBrowser?.CanBrowseNext() ?? false;

    //public static readonly Command Workspaces = new(Menu.Browse, "&Workspaces");

    //public static bool SaveWorkspace() => true;

    public static bool EditWorkspaces() => WorkspaceManager.Workspaces.Count > 0;

    //public static readonly Command ListLayout = new(Menu.Browse, "List Layout");

    public static bool EditListLayout() => HasComicBrowser;

    //public static bool SaveListLayout() => true;

    public static bool EditAllListLayout() => Program.Settings.ListConfigurations.Count > 0;

    //public static bool SetAllListLayout() => true;
    #endregion

    #region Read Menu

    public static bool FirstPage() => CurrentBook?.CanNavigate(-1) ?? false;

    public static bool PreviousPage() => BookIsOpen;

    public static bool NextPage() => BookIsOpen;

    public static bool LastPage() => CurrentBook?.CanNavigate(1) ?? false;

    public static bool PreviousBook() => CurrentBook?.Comic != null;

    public static bool NextBook() => CurrentBook?.Comic != null;

    public static bool RandomBook() => CurrentBook?.Comic != null;

    public static bool BrowseToBook() => BookIsOpen;

    public static bool PreviousTab() => HasMultipleOpenTabs;

    public static bool NextTab() => HasMultipleOpenTabs;

    public static bool ToggleAutoScroll() => true;
    public static bool ToggleDoublePageAutoScroll() => true;
    public static bool ToggleTrackCurrentPage() => true;
    #endregion

    //public static bool PageLayout() => true;

    //public static bool ToggleNavigationOverlay() => true;

    //public static bool ToggleRealisticPages() => true;

    #region Display Menu

    //public static bool ShowDisplaySettings() => true;

    #region Zoom
    //public static bool Zoom() => true;

    public static bool ZoomIn() => ComicDisplay.ImageZoom < 8f;

    public static bool ZoomOut() => ComicDisplay.ImageZoom > 1f;

    //public static bool ToggleZoom() => true;

    //public static bool Zoom100() => true;

    //public static bool Zoom125() => true;

    //public static bool Zoom150() => true;

    //public static bool Zoom200() => true;

    //public static bool Zoom400() => true;

    //public static bool ShowZoomCustom() => true;
    #endregion

    #region Rotation
    //public static bool RotateLeft() => true;

    //public static bool RotateRight() => true;

    //public static bool Rotate0() => true;

    //public static bool Rotate90() => true;

    //public static bool Rotate180() => true;

    //public static bool Rotate270() => true;

    //public static bool ToggleAutoRotate() => true;
    #endregion

    #region Page Layout
    //public static bool OriginalSize() => true;

    //public static bool FitAll() => true;

    //public static bool FitWidth() => true;

    //public static bool FitWidthAdaptive() => true;

    //public static bool FitHeight() => true;

    //public static bool FitBest() => true;

    //public static bool SinglePage() => true;

    //public static bool TwoPages() => true;

    //public static bool TwoPagesAdaptive() => true;

    //public static bool TogglePageFit() => true;

    //public static bool ToggleRtL() => true;

    //public static bool ToggleOversizeFit() => true;
    #endregion

    //public static bool ToggleMinimalGui() => true;

    //public static bool ToggleFullScreen() => true;

    //public static bool ToggleReaderWindow() => true;

    //public static bool ToggleMagnifier() => true;
    #endregion
}
