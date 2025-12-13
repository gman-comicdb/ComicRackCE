using cYo.Projects.ComicRack.Viewer.Properties;
using System.Collections.Generic;
using System.Drawing;

namespace cYo.Projects.ComicRack.Viewer.Controllers;

internal static class CommandIcons
{
    // TODO : make fit model
    public static readonly Dictionary<int, Image> PageRotationImages = new()
    {
        [0] = Resources.Rotate0Permanent,
        [1] = Resources.Rotate90Permanent,
        [2] = Resources.Rotate180Permanent,
        [3] = Resources.Rotate270Permanent
    };

    #region File Menu
    public static Image Open => Resources.Open;
    //public static Image Close => Resources.
    //public static Image CloseAll => Resources.

    public static Image AddTab => Resources.NewTab;

    public static Image AddFolder => Resources.AddFolder;
    public static Image ScanFolders => Resources.Scan;
    public static Image UpdateAllBooks => Resources.UpdateSmall;
    public static Image UpdateWebComics => Resources.UpdateWeb;
    public static Image SynchronizeDevices => Resources.DeviceSync;
    public static Image GenerateCovers => Resources.Screenshot;
    public static Image ShowTasks => Resources.BackgroundJob;
    //public static Image Automation => Resources.

    //public static Image NewComic => Resources.
    public static Image OpenRemoteLibrary => Resources.RemoteDatabase;

    //public static Image OpenBooks => Resources.
    //public static Image RecentBooks => Resources.

    public static Image Restart => Resources.Restart;
    //public static Image Exit => Resources.
    #endregion

    #region Edit Menu
    public static Image ShowInfo => Resources.GetInfo;

    public static Image Undo => Resources.Undo;
    public static Image Redo => Resources.Redo;

    //public static Image MyRating => Resources.
    //public static Image PageType => Resources.
    //public static Image PageRotation => Resources.
    public static Image RotatePage0 => Resources.Rotate0Permanent;
    public static Image RotatePage90 => Resources.Rotate90Permanent;
    public static Image RotatePage180 => Resources.Rotate180Permanent;
    public static Image RotatePage270 => Resources.Rotate270Permanent;

    public static Image Bookmarks => Resources.Bookmark;
    public static Image SetBookmark => Resources.NewBookmark;
    public static Image RemoveBookmark => Resources.RemoveBookmark;
    public static Image PreviousBookmark => Resources.PreviousBookmark;
    public static Image NextBookmark => Resources.NextBookmark;
    //public static Image LastPageRead => Resources.

    public static Image CopyPage => Resources.Copy;
    //public static Image ExportPage => Resources.

    public static Image RefreshView => Resources.Refresh;

    public static Image ShowDevices => Resources.EditDevices;
    public static Image ShowPreferences => Resources.Preferences;
    #endregion

    #region Browse Menu
    public static Image ToggleBrowser => Resources.Browser;

    public static Image ViewLibrary => Resources.Database;
    public static Image ViewFolders => Resources.FileBrowser;
    public static Image ViewPages => Resources.ComicPage;

    public static Image ToggleSidebar => Resources.Sidebar;
    public static Image TogglePreview => Resources.SmallPreview;
    public static Image ToggleSearchFilter => Resources.Search;

    public static Image ToggleInfoPanel => Resources.InfoPanel;

    public static Image PreviousList => Resources.BrowsePrevious;
    public static Image NextList => Resources.BrowseNext;

    public static Image Workspaces => Resources.Workspace;
    //public static Image SaveWorkspace => Resources.
    //public static Image EditWorkspaces => Resources.

    public static Image ListLayout => Resources.ListLayout;
    //public static Image EditListLayout => Resources.
    //public static Image SaveListLayout => Resources.

    //public static Image EditAllListLayout => Resources.
    //public static Image SetAllListLayout => Resources.
    #endregion

    #region Read Menu
    public static Image GoFirstPage => Resources.GoFirst;
    public static Image GoPreviousPage => Resources.GoPrevious;
    public static Image GoNextPage => Resources.GoNext;
    public static Image GoLastPage => Resources.GoLast;

    public static Image PreviousBook => Resources.PrevFromList;
    public static Image NextBook => Resources.NextFromList;
    public static Image RandomBook => Resources.RandomComic;
    public static Image BrowseToBook => Resources.SyncBrowser;

    public static Image PreviousTab => Resources.Previous;
    public static Image NextTab => Resources.Next;

    public static Image AutoScroll => Resources.CursorScroll;
    public static Image DoublePageAutoScroll => Resources.TwoPageAutoscroll;
    //public static Image TrackCurrentPage => Resources.
    #endregion

    #region Display Menu
    public static Image ShowDisplaySettings => Resources.DisplaySettings;

    //public static Image PageLayout => Resources.
    public static Image ToggleOversizeFit => Resources.FileBrowser;

    //public static Image Zoom => Resources.
    public static Image ZoomIn => Resources.ZoomIn;
    public static Image ZoomOut => Resources.ZoomOut;
    //public static Image ToggleZoom => Resources.

    //public static Image Rotation => Resources.
    public static Image RotateDisplayLeft => Resources.RotateLeft;
    public static Image RotateDisplayRight => Resources.RotateRight;
    public static Image RotateDisplay0 => Resources.Rotate0;
    public static Image RotateDisplay90 => Resources.Rotate90;
    public static Image RotateDisplay180 => Resources.Rotate180;
    public static Image RotateDisplay270 => Resources.Rotate270;
    public static Image ToggleAutoRotate => Resources.SmallPreview;

    public static Image ToggleUI => Resources.MenuToggle;
    public static Image ToggleFullScreen => Resources.FullScreen;
    public static Image ToggleReaderWindow => Resources.UndockReader;
    public static Image ToggleMagnifier => Resources.Zoom;
    #endregion

    #region Help Menu
    //public static Image Help => Resources.
    public static Image WebHelp => Resources.Help;

    //public static Image Plugins => Resources.

    //public static Image ChooseHelp => Resources.
    //public static Image QuickIntro => Resources.

    public static Image WebHomepage => Resources.WebBlog;
    public static Image WebUserForum => Resources.WebForum;

    public static Image ShowNews => Resources.News;
    //public static Image CheckUpdate => Resources.

    public static Image ShowAbout => Resources.About;
    #endregion
}
