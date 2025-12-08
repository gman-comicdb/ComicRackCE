using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

partial class BrowseMenu
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
        this.browseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.miToggleBrowser = new System.Windows.Forms.ToolStripMenuItem();
        this.miViewLibrary = new System.Windows.Forms.ToolStripMenuItem();
        this.miViewFolders = new System.Windows.Forms.ToolStripMenuItem();
        this.miViewPages = new System.Windows.Forms.ToolStripMenuItem();
        this.miSidebar = new System.Windows.Forms.ToolStripMenuItem();
        this.miSmallPreview = new System.Windows.Forms.ToolStripMenuItem();
        this.miSearchBrowser = new System.Windows.Forms.ToolStripMenuItem();
        this.miInfoPanel = new System.Windows.Forms.ToolStripMenuItem();
        this.miPreviousList = new System.Windows.Forms.ToolStripMenuItem();
        this.miNextList = new System.Windows.Forms.ToolStripMenuItem();
        this.miWorkspaces = new System.Windows.Forms.ToolStripMenuItem();
        this.miSaveWorkspace = new System.Windows.Forms.ToolStripMenuItem();
        this.miEditWorkspaces = new System.Windows.Forms.ToolStripMenuItem();
        this.miListLayouts = new System.Windows.Forms.ToolStripMenuItem();
        this.miEditListLayout = new System.Windows.Forms.ToolStripMenuItem();
        this.miSaveListLayout = new System.Windows.Forms.ToolStripMenuItem();
        this.miEditLayouts = new System.Windows.Forms.ToolStripMenuItem();
        this.miSetAllListsSame = new System.Windows.Forms.ToolStripMenuItem();

        this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        this.tsSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        this.tsSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        this.tsSeparator5 = new System.Windows.Forms.ToolStripSeparator();

        this.tsSeparator6 = new System.Windows.Forms.ToolStripSeparator();
        this.tsSeparator7 = new System.Windows.Forms.ToolStripSeparator();
        this.tsSeparator8 = new System.Windows.Forms.ToolStripSeparator();
        this.tsSeparator9 = new System.Windows.Forms.ToolStripSeparator();
        this.tsSeparator10 = new System.Windows.Forms.ToolStripSeparator();
        this.SuspendLayout();

        // 
        // browseMenu
        // 
        this.browseMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miToggleBrowser,
        this.tsSeparator1,
        this.miViewLibrary,
        this.miViewFolders,
        this.miViewPages,
        this.tsSeparator2,
        this.miSidebar,
        this.miSmallPreview,
        this.miSearchBrowser,
        this.miInfoPanel,
        this.tsSeparator3,
        this.miPreviousList,
        this.miNextList,
        this.tsSeparator4,
        this.miWorkspaces,
        this.miListLayouts});
        this.browseMenuItem.Name = "browseMenu";
        this.browseMenuItem.Size = new System.Drawing.Size(57, 20);
        this.browseMenuItem.Text = "&Browse";
        // 
        // miToggleBrowser
        // 
        this.miToggleBrowser.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Browser;
        this.miToggleBrowser.Name = "miToggleBrowser";
        this.miToggleBrowser.ShortcutKeys = System.Windows.Forms.Keys.F3;
        this.miToggleBrowser.Size = new System.Drawing.Size(205, 22);
        this.miToggleBrowser.Text = "&Browser";
        // 
        // tsSeparator1
        // 
        this.tsSeparator1.Name = "tsSeparator1";
        this.tsSeparator1.Size = new System.Drawing.Size(202, 6);
        // 
        // miViewLibrary
        // 
        this.miViewLibrary.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Database;
        this.miViewLibrary.Name = "miViewLibrary";
        this.miViewLibrary.ShortcutKeys = System.Windows.Forms.Keys.F6;
        this.miViewLibrary.Size = new System.Drawing.Size(205, 22);
        this.miViewLibrary.Text = "Li&brary";
        // 
        // miViewFolders
        // 
        this.miViewFolders.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FileBrowser;
        this.miViewFolders.Name = "miViewFolders";
        this.miViewFolders.ShortcutKeys = System.Windows.Forms.Keys.F7;
        this.miViewFolders.Size = new System.Drawing.Size(205, 22);
        this.miViewFolders.Text = "&Folders";
        // 
        // miViewPages
        // 
        this.miViewPages.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ComicPage;
        this.miViewPages.Name = "miViewPages";
        this.miViewPages.ShortcutKeys = System.Windows.Forms.Keys.F8;
        this.miViewPages.Size = new System.Drawing.Size(205, 22);
        this.miViewPages.Text = "&Pages";
        // 
        // tsSeparator2
        // 
        this.tsSeparator2.Name = "tsSeparator2";
        this.tsSeparator2.Size = new System.Drawing.Size(202, 6);
        // 
        // miSidebar
        // 
        this.miSidebar.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Sidebar;
        this.miSidebar.Name = "miSidebar";
        this.miSidebar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F6)));
        this.miSidebar.Size = new System.Drawing.Size(205, 22);
        this.miSidebar.Text = "&Sidebar";
        // 
        // miSmallPreview
        // 
        this.miSmallPreview.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.SmallPreview;
        this.miSmallPreview.Name = "miSmallPreview";
        this.miSmallPreview.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F7)));
        this.miSmallPreview.Size = new System.Drawing.Size(205, 22);
        this.miSmallPreview.Text = "S&mall Preview";
        // 
        // miSearchBrowser
        // 
        this.miSearchBrowser.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Search;
        this.miSearchBrowser.Name = "miSearchBrowser";
        this.miSearchBrowser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F8)));
        this.miSearchBrowser.Size = new System.Drawing.Size(205, 22);
        this.miSearchBrowser.Text = "S&earch Browser";
        // 
        // miInfoPanel
        // 
        this.miInfoPanel.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.InfoPanel;
        this.miInfoPanel.Name = "miInfoPanel";
        this.miInfoPanel.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F9)));
        this.miInfoPanel.Size = new System.Drawing.Size(205, 22);
        this.miInfoPanel.Text = "Info Panel";
        // 
        // tsSeparator3
        // 
        this.tsSeparator3.Name = "tsSeparator3";
        this.tsSeparator3.Size = new System.Drawing.Size(202, 6);
        // 
        // miPreviousList
        // 
        this.miPreviousList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.BrowsePrevious;
        this.miPreviousList.Name = "miPreviousList";
        this.miPreviousList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
        this.miPreviousList.Size = new System.Drawing.Size(205, 22);
        this.miPreviousList.Text = "Previous List";
        // 
        // miNextList
        // 
        this.miNextList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.BrowseNext;
        this.miNextList.Name = "miNextList";
        this.miNextList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
        this.miNextList.Size = new System.Drawing.Size(205, 22);
        this.miNextList.Text = "Next List";
        // 
        // tsSeparator4
        // 
        this.tsSeparator4.Name = "tsSeparator4";
        this.tsSeparator4.Size = new System.Drawing.Size(202, 6);
        // 
        // miWorkspaces
        // 
        this.miWorkspaces.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miSaveWorkspace,
        this.miEditWorkspaces,
        this.tsSeparator5});
        this.miWorkspaces.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Workspace;
        this.miWorkspaces.Name = "miWorkspaces";
        this.miWorkspaces.Size = new System.Drawing.Size(205, 22);
        this.miWorkspaces.Text = "&Workspaces";
        // 
        // miSaveWorkspace
        // 
        this.miSaveWorkspace.Name = "miSaveWorkspace";
        this.miSaveWorkspace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
        this.miSaveWorkspace.Size = new System.Drawing.Size(237, 22);
        this.miSaveWorkspace.Text = "&Save Workspace...";
        // 
        // miEditWorkspaces
        // 
        this.miEditWorkspaces.Name = "miEditWorkspaces";
        this.miEditWorkspaces.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
        | System.Windows.Forms.Keys.W)));
        this.miEditWorkspaces.Size = new System.Drawing.Size(237, 22);
        this.miEditWorkspaces.Text = "&Edit Workspaces...";
        // 
        // tsSeparator5
        // 
        this.tsSeparator5.Name = "tsSeparator5";
        this.tsSeparator5.Size = new System.Drawing.Size(234, 6);
        // 
        // miListLayouts
        // 
        this.miListLayouts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miEditListLayout,
        this.miSaveListLayout,
        this.tsSeparator6,
        this.miEditLayouts,
        this.miSetAllListsSame,
        this.tsSeparator7});
        this.miListLayouts.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ListLayout;
        this.miListLayouts.Name = "miListLayouts";
        this.miListLayouts.Size = new System.Drawing.Size(205, 22);
        this.miListLayouts.Text = "List Layout";
        // 
        // miEditListLayout
        // 
        this.miEditListLayout.Name = "miEditListLayout";
        this.miEditListLayout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
        this.miEditListLayout.Size = new System.Drawing.Size(225, 22);
        this.miEditListLayout.Text = "&Edit List Layout...";
        // 
        // miSaveListLayout
        // 
        this.miSaveListLayout.Name = "miSaveListLayout";
        this.miSaveListLayout.Size = new System.Drawing.Size(225, 22);
        this.miSaveListLayout.Text = "&Save List Layout...";
        // 
        // tsSeparator6
        // 
        this.tsSeparator6.Name = "tsSeparator6";
        this.tsSeparator6.Size = new System.Drawing.Size(222, 6);
        // 
        // miEditLayouts
        // 
        this.miEditLayouts.Name = "miEditLayouts";
        this.miEditLayouts.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
        | System.Windows.Forms.Keys.L)));
        this.miEditLayouts.Size = new System.Drawing.Size(225, 22);
        this.miEditLayouts.Text = "&Edit Layouts...";
        // 
        // miSetAllListsSame
        // 
        this.miSetAllListsSame.Name = "miSetAllListsSame";
        this.miSetAllListsSame.Size = new System.Drawing.Size(225, 22);
        this.miSetAllListsSame.Text = "Set all Lists to current Layout";
        // 
        // tsSeparator7
        // 
        this.tsSeparator7.Name = "tsSeparator7";
        this.tsSeparator7.Size = new System.Drawing.Size(222, 6);

        // 
        // tsSeparator1
        // 
        this.tsSeparator1.Name = "tsSeparator1";
        this.tsSeparator1.Size = new System.Drawing.Size(217, 6);
        // 
        // tsSeparator2
        // 
        this.tsSeparator2.Name = "tsSeparator2";
        this.tsSeparator2.Size = new System.Drawing.Size(217, 6);
        // 
        // tsSeparator3
        // 
        this.tsSeparator3.Name = "tsSeparator3";
        this.tsSeparator3.Size = new System.Drawing.Size(217, 6);
        // 
        // tsSeparator4
        // 
        this.tsSeparator4.Name = "tsSeparator4";
        this.tsSeparator4.Size = new System.Drawing.Size(217, 6);
        // 
        // tsSeparator5
        // 
        this.tsSeparator5.Name = "tsSeparator5";
        this.tsSeparator5.Size = new System.Drawing.Size(217, 6);
        // 
        // tsSeparator6
        // 
        this.tsSeparator6.Name = "tsSeparator6";
        this.tsSeparator6.Size = new System.Drawing.Size(217, 6);
        // 
        // tsSeparator7
        // 
        this.tsSeparator7.Name = "tsSeparator7";
        this.tsSeparator7.Size = new System.Drawing.Size(217, 6);
        // 
        // tsSeparator8
        // 
        this.tsSeparator8.Name = "tsSeparator8";
        this.tsSeparator8.Size = new System.Drawing.Size(217, 6);
        // 
        // tsSeparator9
        // 
        this.tsSeparator9.Name = "tsSeparator9";
        this.tsSeparator9.Size = new System.Drawing.Size(217, 6);
        // 
        // tsSeparator10
        // 
        this.tsSeparator10.Name = "tsSeparator10";
        this.tsSeparator10.Size = new System.Drawing.Size(217, 6);

        // 
        // menu
        // 
        this.menu.Items.Add(browseMenuItem);
        this.menu.Location = new System.Drawing.Point(0, 0);
        this.menu.Name = "menu";
        this.menu.Size = new System.Drawing.Size(744, 24);
        this.menu.TabIndex = 0;

        //
        // BrowseMenu
        //
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        this.ClientSize = new System.Drawing.Size(744, 662);
        this.Controls.Add(menu);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private MenuStrip menu;
    private ToolStripMenuItem browseMenuItem;
    private ToolStripMenuItem miViewLibrary;
    private ToolStripMenuItem miViewFolders;
    private ToolStripMenuItem miViewPages;
    private ToolStripMenuItem miSidebar;
    private ToolStripMenuItem miSmallPreview;
    private ToolStripMenuItem miSearchBrowser;
    private ToolStripMenuItem miToggleBrowser;

    private ToolStripMenuItem miInfoPanel;
    private ToolStripMenuItem miPreviousList;
    private ToolStripMenuItem miNextList;

    private ToolStripMenuItem miWorkspaces;
    private ToolStripMenuItem miSaveWorkspace;
    private ToolStripMenuItem miEditWorkspaces;

    private ToolStripMenuItem miListLayouts;
    private ToolStripMenuItem miEditListLayout;
    private ToolStripMenuItem miSaveListLayout;
    private ToolStripMenuItem miEditLayouts;
    private ToolStripMenuItem miSetAllListsSame;

    private ToolStripSeparator tsSeparator1;
    private ToolStripSeparator tsSeparator2;
    private ToolStripSeparator tsSeparator3;
    private ToolStripSeparator tsSeparator4;
    private ToolStripSeparator tsSeparator5;
    private ToolStripSeparator tsSeparator6;
    private ToolStripSeparator tsSeparator7;
    private ToolStripSeparator tsSeparator8;
    private ToolStripSeparator tsSeparator9;
    private ToolStripSeparator tsSeparator10;

    #endregion
}
