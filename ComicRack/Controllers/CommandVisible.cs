using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cYo.Projects.ComicRack.Viewer.Controllers;

internal class CommandVisible
{
    #region Main
    public static MainForm MainForm => Program.MainForm;

    public static NavigatorManager OpenBooks => Program.MainForm.OpenBooks;

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

    #region File Menu
    //public static bool Open() => true;

    //public static bool Close() => true;

    //public static bool CloseAll() => true;

    //public static bool AddTab() => true;

    //public static bool AddFolder() => true;

    //public static bool ScanFolders() => true;

    public static bool UpdateAllBooks() => !Program.Settings.AutoUpdateComicsFiles;

    //public static bool UpdateWebComics() => true;

    //public static bool GenerateCovers() => true;

    //public static bool ShowTasks() => true;

    public static bool Automation() => ScriptUtility.Scripts.GetCommands(PluginEngine.ScriptTypeLibrary).Count() > 1;

    public static bool SynchronizeDevices() => Program.Settings.Devices.Count > 0;

    //public static bool NewComic => true;

    //public static bool OpenRemoteLibrary() => true;

    //public static bool OpenBooks() => true;

    //public static bool RecentBooks() => true;

    //public static bool Restart() => true;

    //public static bool Exit() => true;
    #endregion

    #region Edit Menu
    //public static bool ShowInfo() => true;

    //public static bool Undo() => true;

    //public static bool Redo() => true;

    //public static readonly Command MyRating = new(Menu.Edit, "My R&ating");
    #region Rating
    //public static bool Rate0() => true;
    //public static bool Rate1() => true;
    //public static bool Rate2() => true;
    //public static bool Rate3() => true;
    //public static bool Rate4() => true;
    //public static bool Rate5() => true;
    //public static bool QuickRating() => true;
    #endregion

    //public static bool PageType = GetPageEditor() => true;

    //public static bool PageRotation = GetPageEditor() => true;

    //public static bool Bookmarks => true;

    //public static bool SetBookmark() => true;

    //public static bool RemoveBookmark() => true;

    //public static bool PreviousBookmark() => true;

    //public static bool NextBookmark() => true;

    //public static bool LastPageRead() => true;

    //public static bool CopyPage() => true;

    //public static bool ExportPage() => true;

    // TODO: Add
    //public static bool RefreshView()

    //public static bool ShowDevices() => true;

    //public static bool ShowPreferences() => true;
    #endregion

    #region Browse Menu
    //public static bool ToggleBrowser() => true;

    public static bool ViewLibrary() => !Program.ExtendedSettings.DisableFoldersView;

    public static bool ViewFolders() => !Program.ExtendedSettings.DisableFoldersView;

    //public static bool ViewPages() => true;

    //public static bool ToggleSidebar() => true;

    //public static bool ToggleSmallPreview() => true;

    //public static bool ToggleSearchFilter() => true;

    //public static bool ToggleInfoPanel() => true;

    //public static bool PreviousList() => true;

    //public static bool NextList() => true;

    //public static readonly Command Workspaces = new(Menu.Browse, "&Workspaces");

    //public static bool SaveWorkspace() => true;

    //public static bool EditWorkspaces() => true;

    //public static readonly Command ListLayout = new(Menu.Browse, "List Layout");

    //public static bool EditListLayout() => true;

    //public static bool SaveListLayout() => true;

    //public static bool EditAllListLayout() => true;

    //public static bool SetAllListLayout() => true;
    #endregion

    #region Read Menu

    //public static bool FirstPage() => true;

    //public static bool PreviousPage() => true;

    //public static bool NextPage() => true;

    //public static bool LastPage() => true;

    //public static bool PreviousBook() => true;

    //public static bool NextBook() => true;

    //public static bool RandomBook() => true;

    //public static bool BrowseToBook() => true;

    //public static bool PreviousTab() => true;

    //public static bool NextTab() => true;

    //public static bool ToggleAutoScroll() => true;
    //public static bool ToggleDoublePageAutoScroll() => true;
    //public static bool ToggleTrackCurrentPage() => true;
    #endregion

    //public static bool PageLayout() => true;

    //public static bool ToggleNavigationOverlay() => true;

    //public static bool ToggleRealisticPages() => true;

    #region Display Menu

    //public static bool ShowDisplaySettings() => true;

    #region Zoom
    //public static bool Zoom() => true;

    //public static bool ZoomIn() => true;

    //public static bool ZoomOut() => true;

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
