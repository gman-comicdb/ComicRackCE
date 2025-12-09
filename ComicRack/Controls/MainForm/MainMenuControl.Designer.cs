using cYo.Common.ComponentModel;
using cYo.Common.Win32;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Views;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm;

public partial class MainMenuControl : UserControl
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    //protected override void Dispose(bool disposing)
    //{
    //    if (disposing)
    //    {
    //        IdleProcess.Idle -= Application_Idle;
    //        Program.Database.BookChanged -= WatchedBookHasChanged;
    //        Program.BookFactory.TemporaryBookChanged -= WatchedBookHasChanged;
    //        books.BookOpened -= OnBookOpened;
    //        books.Slots.Changed -= OpenBooks_SlotsChanged;
    //        books.Slots.ForEach(delegate (ComicBookNavigator n)
    //        {
    //            n.SafeDispose();
    //        });
    //        books.Slots.Clear();
    //        Program.Settings.SettingsChanged -= SettingsChanged;
    //        ToolStripStatusLabel toolStripStatusLabel = tsScanActivity;
    //        ToolStripStatusLabel toolStripStatusLabel2 = tsReadInfoActivity;
    //        ToolStripStatusLabel toolStripStatusLabel3 = tsWriteInfoActivity;
    //        ToolStripStatusLabel toolStripStatusLabel4 = tsPageActivity;
    //        bool flag2 = (tsExportActivity.Visible = false);
    //        bool flag4 = (toolStripStatusLabel4.Visible = flag2);
    //        bool flag6 = (toolStripStatusLabel3.Visible = flag4);
    //        bool visible = (toolStripStatusLabel2.Visible = flag6);
    //        toolStripStatusLabel.Visible = visible;
    //        if (comicDisplay != null)
    //        {
    //            comicDisplay.Dispose();
    //        }
    //        if (components != null)
    //        {
    //            components.Dispose();
    //        }
    //    }
    //    base.Dispose(disposing);
    //}

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (controller != null)
        {
            controller.Dispose();
            controller = null;
        }
            
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        //MenuStrip?.Dispose();
        FileMenu?.Dispose();
        EditMenu?.Dispose();
        BrowseMenu?.Dispose();
        ReadMenu?.Dispose();
        DisplayMenu?.Dispose();
        HelpMenu?.Dispose();
        ToolStrip?.Dispose();
        StatusStrip?.Dispose();
        PageContextMenu?.Dispose();
        TabContextMenu?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
        //this.fileMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.FileMenu(controller);
        //this.editMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.EditMenu(controller);
        //this.browseMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.BrowseMenu(controller);
        //this.readMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.ReadMenu(controller);
        //this.displayMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.DisplayMenu(controller);
        //this.helpMenu = new cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.HelpMenu(controller);
        this.fileMenu = this.FileMenu;
        this.editMenu = this.EditMenu;
        this.browseMenu = this.BrowseMenu;
        this.readMenu = this.ReadMenu;
        this.displayMenu = this.DisplayMenu;
        this.helpMenu = this.HelpMenu;
        // 
        // mainMenuStrip
        // 
        this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.editMenu,
            this.browseMenu,
            this.readMenu,
            this.displayMenu,
            this.helpMenu
        });
        this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
        this.mainMenuStrip.Name = "mainMenuStrip";
        this.mainMenuStrip.Size = new System.Drawing.Size(744, 24);
        this.mainMenuStrip.TabIndex = 0;

        // 
        // MainForm
        // 
        //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        this.ClientSize = new System.Drawing.Size(744, 662);
        this.Controls.Add(mainMenuStrip);
        this.ResumeLayout();
    }

    private MenuStrip mainMenuStrip;
    private ToolStripMenuItem fileMenu;
    private ToolStripMenuItem editMenu;
    private ToolStripMenuItem browseMenu;
    private ToolStripMenuItem readMenu;
    private ToolStripMenuItem displayMenu;
    private ToolStripMenuItem helpMenu;
}
