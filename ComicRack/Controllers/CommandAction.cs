using cYo.Common.Collections;
using cYo.Common.Drawing;
using cYo.Common.Localize;
using cYo.Common.Mathematics;
using cYo.Common.Text;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.IO.Network;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Manager;
using cYo.Projects.ComicRack.Viewer.Views;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controllers;

// TODO : remove Program.MainForm calls
/// <summary>
/// Command Actions. Static; don't define anything that needs to refer to an instance.
/// </summary>
internal static class CommandAction
{
    public static IMain main => Program.MainForm;

    public static MainForm MainForm => Program.MainForm;

    public static IApplication app;

    public static Settings Settings => Program.Settings;

    public static ComicDisplay ComicDisplay => ComicDisplay;

    public static IComicBrowser ComicBrowser => MainForm.FindActiveService<IComicBrowser>();

    public static IEditPage GetPageEditor() => MainForm.GetPageEditor();

    public static IEditRating GetRatingEditor() => MainForm.GetRatingEditor();

    private static IEditBookmark GetBookmarkEditor() => MainForm.GetBookmarkEditor();

    private static ILibraryBrowser LibraryBrowser => MainForm.FindActiveService<ILibraryBrowser>();

    private static ISidebar SideBar => MainForm.FindActiveService<ISidebar>();

    private static ISearchOptions SearchFilter => MainForm.FindActiveService<ISearchOptions>();

    #region File Menu
    //public static void Open(string title)
    //{
    //    string filePath = ShowFileDialog.Open(main, title, includeReadingLists: true);
    //    if (filePath != null)
    //        MainForm.OpenFile(filePath, Settings.OpenInNewTab);
    //}

    public static void Open()
    {
        string filePath = ShowFileDialog.Open(main, "", includeReadingLists: true);
        if (filePath != null)
            MainForm.OpenFile(filePath, Settings.OpenInNewTab);
    }

    public static void Close() => main.OpenBooks.Close();
    public static void CloseAll() => main.OpenBooks.CloseAll();
    public static void AddTab() => main.OpenBooks.AddSlot();

    public static void AddFolder()
    {
        var dialog = new OpenFolderDialog()
        {
            Title = "Add Folder to Library", // TODO : Find translation key
            Multiselect = true
        };

        if (dialog.ShowDialog(main) == DialogResult.OK && dialog.ResultPaths.Count > 0)
            foreach (string folderPath in dialog.ResultPaths)
                Program.Scanner.ScanFileOrFolder(folderPath, all: true, removeMissing: false);
    }

    // old method,using FolderBrowserDialog
    //public static void AddFolder()
    //{
    //    using FolderBrowserDialog dialog = new()
    //    {
    //        Description = TR.Messages["AddFolderLibrary", "Books in this Folder and all sub Folders will be added to the library."],
    //        ShowNewFolderButton = true
    //    };
    //
    //    if (dialog.ShowDialog(main) == DialogResult.OK && !string.IsNullOrEmpty(dialog.SelectedPath))
    //        Program.Scanner.ScanFileOrFolder(dialog.SelectedPath, all: true, removeMissing: false);
    //}

    public static void ScanFolders()
        => Program.QueueManager.StartScan(all: true, Settings.RemoveMissingFilesOnFullScan);

    public static void UpdateAllBooks()
        => Program.Database.Books
            .Concat(Program.BookFactory.TemporaryBooks)
            .ForEach(book => Program.QueueManager.AddBookToFileUpdate(book, alwaysWrite: true));

    public static void UpdateWebComics()
        => Program.Database.Books
            .Concat(Program.BookFactory.TemporaryBooks)
            .ForEach(book => Program.QueueManager.AddBookToDynamicUpdate(book, refresh: false));

    public static void GenerateCovers()
        => Program.Database.Books
            .Concat(Program.BookFactory.TemporaryBooks)
            .ForEach(book => Program.ImagePool.GenerateFrontCoverThumbnail(book));

    // TODO : Move
    public static void ShowTasks() => MainForm.ShowPendingTasks();

