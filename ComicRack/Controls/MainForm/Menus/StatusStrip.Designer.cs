using System.Windows.Forms;
using WinStatusStrip = System.Windows.Forms.StatusStrip;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

partial class StatusStrip
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
            ToolStripStatusLabel scanActivity = tsScanActivity;
            ToolStripStatusLabel readInfoActivity = tsReadInfoActivity;
            ToolStripStatusLabel writeInfoActivity = tsWriteInfoActivity;
            ToolStripStatusLabel pageActivity = tsPageActivity;
            bool flag2 = (tsExportActivity.Visible = false);
            bool flag4 = (pageActivity.Visible = flag2);
            bool flag6 = (writeInfoActivity.Visible = flag4);
            bool visible = (readInfoActivity.Visible = flag6);
            scanActivity.Visible = visible;
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

        this.statusStripItem = new System.Windows.Forms.StatusStrip();
        this.tsText = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsDeviceSyncActivity = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsExportActivity = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsReadInfoActivity = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsWriteInfoActivity = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsPageActivity = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsScanActivity = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsDataSourceState = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsBook = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsCurrentPage = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsPageCount = new System.Windows.Forms.ToolStripStatusLabel();
        this.tsServerActivity = new System.Windows.Forms.ToolStripStatusLabel();
        this.SuspendLayout();

        // 
        // statusStrip
        // 
        this.statusStripItem.AutoSize = false;
        this.statusStripItem.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.tsText,
        this.tsDeviceSyncActivity,
        this.tsExportActivity,
        this.tsReadInfoActivity,
        this.tsWriteInfoActivity,
        this.tsPageActivity,
        this.tsScanActivity,
        this.tsDataSourceState,
        this.tsBook,
        this.tsCurrentPage,
        this.tsPageCount,
        this.tsServerActivity});
        this.statusStripItem.Location = new System.Drawing.Point(0, 638);
        this.statusStripItem.MinimumSize = new System.Drawing.Size(0, 24);
        this.statusStripItem.Name = "statusStrip";
        this.statusStripItem.ShowItemToolTips = true;
        this.statusStripItem.Size = new System.Drawing.Size(744, 24);
        this.statusStripItem.TabIndex = 3;
        // 
        // tsText
        // 
        this.tsText.Name = "tsText";
        this.tsText.Size = new System.Drawing.Size(603, 19);
        this.tsText.Spring = true;
        this.tsText.Text = "Ready";
        this.tsText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // tsDeviceSyncActivity
        // 
        this.tsDeviceSyncActivity.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.tsDeviceSyncActivity.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsDeviceSyncActivity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsDeviceSyncActivity.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.DeviceSyncAnimation;
        this.tsDeviceSyncActivity.Name = "tsDeviceSyncActivity";
        this.tsDeviceSyncActivity.Size = new System.Drawing.Size(20, 19);
        this.tsDeviceSyncActivity.Text = "Exporting";
        this.tsDeviceSyncActivity.Visible = false;
        this.tsDeviceSyncActivity.Click += new System.EventHandler(this.tsDeviceSyncActivity_Click);
        // 
        // tsExportActivity
        // 
        this.tsExportActivity.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.tsExportActivity.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsExportActivity.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        this.tsExportActivity.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ExportAnimation;
        this.tsExportActivity.Name = "tsExportActivity";
        this.tsExportActivity.Size = new System.Drawing.Size(20, 19);
        this.tsExportActivity.Text = "Exporting";
        this.tsExportActivity.Visible = false;
        this.tsExportActivity.Click += new System.EventHandler(this.tsExportActivity_Click);
        // 
        // tsReadInfoActivity
        // 
        this.tsReadInfoActivity.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.tsReadInfoActivity.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsReadInfoActivity.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ReadInfoAnimation;
        this.tsReadInfoActivity.Margin = new System.Windows.Forms.Padding(2, 3, 2, 2);
        this.tsReadInfoActivity.Name = "tsReadInfoActivity";
        this.tsReadInfoActivity.Size = new System.Drawing.Size(20, 19);
        this.tsReadInfoActivity.ToolTipText = "Reading info data from files...";
        this.tsReadInfoActivity.Visible = false;
        this.tsReadInfoActivity.Click += new System.EventHandler(this.tsReadInfoActivity_Click);
        // 
        // tsWriteInfoActivity
        // 
        this.tsWriteInfoActivity.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.tsWriteInfoActivity.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsWriteInfoActivity.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.UpdateInfoAnimation;
        this.tsWriteInfoActivity.Margin = new System.Windows.Forms.Padding(2, 3, 2, 2);
        this.tsWriteInfoActivity.Name = "tsWriteInfoActivity";
        this.tsWriteInfoActivity.Size = new System.Drawing.Size(20, 19);
        this.tsWriteInfoActivity.ToolTipText = "Writing info data to files...";
        this.tsWriteInfoActivity.Visible = false;
        this.tsWriteInfoActivity.Click += new System.EventHandler(this.tsUpdateInfoActivity_Click);
        // 
        // tsPageActivity
        // 
        this.tsPageActivity.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.tsPageActivity.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsPageActivity.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ReadPagesAnimation;
        this.tsPageActivity.Name = "tsPageActivity";
        this.tsPageActivity.Size = new System.Drawing.Size(20, 19);
        this.tsPageActivity.ToolTipText = "Getting Pages and Thumbnails...";
        this.tsPageActivity.Visible = false;
        this.tsPageActivity.Click += new System.EventHandler(this.tsPageActivity_Click);
        // 
        // tsScanActivity
        // 
        this.tsScanActivity.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.tsScanActivity.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsScanActivity.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ScanAnimation;
        this.tsScanActivity.Margin = new System.Windows.Forms.Padding(2, 3, 2, 2);
        this.tsScanActivity.Name = "tsScanActivity";
        this.tsScanActivity.Size = new System.Drawing.Size(20, 19);
        this.tsScanActivity.ToolTipText = "A scan is running...";
        this.tsScanActivity.Visible = false;
        this.tsScanActivity.Click += new System.EventHandler(this.tsScanActivity_Click);
        // 
        // tsDataSourceState
        // 
        this.tsDataSourceState.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.tsDataSourceState.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsDataSourceState.Margin = new System.Windows.Forms.Padding(2, 3, 2, 2);
        this.tsDataSourceState.Name = "tsDataSourceState";
        this.tsDataSourceState.Size = new System.Drawing.Size(4, 19);
        this.tsDataSourceState.Visible = false;
        // 
        // tsBook
        // 
        this.tsBook.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.tsBook.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsBook.Name = "tsBook";
        this.tsBook.Size = new System.Drawing.Size(38, 19);
        this.tsBook.Text = "Book";
        this.tsBook.ToolTipText = "Name of the opened Book";
        // 
        // tsCurrentPage
        // 
        this.tsCurrentPage.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.tsCurrentPage.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsCurrentPage.Name = "tsCurrentPage";
        this.tsCurrentPage.Size = new System.Drawing.Size(37, 19);
        this.tsCurrentPage.Text = "Page";
        this.tsCurrentPage.ToolTipText = "Current Page of the open Book";
        this.tsCurrentPage.Click += new System.EventHandler(this.tsCurrentPage_Click);
        // 
        // tsPageCount
        // 
        this.tsPageCount.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
        this.tsPageCount.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsPageCount.Name = "tsPageCount";
        this.tsPageCount.Size = new System.Drawing.Size(51, 19);
        this.tsPageCount.Text = "0 Pages";
        this.tsPageCount.ToolTipText = "Page count of the open Book";
        // 
        // tsServerActivity
        // 
        this.tsServerActivity.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
        this.tsServerActivity.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.GrayLight;
        this.tsServerActivity.Name = "tsServerActivity";
        this.tsServerActivity.Size = new System.Drawing.Size(16, 19);
        this.tsServerActivity.Visible = false;
        this.tsServerActivity.Click += new System.EventHandler(this.tsServerActivity_Click);

        //
        // HelpMenu
        //
        //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        this.ClientSize = new System.Drawing.Size(744, 662);
        this.Controls.Add(statusStripItem);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private WinStatusStrip statusStripItem;
    private ToolStripStatusLabel tsText;
    private ToolStripStatusLabel tsDeviceSyncActivity;
    private ToolStripStatusLabel tsWriteInfoActivity;
    private ToolStripStatusLabel tsExportActivity;
    private ToolStripStatusLabel tsReadInfoActivity;

    private ToolStripStatusLabel tsPageActivity;
    private ToolStripStatusLabel tsScanActivity;
    private ToolStripStatusLabel tsDataSourceState;
    private ToolStripStatusLabel tsBook;
    private ToolStripStatusLabel tsCurrentPage;
    private ToolStripStatusLabel tsPageCount;
    private ToolStripStatusLabel tsServerActivity;
    #endregion
}
