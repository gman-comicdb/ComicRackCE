using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class FileMenu : UserControl
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

        this.menu = new System.Windows.Forms.MenuStrip();
        this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.miOpenComic = new System.Windows.Forms.ToolStripMenuItem();
        this.miCloseComic = new System.Windows.Forms.ToolStripMenuItem();
        this.miCloseAllComics = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
        this.miAddTab = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
        this.miAddFolderToLibrary = new System.Windows.Forms.ToolStripMenuItem();
        this.miScan = new System.Windows.Forms.ToolStripMenuItem();
        this.miUpdateAllComicFiles = new System.Windows.Forms.ToolStripMenuItem();
        this.miUpdateWebComics = new System.Windows.Forms.ToolStripMenuItem();
        this.miSynchronizeDevices = new System.Windows.Forms.ToolStripMenuItem();
        this.miCacheThumbnails = new System.Windows.Forms.ToolStripMenuItem();
        this.miTasks = new System.Windows.Forms.ToolStripMenuItem();
        this.miFileAutomation = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem57 = new System.Windows.Forms.ToolStripSeparator();
        this.miNewComic = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem42 = new System.Windows.Forms.ToolStripSeparator();
        this.miOpenRemoteLibrary = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripInsertSeperator = new System.Windows.Forms.ToolStripSeparator();
        this.miOpenNow = new System.Windows.Forms.ToolStripMenuItem();
        this.miOpenRecent = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
        this.miRestart = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem24 = new System.Windows.Forms.ToolStripSeparator();
        this.miExit = new System.Windows.Forms.ToolStripMenuItem();
        this.SuspendLayout();

        // 
        // fileMenu
        // 
        this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miOpenComic,
        this.miCloseComic,
        this.miCloseAllComics,
        this.toolStripMenuItem7,
        this.miAddTab,
        this.toolStripMenuItem14,
        this.miAddFolderToLibrary,
        this.miScan,
        this.miUpdateAllComicFiles,
        this.miUpdateWebComics,
        this.miCacheThumbnails,
        this.miSynchronizeDevices,
        this.miTasks,
        this.miFileAutomation,
        this.toolStripMenuItem57,
        this.miNewComic,
        this.toolStripMenuItem42,
        this.miOpenRemoteLibrary,
        this.toolStripInsertSeperator,
        this.miOpenNow,
        this.miOpenRecent,
        this.toolStripMenuItem4,
        this.miRestart,
        this.toolStripMenuItem24,
        this.miExit});
        this.fileMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
        this.fileMenuItem.Name = "fileMenu";
        this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
        this.fileMenuItem.Text = "&File";
        this.fileMenuItem.DropDownOpening += new System.EventHandler(this.fileMenu_DropDownOpening);
        // 
        // miOpenComic
        // 
        this.miOpenComic.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Open;
        this.miOpenComic.Name = "miOpenComic";
        this.miOpenComic.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
        this.miOpenComic.Size = new System.Drawing.Size(280, 22);
        this.miOpenComic.Text = "&Open File...";
        // 
        // miCloseComic
        // 
        this.miCloseComic.Name = "miCloseComic";
        this.miCloseComic.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
        this.miCloseComic.Size = new System.Drawing.Size(280, 22);
        this.miCloseComic.Text = "&Close";
        // 
        // miCloseAllComics
        // 
        this.miCloseAllComics.Name = "miCloseAllComics";
        this.miCloseAllComics.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.X)));
        this.miCloseAllComics.Size = new System.Drawing.Size(280, 22);
        this.miCloseAllComics.Text = "Close A&ll";
        // 
        // toolStripMenuItem7
        // 
        this.toolStripMenuItem7.Name = "toolStripMenuItem7";
        this.toolStripMenuItem7.Size = new System.Drawing.Size(277, 6);
        // 
        // miAddTab
        // 
        this.miAddTab.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.NewTab;
        this.miAddTab.Name = "miAddTab";
        this.miAddTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
        this.miAddTab.Size = new System.Drawing.Size(280, 22);
        this.miAddTab.Text = "New &Tab";
        // 
        // toolStripMenuItem14
        // 
        this.toolStripMenuItem14.Name = "toolStripMenuItem14";
        this.toolStripMenuItem14.Size = new System.Drawing.Size(277, 6);
        // 
        // miAddFolderToLibrary
        // 
        this.miAddFolderToLibrary.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.AddFolder;
        this.miAddFolderToLibrary.Name = "miAddFolderToLibrary";
        this.miAddFolderToLibrary.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.A)));
        this.miAddFolderToLibrary.Size = new System.Drawing.Size(280, 22);
        this.miAddFolderToLibrary.Text = "&Add Folder to Library...";
        // 
        // miScan
        // 
        this.miScan.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Scan;
        this.miScan.Name = "miScan";
        this.miScan.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.S)));
        this.miScan.Size = new System.Drawing.Size(280, 22);
        this.miScan.Text = "Scan Book &Folders";
        // 
        // miUpdateAllComicFiles
        // 
        this.miUpdateAllComicFiles.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.UpdateSmall;
        this.miUpdateAllComicFiles.Name = "miUpdateAllComicFiles";
        this.miUpdateAllComicFiles.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.U)));
        this.miUpdateAllComicFiles.Size = new System.Drawing.Size(280, 22);
        this.miUpdateAllComicFiles.Text = "Update all Book Files";
        // 
        // miUpdateWebComics
        // 
        this.miUpdateWebComics.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.UpdateWeb;
        this.miUpdateWebComics.Name = "miUpdateWebComics";
        this.miUpdateWebComics.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.W)));
        this.miUpdateWebComics.Size = new System.Drawing.Size(280, 22);
        this.miUpdateWebComics.Text = "Update Web Comics";
        // 
        // miSynchronizeDevices
        // 
        this.miSynchronizeDevices.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.DeviceSync;
        this.miSynchronizeDevices.Name = "miSynchronizeDevices";
        this.miSynchronizeDevices.Size = new System.Drawing.Size(280, 22);
        this.miSynchronizeDevices.Text = "Synchronize Devices";
        // 
        // miCacheThumbnails
        // 
        this.miCacheThumbnails.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Screenshot;
        this.miCacheThumbnails.Name = "miCacheThumbnails";
        this.miCacheThumbnails.Size = new System.Drawing.Size(280, 22);
        this.miCacheThumbnails.Text = "Generate Cover Thumbnails";
        // 
        // miTasks
        // 
        this.miTasks.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.BackgroundJob;
        this.miTasks.Name = "miTasks";
        this.miTasks.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.T)));
        this.miTasks.Size = new System.Drawing.Size(280, 22);
        this.miTasks.Text = "&Tasks...";
        // 
        // miFileAutomation
        // 
        this.miFileAutomation.Name = "miFileAutomation";
        this.miFileAutomation.Size = new System.Drawing.Size(280, 22);
        this.miFileAutomation.Text = "A&utomation";
        // 
        // toolStripMenuItem57
        // 
        this.toolStripMenuItem57.Name = "toolStripMenuItem57";
        this.toolStripMenuItem57.Size = new System.Drawing.Size(277, 6);
        // 
        // miNewComic
        // 
        this.miNewComic.Name = "miNewComic";
        this.miNewComic.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.N)));
        this.miNewComic.Size = new System.Drawing.Size(280, 22);
        this.miNewComic.Text = "&New fileless Book Entry...";
        // 
        // toolStripMenuItem42
        // 
        this.toolStripMenuItem42.Name = "toolStripMenuItem42";
        this.toolStripMenuItem42.Size = new System.Drawing.Size(277, 6);
        // 
        // miOpenRemoteLibrary
        // 
        this.miOpenRemoteLibrary.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RemoteDatabase;
        this.miOpenRemoteLibrary.Name = "miOpenRemoteLibrary";
        this.miOpenRemoteLibrary.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.R)));
        this.miOpenRemoteLibrary.Size = new System.Drawing.Size(280, 22);
        this.miOpenRemoteLibrary.Text = "Open Remote Library...";
        // 
        // toolStripInsertSeperator
        // 
        this.toolStripInsertSeperator.Name = "toolStripInsertSeperator";
        this.toolStripInsertSeperator.Size = new System.Drawing.Size(277, 6);
        // 
        // miOpenNow
        // 
        this.miOpenNow.Name = "miOpenNow";
        this.miOpenNow.Size = new System.Drawing.Size(280, 22);
        this.miOpenNow.Text = "Open Books";
        // 
        // miOpenRecent
        // 
        this.miOpenRecent.Name = "miOpenRecent";
        this.miOpenRecent.Size = new System.Drawing.Size(280, 22);
        this.miOpenRecent.Text = "&Recent Books";
        this.miOpenRecent.DropDownOpening += new System.EventHandler(this.RecentFilesMenuOpening);
        // 
        // toolStripMenuItem4
        // 
        this.toolStripMenuItem4.Name = "toolStripMenuItem4";
        this.toolStripMenuItem4.Size = new System.Drawing.Size(277, 6);
        // 
        // miRestart
        // 
        this.miRestart.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Restart;
        this.miRestart.Name = "miRestart";
        this.miRestart.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.Q)));
        this.miRestart.Size = new System.Drawing.Size(280, 22);
        this.miRestart.Text = "Rest&art";
        // 
        // toolStripMenuItem24
        // 
        this.toolStripMenuItem24.Name = "toolStripMenuItem24";
        this.toolStripMenuItem24.Size = new System.Drawing.Size(277, 6);
        // 
        // miExit
        // 
        this.miExit.Name = "miExit";
        this.miExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
        this.miExit.Size = new System.Drawing.Size(280, 22);
        this.miExit.Text = "&Exit";

        // 
        // menu
        // 
        this.menu.Items.Add(fileMenuItem);
        this.menu.Location = new System.Drawing.Point(0, 0);
        this.menu.Name = "menu";
        this.menu.Size = new System.Drawing.Size(744, 24);
        this.menu.TabIndex = 0;

        //
        // FileMenu
        //
        //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        this.ClientSize = new System.Drawing.Size(744, 662);
        this.Controls.Add(menu);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private MenuStrip menu;
    private ToolStripMenuItem fileMenuItem;
    private ToolStripMenuItem miOpenComic;
    private ToolStripMenuItem miCloseComic;
    private ToolStripMenuItem miCloseAllComics;
    private ToolStripSeparator toolStripMenuItem7;
    private ToolStripMenuItem miAddTab;
    private ToolStripSeparator toolStripMenuItem14;
    private ToolStripMenuItem miAddFolderToLibrary;
    private ToolStripMenuItem miScan;
    private ToolStripMenuItem miUpdateAllComicFiles;
    private ToolStripMenuItem miUpdateWebComics;
    private ToolStripMenuItem miCacheThumbnails;
    private ToolStripMenuItem miSynchronizeDevices;
    private ToolStripMenuItem miTasks;
    private ToolStripMenuItem miFileAutomation;
    private ToolStripSeparator toolStripMenuItem57;
    private ToolStripMenuItem miNewComic;
    private ToolStripSeparator toolStripMenuItem42;
    private ToolStripMenuItem miOpenRemoteLibrary;
    private ToolStripSeparator toolStripInsertSeperator;
    private ToolStripMenuItem miOpenNow;
    private ToolStripMenuItem miOpenRecent;
    private ToolStripSeparator toolStripMenuItem4;
    private ToolStripMenuItem miRestart;
    private ToolStripSeparator toolStripMenuItem24;
    private ToolStripMenuItem miExit;
    #endregion
}
