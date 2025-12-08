using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

partial class ReadMenu
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
        this.readMenuItem = new System.Windows.Forms.ToolStripMenuItem();

        this.miFirstPage = new System.Windows.Forms.ToolStripMenuItem();
        this.miPrevPage = new System.Windows.Forms.ToolStripMenuItem();
        this.miNextPage = new System.Windows.Forms.ToolStripMenuItem();
        this.miLastPage = new System.Windows.Forms.ToolStripMenuItem();
        this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.miPrevFromList = new System.Windows.Forms.ToolStripMenuItem();
        this.miNextFromList = new System.Windows.Forms.ToolStripMenuItem();
        this.miRandomFromList = new System.Windows.Forms.ToolStripMenuItem();
        this.miSyncBrowser = new System.Windows.Forms.ToolStripMenuItem();
        this.miPrevTab = new System.Windows.Forms.ToolStripMenuItem();
        this.miNextTab = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
        this.miAutoScroll = new System.Windows.Forms.ToolStripMenuItem();
        this.miDoublePageAutoScroll = new System.Windows.Forms.ToolStripMenuItem();
        this.miTrackCurrentPage = new System.Windows.Forms.ToolStripMenuItem();

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
        // readMenu
        // 
        this.readMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miFirstPage,
        this.miPrevPage,
        this.miNextPage,
        this.miLastPage,
        this.tsSeparator1,
        this.miPrevFromList,
        this.miNextFromList,
        this.miRandomFromList,
        this.miSyncBrowser,
        this.tsSeparator2,
        this.miPrevTab,
        this.miNextTab,
        this.toolStripMenuItem1,
        this.miAutoScroll,
        this.miDoublePageAutoScroll,
        this.tsSeparator3,
        this.miTrackCurrentPage});
        this.readMenuItem.Name = "readMenu";
        this.readMenuItem.Size = new System.Drawing.Size(45, 20);
        this.readMenuItem.Text = "&Read";
        // 
        // miFirstPage
        // 
        this.miFirstPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GoFirst;
        this.miFirstPage.Name = "miFirstPage";
        this.miFirstPage.ShortcutKeyDisplayString = "";
        this.miFirstPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
        this.miFirstPage.Size = new System.Drawing.Size(287, 22);
        this.miFirstPage.Text = "&First Page";
        // 
        // miPrevPage
        // 
        this.miPrevPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GoPrevious;
        this.miPrevPage.Name = "miPrevPage";
        this.miPrevPage.ShortcutKeyDisplayString = "";
        this.miPrevPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
        this.miPrevPage.Size = new System.Drawing.Size(287, 22);
        this.miPrevPage.Text = "&Previous Page";
        // 
        // miNextPage
        // 
        this.miNextPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GoNext;
        this.miNextPage.Name = "miNextPage";
        this.miNextPage.ShortcutKeyDisplayString = "";
        this.miNextPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
        this.miNextPage.Size = new System.Drawing.Size(287, 22);
        this.miNextPage.Text = "&Next Page";
        // 
        // miLastPage
        // 
        this.miLastPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GoLast;
        this.miLastPage.Name = "miLastPage";
        this.miLastPage.ShortcutKeyDisplayString = "";
        this.miLastPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
        this.miLastPage.Size = new System.Drawing.Size(287, 22);
        this.miLastPage.Text = "&Last Page";
        // 
        // tsSeparator1
        // 
        this.tsSeparator1.Name = "tsSeparator1";
        this.tsSeparator1.Size = new System.Drawing.Size(284, 6);
        // 
        // miPrevFromList
        // 
        this.miPrevFromList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.PrevFromList;
        this.miPrevFromList.Name = "miPrevFromList";
        this.miPrevFromList.ShortcutKeyDisplayString = "";
        this.miPrevFromList.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
        | System.Windows.Forms.Keys.P)));
        this.miPrevFromList.Size = new System.Drawing.Size(287, 22);
        this.miPrevFromList.Text = "Pre&vious Book";
        // 
        // miNextFromList
        // 
        this.miNextFromList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.NextFromList;
        this.miNextFromList.Name = "miNextFromList";
        this.miNextFromList.ShortcutKeyDisplayString = "";
        this.miNextFromList.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
        | System.Windows.Forms.Keys.N)));
        this.miNextFromList.Size = new System.Drawing.Size(287, 22);
        this.miNextFromList.Text = "Ne&xt Book";
        // 
        // miRandomFromList
        // 
        this.miRandomFromList.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RandomComic;
        this.miRandomFromList.Name = "miRandomFromList";
        this.miRandomFromList.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
        | System.Windows.Forms.Keys.O)));
        this.miRandomFromList.Size = new System.Drawing.Size(287, 22);
        this.miRandomFromList.Text = "Random Book";
        // 
        // miSyncBrowser
        // 
        this.miSyncBrowser.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.SyncBrowser;
        this.miSyncBrowser.Name = "miSyncBrowser";
        this.miSyncBrowser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F3)));
        this.miSyncBrowser.Size = new System.Drawing.Size(287, 22);
        this.miSyncBrowser.Text = "Show in &Browser";
        // 
        // tsSeparator2
        // 
        this.tsSeparator2.Name = "tsSeparator2";
        this.tsSeparator2.Size = new System.Drawing.Size(284, 6);
        // 
        // miPrevTab
        // 
        this.miPrevTab.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Previous;
        this.miPrevTab.Name = "miPrevTab";
        this.miPrevTab.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.J)));
        this.miPrevTab.Size = new System.Drawing.Size(287, 22);
        this.miPrevTab.Text = "&Previous Tab";
        // 
        // miNextTab
        // 
        this.miNextTab.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Next;
        this.miNextTab.Name = "miNextTab";
        this.miNextTab.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.K)));
        this.miNextTab.Size = new System.Drawing.Size(287, 22);
        this.miNextTab.Text = "Next &Tab";
        // 
        // toolStripMenuItem1
        // 
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new System.Drawing.Size(284, 6);
        // 
        // miAutoScroll
        // 
        this.miAutoScroll.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.CursorScroll;
        this.miAutoScroll.Name = "miAutoScroll";
        this.miAutoScroll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
        this.miAutoScroll.Size = new System.Drawing.Size(287, 22);
        this.miAutoScroll.Text = "&Auto Scrolling";
        // 
        // miDoublePageAutoScroll
        // 
        this.miDoublePageAutoScroll.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.TwoPageAutoscroll;
        this.miDoublePageAutoScroll.Name = "miDoublePageAutoScroll";
        this.miDoublePageAutoScroll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.S)));
        this.miDoublePageAutoScroll.Size = new System.Drawing.Size(287, 22);
        this.miDoublePageAutoScroll.Text = "Double Page Auto Scrolling";
        // 
        // tsSeparator3
        // 
        this.tsSeparator3.Name = "tsSeparator3";
        this.tsSeparator3.Size = new System.Drawing.Size(284, 6);
        // 
        // miTrackCurrentPage
        // 
        this.miTrackCurrentPage.Name = "miTrackCurrentPage";
        this.miTrackCurrentPage.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.T)));
        this.miTrackCurrentPage.Size = new System.Drawing.Size(287, 22);
        this.miTrackCurrentPage.Text = "Track current Page";

        // 
        // menu
        // 
        this.menu.Items.Add(readMenuItem);
        this.menu.Location = new System.Drawing.Point(0, 0);
        this.menu.Name = "menu";
        this.menu.Size = new System.Drawing.Size(744, 24);
        this.menu.TabIndex = 0;

        //
        // ReadMenu
        //
        //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        this.ClientSize = new System.Drawing.Size(744, 662);
        this.Controls.Add(menu);
        this.ResumeLayout();
    }

    private MenuStrip menu;
    private ToolStripMenuItem readMenuItem;
    private ToolStripMenuItem miFirstPage;
    private ToolStripMenuItem miPrevPage;
    private ToolStripMenuItem miNextPage;
    private ToolStripMenuItem miLastPage;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem miPrevFromList;
    private ToolStripMenuItem miNextFromList;
    private ToolStripMenuItem miRandomFromList;

    private ToolStripMenuItem miSyncBrowser;
    private ToolStripMenuItem miPrevTab;
    private ToolStripMenuItem miNextTab;

    private ToolStripMenuItem miAutoScroll;
    private ToolStripMenuItem miRightToLeft;
    private ToolStripMenuItem miDoublePageAutoScroll;
    private ToolStripMenuItem miTrackCurrentPage;

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
