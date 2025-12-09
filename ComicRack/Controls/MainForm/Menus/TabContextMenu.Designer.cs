using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

partial class TabContextMenu
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

        this.tabContextMenuItem = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.cmClose = new System.Windows.Forms.ToolStripMenuItem();
        this.cmCloseAllButThis = new System.Windows.Forms.ToolStripMenuItem();
        this.cmCloseAllToTheRight = new System.Windows.Forms.ToolStripMenuItem();
        this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.cmSyncBrowser = new System.Windows.Forms.ToolStripMenuItem();
        this.tsSeparatorReveal = new System.Windows.Forms.ToolStripSeparator();
        this.cmRevealInExplorer = new System.Windows.Forms.ToolStripMenuItem();
        this.cmCopyPath = new System.Windows.Forms.ToolStripMenuItem();
        this.SuspendLayout();

        // 
        // tabContextMenu
        // 
        this.tabContextMenuItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.cmClose,
        this.cmCloseAllButThis,
        this.cmCloseAllToTheRight,
        this.tsSeparator1,
        this.cmSyncBrowser,
        this.tsSeparatorReveal,
        this.cmRevealInExplorer,
        this.cmCopyPath});
        this.tabContextMenuItem.Name = "tabContextMenu";
        this.tabContextMenuItem.Size = new System.Drawing.Size(221, 148);
        // 
        // cmClose
        // 
        this.cmClose.Name = "cmClose";
        this.cmClose.Size = new System.Drawing.Size(220, 22);
        this.cmClose.Text = "Close";
        // 
        // cmCloseAllButThis
        // 
        this.cmCloseAllButThis.Name = "cmCloseAllButThis";
        this.cmCloseAllButThis.Size = new System.Drawing.Size(220, 22);
        this.cmCloseAllButThis.Text = "Close All But This";
        // 
        // cmCloseAllToTheRight
        // 
        this.cmCloseAllToTheRight.Name = "cmCloseAllToTheRight";
        this.cmCloseAllToTheRight.Size = new System.Drawing.Size(220, 22);
        this.cmCloseAllToTheRight.Text = "Close All to the Right";
        // 
        // tsSeparator1
        // 
        this.tsSeparator1.Name = "toolStripMenuItem35";
        this.tsSeparator1.Size = new System.Drawing.Size(217, 6);
        // 
        // cmSyncBrowser
        // 
        this.cmSyncBrowser.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.SyncBrowser;
        this.cmSyncBrowser.Name = "cmSyncBrowser";
        this.cmSyncBrowser.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F3)));
        this.cmSyncBrowser.Size = new System.Drawing.Size(220, 22);
        this.cmSyncBrowser.Text = "Show in &Browser";
        // 
        // tsSeparator2
        // 
        this.tsSeparatorReveal.Name = "tsSeparatorReveal";
        this.tsSeparatorReveal.Size = new System.Drawing.Size(217, 6);
        // 
        // cmRevealInExplorer
        // 
        this.cmRevealInExplorer.Name = "cmRevealInExplorer";
        this.cmRevealInExplorer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
        this.cmRevealInExplorer.Size = new System.Drawing.Size(220, 22);
        this.cmRevealInExplorer.Text = "Reveal in Explorer";
        // 
        // cmCopyPath
        // 
        this.cmCopyPath.Name = "cmCopyPath";
        this.cmCopyPath.Size = new System.Drawing.Size(220, 22);
        this.cmCopyPath.Text = "Copy Full Path to Clipboard";

        //
        // TabContextMenu
        //
        //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        this.ClientSize = new System.Drawing.Size(744, 662);
        this.ResumeLayout();
    }

    private ContextMenuStrip tabContextMenuItem;
    
    private ToolStripMenuItem cmClose;
    private ToolStripMenuItem cmCloseAllButThis;
    private ToolStripSeparator tsSeparator1;
    private ToolStripMenuItem cmSyncBrowser;
    private ToolStripSeparator tsSeparatorReveal;
    private ToolStripMenuItem cmCloseAllToTheRight;
    private ToolStripMenuItem cmRevealInExplorer;
    private ToolStripMenuItem cmCopyPath;

    #endregion
}
