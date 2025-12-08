using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Views;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

partial class EditMenu : UserControl
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
        this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.miShowInfo = new System.Windows.Forms.ToolStripMenuItem();

        this.miUndo = new System.Windows.Forms.ToolStripMenuItem();
        this.miRedo = new System.Windows.Forms.ToolStripMenuItem();

        this.miRating = new System.Windows.Forms.ToolStripMenuItem();
        this.contextRating = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.miRate0 = new System.Windows.Forms.ToolStripMenuItem();

        this.miRate1 = new System.Windows.Forms.ToolStripMenuItem();
        this.miRate2 = new System.Windows.Forms.ToolStripMenuItem();
        this.miRate3 = new System.Windows.Forms.ToolStripMenuItem();
        this.miRate4 = new System.Windows.Forms.ToolStripMenuItem();
        this.miRate5 = new System.Windows.Forms.ToolStripMenuItem();

        this.miQuickRating = new System.Windows.Forms.ToolStripMenuItem();
        this.miPageType = new System.Windows.Forms.ToolStripMenuItem();
        this.miPageRotate = new System.Windows.Forms.ToolStripMenuItem();
        this.miBookmarks = new System.Windows.Forms.ToolStripMenuItem();
        this.miSetBookmark = new System.Windows.Forms.ToolStripMenuItem();
        this.miRemoveBookmark = new System.Windows.Forms.ToolStripMenuItem();

        this.miPrevBookmark = new System.Windows.Forms.ToolStripMenuItem();
        this.miNextBookmark = new System.Windows.Forms.ToolStripMenuItem();

        this.miLastPageRead = new System.Windows.Forms.ToolStripMenuItem();


        this.miCopyPage = new System.Windows.Forms.ToolStripMenuItem();
        this.miExportPage = new System.Windows.Forms.ToolStripMenuItem();

        this.miViewRefresh = new System.Windows.Forms.ToolStripMenuItem();

        this.miDevices = new System.Windows.Forms.ToolStripMenuItem();
        this.miPreferences = new System.Windows.Forms.ToolStripMenuItem();
        this.miRandomFromList = new System.Windows.Forms.ToolStripMenuItem();

        this.cmPageRotate = new System.Windows.Forms.ToolStripMenuItem();

        this.cmComics = new System.Windows.Forms.ToolStripMenuItem();
        this.cmOpenComic = new System.Windows.Forms.ToolStripMenuItem();
        this.cmCloseComic = new System.Windows.Forms.ToolStripMenuItem();

        this.cmPrevFromList = new System.Windows.Forms.ToolStripMenuItem();
        this.cmNextFromList = new System.Windows.Forms.ToolStripMenuItem();
        this.cmRandomFromList = new System.Windows.Forms.ToolStripMenuItem();


        this.cmRotate0 = new System.Windows.Forms.ToolStripMenuItem();
        this.cmRotate90 = new System.Windows.Forms.ToolStripMenuItem();
        this.cmRotate180 = new System.Windows.Forms.ToolStripMenuItem();
        this.cmRotate270 = new System.Windows.Forms.ToolStripMenuItem();

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

        this.tsSeparatorBms = new System.Windows.Forms.ToolStripSeparator();
        this.SuspendLayout();

        // 
        // editMenu
        // 
        this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miShowInfo,
        this.tsSeparator1,
        this.miUndo,
        this.miRedo,
        this.tsSeparator2,
        this.miRating,
        this.miPageType,
        this.miPageRotate,
        this.miBookmarks,
        this.tsSeparator3,
        this.miCopyPage,
        this.miExportPage,
        this.tsSeparator4,
        this.miViewRefresh,
        this.tsSeparator5,
        this.miDevices,
        this.miPreferences});
        this.editMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
        this.editMenuItem.Name = "editMenu";
        this.editMenuItem.Size = new System.Drawing.Size(39, 20);
        this.editMenuItem.Text = "&Edit";
        this.editMenuItem.DropDownOpening += new System.EventHandler(this.editMenu_DropDownOpening);
        // 
        // miShowInfo
        // 
        this.miShowInfo.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GetInfo;
        this.miShowInfo.Name = "miShowInfo";
        this.miShowInfo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
        this.miShowInfo.Size = new System.Drawing.Size(220, 22);
        this.miShowInfo.Text = "Info...";
        // 
        // miUndo
        // 
        this.miUndo.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Undo;
        this.miUndo.Name = "miUndo";
        this.miUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
        this.miUndo.Size = new System.Drawing.Size(220, 22);
        this.miUndo.Text = "&Undo";
        // 
        // miRedo
        // 
        this.miRedo.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Redo;
        this.miRedo.Name = "miRedo";
        this.miRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
        this.miRedo.Size = new System.Drawing.Size(220, 22);
        this.miRedo.Text = "&Redo";
        // 
        // miRating
        // 
        this.miRating.DropDown = this.contextRating;
        this.miRating.Name = "miRating";
        this.miRating.Size = new System.Drawing.Size(220, 22);
        this.miRating.Text = "My R&ating";
        // 
        // miRate1
        // 
        this.miRate1.Name = "miRate1";
        this.miRate1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D1)));
        this.miRate1.Size = new System.Drawing.Size(285, 22);
        this.miRate1.Text = "* (1 Star)";
        // 
        // miRate2
        // 
        this.miRate2.Name = "miRate2";
        this.miRate2.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D2)));
        this.miRate2.Size = new System.Drawing.Size(285, 22);
        this.miRate2.Text = "** (2 Stars)";
        // 
        // miRate3
        // 
        this.miRate3.Name = "miRate3";
        this.miRate3.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D3)));
        this.miRate3.Size = new System.Drawing.Size(285, 22);
        this.miRate3.Text = "*** (3 Stars)";
        // 
        // miRate4
        // 
        this.miRate4.Name = "miRate4";
        this.miRate4.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D4)));
        this.miRate4.Size = new System.Drawing.Size(285, 22);
        this.miRate4.Text = "**** (4 Stars)";
        // 
        // miRate5
        // 
        this.miRate5.Name = "miRate5";
        this.miRate5.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D5)));
        this.miRate5.Size = new System.Drawing.Size(285, 22);
        this.miRate5.Text = "***** (5 Stars)";
        // 
        // miQuickRating
        // 
        this.miQuickRating.Name = "miQuickRating";
        this.miQuickRating.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.Q)));
        this.miQuickRating.Size = new System.Drawing.Size(285, 22);
        this.miQuickRating.Text = "Quick Rating and Review...";
        // 
        // miPageType
        // 
        this.miPageType.Name = "miPageType";
        this.miPageType.Size = new System.Drawing.Size(220, 22);
        this.miPageType.Text = "&Page Type";
        // 
        // miPageRotate
        // 
        this.miPageRotate.Name = "miPageRotate";
        this.miPageRotate.Size = new System.Drawing.Size(220, 22);
        this.miPageRotate.Text = "Page Rotation";
        // 
        // miBookmarks
        // 
        this.miBookmarks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miSetBookmark,
        this.miRemoveBookmark,
        this.tsSeparator6,
        this.miPrevBookmark,
        this.miNextBookmark,
        this.tsSeparator7,
        this.miLastPageRead,
        this.tsSeparatorBms});
        this.miBookmarks.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Bookmark;
        this.miBookmarks.Name = "miBookmarks";
        this.miBookmarks.Size = new System.Drawing.Size(220, 22);
        this.miBookmarks.Text = "&Bookmarks";
        this.miBookmarks.DropDownOpening += new System.EventHandler(this.miBookmarks_DropDownOpening);
        // 
        // miSetBookmark
        // 
        this.miSetBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.NewBookmark;
        this.miSetBookmark.Name = "miSetBookmark";
        this.miSetBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.B)));
        this.miSetBookmark.Size = new System.Drawing.Size(249, 22);
        this.miSetBookmark.Text = "Set Bookmark...";
        // 
        // miRemoveBookmark
        // 
        this.miRemoveBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RemoveBookmark;
        this.miRemoveBookmark.Name = "miRemoveBookmark";
        this.miRemoveBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D)));
        this.miRemoveBookmark.Size = new System.Drawing.Size(249, 22);
        this.miRemoveBookmark.Text = "Remove Bookmark";
        // 
        // miPrevBookmark
        // 
        this.miPrevBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.PreviousBookmark;
        this.miPrevBookmark.Name = "miPrevBookmark";
        this.miPrevBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.P)));
        this.miPrevBookmark.Size = new System.Drawing.Size(249, 22);
        this.miPrevBookmark.Text = "Previous Bookmark";
        // 
        // miNextBookmark
        // 
        this.miNextBookmark.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.NextBookmark;
        this.miNextBookmark.Name = "miNextBookmark";
        this.miNextBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.N)));
        this.miNextBookmark.Size = new System.Drawing.Size(249, 22);
        this.miNextBookmark.Text = "Next Bookmark";
        // 
        // miLastPageRead
        // 
        this.miLastPageRead.Name = "miLastPageRead";
        this.miLastPageRead.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.L)));
        this.miLastPageRead.Size = new System.Drawing.Size(249, 22);
        this.miLastPageRead.Text = "L&ast Page Read";
        // 
        // miCopyPage
        // 
        this.miCopyPage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Copy;
        this.miCopyPage.Name = "miCopyPage";
        this.miCopyPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
        this.miCopyPage.Size = new System.Drawing.Size(220, 22);
        this.miCopyPage.Text = "&Copy Page";
        // 
        // miExportPage
        // 
        this.miExportPage.Name = "miExportPage";
        this.miExportPage.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.C)));
        this.miExportPage.Size = new System.Drawing.Size(220, 22);
        this.miExportPage.Text = "&Export Page...";
        // 
        // miViewRefresh
        // 
        this.miViewRefresh.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Refresh;
        this.miViewRefresh.Name = "miViewRefresh";
        this.miViewRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
        this.miViewRefresh.Size = new System.Drawing.Size(220, 22);
        this.miViewRefresh.Text = "&Refresh";
        // 
        // miDevices
        // 
        this.miDevices.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.EditDevices;
        this.miDevices.Name = "miDevices";
        this.miDevices.Size = new System.Drawing.Size(220, 22);
        this.miDevices.Text = "Devices...";
        // 
        // miPreferences
        // 
        this.miPreferences.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Preferences;
        this.miPreferences.Name = "miPreferences";
        this.miPreferences.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F9)));
        this.miPreferences.Size = new System.Drawing.Size(220, 22);
        this.miPreferences.Text = "&Preferences...";
        // 
        // contextRating
        // 
        this.contextRating.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miRate0,
        this.tsSeparator8,
        this.miRate1,
        this.miRate2,
        this.miRate3,
        this.miRate4,
        this.miRate5,
        this.tsSeparator9,
        this.miQuickRating});
        this.contextRating.Name = "contextRating";
        this.contextRating.OwnerItem = this.miRating;
        this.contextRating.Size = new System.Drawing.Size(286, 170);
        // 
        // miRate0
        // 
        this.miRate0.Name = "miRate0";
        this.miRate0.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D0)));
        this.miRate0.Size = new System.Drawing.Size(285, 22);
        this.miRate0.Text = "None";
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
        // tsSeparatorBms
        // 
        this.tsSeparatorBms.Name = "tsSeparatorBms";
        this.tsSeparatorBms.Size = new System.Drawing.Size(246, 6);
        this.tsSeparatorBms.Tag = "bms";

        // 
        // menu
        // 
        this.menu.Items.Add(editMenuItem);
        this.menu.Location = new System.Drawing.Point(0, 0);
        this.menu.Name = "menu";
        this.menu.Size = new System.Drawing.Size(744, 24);
        this.menu.TabIndex = 0;

        //
        // HelpMenu
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
    private ToolStripMenuItem editMenuItem;
    private ToolStripMenuItem miShowInfo;
    private ToolStripMenuItem miRating;
    private ToolStripMenuItem miPreferences;
    private ToolStripMenuItem miViewRefresh;

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
    private ToolStripSeparator tsSeparatorBms;

    private ToolStripMenuItem miPageType;
    private ToolStripMenuItem miBookmarks;
    private ToolStripMenuItem miSetBookmark;
    private ToolStripMenuItem miRemoveBookmark;

    private ToolStripMenuItem miPrevBookmark;
    private ToolStripMenuItem miNextBookmark;

    private ToolStripMenuItem miLastPageRead;
    private ToolStripMenuItem miCopyPage;
    private ToolStripMenuItem miExportPage;
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
    private ToolStripMenuItem miPageRotate;

    private ToolStripMenuItem miRandomFromList;
    private ToolStripMenuItem cmRandomFromList;
    private ToolStripMenuItem miUpdateAllComicFiles;

    private ToolStripMenuItem miUndo;
    private ToolStripMenuItem miRedo;
    private ContextMenuStrip contextRating;
    private ToolStripMenuItem miRate0;
    private ToolStripMenuItem miRate1;
    private ToolStripMenuItem miRate2;
    private ToolStripMenuItem miRate3;
    private ToolStripMenuItem miRate4;
    private ToolStripMenuItem miRate5;
    private ToolStripMenuItem miDevices;
    private ToolStripMenuItem miQuickRating;
    #endregion
}
