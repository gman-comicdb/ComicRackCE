using System.Windows.Forms;
using cYo.Common.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus
{
    partial class MainToolStrip
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.readerContainer = new System.Windows.Forms.Panel();
            //this.fileTabs = new cYo.Common.Windows.Forms.TabBar();

            this.mainToolStripItem = new System.Windows.Forms.ToolStrip();
            this.tbPrevPage = new System.Windows.Forms.ToolStripSplitButton();
            this.tbFirstPage = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPrevBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPrevFromList = new System.Windows.Forms.ToolStripMenuItem();
            this.tbNextPage = new System.Windows.Forms.ToolStripSplitButton();
            this.tbLastPage = new System.Windows.Forms.ToolStripMenuItem();
            this.tbNextBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.tbLastPageRead = new System.Windows.Forms.ToolStripMenuItem();
            this.tbNextFromList = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRandomFromList = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPageLayout = new System.Windows.Forms.ToolStripSplitButton();
            this.tbSinglePage = new System.Windows.Forms.ToolStripMenuItem();
            this.tbTwoPages = new System.Windows.Forms.ToolStripMenuItem();
            this.tbTwoPagesAdaptive = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRightToLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tbFit = new System.Windows.Forms.ToolStripSplitButton();
            this.tbOriginal = new System.Windows.Forms.ToolStripMenuItem();
            this.tbFitAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tbFitWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.tbFitWidthAdaptive = new System.Windows.Forms.ToolStripMenuItem();
            this.tbFitHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.tbBestFit = new System.Windows.Forms.ToolStripMenuItem();
            this.tbOnlyFitOversized = new System.Windows.Forms.ToolStripMenuItem();
            this.tbZoom = new System.Windows.Forms.ToolStripSplitButton();
            this.tbZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.tbZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tbZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbZoom125 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbZoom150 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbZoomCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRotate = new System.Windows.Forms.ToolStripSplitButton();
            this.tbRotateLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRotateRight = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRotate0 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRotate90 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRotate180 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRotate270 = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAutoRotate = new System.Windows.Forms.ToolStripMenuItem();
            this.tbMagnify = new System.Windows.Forms.ToolStripSplitButton();
            this.tbFullScreen = new System.Windows.Forms.ToolStripButton();
            this.tbTools = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolsContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tbOpenComic = new System.Windows.Forms.ToolStripMenuItem();
            this.tbOpenRemoteLibrary = new System.Windows.Forms.ToolStripMenuItem();
            this.tbShowInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsWorkspaces = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSaveWorkspace = new System.Windows.Forms.ToolStripMenuItem();
            this.tsEditWorkspaces = new System.Windows.Forms.ToolStripMenuItem();
            this.tsWorkspaceSep = new System.Windows.Forms.ToolStripSeparator();
            this.tbBookmarks = new System.Windows.Forms.ToolStripMenuItem();
            this.tbSetBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRemoveBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.tbBookmarkSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tbAutoScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.tbMinimalGui = new System.Windows.Forms.ToolStripMenuItem();
            this.tbReaderUndocked = new System.Windows.Forms.ToolStripMenuItem();
            this.tbScan = new System.Windows.Forms.ToolStripMenuItem();
            this.tbUpdateAllComicFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tbUpdateWebComics = new System.Windows.Forms.ToolStripMenuItem();
            this.tbCacheThumbnails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSynchronizeDevices = new System.Windows.Forms.ToolStripMenuItem();
            this.tbComicDisplaySettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPreferences = new System.Windows.Forms.ToolStripMenuItem();
            this.tbAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tbShowMainMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.tbExit = new System.Windows.Forms.ToolStripMenuItem();
            this.SuspendLayout();

            // 
            // mainToolStrip
            // 
            this.mainToolStripItem.BackColor = System.Drawing.Color.Transparent;
            this.mainToolStripItem.Dock = System.Windows.Forms.DockStyle.Right;
            this.mainToolStripItem.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStripItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbPrevPage,
            this.tbNextPage,
            this.tbPageLayout,
            this.tbFit,
            this.tbZoom,
            this.tbRotate,
            this.tbMagnify,
            this.tbFullScreen,
            this.tbTools});
            this.mainToolStripItem.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mainToolStripItem.Location = new System.Drawing.Point(400, 1);
            this.mainToolStripItem.MinimumSize = new System.Drawing.Size(0, 24);
            this.mainToolStripItem.Name = "mainToolStrip";
            this.mainToolStripItem.Size = new System.Drawing.Size(344, 25);
            this.mainToolStripItem.TabIndex = 2;
            this.mainToolStripItem.Text = "mainToolStrip";
            // 
            // tbPrevPage
            // 
            this.tbPrevPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPrevPage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbFirstPage,
            this.tbPrevBookmark,
            this.tbPrevFromList});
            this.tbPrevPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GoPrevious;
            this.tbPrevPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPrevPage.Name = "tbPrevPage";
            this.tbPrevPage.Size = new System.Drawing.Size(32, 22);
            this.tbPrevPage.Text = "Previous Page";
            // 
            // tbFirstPage
            // 
            this.tbFirstPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GoFirst;
            this.tbFirstPage.Name = "tbFirstPage";
            this.tbFirstPage.ShortcutKeyDisplayString = "";
            this.tbFirstPage.Size = new System.Drawing.Size(268, 22);
            this.tbFirstPage.Text = "&First Page";
            // 
            // tbPrevBookmark
            // 
            this.tbPrevBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.PreviousBookmark;
            this.tbPrevBookmark.Name = "tbPrevBookmark";
            this.tbPrevBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.P)));
            this.tbPrevBookmark.Size = new System.Drawing.Size(268, 22);
            this.tbPrevBookmark.Text = "Previous Bookmark";
            // 
            // tbPrevFromList
            // 
            this.tbPrevFromList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.PrevFromList;
            this.tbPrevFromList.Name = "tbPrevFromList";
            this.tbPrevFromList.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.P)));
            this.tbPrevFromList.Size = new System.Drawing.Size(268, 22);
            this.tbPrevFromList.Text = "Previous Book from List";
            // 
            // tbNextPage
            // 
            this.tbNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbNextPage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbLastPage,
            this.tbNextBookmark,
            this.tbLastPageRead,
            this.tbNextFromList,
            this.tbRandomFromList});
            this.tbNextPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GoNext;
            this.tbNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbNextPage.Name = "tbNextPage";
            this.tbNextPage.Size = new System.Drawing.Size(32, 22);
            this.tbNextPage.Text = "Next Page";
            // 
            // tbLastPage
            // 
            this.tbLastPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GoLast;
            this.tbLastPage.Name = "tbLastPage";
            this.tbLastPage.ShortcutKeyDisplayString = "";
            this.tbLastPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tbLastPage.Size = new System.Drawing.Size(250, 22);
            this.tbLastPage.Text = "&Last Page";
            // 
            // tbNextBookmark
            // 
            this.tbNextBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.NextBookmark;
            this.tbNextBookmark.Name = "tbNextBookmark";
            this.tbNextBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.N)));
            this.tbNextBookmark.Size = new System.Drawing.Size(250, 22);
            this.tbNextBookmark.Text = "Next Bookmark";
            // 
            // tbLastPageRead
            // 
            this.tbLastPageRead.Name = "tbLastPageRead";
            this.tbLastPageRead.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.L)));
            this.tbLastPageRead.Size = new System.Drawing.Size(250, 22);
            this.tbLastPageRead.Text = "L&ast Page Read";
            // 
            // tbNextFromList
            // 
            this.tbNextFromList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.NextFromList;
            this.tbNextFromList.Name = "tbNextFromList";
            this.tbNextFromList.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.N)));
            this.tbNextFromList.Size = new System.Drawing.Size(250, 22);
            this.tbNextFromList.Text = "Next Book from List";
            // 
            // tbRandomFromList
            // 
            this.tbRandomFromList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RandomComic;
            this.tbRandomFromList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRandomFromList.Name = "tbRandomFromList";
            this.tbRandomFromList.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                | System.Windows.Forms.Keys.N)));
            this.tbRandomFromList.Size = new System.Drawing.Size(250, 22);
            this.tbRandomFromList.Text = "Random Book";
            // 
            // toolStripSeparator5
            // 
            // 
            // tbPageLayout
            // 
            this.tbPageLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPageLayout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbSinglePage,
            this.tbTwoPages,
            this.tbTwoPagesAdaptive,
            this.tbRightToLeft});
            this.tbPageLayout.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.SinglePage;
            this.tbPageLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPageLayout.Name = "tbPageLayout";
            this.tbPageLayout.Size = new System.Drawing.Size(32, 22);
            this.tbPageLayout.Text = "Page Layout";
            // 
            // tbSinglePage
            // 
            this.tbSinglePage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.SinglePage;
            this.tbSinglePage.Name = "tbSinglePage";
            this.tbSinglePage.Size = new System.Drawing.Size(225, 22);
            this.tbSinglePage.Text = "Single Page";
            // 
            // tbTwoPages
            // 
            this.tbTwoPages.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.TwoPageForced;
            this.tbTwoPages.Name = "tbTwoPages";
            this.tbTwoPages.Size = new System.Drawing.Size(225, 22);
            this.tbTwoPages.Text = "Two Pages";
            // 
            // tbTwoPagesAdaptive
            // 
            this.tbTwoPagesAdaptive.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.TwoPage;
            this.tbTwoPagesAdaptive.Name = "tbTwoPagesAdaptive";
            this.tbTwoPagesAdaptive.Size = new System.Drawing.Size(225, 22);
            this.tbTwoPagesAdaptive.Text = "Two Pages (adaptive)";
            this.tbTwoPagesAdaptive.ToolTipText = "Show one or two pages";
            // 
            // toolStripMenuItem54
            // 
            // 
            // tbRightToLeft
            // 
            this.tbRightToLeft.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RightToLeft;
            this.tbRightToLeft.Name = "tbRightToLeft";
            this.tbRightToLeft.Size = new System.Drawing.Size(225, 22);
            this.tbRightToLeft.Text = "Right to Left";
            // 
            // tbFit
            // 
            this.tbFit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbFit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbOriginal,
            this.tbFitAll,
            this.tbFitWidth,
            this.tbFitWidthAdaptive,
            this.tbFitHeight,
            this.tbBestFit,
            this.tbOnlyFitOversized});
            this.tbFit.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitAll;
            this.tbFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbFit.Name = "tbFit";
            this.tbFit.Size = new System.Drawing.Size(32, 22);
            this.tbFit.Text = "Fit";
            this.tbFit.ToolTipText = "Toggle Fit Mode";
            // 
            // tbOriginal
            // 
            this.tbOriginal.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Original;
            this.tbOriginal.Name = "tbOriginal";
            this.tbOriginal.Size = new System.Drawing.Size(247, 22);
            this.tbOriginal.Text = "Original Size";
            // 
            // tbFitAll
            // 
            this.tbFitAll.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitAll;
            this.tbFitAll.Name = "tbFitAll";
            this.tbFitAll.Size = new System.Drawing.Size(247, 22);
            this.tbFitAll.Text = "Fit All";
            // 
            // tbFitWidth
            // 
            this.tbFitWidth.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitWidth;
            this.tbFitWidth.Name = "tbFitWidth";
            this.tbFitWidth.Size = new System.Drawing.Size(247, 22);
            this.tbFitWidth.Text = "Fit Width";
            // 
            // tbFitWidthAdaptive
            // 
            this.tbFitWidthAdaptive.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitWidthAdaptive;
            this.tbFitWidthAdaptive.Name = "tbFitWidthAdaptive";
            this.tbFitWidthAdaptive.Size = new System.Drawing.Size(247, 22);
            this.tbFitWidthAdaptive.Text = "Fit Width (adaptive)";
            // 
            // tbFitHeight
            // 
            this.tbFitHeight.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitHeight;
            this.tbFitHeight.Name = "tbFitHeight";
            this.tbFitHeight.Size = new System.Drawing.Size(247, 22);
            this.tbFitHeight.Text = "Fit Height";
            // 
            // tbBestFit
            // 
            this.tbBestFit.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitBest;
            this.tbBestFit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbBestFit.Name = "tbBestFit";
            this.tbBestFit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.tbBestFit.Size = new System.Drawing.Size(247, 22);
            this.tbBestFit.Text = "Fit Best";
            // 
            // toolStripMenuItem20
            // 
            // 
            // tbOnlyFitOversized
            // 
            this.tbOnlyFitOversized.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Oversized;
            this.tbOnlyFitOversized.Name = "tbOnlyFitOversized";
            this.tbOnlyFitOversized.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.D0)));
            this.tbOnlyFitOversized.Size = new System.Drawing.Size(247, 22);
            this.tbOnlyFitOversized.Text = "&Only fit if oversized";
            // 
            // tbZoom
            // 
            this.tbZoom.AutoToolTip = false;
            this.tbZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbZoomIn,
            this.tbZoomOut,
            this.tbZoom100,
            this.tbZoom125,
            this.tbZoom150,
            this.tbZoom200,
            this.tbZoom400,
            this.tbZoomCustom});
            this.tbZoom.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ZoomIn;
            this.tbZoom.Name = "tbZoom";
            this.tbZoom.Size = new System.Drawing.Size(60, 22); // changed (Width set to 60 in MainForm())
            this.tbZoom.Text = "100 %";
            this.tbZoom.ToolTipText = "Change the page zoom";
            // 
            // tbZoomIn
            // 
            this.tbZoomIn.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ZoomIn;
            this.tbZoomIn.Name = "tbZoomIn";
            this.tbZoomIn.Size = new System.Drawing.Size(222, 22);
            this.tbZoomIn.Text = "Zoom &In";
            // 
            // tbZoomOut
            // 
            this.tbZoomOut.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ZoomOut;
            this.tbZoomOut.Name = "tbZoomOut";
            this.tbZoomOut.Size = new System.Drawing.Size(222, 22);
            this.tbZoomOut.Text = "Zoom &Out";
            // 
            // toolStripMenuItem30
            // 
            // 
            // tbZoom100
            // 
            this.tbZoom100.Name = "tbZoom100";
            this.tbZoom100.Size = new System.Drawing.Size(222, 22);
            this.tbZoom100.Text = "100%";
            // 
            // tbZoom125
            // 
            this.tbZoom125.Name = "tbZoom125";
            this.tbZoom125.Size = new System.Drawing.Size(222, 22);
            this.tbZoom125.Text = "125%";
            // 
            // tbZoom150
            // 
            this.tbZoom150.Name = "tbZoom150";
            this.tbZoom150.Size = new System.Drawing.Size(222, 22);
            this.tbZoom150.Text = "150%";
            // 
            // tbZoom200
            // 
            this.tbZoom200.Name = "tbZoom200";
            this.tbZoom200.Size = new System.Drawing.Size(222, 22);
            this.tbZoom200.Text = "200%";
            // 
            // tbZoom400
            // 
            this.tbZoom400.Name = "tbZoom400";
            this.tbZoom400.Size = new System.Drawing.Size(222, 22);
            this.tbZoom400.Text = "400%";
            // 
            // toolStripMenuItem31
            // 
            // 
            // tbZoomCustom
            // 
            this.tbZoomCustom.Name = "tbZoomCustom";
            this.tbZoomCustom.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.Z)));
            this.tbZoomCustom.Size = new System.Drawing.Size(222, 22);
            this.tbZoomCustom.Text = "&Custom...";
            // 
            // tbRotate
            // 
            this.tbRotate.AutoToolTip = false;
            this.tbRotate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbRotateLeft,
            this.tbRotateRight,
            this.tbRotate0,
            this.tbRotate90,
            this.tbRotate180,
            this.tbRotate270,
            this.tbAutoRotate});
            this.tbRotate.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RotateRight;
            this.tbRotate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRotate.Name = "tbRotate";
            this.tbRotate.Size = new System.Drawing.Size(50, 22);
            this.tbRotate.Text = "0°";
            this.tbRotate.ToolTipText = "Change the page rotation";
            // 
            // tbRotateLeft
            // 
            this.tbRotateLeft.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RotateLeft;
            this.tbRotateLeft.Name = "tbRotateLeft";
            this.tbRotateLeft.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.OemMinus)));
            this.tbRotateLeft.Size = new System.Drawing.Size(256, 22);
            this.tbRotateLeft.Text = "Rotate Left";
            // 
            // tbRotateRight
            // 
            this.tbRotateRight.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RotateRight;
            this.tbRotateRight.Name = "tbRotateRight";
            this.tbRotateRight.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.Oemplus)));
            this.tbRotateRight.Size = new System.Drawing.Size(256, 22);
            this.tbRotateRight.Text = "Rotate Right";
            // 
            // toolStripSeparator11
            // 
            // 
            // tbRotate0
            // 
            this.tbRotate0.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate0;
            this.tbRotate0.Name = "tbRotate0";
            this.tbRotate0.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.D7)));
            this.tbRotate0.Size = new System.Drawing.Size(256, 22);
            this.tbRotate0.Text = "&No Rotation";
            // 
            // tbRotate90
            // 
            this.tbRotate90.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate90;
            this.tbRotate90.Name = "tbRotate90";
            this.tbRotate90.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.D8)));
            this.tbRotate90.Size = new System.Drawing.Size(256, 22);
            this.tbRotate90.Text = "90°";
            // 
            // tbRotate180
            // 
            this.tbRotate180.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate180;
            this.tbRotate180.Name = "tbRotate180";
            this.tbRotate180.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.D9)));
            this.tbRotate180.Size = new System.Drawing.Size(256, 22);
            this.tbRotate180.Text = "180°";
            // 
            // tbRotate270
            // 
            this.tbRotate270.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate270;
            this.tbRotate270.Name = "tbRotate270";
            this.tbRotate270.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.D0)));
            this.tbRotate270.Size = new System.Drawing.Size(256, 22);
            this.tbRotate270.Text = "270°";
            // 
            // toolStripMenuItem34
            // 
            // 
            // tbAutoRotate
            // 
            this.tbAutoRotate.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.AutoRotate;
            this.tbAutoRotate.Name = "tbAutoRotate";
            this.tbAutoRotate.Size = new System.Drawing.Size(256, 22);
            this.tbAutoRotate.Text = "Autorotate Double Pages";
            // 
            // toolStripSeparator7
            // 
            // 
            // tbMagnify
            // 
            this.tbMagnify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbMagnify.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Zoom;
            this.tbMagnify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbMagnify.Name = "tbMagnify";
            this.tbMagnify.Size = new System.Drawing.Size(32, 22);
            this.tbMagnify.Text = "Magnifier";
            // 
            // tbFullScreen
            // 
            this.tbFullScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbFullScreen.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FullScreen;
            this.tbFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbFullScreen.Name = "tbFullScreen";
            this.tbFullScreen.Size = new System.Drawing.Size(23, 22);
            this.tbFullScreen.Text = "Full Screen";
            this.tbFullScreen.ToolTipText = "Full Screen";
            // 
            // toolStripSeparator1
            // 
            // 
            // tbTools
            // 
            this.tbTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbTools.DropDown = this.toolsContextMenu;
            this.tbTools.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Tools;
            this.tbTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbTools.Name = "tbTools";
            this.tbTools.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tbTools.ShowDropDownArrow = false;
            this.tbTools.Size = new System.Drawing.Size(20, 22);
            this.tbTools.Text = "Tools";
            // 
            // toolsContextMenu
            // 
            this.toolsContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbOpenComic,
            this.tbOpenRemoteLibrary,
            this.tbShowInfo,
            this.tsWorkspaces,
            this.tbBookmarks,
            this.tbAutoScroll,
            this.tbMinimalGui,
            this.tbReaderUndocked,
            this.tbScan,
            this.tbUpdateAllComicFiles,
            this.tbUpdateWebComics,
            this.tbCacheThumbnails,
            this.tsSynchronizeDevices,
            this.tbComicDisplaySettings,
            this.tbPreferences,
            this.tbAbout,
            this.tbShowMainMenu,
            this.tbExit});
            this.toolsContextMenu.Name = "toolsContextMenu";
            this.toolsContextMenu.OwnerItem = this.tbTools;
            this.toolsContextMenu.Size = new System.Drawing.Size(269, 436);
            // 
            // tbOpenComic
            // 
            this.tbOpenComic.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Open;
            this.tbOpenComic.Name = "tbOpenComic";
            this.tbOpenComic.Size = new System.Drawing.Size(268, 22);
            this.tbOpenComic.Text = "&Open Book...";
            // 
            // tbOpenRemoteLibrary
            // 
            this.tbOpenRemoteLibrary.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RemoteDatabase;
            this.tbOpenRemoteLibrary.Name = "tbOpenRemoteLibrary";
            this.tbOpenRemoteLibrary.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.R)));
            this.tbOpenRemoteLibrary.Size = new System.Drawing.Size(268, 22);
            this.tbOpenRemoteLibrary.Text = "Open Remote Library...";
            // 
            // tbShowInfo
            // 
            this.tbShowInfo.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GetInfo;
            this.tbShowInfo.Name = "tbShowInfo";
            this.tbShowInfo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tbShowInfo.Size = new System.Drawing.Size(268, 22);
            this.tbShowInfo.Text = "Info...";
            this.tbShowInfo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // 
            // 
            // tsWorkspaces
            // 
            this.tsWorkspaces.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSaveWorkspace,
            this.tsEditWorkspaces,
            this.tsWorkspaceSep});
            this.tsWorkspaces.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Workspace;
            this.tsWorkspaces.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsWorkspaces.Name = "tsWorkspaces";
            this.tsWorkspaces.Size = new System.Drawing.Size(268, 22);
            this.tsWorkspaces.Text = "Workspaces";
            // 
            // tsSaveWorkspace
            // 
            this.tsSaveWorkspace.Name = "tsSaveWorkspace";
            this.tsSaveWorkspace.Size = new System.Drawing.Size(237, 22);
            this.tsSaveWorkspace.Text = "&Save Workspace...";
            // 
            // tsEditWorkspaces
            // 
            this.tsEditWorkspaces.Name = "tsEditWorkspaces";
            this.tsEditWorkspaces.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                | System.Windows.Forms.Keys.W)));
            this.tsEditWorkspaces.Size = new System.Drawing.Size(237, 22);
            this.tsEditWorkspaces.Text = "&Edit Workspaces...";
            // 
            // tsWorkspaceSep
            // 
            this.tsWorkspaceSep.Name = "tsWorkspaceSep";
            this.tsWorkspaceSep.Size = new System.Drawing.Size(234, 6);
            // 
            // tbBookmarks
            // 
            this.tbBookmarks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbSetBookmark,
            this.tbRemoveBookmark,
            this.tbBookmarkSeparator});
            this.tbBookmarks.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Bookmark;
            this.tbBookmarks.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbBookmarks.Name = "tbBookmarks";
            this.tbBookmarks.Size = new System.Drawing.Size(268, 22);
            this.tbBookmarks.Text = "Bookmarks";
            // 
            // tbSetBookmark
            // 
            this.tbSetBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.NewBookmark;
            this.tbSetBookmark.Name = "tbSetBookmark";
            this.tbSetBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.B)));
            this.tbSetBookmark.Size = new System.Drawing.Size(248, 22);
            this.tbSetBookmark.Text = "Set Bookmark...";
            // 
            // tbRemoveBookmark
            // 
            this.tbRemoveBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RemoveBookmark;
            this.tbRemoveBookmark.Name = "tbRemoveBookmark";
            this.tbRemoveBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.D)));
            this.tbRemoveBookmark.Size = new System.Drawing.Size(248, 22);
            this.tbRemoveBookmark.Text = "Remove Bookmark";
            // 
            // tbBookmarkSeparator
            // 
            this.tbBookmarkSeparator.Name = "tbBookmarkSeparator";
            this.tbBookmarkSeparator.Size = new System.Drawing.Size(245, 6);
            this.tbBookmarkSeparator.Tag = "bms";
            // 
            // tbAutoScroll
            // 
            this.tbAutoScroll.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.CursorScroll;
            this.tbAutoScroll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAutoScroll.Name = "tbAutoScroll";
            this.tbAutoScroll.Size = new System.Drawing.Size(268, 22);
            this.tbAutoScroll.Text = "Auto Scrolling";
            // 
            // 
            // 
            // tbMinimalGui
            // 
            this.tbMinimalGui.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.MenuToggle;
            this.tbMinimalGui.Name = "tbMinimalGui";
            this.tbMinimalGui.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.tbMinimalGui.Size = new System.Drawing.Size(268, 22);
            this.tbMinimalGui.Text = "Minimal User Interface";
            // 
            // tbReaderUndocked
            // 
            this.tbReaderUndocked.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.UndockReader;
            this.tbReaderUndocked.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbReaderUndocked.Name = "tbReaderUndocked";
            this.tbReaderUndocked.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.tbReaderUndocked.Size = new System.Drawing.Size(268, 22);
            this.tbReaderUndocked.Text = "Reader in own Window";
            // 
            // toolStripMenuItem52
            // 
            // 
            // tbScan
            // 
            this.tbScan.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Scan;
            this.tbScan.Name = "tbScan";
            this.tbScan.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.S)));
            this.tbScan.Size = new System.Drawing.Size(268, 22);
            this.tbScan.Text = "Scan Book &Folders";
            // 
            // tbUpdateAllComicFiles
            // 
            this.tbUpdateAllComicFiles.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.UpdateSmall;
            this.tbUpdateAllComicFiles.Name = "tbUpdateAllComicFiles";
            this.tbUpdateAllComicFiles.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.U)));
            this.tbUpdateAllComicFiles.Size = new System.Drawing.Size(268, 22);
            this.tbUpdateAllComicFiles.Text = "Update all Book Files";
            // 
            // tbUpdateWebComics
            // 
            this.tbUpdateWebComics.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.UpdateWeb;
            this.tbUpdateWebComics.Name = "tbUpdateWebComics";
            this.tbUpdateWebComics.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                | System.Windows.Forms.Keys.W)));
            this.tbUpdateWebComics.Size = new System.Drawing.Size(268, 22);
            this.tbUpdateWebComics.Text = "Update Web Comics";
            // 
            // tbCacheThumbnails
            // 
            this.tbCacheThumbnails.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Screenshot;
            this.tbCacheThumbnails.Name = "tbCacheThumbnails";
            this.tbCacheThumbnails.Size = new System.Drawing.Size(268, 22);
            this.tbCacheThumbnails.Text = "Generate Cover Thumbnails";
            // 
            // tsSynchronizeDevices
            // 
            this.tsSynchronizeDevices.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.DeviceSync;
            this.tsSynchronizeDevices.Name = "tsSynchronizeDevices";
            this.tsSynchronizeDevices.Size = new System.Drawing.Size(268, 22);
            this.tsSynchronizeDevices.Text = "Synchronize Devices";
            // 
            // 
            // 
            // tbComicDisplaySettings
            // 
            this.tbComicDisplaySettings.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.DisplaySettings;
            this.tbComicDisplaySettings.Name = "tbComicDisplaySettings";
            this.tbComicDisplaySettings.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.tbComicDisplaySettings.Size = new System.Drawing.Size(268, 22);
            this.tbComicDisplaySettings.Text = "Book Display Settings...";
            // 
            // tbPreferences
            // 
            this.tbPreferences.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Preferences;
            this.tbPreferences.Name = "tbPreferences";
            this.tbPreferences.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9)));
            this.tbPreferences.Size = new System.Drawing.Size(268, 22);
            this.tbPreferences.Text = "&Preferences...";
            // 
            // tbAbout
            // 
            this.tbAbout.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.About;
            this.tbAbout.Name = "tbAbout";
            this.tbAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F1)));
            this.tbAbout.Size = new System.Drawing.Size(268, 22);
            this.tbAbout.Text = "&About...";
            // 
            // toolStripMenuItem50
            // 
            // 
            // tbShowMainMenu
            // 
            this.tbShowMainMenu.Name = "tbShowMainMenu";
            this.tbShowMainMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F10)));
            this.tbShowMainMenu.Size = new System.Drawing.Size(268, 22);
            this.tbShowMainMenu.Text = "Show Main Menu";
            // 
            // toolStripMenuItem51
            // 
            // 
            // tbExit
            // 
            this.tbExit.Name = "tbExit";
            this.tbExit.Size = new System.Drawing.Size(268, 22);
            this.tbExit.Text = "&Exit";

            // 
            // readerContainer
            // 
            this.readerContainer.Controls.Add(this.mainToolStripItem);
            this.readerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readerContainer.Location = new System.Drawing.Point(0, 0);
            this.readerContainer.Name = "readerContainer";
            this.readerContainer.Size = new System.Drawing.Size(744, 364);
            // 
            // fileTabs
            // 
            //this.fileTabs.AllowDrop = true;
            //this.fileTabs.CloseImage = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Close;
            //this.fileTabs.Controls.Add(this.mainToolStripItem);
            //this.fileTabs.Dock = System.Windows.Forms.DockStyle.Top;
            //this.fileTabs.DragDropReorder = true;
            //this.fileTabs.LeftIndent = 8;
            //this.fileTabs.Location = new System.Drawing.Point(0, 0);
            //this.fileTabs.Name = "fileTabs";
            //this.fileTabs.OwnerDrawnTooltips = true;
            //this.fileTabs.Size = new System.Drawing.Size(744, 31);
            //this.fileTabs.TabIndex = 1;

            //
            // MainToolStrip
            //
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(744, 662);
            this.Controls.Add(this.readerContainer);
            this.ResumeLayout();
        }

        private Panel readerContainer;
        //private TabBar fileTabs;
        private ToolStrip mainToolStripItem;
        private ToolStripSplitButton tbFit;
        private ToolStripMenuItem tbOriginal;
        private ToolStripMenuItem tbFitAll;
        private ToolStripMenuItem tbFitWidth;
        private ToolStripMenuItem tbFitWidthAdaptive;
        private ToolStripMenuItem tbFitHeight;
        private ToolStripMenuItem tbBestFit;
        private ToolStripMenuItem tbOnlyFitOversized;
        private ToolStripSplitButton tbZoom;
        private ToolStripMenuItem tbZoomIn;
        private ToolStripMenuItem tbZoomOut;
        private ToolStripMenuItem tbZoom100;
        private ToolStripMenuItem tbZoom125;
        private ToolStripMenuItem tbZoom150;
        private ToolStripMenuItem tbZoom200;
        private ToolStripMenuItem tbZoom400;
        private ToolStripMenuItem tbZoomCustom;
        private ToolStripSplitButton tbRotate;
        private ToolStripMenuItem tbRotateLeft;
        private ToolStripMenuItem tbRotateRight;
        private ToolStripMenuItem tbRotate0;
        private ToolStripMenuItem tbRotate90;
        private ToolStripMenuItem tbRotate180;
        private ToolStripMenuItem tbRotate270;
        private ToolStripMenuItem tbAutoRotate;
        private ToolStripSplitButton tbMagnify;
        private ToolStripDropDownButton tbTools;
        private ToolStripSplitButton tbPrevPage;
        private ToolStripMenuItem tbFirstPage;
        private ToolStripMenuItem tbPrevFromList;
        private ToolStripSplitButton tbNextPage;
        private ToolStripMenuItem tbLastPage;
        private ToolStripMenuItem tbNextFromList;
        private ToolStripMenuItem tbRandomFromList;
        private ContextMenuStrip toolsContextMenu;
        private ToolStripMenuItem tbOpenComic;
        private ToolStripMenuItem tbShowInfo;
        private ToolStripMenuItem tsWorkspaces;
        private ToolStripMenuItem tsSaveWorkspace;
        private ToolStripMenuItem tsEditWorkspaces;
        private ToolStripSeparator tsWorkspaceSep;
        private ToolStripMenuItem tbBookmarks;
        private ToolStripMenuItem tbSetBookmark;
        private ToolStripMenuItem tbRemoveBookmark;
        private ToolStripSeparator tbBookmarkSeparator;
        private ToolStripMenuItem tbAutoScroll;
        private ToolStripMenuItem tbMinimalGui;
        private ToolStripMenuItem tbReaderUndocked;
        private ToolStripMenuItem tbScan;
        private ToolStripMenuItem tbUpdateAllComicFiles;
        private ToolStripMenuItem tbComicDisplaySettings;
        private ToolStripMenuItem tbPreferences;
        private ToolStripMenuItem tbAbout;
        private ToolStripMenuItem tbShowMainMenu;
        private ToolStripMenuItem tbExit;
        private ToolStripMenuItem tbPrevBookmark;
        private ToolStripMenuItem tbNextBookmark;
        private ToolStripMenuItem tbLastPageRead;
        private ToolStripMenuItem tsSynchronizeDevices;
        private ToolStripMenuItem tbCacheThumbnails;
        private ToolStripSplitButton tbPageLayout;
        private ToolStripMenuItem tbTwoPagesAdaptive;
        private ToolStripMenuItem tbSinglePage;
        private ToolStripMenuItem tbTwoPages;
        private ToolStripMenuItem tbRightToLeft;
        private ToolStripMenuItem tbUpdateWebComics;
        private ToolStripMenuItem tbOpenRemoteLibrary;
        private ToolStripButton tbFullScreen;

        #endregion
    }
}
