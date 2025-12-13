using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Keys = cYo.Common.Windows.CommandKey;

namespace cYo.Projects.ComicRack.Viewer.Controllers;

internal static class CommandKeys
{
    #region File Menu
    //public static readonly Keys[] Open = [Keys.Ctrl | Keys.O];
    //public static readonly Keys[] Close = [Keys.Ctrl | Keys.X];
    //public static readonly Keys[] CloseAll = [Keys.Ctrl | Keys.Shift | Keys.X];

    //public static readonly Keys[] AddTab = [Keys.Ctrl | Keys.T];

    //public static readonly Keys[] AddFolder = [Keys.Ctrl | Keys.Shift | Keys.A];
    //public static readonly Keys[] ScanFolders =  [Keys.Ctrl | Keys.Shift | Keys.S];
    //public static readonly Keys[] UpdateAllBooks = [Keys.Ctrl | Keys.Shift | Keys.U];
    //public static readonly Keys[] UpdateWebComics = [Keys.Ctrl | Keys.Shift | Keys.W];
    ////public static readonly Keys[] SynchronizeDevices = 
    ////public static readonly Keys[] GenerateCovers =
    //public static readonly Keys[] ShowTasks = [Keys.Ctrl | Keys.Shift | Keys.T];
    ////public static readonly Keys[] Automation =

    //public static readonly Keys[] NewComic = [Keys.Ctrl | Keys.Shift | Keys.N];
    ////public static readonly Keys[] OpenRemoteLibrary =

    ////public static readonly Keys[] OpenBooks =
    ////public static readonly Keys[] RecentBooks =

    //public static readonly Keys[] Restart = [Keys.Ctrl | Keys.Shift | Keys.Q];
    //public static readonly Keys[] Exit = [Keys.Ctrl | Keys.Q];
    #endregion

    #region File Menu
    public static readonly Keys[] Open = [Keys.Control | Keys.O];
    public static readonly Keys[] Close = [Keys.Control | Keys.X];
    public static readonly Keys[] CloseAll = [Keys.Control | Keys.Shift | Keys.X];

    public static readonly Keys[] AddTab = [Keys.Control | Keys.T];

    public static readonly Keys[] AddFolder = [Keys.Control | Keys.Shift | Keys.A];
    public static readonly Keys[] ScanFolders = [Keys.Control | Keys.Shift | Keys.S];
    public static readonly Keys[] UpdateAllBooks = [Keys.Control | Keys.Shift | Keys.U];
    public static readonly Keys[] UpdateWebComics = [Keys.Control | Keys.Shift | Keys.W];
    //public static readonly Keys[] SynchronizeDevices = 
    //public static readonly Keys[] GenerateCovers =
    public static readonly Keys[] ShowTasks = [Keys.Control | Keys.Shift | Keys.T];
    //public static readonly Keys[] Automation =

    public static readonly Keys[] NewComic = [Keys.Control | Keys.Shift | Keys.N];
    //public static readonly Keys[] OpenRemoteLibrary =

    //public static readonly Keys[] OpenBooks =
    //public static readonly Keys[] RecentBooks =

    public static readonly Keys[] Restart = [Keys.Control | Keys.Shift | Keys.Q];
    public static readonly Keys[] Exit = [Keys.Control | Keys.Q];
    #endregion

    #region Edit Menu
    //    public static readonly Keys[] ShowInfo =

    //    public static readonly Keys[] Undo =
    //    public static readonly Keys[] Redo =

    //    public static readonly Keys[] MyRating =
    //    public static readonly Keys[] Rate0 =
    //    public static readonly Keys[] Rate1 =
    //    public static readonly Keys[] Rate2 =
    //    public static readonly Keys[] Rate3 =
    //    public static readonly Keys[] Rate4 =
    //    public static readonly Keys[] Rate5 =
    //    public static readonly Keys[] QuickRating =

    //    public static readonly Keys[] PageType =

    //    public static readonly Keys[] PageRotation =
    public static readonly Keys[] RotatePage0 = [Keys.Y];
    //    public static readonly Keys[] RotatePage90 =
    //    public static readonly Keys[] RotatePage180 =
    public static readonly Keys[] RotatePage270 = [Keys.Shift | Keys.Y]; // does order matter here?

    //    public static readonly Keys[] Bookmarks =
    //    public static readonly Keys[] SetBookmark =
    //    public static readonly Keys[] RemoveBookmark =
    //    public static readonly Keys[] PreviousBookmark =
    //    public static readonly Keys[] NextBookmark =
    //    public static readonly Keys[] LastPageRead =

    //    public static readonly Keys[] CopyPage =
    //    public static readonly Keys[] ExportPage =

    //    public static readonly Keys[] RefreshView =

    //    public static readonly Keys[] ShowDevices =
    //    public static readonly Keys[] ShowPreferences =
    #endregion

    #region Browse Menu
    public static readonly Keys[] ToggleBrowser = [Keys.Escape]; //CommandKey.MouseLeft