    //public static readonly Command Automation = new(Menu.File, "A&utomation");

    public static void SynchronizeDevices()
    {
        main.StoreWorkspace(); // save workspace before sync, so sorted lists key are up to date
        if (!Program.QueueManager.SynchronizeDevices())
            main.ShowPortableDevices(null, null);
    }

    //public static readonly Command NewComic = new(Menu.File, "NewComic", " & New fileless Book Entry...");

    public static void OpenRemoteLibrary()
    {
        RemoteShareItem share = OpenRemoteDialog.GetShare(main,
            Settings.RemoteShares.First,
            Settings.RemoteShares,
            showPublic: false);

        if (share != null && !string.IsNullOrEmpty(share.Uri))
        {
            string serverName = share.Uri;
            ShareInformation serverInfo = null;
            AutomaticProgressDialog.Process(main, TR.Messages["ConnectToServer", "Connecting to Server"], TR.Messages["GetShareInfoText", "Getting information about the shared Library"], 1000, delegate
            {
                serverInfo = ComicLibraryClient.GetServerInfo(serverName);
            }, AutomaticProgressDialogOptions.EnableCancel);

            // TODO : AddRemoteLibrary
            if (serverInfo == null || !MainController.Commands.AddRemoteLibrary(serverInfo, MainView.AddRemoteLibraryOptions.Open | MainView.AddRemoteLibraryOptions.Select))
            {
                MessageBox.Show(
                    main,
                    StringUtility.Format(TR.Messages["ConnectRemoteError", "Failed to connect to remote Server"], share),
                    TR.Messages["Error", "Error"],
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                Settings.RemoteShares.UpdateMostRecent(new RemoteShareItem(serverInfo));
            }
        }
    }

    //public static readonly Command OpenBooks = new(Menu.File, "Open Books");

    //public static readonly Command RecentBooks = new(Menu.File, "&Recent Books");

    public static void Restart() => MainForm.MenuRestart();

    public static void Exit() => MainForm.MenuClose();
    #endregion

    #region Edit Menu
    public static void ShowInfo() => main.ShowInfo();

    public static void Undo() => Program.Database.Undo.Undo();

    public static void Redo() => Program.Database.Undo.Redo();

    //public static void MyRating() =>;
    #region Rating
    public static void Rate0() => GetRatingEditor().SetRating(0f);
    public static void Rate1() => GetRatingEditor().SetRating(1f);
    public static void Rate2() => GetRatingEditor().SetRating(2f);
    public static void Rate3() => GetRatingEditor().SetRating(3f);
    public static void Rate4() => GetRatingEditor().SetRating(4f);
    public static void Rate5() => GetRatingEditor().SetRating(5f);
    public static void QuickRating() => GetRatingEditor().QuickRatingAndReview();
    #endregion

    //public static void PageType() =>;

    //public static void PageRotation() =>;
    //public static void RotatePage0() => 
    //public static void RotatePage90() => GetPageEditor().Rotation = GetPageEditor().Rotation.RotateRight();
    //public static void RotatePage180() => 
    //public static void RotatePage270() => GetPageEditor().Rotation = GetPageEditor().Rotation.RotateLeft();

    //public static void Bookmarks() =>;

    public static void SetBookmark()
    {
        var bookmarkEditor = GetBookmarkEditor();
        if (bookmarkEditor.CanBookmark)
            bookmarkEditor.Bookmark = SelectItemDialog.GetName<string>(main,
                TR.Default["Bookmark", "Bookmark"],
                bookmarkEditor.BookmarkProposal,
                null);
    }

    public static void RemoveBookmark() => GetBookmarkEditor().Bookmark = string.Empty;

    public static void PreviousBookmark() => ComicDisplay.DisplayPreviousBookmarkedPage();

    public static void NextBookmark() => ComicDisplay.DisplayNextBookmarkedPage();

    public static void LastPageRead() => ComicDisplay.DisplayLastPageRead();

    public static void CopyPage()
    {
        try
        {
            Clipboard.SetImage(ComicDisplay.CreatePageImage());
        }
        catch
        {
        }
    }

    public static void ExportPage()
    {
        if (ComicDisplay.Book == null || ComicDisplay.Book.Comic == null)
            return;

        using Image image = ComicDisplay.CreatePageImage();
        if (image != null)
        {
            string name = StringUtility.Format("{0} - {1} {2}", ComicDisplay.Book.Comic.Caption, TR.Default["Page", "Page"], ComicDisplay.Book.CurrentPage + 1);
            string filePath = ShowFileDialog.SaveImage(main, name);

            if (filePath != null)
                SaveImage(image, filePath, Program.Settings.LastExportPageFilterIndex);
        }
    }

    // TODO: Add
    //public static void RefreshView()

    public static void ShowDevices() => main.ShowPortableDevices(null, null);

    public static void ShowPreferences() => ShowPreferences();
    #endregion

    #region Browse Menu

    public static void ToggleBrowser() => main.ToggleBrowser();

    public static void ViewLibrary()
    {
        main.BrowserVisible = true;
        MainForm.ShowLibrary();
    }

    public static void ViewFolders()
    {
        main.BrowserVisible = true;
        MainForm.ShowFolders();
    }

    public static void ViewPages()
    {
        main.BrowserVisible = true;
        MainForm.ShowPages();
    }

    public static void ToggleSidebar()
    {
        if (SideBar != null)
            SideBar.Visible = !SideBar.Visible;
    }

    public static void ToggleSmallPreview()
    {
        if (SideBar != null)
            SideBar.Preview = !SideBar.Preview;
    }

    public static void ToggleSearchFilter()
    {
        if (SearchFilter != null)
            SearchFilter.SearchBrowserVisible = !SearchFilter.SearchBrowserVisible;
    }

    public static void ToggleInfoPanel()
    {
        if (SideBar != null)
            SideBar.Info = !SideBar.Info;
    }

    public static void PreviousList() => LibraryBrowser?.BrowsePrevious();

    public static void NextList() => LibraryBrowser?.BrowseNext();

    //public static readonly Command Workspaces = new(Menu.Browse, "&Workspaces");

    public static void SaveWorkspace() => WorkspaceManager.SaveWorkspace();

    public static void EditWorkspaces() => WorkspaceManager.EditWorkspace(main);

    //public static readonly Command ListLayout = new(Menu.Browse, "List Layout");

    public static void EditListLayout() => ListManager.EditListLayout();

    public static void SaveListLayout() => ListManager.SaveListLayout();

    public static void EditAllListLayout() => ListManager.EditListLayouts();

    public static void SetAllListLayout() => ListManager.SetListLayoutToAll();
    #endregion

    #region Read Menu

    public static void FirstPage() => ComicDisplay.DisplayFirstPage();

    public static void PreviousPage() => ComicDisplay.DisplayPreviousPage(ComicDisplay.PagingMode.Double);

    public static void NextPage() => ComicDisplay.DisplayNextPage(ComicDisplay.PagingMode.Double);

    public static void LastPage() => ComicDisplay.DisplayLastPage();

    public static void PreviousBook() => MainForm.OpenPrevComic();

    public static void NextBook() => MainForm.OpenNextComic();

    public static void RandomBook() => MainForm.OpenRandomComic();

    // original. afaik returned bool is not used
    //public bool SyncBrowser()
    //{
    //    if (ComicDisplay == null || ComicDisplay.Book == null || ComicDisplay.Book.Comic == null)
    //    {
    //        return false;
    //    }
    //    ComicBook comic = ComicDisplay.Book.Comic;
    //    IComicBrowser comicBrowser = FindServices<IComicBrowser>().FirstOrDefault((IComicBrowser b) => b.Library == comic.Container);
    //    if (comicBrowser == null)
    //    {
    //        return false;
    //    }
    //    ToggleBrowser(alwaysShow: true, comicBrowser);
    //    if (comicBrowser.SelectComic(ComicDisplay.Book.Comic))
    //    {
    //        return true;
    //    }
    //    if (comic.LastOpenedFromListId != Guid.Empty)
    //    {
    //        ComicListItem comicListItem = comicBrowser.Library.ComicLists.GetItems<ComicListItem>().FirstOrDefault((ComicListItem li) => li.Id == comic.LastOpenedFromListId);
    //        if (comicListItem != null && ShowBookInList(comicBrowser.Library, comicListItem, comic))
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    public static void BrowseToBook()
    {
        if (ComicDisplay?.Book?.Comic == null)
            return;

        ComicBook comic = ComicDisplay.Book.Comic;
        IComicBrowser comicBrowser = MainForm
            .FindServices<IComicBrowser>()
            .FirstOrDefault((IComicBrowser b) => b.Library == comic.Container);
        if (comicBrowser == null)
            return;

        MainForm.ToggleBrowser(alwaysShow: true, comicBrowser);
        if (comicBrowser.SelectComic(ComicDisplay.Book.Comic))
            return;

        if (comic.LastOpenedFromListId != Guid.Empty)
        {
            ComicListItem comicListItem = comicBrowser.Library.ComicLists.GetItems<ComicListItem>().FirstOrDefault((ComicListItem li) => li.Id == comic.LastOpenedFromListId);
            if (comicListItem != null && MainForm.ShowBookInList(comicBrowser.Library, comicListItem, comic))
                return;
        }
        return;
    }

    public static void PreviousTab() => main.OpenBooks.PreviousSlot();

    public static void NextTab() => main.OpenBooks.NextSlot();

    public static void ToggleAutoScroll() => Settings.AutoScrolling = !Settings.AutoScrolling;
    public static void ToggleDoublePageAutoScroll() => ComicDisplay.TwoPageNavigation = !ComicDisplay.TwoPageNavigation;
    public static void ToggleTrackCurrentPage() => Settings.TrackCurrentPage = !Settings.TrackCurrentPage;

    public static void PageLayout()
    {
        switch (ComicDisplay.PageLayout)
        {
            case PageLayoutMode.Single:
                ComicDisplay.PageLayout = PageLayoutMode.Double;
                break;
            case PageLayoutMode.Double:
                ComicDisplay.PageLayout = PageLayoutMode.DoubleAdaptive;
                break;
            case PageLayoutMode.DoubleAdaptive:
                ComicDisplay.PageLayout = PageLayoutMode.Single;
                break;
        }
    }

    public static void ToggleNavigationOverlay() => ComicDisplay.NavigationOverlayVisible = !ComicDisplay.NavigationOverlayVisible;

    public static void ToggleRealisticPages() => ComicDisplay.RealisticPages = !ComicDisplay.RealisticPages;

    #endregion

    #region Display Menu

    public static void ShowDisplaySettings() => WorkspaceManager.EditWorkspaceDisplaySettings();

    #region Zoom
    public static void Zoom() => ComicDisplay.ImageZoom = Numeric.Select(
        ComicDisplay.ImageZoom,
        [
            1f,
            1.25f,
            1.5f,
            2f
        ],
        wrap: true);

    public static void ZoomIn() => ComicDisplay.ImageZoom = (ComicDisplay.ImageZoom + 0.1f).Clamp(1f, 8f);

    public static void ZoomOut() => ComicDisplay.ImageZoom = (ComicDisplay.ImageZoom - 0.1f).Clamp(1f, 8f);

    //public static readonly Command ToggleZoom = new(Menu.Display, SubMenu.Zoom, "Toggle Zoom");

    public static void Zoom100() => ComicDisplay.ImageZoom = 1f;

    public static void Zoom125() => ComicDisplay.ImageZoom = 1.25f;

    public static void Zoom150() => ComicDisplay.ImageZoom = 1.50f;

    public static void Zoom200() => ComicDisplay.ImageZoom = 2f;

    public static void Zoom400() => ComicDisplay.ImageZoom = 4f;

    public static void ShowZoomCustom() => ComicDisplay.ImageZoom = ZoomDialog.Show(main, ComicDisplay.ImageZoom);
    #endregion

    #region Rotation
    public static void RotateDisplayLeft() => ComicDisplay.ImageRotation = ComicDisplay.ImageRotation.RotateLeft();

    public static void RotateDisplayRight() => ComicDisplay.ImageRotation = ComicDisplay.ImageRotation.RotateRight();

    public static void RotateDisplay0() => ComicDisplay.ImageRotation = ImageRotation.None;

    public static void RotateDisplay90() => ComicDisplay.ImageRotation = ImageRotation.Rotate90;

    public static void RotateDisplay180() => ComicDisplay.ImageRotation = ImageRotation.Rotate180;

    public static void RotateDisplay270() => ComicDisplay.ImageRotation = ImageRotation.Rotate270;

    public static void ToggleAutoRotate() => ComicDisplay.ImageAutoRotate = !ComicDisplay.ImageAutoRotate;
    #endregion

    #region Page Layout
    public static void OriginalSize() => ComicDisplay.ImageFitMode = ImageFitMode.Original;

    public static void FitAll() => ComicDisplay.ImageFitMode = ImageFitMode.Fit;

    public static void FitWidth() => ComicDisplay.ImageFitMode = ImageFitMode.FitWidth;

    public static void FitWidthAdaptive() => ComicDisplay.ImageFitMode = ImageFitMode.FitWidthAdaptive;

    public static void FitHeight() => ComicDisplay.ImageFitMode = ImageFitMode.FitHeight;

    public static void FitBest() => ComicDisplay.ImageFitMode = ImageFitMode.BestFit;

    public static void SinglePage() => ComicDisplay.PageLayout = PageLayoutMode.DoubleAdaptive;

    public static void TwoPages() => ComicDisplay.PageLayout = PageLayoutMode.DoubleAdaptive;

    public static void TwoPagesAdaptive() => ComicDisplay.PageLayout = PageLayoutMode.DoubleAdaptive;

    public static void TogglePageFit() => ComicDisplay.ImageFitMode = (ImageFitMode)((int)(ComicDisplay.ImageFitMode + 1) % 6);

    public static void ToggleRtL() => ComicDisplay.RightToLeftReading = !ComicDisplay.RightToLeftReading;

    public static void ToggleOversizeFit() => ComicDisplay.ImageFitOnlyIfOversized = !ComicDisplay.ImageFitOnlyIfOversized;
    #endregion

    public static void ToggleMinimalGui() => main.MinimalGui = !main.MinimalGui;

    public static void ToggleFullScreen() => ComicDisplay.FullScreen = !ComicDisplay.FullScreen;

    public static void ToggleReaderWindow()
    {
        // ToggleUndockReader
        if (!main.ReaderUndocked)
            ComicDisplay.FullScreen = false;
        main.ReaderUndocked = !main.ReaderUndocked;
    }

    public static void ToggleMagnifier() => ComicDisplay.MagnifierVisible = !ComicDisplay.MagnifierVisible;
    #endregion

    #region Helpers

    private static void SaveImage(Image image, string filePath, int filterIndex)
    {
        try
        {
            switch (filterIndex)
            {
                case 1:
                    image.SaveImage(AddExtension(filePath, ".jpg"), ImageFormat.Jpeg, 24);
                    break;
                case 2:
                    image.SaveImage(AddExtension(filePath, ".bmp"), ImageFormat.Bmp, 24);
                    break;
                case 3:
                    image.SaveImage(AddExtension(filePath, ".png"), ImageFormat.Png, 24);
                    break;
                case 4:
                    image.SaveImage(AddExtension(filePath, ".gif"), ImageFormat.Gif, 8);
                    break;
                case 5:
                    image.SaveImage(AddExtension(filePath, ".tif"), ImageFormat.Tiff, 24);
                    break;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(main, StringUtility.Format(TR.Messages["CouldNotSaveImage", "Could not save the page image!\nReason: {0}"], ex.Message), TR.Messages["Error", "Error"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
    
    private static string AddExtension(string file, string ext)
    {
        if (!Path.HasExtension(file))
            return file + ext;
        return file;
    }

    #endregion
}
