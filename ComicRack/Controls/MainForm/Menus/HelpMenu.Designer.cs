using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

partial class HelpMenu
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
        this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
        this.miWebHelp = new System.Windows.Forms.ToolStripMenuItem();
        this.miHelpPlugins = new System.Windows.Forms.ToolStripMenuItem();
        this.miChooseHelpSystem = new System.Windows.Forms.ToolStripMenuItem();
        this.miHelpQuickIntro = new System.Windows.Forms.ToolStripMenuItem();
        this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.miWebHome = new System.Windows.Forms.ToolStripMenuItem();
        this.miWebUserForum = new System.Windows.Forms.ToolStripMenuItem();
        this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        this.miNews = new System.Windows.Forms.ToolStripMenuItem();
        this.tsSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
        this.miCheckUpdate = new System.Windows.Forms.ToolStripMenuItem();
        this.SuspendLayout();

        // 
        // helpMenu
        // 
        this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miHelp,
        this.miWebHelp,
        this.miHelpPlugins,
        this.miChooseHelpSystem,
        this.miHelpQuickIntro,
        this.tsSeparator1,
        this.miWebHome,
        this.miWebUserForum,
        this.tsSeparator2,
        this.miNews,
        this.miCheckUpdate,
        this.tsSeparator3,
        this.miAbout});
        this.helpMenuItem.Name = "helpMenu";
        this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
        this.helpMenuItem.Text = "&Help";
        // 
        // miHelp
        // 
        this.miHelp.Name = "miHelp";
        this.miHelp.Size = new System.Drawing.Size(256, 22);
        this.miHelp.Text = "Help";
        this.miHelp.Visible = false;
        // 
        // miWebHelp
        // 
        this.miWebHelp.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Help;
        this.miWebHelp.Name = "miWebHelp";
        this.miWebHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
        this.miWebHelp.Size = new System.Drawing.Size(256, 22);
        this.miWebHelp.Text = "ComicRack Documentation...";
        // 
        // miHelpPlugins
        // 
        this.miHelpPlugins.Name = "miHelpPlugins";
        this.miHelpPlugins.Size = new System.Drawing.Size(256, 22);
        this.miHelpPlugins.Text = "Plugins";
        // 
        // miChooseHelpSystem
        // 
        this.miChooseHelpSystem.Name = "miChooseHelpSystem";
        this.miChooseHelpSystem.Size = new System.Drawing.Size(256, 22);
        this.miChooseHelpSystem.Text = "Choose Help System";
        // 
        // miHelpQuickIntro
        // 
        this.miHelpQuickIntro.Name = "miHelpQuickIntro";
        this.miHelpQuickIntro.Size = new System.Drawing.Size(256, 22);
        this.miHelpQuickIntro.Text = "Quick Introduction";
        // 
        // tsSeparator1
        // 
        this.tsSeparator1.Name = "tsSeparator1";
        this.tsSeparator1.Size = new System.Drawing.Size(253, 6);
        // 
        // miWebHome
        // 
        this.miWebHome.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.WebBlog;
        this.miWebHome.Name = "miWebHome";
        this.miWebHome.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F1)));
        this.miWebHome.Size = new System.Drawing.Size(256, 22);
        this.miWebHome.Text = "ComicRack Homepage...";
        // 
        // miWebUserForum
        // 
        this.miWebUserForum.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.WebForum;
        this.miWebUserForum.Name = "miWebUserForum";
        this.miWebUserForum.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
        this.miWebUserForum.Size = new System.Drawing.Size(256, 22);
        this.miWebUserForum.Text = "ComicRack User Forum...";
        // 
        // tsSeparator2
        // 
        this.tsSeparator2.Name = "tsSeparator2";
        this.tsSeparator2.Size = new System.Drawing.Size(253, 6);
        // 
        // miNews
        // 
        this.miNews.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.News;
        this.miNews.Name = "miNews";
        this.miNews.Size = new System.Drawing.Size(256, 22);
        this.miNews.Text = "&News...";
        // 
        // miCheckUpdate
        // 
        this.miCheckUpdate.Name = "miCheckUpdate";
        this.miCheckUpdate.Size = new System.Drawing.Size(256, 22);
        this.miCheckUpdate.Text = "Check For Update...";
        // 
        // tsSeparator3
        // 
        this.tsSeparator3.Name = "tsSeparator3";
        this.tsSeparator3.Size = new System.Drawing.Size(253, 6);
        // 
        // miAbout
        // 
        this.miAbout.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.About;
        this.miAbout.Name = "miAbout";
        this.miAbout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F1)));
        this.miAbout.Size = new System.Drawing.Size(256, 22);
        this.miAbout.Text = "&About...";

        // 
        // menu
        // 
        this.menu.Items.Add(helpMenuItem);
        this.menu.Location = new System.Drawing.Point(0, 0);
        this.menu.Name = "menu";
        this.menu.Size = new System.Drawing.Size(744, 24);
        this.menu.TabIndex = 0;

        //
        // HelpMenu
        //
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        this.ClientSize = new System.Drawing.Size(744, 662);
        this.menu.Items.Add(helpMenuItem);
        this.Controls.Add(menu);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private MenuStrip menu;
    private ToolStripMenuItem helpMenuItem;

    private ToolStripMenuItem miWebHome;
    private ToolStripMenuItem miWebUserForum;
    private ToolStripMenuItem miHelp;
    private ToolStripMenuItem miChooseHelpSystem;
    private ToolStripMenuItem miWebHelp;
    private ToolStripMenuItem miHelpPlugins;
    private ToolStripMenuItem miHelpQuickIntro;
    private ToolStripMenuItem miNews;
    private ToolStripMenuItem miCheckUpdate;
    private ToolStripMenuItem miAbout;

    private ToolStripSeparator tsSeparator1;
    private ToolStripSeparator tsSeparator2;
    private ToolStripSeparator tsSeparator3;
    #endregion
}