    //    public static readonly Keys[] ViewLibrary =
    //    public static readonly Keys[] ViewFolders =
    //    public static readonly Keys[] ViewPages =

    //    public static readonly Keys[] ToggleSidebar =
    //    public static readonly Keys[] TogglePreview =
    //    public static readonly Keys[] ToggleFilters =

    //    public static readonly Keys[] ToggleInfoPanel =

    //    public static readonly Keys[] PreviousList =
    //    public static readonly Keys[] NextList =

    //    public static readonly Keys[] Workspaces =
    //    public static readonly Keys[] SaveWorkspace =
    //    public static readonly Keys[] EditWorkspaces =

    //    public static readonly Keys[] ListLayout =
    //    public static readonly Keys[] EditListLayout =
    //    public static readonly Keys[] SaveListLayout =
    //    public static readonly Keys[] EditAllListLayout =
    //    public static readonly Keys[] SetAllListLayout =
    #endregion

    #region Read Menu
    public static readonly Keys[] FirstPage =  [Keys.Control | Keys.Home]; //CommandKey.GestureDouble1
    public static readonly Keys[] PreviousPage = [Keys.Shift | Keys.PageUp]; // CommandKey.PageDown | CommandKey.Shift
    public static readonly Keys[] NextPage = [Keys.Shift | Keys.PageDown]; // CommandKey.PageDown | CommandKey.Shift
                                                                           //public static readonly Keys[] LastPage =

    public static readonly Keys[] PreviousBook = [Keys.P];
    //    public static readonly Keys[] NextBook = [Keys.N];
    //    public static readonly Keys[] RandomBook = [Keys.L];
    //    public static readonly Keys[] SyncBrowser =

    //    public static readonly Keys[] PreviousTab =
    //    public static readonly Keys[] NextTab =

    //    public static readonly Keys[] AutoScroll =
    //    public static readonly Keys[] DoublePageAutoScroll =
    //    public static readonly Keys[] TrackCurrentPage =
    #endregion

    #region Display Menu
    //    public static readonly Keys[] ShowDisplaySettings =

    //    public static readonly Keys[] PageLayout =
    public static readonly Keys[] OriginalSize = [Keys.D1];
    public static readonly Keys[] FitAll = [Keys.D2];
    public static readonly Keys[] FitWidth = [Keys.D3];
    public static readonly Keys[] FitWidthAdaptive = [Keys.D4];
    public static readonly Keys[] FitHeight = [Keys.D5];
    public static readonly Keys[] FitBest = [Keys.D6];
    public static readonly Keys[] SinglePage = [Keys.D7];
    public static readonly Keys[] TwoPages = [Keys.D8];
    public static readonly Keys[] TwoPagesAdaptive = [Keys.D1];
    public static readonly Keys[] ToggleRtL = [Keys.D0];
    public static readonly Keys[] ToggleOversizeFit = [Keys.O];

    public static readonly Keys[] ToggleTwoPages = [Keys.T];

    //    public static readonly Keys[] Zoom =
    //public static readonly Keys[] ZoomIn = // CommandKey.MouseWheelUp | CommandKey.Ctrl
    //    public static readonly Keys[] ZoomOut = // CommandKey.MouseWheelDown | CommandKey.Ctrl
    //    public static readonly Keys[] ToggleZoom = // CommandKey.TouchDoubleTap
    //    public static readonly Keys[] Zoom100 =
    //    public static readonly Keys[] Zoom125 =
    //    public static readonly Keys[] Zoom150 =
    //    public static readonly Keys[] Zoom200 =
    //    public static readonly Keys[] Zoom400 =
    //    public static readonly Keys[] ShowZoomCustom =

    //    public static readonly Keys[] Rotation =
    public static readonly Keys[] RotateLeft = [Keys.Shift | Keys.R];
        public static readonly Keys[] RotateRight = [Keys.R];
    //    public static readonly Keys[] Rotate0 =
    //    public static readonly Keys[] Rotate90 =
    //    public static readonly Keys[] Rotate180 =
    //    public static readonly Keys[] Rotate270 =
    public static readonly Keys[] ToggleAutoRotate = [Keys.A];

    //    public static readonly Keys[] ToggleMinimalGui =
    public static readonly Keys[] ToggleFullScreen = [Keys.F]; //CommandKey.MouseDoubleLeft, CommandKey.Gesture2
    public static readonly Keys[] ToggleReaderWindow = [Keys.D];
    public static readonly Keys[] ToggleMagnifier = [Keys.M]; // CommandKey.TouchPressAndTap
    #endregion

    #region Help Menu
    //    public static readonly Keys[] Help =
    //    public static readonly Keys[] WebHelp =

    //    public static readonly Keys[] Plugins =

    //    public static readonly Keys[] ChooseHelp =

    //    public static readonly Keys[] QuickIntro =

    //    public static readonly Keys[] WebHomepage =
    //    public static readonly Keys[] WebUserForum =

    //    public static readonly Keys[] ShowNews =
    //    public static readonly Keys[] CheckUpdate =

    //    public static readonly Keys[] ShowAbout =
    #endregion
}
