using cYo.Common.ComponentModel;
using cYo.Common.Win32;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Views;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer
{
    public partial class MainForm
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            // TODO ; FIX
            if (disposing)
            {
                IdleProcess.Idle -= Application_Idle;
                Program.Database.BookChanged -= WatchedBookHasChanged;
                Program.BookFactory.TemporaryBookChanged -= WatchedBookHasChanged;
                books.BookOpened -= OnBookOpened;
                books.Slots.Changed -= OpenBooks_SlotsChanged;
                books.Slots.ForEach(delegate (ComicBookNavigator n)
                {
                    n.SafeDispose();
                });
                books.Slots.Clear();
                Program.Settings.SettingsChanged -= SettingsChanged;
                statusStrip.Dispose(); // changed
                if (comicDisplay != null)
                {
                    comicDisplay.Dispose();
                }
                if (components != null)
                {
                    components.Dispose(); // added
                }
            }
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mouseDisableTimer = new System.Windows.Forms.Timer(this.components);
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();

            this.fileMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.FileMenu();
            this.editMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.EditMenu();
            this.browseMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.BrowseMenu();
            this.readMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.ReadMenu();
            this.displayMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.DisplayMenu();
            this.helpMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.HelpMenu();
            this.mainToolStrip = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.MainToolStrip();

            //this.pageContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pageContextMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.PageContextMenu();
            this.statusStrip = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.StatusStrip();

            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notfifyContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmNotifyRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.cmNotifyExit = new System.Windows.Forms.ToolStripMenuItem();
            this.viewContainer = new System.Windows.Forms.Panel();
            this.panelReader = new System.Windows.Forms.Panel();
            this.readerContainer = new System.Windows.Forms.Panel();
            this.quickOpenView = new cYo.Projects.ComicRack.Viewer.Views.QuickOpenView();
            this.fileTabs = new cYo.Common.Windows.Forms.TabBar();

            this.trimTimer = new System.Windows.Forms.Timer(this.components);
            this.mainViewContainer = new cYo.Common.Windows.Forms.SizableContainer();
            this.mainView = new cYo.Projects.ComicRack.Viewer.Views.MainView();
            this.updateActivityTimer = new System.Windows.Forms.Timer(this.components);
            this.mainMenuStrip.SuspendLayout();
            this.notfifyContextMenu.SuspendLayout();
            this.viewContainer.SuspendLayout();
            this.panelReader.SuspendLayout();
            this.readerContainer.SuspendLayout();
            this.fileTabs.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.tabContextMenu.SuspendLayout();
            this.mainViewContainer.SuspendLayout();
            // 
            // mouseDisableTimer
            // 
            this.mouseDisableTimer.Interval = 500;
            this.mouseDisableTimer.Tick += new System.EventHandler(this.showDisableTimer_Tick);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.browseMenu,
            this.readMenu,
            this.displayMenu,
            this.helpMenu});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(744, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.MenuDeactivate += new System.EventHandler(this.mainMenuStrip_MenuDeactivate);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notfifyContextMenu;
            this.notifyIcon.Text = "Double Click to restore";
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            // 
            // notfifyContextMenu
            // 
            this.notfifyContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmNotifyRestore,
            this.cmNotifyExit});
            this.notfifyContextMenu.Name = "notfifyContextMenu";
            this.notfifyContextMenu.Size = new System.Drawing.Size(114, 54);
            // 
            // cmNotifyRestore
            // 
            this.cmNotifyRestore.Name = "cmNotifyRestore";
            this.cmNotifyRestore.Size = new System.Drawing.Size(113, 22);
            this.cmNotifyRestore.Text = "&Restore";
            // 
            // cmNotifyExit
            // 
            this.cmNotifyExit.Name = "cmNotifyExit";
            this.cmNotifyExit.Size = new System.Drawing.Size(113, 22);
            this.cmNotifyExit.Text = "&Exit";
            // 
            // viewContainer
            // 
            this.viewContainer.Controls.Add(this.panelReader);
            this.viewContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewContainer.Location = new System.Drawing.Point(0, 24);
            this.viewContainer.Name = "viewContainer";
            this.viewContainer.Size = new System.Drawing.Size(744, 364);
            this.viewContainer.TabIndex = 14;
            // 
            // panelReader
            // 
            this.panelReader.Controls.Add(this.readerContainer);
            this.panelReader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReader.Location = new System.Drawing.Point(0, 0);
            this.panelReader.Name = "panelReader";
            this.panelReader.Size = new System.Drawing.Size(744, 364);
            this.panelReader.TabIndex = 2;
            // 
            // readerContainer
            // 
            this.readerContainer.Controls.Add(this.quickOpenView);
            this.readerContainer.Controls.Add(this.fileTabs);
            this.readerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.readerContainer.Location = new System.Drawing.Point(0, 0);
            this.readerContainer.Name = "readerContainer";
            this.readerContainer.Size = new System.Drawing.Size(744, 364);
            this.readerContainer.TabIndex = 0;
            this.readerContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.readerContainer_Paint);
            // 
            // quickOpenView
            // 
            this.quickOpenView.AllowDrop = true;
            this.quickOpenView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.quickOpenView.BackColor = SystemColors.Window;
            this.quickOpenView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.quickOpenView.Caption = "Quick Open";
            this.quickOpenView.CaptionMargin = new System.Windows.Forms.Padding(2);
            this.quickOpenView.Location = new System.Drawing.Point(63, 50);
            this.quickOpenView.Margin = new System.Windows.Forms.Padding(12);
            this.quickOpenView.MinimumSize = new System.Drawing.Size(300, 250);
            this.quickOpenView.Name = "quickOpenView";
            this.quickOpenView.ShowBrowserCommand = true;
            this.quickOpenView.Size = new System.Drawing.Size(616, 289);
            this.quickOpenView.TabIndex = 2;
            this.quickOpenView.ThumbnailSize = 128;
            this.quickOpenView.Visible = false;
            this.quickOpenView.BookActivated += new System.EventHandler(this.QuickOpenBookActivated);
            this.quickOpenView.ShowBrowser += new System.EventHandler(this.quickOpenView_ShowBrowser);
            this.quickOpenView.OpenFile += new System.EventHandler(this.quickOpenView_OpenFile);
            this.quickOpenView.VisibleChanged += new System.EventHandler(this.QuickOpenVisibleChanged);
            this.quickOpenView.DragDrop += new System.Windows.Forms.DragEventHandler(this.BookDragDrop);
            this.quickOpenView.DragEnter += new System.Windows.Forms.DragEventHandler(this.BookDragEnter);
            // 
            // fileTabs
            // 
            this.fileTabs.AllowDrop = true;
            this.fileTabs.CloseImage = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Close;
            this.fileTabs.Controls.Add(this.mainToolStrip);
            this.fileTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.fileTabs.DragDropReorder = true;
            this.fileTabs.LeftIndent = 8;
            this.fileTabs.Location = new System.Drawing.Point(0, 0);
            this.fileTabs.Name = "fileTabs";
            this.fileTabs.OwnerDrawnTooltips = true;
            this.fileTabs.Size = new System.Drawing.Size(744, 31);
            this.fileTabs.TabIndex = 1;
            // 
            // trimTimer
            // 
            this.trimTimer.Enabled = true;
            this.trimTimer.Interval = 5000;
            this.trimTimer.Tick += new System.EventHandler(this.trimTimer_Tick);
            // 
            // mainViewContainer
            // 
            this.mainViewContainer.AutoGripPosition = true;
            this.mainViewContainer.BackColor = SystemColors.Control;
            this.mainViewContainer.Controls.Add(this.mainView);
            this.mainViewContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mainViewContainer.Location = new System.Drawing.Point(0, 388);
            this.mainViewContainer.Name = "mainViewContainer";
            this.mainViewContainer.Size = new System.Drawing.Size(744, 250);
            this.mainViewContainer.TabIndex = 2;
            this.mainViewContainer.ExpandedChanged += new System.EventHandler(this.mainViewContainer_ExpandedChanged);
            this.mainViewContainer.DockChanged += new System.EventHandler(this.mainViewContainer_DockChanged);
            // 
            // mainView
            // 
            this.mainView.BackColor = System.Drawing.Color.Transparent;
            this.mainView.Caption = "";
            this.mainView.CaptionMargin = new System.Windows.Forms.Padding(2);
            this.mainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainView.InfoPanelRight = false;
            this.mainView.Location = new System.Drawing.Point(0, 6);
            this.mainView.Margin = new System.Windows.Forms.Padding(6);
            this.mainView.Name = "mainView";
            this.mainView.Size = new System.Drawing.Size(744, 244);
            this.mainView.TabBarVisible = true;
            this.mainView.TabIndex = 0;
            this.mainView.TabChanged += new System.EventHandler(this.mainView_TabChanged);
            // 
            // updateActivityTimer
            // 
            this.updateActivityTimer.Enabled = true;
            this.updateActivityTimer.Interval = 1000;
            this.updateActivityTimer.Tick += new System.EventHandler(this.UpdateActivityTimerTick);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(744, 662);
            this.Controls.Add(this.viewContainer);
            this.Controls.Add(this.mainViewContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "ComicRack";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.pageContextMenu.ResumeLayout(false);
            this.notfifyContextMenu.ResumeLayout(false);
            this.viewContainer.ResumeLayout(false);
            this.panelReader.ResumeLayout(false);
            this.readerContainer.ResumeLayout(false);
            this.readerContainer.PerformLayout();
            this.fileTabs.ResumeLayout(false);
            this.fileTabs.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.tabContextMenu.ResumeLayout(false);
            this.mainViewContainer.ResumeLayout(false);
        }

        private ComicDisplay comicDisplay;
        private readonly NavigatorManager books;
        private Timer mouseDisableTimer;
        private MenuStrip mainMenuStrip;
        private MainView mainView;

        private ToolStripMenuItem fileMenu;
        private ToolStripMenuItem editMenu;
        private ToolStripMenuItem browseMenu;
        private ToolStripMenuItem readMenu;
        private ToolStripMenuItem displayMenu;
        private ToolStripMenuItem helpMenu;

        private ToolStrip mainToolStrip;
        private ContextMenuStrip pageContextMenu;
        private ContextMenuStrip tabContextMenu;
        private StatusStrip statusStrip;

        private NotifyIcon notifyIcon;

        private SizableContainer mainViewContainer;
        private ContextMenuStrip notfifyContextMenu;
        private ToolStripMenuItem cmNotifyRestore;
        private ToolStripMenuItem cmNotifyExit;
        private TabBar fileTabs;
        private Panel viewContainer;
        private ToolStripSeparator miLayoutSep;
        private Panel panelReader;
        private Panel readerContainer;
        private ToolStripSeparator miWorkspaceSep;
        private Timer trimTimer;

        private ToolStripSeparator cmBookmarkSeparator;
        private Timer updateActivityTimer;
        private QuickOpenView quickOpenView;
        private ToolStripSeparator cmComicsSep;

    }
}