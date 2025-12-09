using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus
{
    partial class PageContextMenu
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

            this.pageContextMenuItem = new System.Windows.Forms.ContextMenuStrip(this.components);

            this.cmShowInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRating = new System.Windows.Forms.ToolStripMenuItem();
            this.contextRating = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmRate0 = new System.Windows.Forms.ToolStripMenuItem();

            this.cmRate1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRate2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRate3 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRate4 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRate5 = new System.Windows.Forms.ToolStripMenuItem();

            this.cmQuickRating = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPageType = new System.Windows.Forms.ToolStripMenuItem();
            this.cmPageRotate = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBookmarks = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSetBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRemoveBookmark = new System.Windows.Forms.ToolStripMenuItem();

            this.cmPrevBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.cmNextBookmark = new System.Windows.Forms.ToolStripMenuItem();

            this.cmLastPageRead = new System.Windows.Forms.ToolStripMenuItem();


            this.cmComics = new System.Windows.Forms.ToolStripMenuItem();
            this.cmOpenComic = new System.Windows.Forms.ToolStripMenuItem();
            this.cmCloseComic = new System.Windows.Forms.ToolStripMenuItem();

            this.cmPrevFromList = new System.Windows.Forms.ToolStripMenuItem();
            this.cmNextFromList = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRandomFromList = new System.Windows.Forms.ToolStripMenuItem();

            this.cmPageLayout = new System.Windows.Forms.ToolStripMenuItem();
            this.cmOriginal = new System.Windows.Forms.ToolStripMenuItem();
            this.cmFitAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cmFitWidth = new System.Windows.Forms.ToolStripMenuItem();
            this.cmFitWidthAdaptive = new System.Windows.Forms.ToolStripMenuItem();
            this.cmFitHeight = new System.Windows.Forms.ToolStripMenuItem();
            this.cmFitBest = new System.Windows.Forms.ToolStripMenuItem();

            this.cmSinglePage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmTwoPages = new System.Windows.Forms.ToolStripMenuItem();
            this.cmTwoPagesAdaptive = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRightToLeft = new System.Windows.Forms.ToolStripMenuItem();

            this.cmRotate0 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRotate90 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRotate180 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmRotate270 = new System.Windows.Forms.ToolStripMenuItem();

            this.cmOnlyFitOversized = new System.Windows.Forms.ToolStripMenuItem();
            this.cmMagnify = new System.Windows.Forms.ToolStripMenuItem();

            this.cmCopyPage = new System.Windows.Forms.ToolStripMenuItem();
            this.cmExportPage = new System.Windows.Forms.ToolStripMenuItem();

            this.cmRefreshPage = new System.Windows.Forms.ToolStripMenuItem();

            this.cmMinimalGui = new System.Windows.Forms.ToolStripMenuItem();

            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparatorComics = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator13 = new System.Windows.Forms.ToolStripSeparator();

            this.tsSeparatorBms = new System.Windows.Forms.ToolStripSeparator();
            this.SuspendLayout();

            this.pageContextMenuItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmShowInfo,
            this.cmRating,
            this.cmPageType,
            this.cmPageRotate,
            this.cmBookmarks,
            this.tsSeparator1,
            this.cmComics,
            this.cmPageLayout,
            this.cmMagnify,
            this.tsSeparator2,
            this.cmCopyPage,
            this.cmExportPage,
            this.tsSeparator3,
            this.cmRefreshPage,
            this.tsSeparator4,
            this.cmMinimalGui});
            this.pageContextMenuItem.Name = "pageContextMenu";
            this.pageContextMenuItem.Size = new System.Drawing.Size(221, 292);
            // 
            // cmShowInfo
            // 
            this.cmShowInfo.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GetInfo;
            this.cmShowInfo.Name = "cmShowInfo";
            this.cmShowInfo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.cmShowInfo.Size = new System.Drawing.Size(220, 22);
            this.cmShowInfo.Text = "Info...";
            // 
            // cmRating
            // 
            this.cmRating.DropDown = this.contextRating;
            this.cmRating.Name = "cmRating";
            this.cmRating.Size = new System.Drawing.Size(220, 22);
            this.cmRating.Text = "My R&ating";
            // 
            // contextRating
            // 
            this.contextRating.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmRate0,
            this.tsSeparator5,
            this.cmRate1,
            this.cmRate2,
            this.cmRate3,
            this.cmRate4,
            this.cmRate5,
            this.tsSeparator6,
            this.cmQuickRating});
            this.contextRating.Name = "contextRating";
            this.contextRating.OwnerItem = this.cmRating;
            this.contextRating.Size = new System.Drawing.Size(286, 170);
            // 
            // cmRate0
            // 
            this.cmRate0.Name = "cmRate0";
            this.cmRate0.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D0)));
            this.cmRate0.Size = new System.Drawing.Size(285, 22);
            this.cmRate0.Text = "None";
            // 
            // tsSeparator5
            // 
            this.tsSeparator5.Name = "tsSeparator5";
            this.tsSeparator5.Size = new System.Drawing.Size(282, 6);
            // 
            // cmRate1
            // 
            this.cmRate1.Name = "cmRate1";
            this.cmRate1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D1)));
            this.cmRate1.Size = new System.Drawing.Size(285, 22);
            this.cmRate1.Text = "* (1 Star)";
            // 
            // cmRate2
            // 
            this.cmRate2.Name = "cmRate2";
            this.cmRate2.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D2)));
            this.cmRate2.Size = new System.Drawing.Size(285, 22);
            this.cmRate2.Text = "** (2 Stars)";
            // 
            // cmRate3
            // 
            this.cmRate3.Name = "cmRate3";
            this.cmRate3.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D3)));
            this.cmRate3.Size = new System.Drawing.Size(285, 22);
            this.cmRate3.Text = "*** (3 Stars)";
            // 
            // cmRate4
            // 
            this.cmRate4.Name = "cmRate4";
            this.cmRate4.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D4)));
            this.cmRate4.Size = new System.Drawing.Size(285, 22);
            this.cmRate4.Text = "**** (4 Stars)";
            // 
            // cmRate5
            // 
            this.cmRate5.Name = "cmRate5";
            this.cmRate5.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D5)));
            this.cmRate5.Size = new System.Drawing.Size(285, 22);
            this.cmRate5.Text = "***** (5 Stars)";
            // 
            // tsSeparator6
            // 
            this.tsSeparator6.Name = "tsSeparator6";
            this.tsSeparator6.Size = new System.Drawing.Size(282, 6);
            // 
            // cmQuickRating
            // 
            this.cmQuickRating.Name = "cmQuickRating";
            this.cmQuickRating.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.Q)));
            this.cmQuickRating.Size = new System.Drawing.Size(285, 22);
            this.cmQuickRating.Text = "Quick Rating and Review...";
            // 
            // cmPageType
            // 
            this.cmPageType.Name = "cmPageType";
            this.cmPageType.Size = new System.Drawing.Size(220, 22);
            this.cmPageType.Text = "&Page Type";
            // 
            // cmPageRotate
            // 
            this.cmPageRotate.Name = "cmPageRotate";
            this.cmPageRotate.Size = new System.Drawing.Size(220, 22);
            this.cmPageRotate.Text = "Page Rotation";
            // 
            // cmBookmarks
            // 
            this.cmBookmarks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmSetBookmark,
            this.cmRemoveBookmark,
            this.tsSeparator7,
            this.cmPrevBookmark,
            this.cmNextBookmark,
            this.tsSeparator8,
            this.cmLastPageRead,
            this.tsSeparatorBms});
            this.cmBookmarks.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Bookmark;
            this.cmBookmarks.Name = "cmBookmarks";
            this.cmBookmarks.Size = new System.Drawing.Size(220, 22);
            this.cmBookmarks.Text = "&Bookmarks";
            // 
            // cmSetBookmark
            // 
            this.cmSetBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.NewBookmark;
            this.cmSetBookmark.Name = "cmSetBookmark";
            this.cmSetBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.B)));
            this.cmSetBookmark.Size = new System.Drawing.Size(249, 22);
            this.cmSetBookmark.Text = "Set Bookmark...";
            // 
            // cmRemoveBookmark
            // 
            this.cmRemoveBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RemoveBookmark;
            this.cmRemoveBookmark.Name = "cmRemoveBookmark";
            this.cmRemoveBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D)));
            this.cmRemoveBookmark.Size = new System.Drawing.Size(249, 22);
            this.cmRemoveBookmark.Text = "Remove Bookmark";
            // 
            // tsSeparator7
            // 
            this.tsSeparator7.Name = "tsSeparator7";
            this.tsSeparator7.Size = new System.Drawing.Size(246, 6);
            // 
            // cmPrevBookmark
            // 
            this.cmPrevBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.PreviousBookmark;
            this.cmPrevBookmark.Name = "cmPrevBookmark";
            this.cmPrevBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.P)));
            this.cmPrevBookmark.Size = new System.Drawing.Size(249, 22);
            this.cmPrevBookmark.Text = "Previous Bookmark";
            // 
            // cmNextBookmark
            // 
            this.cmNextBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.NextBookmark;
            this.cmNextBookmark.Name = "cmNextBookmark";
            this.cmNextBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.N)));
            this.cmNextBookmark.Size = new System.Drawing.Size(249, 22);
            this.cmNextBookmark.Text = "Next Bookmark";
            // 
            // tsSeparator8
            // 
            this.tsSeparator8.Name = "tsSeparator8";
            this.tsSeparator8.Size = new System.Drawing.Size(246, 6);
            // 
            // cmLastPageRead
            // 
            this.cmLastPageRead.Name = "cmLastPageRead";
            this.cmLastPageRead.Size = new System.Drawing.Size(249, 22);
            this.cmLastPageRead.Text = "L&ast Page Read";
            // 
            // tsSeparatorBms
            // 
            this.tsSeparatorBms.Name = "tsSeparatorBms";
            this.tsSeparatorBms.Size = new System.Drawing.Size(246, 6);
            this.tsSeparatorBms.Tag = "bms";
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // cmComics
            // 
            this.cmComics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmOpenComic,
            this.cmCloseComic,
            this.tsSeparator9,
            this.cmPrevFromList,
            this.cmNextFromList,
            this.cmRandomFromList,
            this.tsSeparatorComics});
            this.cmComics.Name = "cmComics";
            this.cmComics.Size = new System.Drawing.Size(220, 22);
            this.cmComics.Text = "Books";
            // 
            // cmOpenComic
            // 
            this.cmOpenComic.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Open;
            this.cmOpenComic.Name = "cmOpenComic";
            this.cmOpenComic.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.cmOpenComic.Size = new System.Drawing.Size(218, 22);
            this.cmOpenComic.Text = "&Open File...";
            // 
            // cmCloseComic
            // 
            this.cmCloseComic.Name = "cmCloseComic";
            this.cmCloseComic.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cmCloseComic.Size = new System.Drawing.Size(218, 22);
            this.cmCloseComic.Text = "&Close";
            // 
            // tsSeparator9
            // 
            this.tsSeparator9.Name = "tsSeparator9";
            this.tsSeparator9.Size = new System.Drawing.Size(215, 6);
            // 
            // cmPrevFromList
            // 
            this.cmPrevFromList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.PrevFromList;
            this.cmPrevFromList.Name = "cmPrevFromList";
            this.cmPrevFromList.ShortcutKeyDisplayString = "";
            this.cmPrevFromList.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.P)));
            this.cmPrevFromList.Size = new System.Drawing.Size(218, 22);
            this.cmPrevFromList.Text = "Pre&vious Book";
            // 
            // cmNextFromList
            // 
            this.cmNextFromList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.NextFromList;
            this.cmNextFromList.Name = "cmNextFromList";
            this.cmNextFromList.ShortcutKeyDisplayString = "";
            this.cmNextFromList.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.N)));
            this.cmNextFromList.Size = new System.Drawing.Size(218, 22);
            this.cmNextFromList.Text = "Ne&xt Book";
            // 
            // cmRandomFromList
            // 
            this.cmRandomFromList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RandomComic;
            this.cmRandomFromList.Name = "cmRandomFromList";
            this.cmRandomFromList.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
            | System.Windows.Forms.Keys.N)));
            this.cmRandomFromList.Size = new System.Drawing.Size(218, 22);
            this.cmRandomFromList.Text = "Random Book";
            // 
            // tsSeparator10
            // 
            this.tsSeparatorComics.Name = "tsSeparatorComics";
            this.tsSeparatorComics.Size = new System.Drawing.Size(215, 6);
            //
            // cmPageLayout
            //
            this.cmPageLayout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmOriginal,
            this.cmFitAll,
            this.cmFitWidth,
            this.cmFitWidthAdaptive,
            this.cmFitHeight,
            this.cmFitBest,
            this.tsSeparator11,
            this.cmSinglePage,
            this.cmTwoPages,
            this.cmTwoPagesAdaptive,
            this.cmRightToLeft,
            this.tsSeparator12,
            this.cmRotate0,
            this.cmRotate90,
            this.cmRotate180,
            this.cmRotate270,
            this.tsSeparator13,
            this.cmOnlyFitOversized});
            this.cmPageLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmPageLayout.Name = "cmPageLayout";
            this.cmPageLayout.Size = new System.Drawing.Size(220, 22);
            this.cmPageLayout.Text = "Page Layout";
            // 
            // cmOriginal
            // 
            this.cmOriginal.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Original;
            this.cmOriginal.Name = "cmOriginal";
            this.cmOriginal.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.cmOriginal.Size = new System.Drawing.Size(241, 22);
            this.cmOriginal.Text = "Original";
            // 
            // cmFitAll
            // 
            this.cmFitAll.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitAll;
            this.cmFitAll.Name = "cmFitAll";
            this.cmFitAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.cmFitAll.Size = new System.Drawing.Size(241, 22);
            this.cmFitAll.Text = "Fit All";
            // 
            // cmFitWidth
            // 
            this.cmFitWidth.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitWidth;
            this.cmFitWidth.Name = "cmFitWidth";
            this.cmFitWidth.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.cmFitWidth.Size = new System.Drawing.Size(241, 22);
            this.cmFitWidth.Text = "Fit Width";
            // 
            // cmFitWidthAdaptive
            // 
            this.cmFitWidthAdaptive.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitWidthAdaptive;
            this.cmFitWidthAdaptive.Name = "cmFitWidthAdaptive";
            this.cmFitWidthAdaptive.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.cmFitWidthAdaptive.Size = new System.Drawing.Size(241, 22);
            this.cmFitWidthAdaptive.Text = "Fit Width (adaptive)";
            // 
            // cmFitHeight
            // 
            this.cmFitHeight.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitHeight;
            this.cmFitHeight.Name = "cmFitHeight";
            this.cmFitHeight.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.cmFitHeight.Size = new System.Drawing.Size(241, 22);
            this.cmFitHeight.Text = "Fit Height";
            // 
            // cmFitBest
            // 
            this.cmFitBest.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitBest;
            this.cmFitBest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmFitBest.Name = "cmFitBest";
            this.cmFitBest.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.cmFitBest.Size = new System.Drawing.Size(241, 22);
            this.cmFitBest.Text = "Fit Best";
            // 
            // tsSeparator11
            // 
            this.tsSeparator11.Name = "tsSeparator11";
            this.tsSeparator11.Size = new System.Drawing.Size(238, 6);
            // 
            // cmSinglePage
            // 
            this.cmSinglePage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.SinglePage;
            this.cmSinglePage.Name = "cmSinglePage";
            this.cmSinglePage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D7)));
            this.cmSinglePage.Size = new System.Drawing.Size(241, 22);
            this.cmSinglePage.Text = "Single Page";
            // 
            // cmTwoPages
            // 
            this.cmTwoPages.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.TwoPageForced;
            this.cmTwoPages.Name = "cmTwoPages";
            this.cmTwoPages.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D8)));
            this.cmTwoPages.Size = new System.Drawing.Size(241, 22);
            this.cmTwoPages.Text = "Two Pages";
            // 
            // cmTwoPagesAdaptive
            // 
            this.cmTwoPagesAdaptive.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.TwoPage;
            this.cmTwoPagesAdaptive.Name = "cmTwoPagesAdaptive";
            this.cmTwoPagesAdaptive.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D9)));
            this.cmTwoPagesAdaptive.Size = new System.Drawing.Size(241, 22);
            this.cmTwoPagesAdaptive.Text = "Two Pages (adaptive)";
            this.cmTwoPagesAdaptive.ToolTipText = "Show one or two pages";
            // 
            // cmRightToLeft
            // 
            this.cmRightToLeft.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RightToLeft;
            this.cmRightToLeft.Name = "cmRightToLeft";
            this.cmRightToLeft.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
            this.cmRightToLeft.Size = new System.Drawing.Size(241, 22);
            this.cmRightToLeft.Text = "Right to Left";
            // 
            // tsSeparator12
            // 
            this.tsSeparator12.Name = "tsSeparator12";
            this.tsSeparator12.Size = new System.Drawing.Size(238, 6);
            // 
            // cmRotate0
            // 
            this.cmRotate0.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate0;
            this.cmRotate0.Name = "cmRotate0";
            this.cmRotate0.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D7)));
            this.cmRotate0.Size = new System.Drawing.Size(241, 22);
            this.cmRotate0.Text = "&No Rotation";
            // 
            // cmRotate90
            // 
            this.cmRotate90.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate90;
            this.cmRotate90.Name = "cmRotate90";
            this.cmRotate90.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D8)));
            this.cmRotate90.Size = new System.Drawing.Size(241, 22);
            this.cmRotate90.Text = "90°";
            // 
            // cmRotate180
            // 
            this.cmRotate180.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate180;
            this.cmRotate180.Name = "cmRotate180";
            this.cmRotate180.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D9)));
            this.cmRotate180.Size = new System.Drawing.Size(241, 22);
            this.cmRotate180.Text = "180°";
            // 
            // cmRotate270
            // 
            this.cmRotate270.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate270;
            this.cmRotate270.Name = "cmRotate270";
            this.cmRotate270.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D0)));
            this.cmRotate270.Size = new System.Drawing.Size(241, 22);
            this.cmRotate270.Text = "270°";
            // 
            // tsSeparator13
            // 
            this.tsSeparator13.Name = "tsSeparator13";
            this.tsSeparator13.Size = new System.Drawing.Size(238, 6);
            // 
            // cmOnlyFitOversized
            // 
            this.cmOnlyFitOversized.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Oversized;
            this.cmOnlyFitOversized.Name = "cmOnlyFitOversized";
            this.cmOnlyFitOversized.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
            | System.Windows.Forms.Keys.O)));
            this.cmOnlyFitOversized.Size = new System.Drawing.Size(241, 22);
            this.cmOnlyFitOversized.Text = "&Only fit if oversized";
            // 
            // cmMagnify
            // 
            this.cmMagnify.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Zoom;
            this.cmMagnify.Name = "cmMagnify";
            this.cmMagnify.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.cmMagnify.Size = new System.Drawing.Size(220, 22);
            this.cmMagnify.Text = "&Magnifier";
            // 
            // tsSeparator2
            // 
            this.tsSeparator2.Name = "tsSeparator2";
            this.tsSeparator2.Size = new System.Drawing.Size(217, 6);
            // 
            // cmCopyPage
            // 
            this.cmCopyPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Copy;
            this.cmCopyPage.Name = "cmCopyPage";
            this.cmCopyPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.cmCopyPage.Size = new System.Drawing.Size(220, 22);
            this.cmCopyPage.Text = "&Copy Page";
            // 
            // cmExportPage
            // 
            this.cmExportPage.Name = "cmExportPage";
            this.cmExportPage.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.C)));
            this.cmExportPage.Size = new System.Drawing.Size(220, 22);
            this.cmExportPage.Text = "&Export Page...";
            // 
            // tsSeparator3
            // 
            this.tsSeparator3.Name = "tsSeparator3";
            this.tsSeparator3.Size = new System.Drawing.Size(217, 6);
            // 
            // cmRefreshPage
            // 
            this.cmRefreshPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Refresh;
            this.cmRefreshPage.Name = "cmRefreshPage";
            this.cmRefreshPage.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.cmRefreshPage.Size = new System.Drawing.Size(220, 22);
            this.cmRefreshPage.Text = "&Refresh";
            // 
            // tsSeparator4
            // 
            this.tsSeparator4.Name = "tsSeparator4";
            this.tsSeparator4.Size = new System.Drawing.Size(217, 6);
            // 
            // cmMinimalGui
            // 
            this.cmMinimalGui.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.MenuToggle;
            this.cmMinimalGui.Name = "cmMinimalGui";
            this.cmMinimalGui.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.cmMinimalGui.Size = new System.Drawing.Size(220, 22);
            this.cmMinimalGui.Text = "&Minimal User Interface";

            //
            // PageContextMenu
            //
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(744, 662);
            this.ResumeLayout();
        }

        private ContextMenuStrip pageContextMenuItem;
        private ToolStripMenuItem cmShowInfo;
        private ToolStripMenuItem cmRating;

        private ToolStripMenuItem cmCopyPage;

        private ToolStripMenuItem cmRefreshPage;
        private ToolStripMenuItem cmMagnify;
        private ToolStripMenuItem cmPageType;
        private ToolStripMenuItem cmExportPage;
        private ToolStripMenuItem cmPageLayout;
        private ToolStripMenuItem cmFitAll;
        private ToolStripMenuItem cmFitWidth;
        private ToolStripMenuItem cmFitHeight;
        private ToolStripMenuItem cmFitBest;
        private ToolStripMenuItem cmOriginal;
        private ToolStripMenuItem cmFitWidthAdaptive;
        private ToolStripMenuItem cmOnlyFitOversized;
        private ToolStripMenuItem cmRightToLeft;
        private ToolStripMenuItem cmMinimalGui;
        private ToolStripMenuItem cmBookmarks;
        private ToolStripMenuItem cmSetBookmark;
        private ToolStripMenuItem cmRemoveBookmark;
        private ToolStripMenuItem cmLastPageRead;
        private ToolStripMenuItem cmPrevBookmark;
        private ToolStripMenuItem cmNextBookmark;
        private ToolStripMenuItem cmClose;
        private ToolStripMenuItem cmCopyPath;
        private ToolStripMenuItem cmRevealInExplorer;
        private ToolStripMenuItem cmSyncBrowser;
        private ToolStripMenuItem cmComics;
        private ToolStripMenuItem cmOpenComic;
        private ToolStripMenuItem cmCloseComic;
        private ToolStripMenuItem cmPrevFromList;
        private ToolStripMenuItem cmNextFromList;
        private ToolStripMenuItem cmPageRotate;
        private ToolStripMenuItem cmRotate0;
        private ToolStripMenuItem cmRotate90;
        private ToolStripMenuItem cmRotate180;
        private ToolStripMenuItem cmRotate270;
        private ToolStripMenuItem cmRandomFromList;
        private ToolStripMenuItem cmSinglePage;
        private ToolStripMenuItem cmTwoPages;
        private ToolStripMenuItem cmTwoPagesAdaptive;

        private ContextMenuStrip contextRating;
        private ToolStripMenuItem cmRate0;

        private ToolStripMenuItem cmRate1;
        private ToolStripMenuItem cmRate2;
        private ToolStripMenuItem cmRate3;
        private ToolStripMenuItem cmRate4;
        private ToolStripMenuItem cmRate5;
        private ToolStripMenuItem cmCloseAllToTheRight;

        private ToolStripMenuItem cmQuickRating;

        private ToolStripSeparator tsSeparator1;
        private ToolStripSeparator tsSeparator2;
        private ToolStripSeparator tsSeparator3;
        private ToolStripSeparator tsSeparator4;
        private ToolStripSeparator tsSeparator5;
        private ToolStripSeparator tsSeparator6;
        private ToolStripSeparator tsSeparator7;
        private ToolStripSeparator tsSeparator8;
        private ToolStripSeparator tsSeparator9;
        private ToolStripSeparator tsSeparatorComics;
        private ToolStripSeparator tsSeparator11;
        private ToolStripSeparator tsSeparator12;
        private ToolStripSeparator tsSeparator13;

        private ToolStripSeparator tsSeparatorBms;
        #endregion
    }
}
