using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using cYo.Projects.ComicRack.Viewer.Properties;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cYo.Projects.ComicRack.Viewer.Manager;


namespace cYo.Projects.ComicRack.Viewer.Controllers;

internal class CommandUpdate
{
    public static MainForm MainForm => Program.MainForm;

    //public static NavigatorManager OpenBooks => Program.MainForm.OpenBooks;

    public static ComicDisplay ComicDisplay => Program.MainForm.ComicDisplay;

    public static ComicBookNavigator CurrentBook => ComicDisplay.Book;

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
    //public static void Open() => {};

    //public static void Close() => {};

    //public static void CloseAll() => {};

    //public static void AddTab() => {};

    //public static void AddFolder() => {};

    //public static void ScanFolders() => {};

    //public static void UpdateAllBooks() => {};

    //public static void UpdateWebComics() => {};

    //public static void GenerateCovers() => {};

    //public static void ShowTasks() => {};

    //public static void Automation() => {};

    //public static void SynchronizeDevices() => { };

    //public static void NewComic => {};

    //public static void OpenRemoteLibrary() => {};

    //public static void OpenBooks() => {};

    //public static void RecentBooks() => {};

    //public static void Restart() => {};

    //public static void Exit() => {};
    #endregion

    #region Edit Menu
    //public static void ShowInfo() { }

    //public static void Undo() { }

    //public static void Redo() { }

    //public static readonly Command MyRating = new(Menu.Edit, "My R&ating");
    #region Rating
    //public static void Rate0() { }
    //public static void Rate1() { }
    //public static void Rate2() { }
    //public static void Rate3() { }
    //public static void Rate4() { }
    //public static void Rate5() { }
    //public static void QuickRating() { }
    #endregion

    //public static void PageType = GetPageEditor() { }

    //public static void PageRotation = GetPageEditor() { }

    //public static void Bookmarks => true;

    //public static void SetBookmark() { }

    //public static void RemoveBookmark() { }

    //public static void PreviousBookmark() { }

    //public static void NextBookmark() { }

    //public static void LastPageRead() { }

    //public static void CopyPage() { }

    //public static void ExportPage() { }

    // TODO: Add
    //public static void RefreshView()

    //public static void ShowDevices() { }

    //public static void ShowPreferences() { }
    #endregion

    #region Browse Menu
    //public static void ToggleBrowser() { }

    //public static void ViewLibrary() { }

    //public static void ViewFolders() { }

    //public static void ViewPages() { }

    //public static void ToggleSidebar() { }

    //public static void ToggleSmallPreview() { }

    //public static void ToggleSearchFilter() { }

    //public static void ToggleInfoPanel() { }

    //public static void PreviousList() { }

    //public static void NextList() { }

    public static void Workspaces(ToolStripItem tsItem)
        => WorkspaceManager.UpdateWorkspaceMenus(((ToolStripMenuItem)tsItem).DropDownItems);

    //public static void SaveWorkspace() { }

    //public static void EditWorkspaces() { }

    public static void ListLayout(ToolStripItem tsItem)
        => ListManager.UpdateListConfigMenus(((ToolStripMenuItem)tsItem).DropDownItems);

    //public static void EditListLayout() { }

    //public static void SaveListLayout() { }

    //public static void EditAllListLayout() { }

    //public static void SetAllListLayout() { }
    #endregion

    #region Read Menu

    //public static void FirstPage() { }

    //public static void PreviousPage() { }

    //public static void NextPage() { }

    //public static void LastPage() { }

    //public static void PreviousBook() { }

    //public static void NextBook() { }

    //public static void RandomBook() { }

    //public static void BrowseToBook() { }

    //public static void PreviousTab() { }

    //public static void NextTab() { }

    //public static void ToggleAutoScroll() { }
    //public static void ToggleDoublePageAutoScroll() { }
    //public static void ToggleTrackCurrentPage() { }
    #endregion

    //public static void PageLayout() { }

    //public static void ToggleNavigationOverlay() { }

    //public static void ToggleRealisticPages() { }

    #region Display Menu

    //public static void ShowDisplaySettings(){}

    #region Zoom
    //public static void Zoom(){}

    //public static void ZoomIn(){}

    //public static void ZoomOut(){}

    //public static void ToggleZoom(){}

    //public static void Zoom100(){}

    //public static void Zoom125(){}

    //public static void Zoom150(){}

    //public static void Zoom200(){}

    //public static void Zoom400(){}

    //public static void ShowZoomCustom(){}
    #endregion

    #region Rotation
    //public static void RotateLeft(){}

    //public static void RotateRight(){}

    //public static void Rotate0(){}

    //public static void Rotate90(){}

    //public static void Rotate180(){}

    //public static void Rotate270(){}

    //public static void ToggleAutoRotate(){}
    #endregion

    #region Page Layout
    //public static void OriginalSize(){}

    //public static void FitAll(){}

    //public static void FitWidth(){}

    //public static void FitWidthAdaptive(){}

    //public static void FitHeight(){}

    //public static void FitBest(){}

    //public static void SinglePage(){}

    //public static void TwoPages(){}

    //public static void TwoPagesAdaptive(){}

    //public static void TogglePageFit(){}

    //public static void ToggleRtL(){}

    //public static void ToggleOversizeFit(){}
    #endregion

    //public static void ToggleMinimalGui(){}

    //public static void ToggleFullScreen(){}

    //public static void ToggleReaderWindow(){}

    public static void ToggleMagnifier(ToolStripItem tsItem)
        => ((ToolStripMenuItem)tsItem).Image = ComicDisplay.MagnifierVisible ? Resources.Zoom : Resources.ZoomClear;
   #endregion
}
