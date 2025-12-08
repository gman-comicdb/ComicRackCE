using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

partial class DisplayMenu
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
        this.displayMenuItem = new System.Windows.Forms.ToolStripMenuItem();

        this.miComicDisplaySettings = new System.Windows.Forms.ToolStripMenuItem();

        this.miPageLayout = new System.Windows.Forms.ToolStripMenuItem();
        this.miOriginal = new System.Windows.Forms.ToolStripMenuItem();
        this.miFitAll = new System.Windows.Forms.ToolStripMenuItem();
        this.miFitWidth = new System.Windows.Forms.ToolStripMenuItem();
        this.miFitWidthAdaptive = new System.Windows.Forms.ToolStripMenuItem();
        this.miFitHeight = new System.Windows.Forms.ToolStripMenuItem();
        this.miBestFit = new System.Windows.Forms.ToolStripMenuItem();

        this.miSinglePage = new System.Windows.Forms.ToolStripMenuItem();
        this.miTwoPages = new System.Windows.Forms.ToolStripMenuItem();
        this.miTwoPagesAdaptive = new System.Windows.Forms.ToolStripMenuItem();
        this.miRightToLeft = new System.Windows.Forms.ToolStripMenuItem();

        this.miOnlyFitOversized = new System.Windows.Forms.ToolStripMenuItem();
        this.miZoom = new System.Windows.Forms.ToolStripMenuItem();
        this.miZoomIn = new System.Windows.Forms.ToolStripMenuItem();
        this.miZoomOut = new System.Windows.Forms.ToolStripMenuItem();
        this.miToggleZoom = new System.Windows.Forms.ToolStripMenuItem();

        this.miZoom100 = new System.Windows.Forms.ToolStripMenuItem();
        this.miZoom125 = new System.Windows.Forms.ToolStripMenuItem();
        this.miZoom150 = new System.Windows.Forms.ToolStripMenuItem();
        this.miZoom200 = new System.Windows.Forms.ToolStripMenuItem();
        this.miZoom400 = new System.Windows.Forms.ToolStripMenuItem();

        this.miZoomCustom = new System.Windows.Forms.ToolStripMenuItem();
        this.miRotation = new System.Windows.Forms.ToolStripMenuItem();
        this.miRotateLeft = new System.Windows.Forms.ToolStripMenuItem();
        this.miRotateRight = new System.Windows.Forms.ToolStripMenuItem();

        this.miRotate0 = new System.Windows.Forms.ToolStripMenuItem();
        this.miRotate90 = new System.Windows.Forms.ToolStripMenuItem();
        this.miRotate180 = new System.Windows.Forms.ToolStripMenuItem();
        this.miRotate270 = new System.Windows.Forms.ToolStripMenuItem();

        this.miAutoRotate = new System.Windows.Forms.ToolStripMenuItem();

        this.miMinimalGui = new System.Windows.Forms.ToolStripMenuItem();
        this.miFullScreen = new System.Windows.Forms.ToolStripMenuItem();
        this.miReaderUndocked = new System.Windows.Forms.ToolStripMenuItem();

        this.miMagnify = new System.Windows.Forms.ToolStripMenuItem();

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
        // displayMenu
        // 
        this.displayMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miComicDisplaySettings,
        this.tsSeparator1,
        this.miPageLayout,
        this.miZoom,
        this.miRotation,
        this.tsSeparator2,
        this.miMinimalGui,
        this.miFullScreen,
        this.miReaderUndocked,
        this.tsSeparator3,
        this.miMagnify});
        this.displayMenuItem.ImageTransparentColor = System.Drawing.Color.Fuchsia;
        this.displayMenuItem.Name = "displayMenu";
        this.displayMenuItem.Size = new System.Drawing.Size(57, 20);
        this.displayMenuItem.Text = "&Display";
        // 
        // miComicDisplaySettings
        // 
        this.miComicDisplaySettings.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.DisplaySettings;
        this.miComicDisplaySettings.Name = "miComicDisplaySettings";
        this.miComicDisplaySettings.ShortcutKeys = System.Windows.Forms.Keys.F9;
        this.miComicDisplaySettings.Size = new System.Drawing.Size(221, 22);
        this.miComicDisplaySettings.Text = "Book Display Settings...";
        // 
        // tsSeparator1
        // 
        this.tsSeparator1.Name = "tsSeparator1";
        this.tsSeparator1.Size = new System.Drawing.Size(218, 6);
        // 
        // miPageLayout
        // 
        this.miPageLayout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miOriginal,
        this.miFitAll,
        this.miFitWidth,
        this.miFitWidthAdaptive,
        this.miFitHeight,
        this.miBestFit,
        this.tsSeparator4,
        this.miSinglePage,
        this.miTwoPages,
        this.miTwoPagesAdaptive,
        this.miRightToLeft,
        this.tsSeparator5,
        this.miOnlyFitOversized});
        this.miPageLayout.Name = "miPageLayout";
        this.miPageLayout.Size = new System.Drawing.Size(221, 22);
        this.miPageLayout.Text = "&Page Layout";
        // 
        // miOriginal
        // 
        this.miOriginal.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Original;
        this.miOriginal.Name = "miOriginal";
        this.miOriginal.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
        this.miOriginal.Size = new System.Drawing.Size(247, 22);
        this.miOriginal.Text = "Original Size";
        // 
        // miFitAll
        // 
        this.miFitAll.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitAll;
        this.miFitAll.Name = "miFitAll";
        this.miFitAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
        this.miFitAll.Size = new System.Drawing.Size(247, 22);
        this.miFitAll.Text = "Fit &All";
        // 
        // miFitWidth
        // 
        this.miFitWidth.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitWidth;
        this.miFitWidth.Name = "miFitWidth";
        this.miFitWidth.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
        this.miFitWidth.Size = new System.Drawing.Size(247, 22);
        this.miFitWidth.Text = "Fit &Width";
        // 
        // miFitWidthAdaptive
        // 
        this.miFitWidthAdaptive.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitWidthAdaptive;
        this.miFitWidthAdaptive.Name = "miFitWidthAdaptive";
        this.miFitWidthAdaptive.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
        this.miFitWidthAdaptive.Size = new System.Drawing.Size(247, 22);
        this.miFitWidthAdaptive.Text = "Fit Width (adaptive)";
        // 
        // miFitHeight
        // 
        this.miFitHeight.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitHeight;
        this.miFitHeight.Name = "miFitHeight";
        this.miFitHeight.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
        this.miFitHeight.Size = new System.Drawing.Size(247, 22);
        this.miFitHeight.Text = "Fit &Height";
        // 
        // miBestFit
        // 
        this.miBestFit.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FitBest;
        this.miBestFit.Name = "miBestFit";
        this.miBestFit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
        this.miBestFit.Size = new System.Drawing.Size(247, 22);
        this.miBestFit.Text = "Fit &Best";
        // 
        // tsSeparator4
        // 
        this.tsSeparator4.Name = "toolStripMenuItem27";
        this.tsSeparator4.Size = new System.Drawing.Size(244, 6);
        // 
        // miSinglePage
        // 
        this.miSinglePage.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.SinglePage;
        this.miSinglePage.Name = "miSinglePage";
        this.miSinglePage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D7)));
        this.miSinglePage.Size = new System.Drawing.Size(247, 22);
        this.miSinglePage.Text = "Single Page";
        // 
        // miTwoPages
        // 
        this.miTwoPages.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.TwoPageForced;
        this.miTwoPages.Name = "miTwoPages";
        this.miTwoPages.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D8)));
        this.miTwoPages.Size = new System.Drawing.Size(247, 22);
        this.miTwoPages.Text = "Two Pages";
        // 
        // miTwoPagesAdaptive
        // 
        this.miTwoPagesAdaptive.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.TwoPage;
        this.miTwoPagesAdaptive.Name = "miTwoPagesAdaptive";
        this.miTwoPagesAdaptive.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D9)));
        this.miTwoPagesAdaptive.Size = new System.Drawing.Size(247, 22);
        this.miTwoPagesAdaptive.Text = "Two Pages (adaptive)";
        this.miTwoPagesAdaptive.ToolTipText = "Show one or two pages";
        // 
        // miRightToLeft
        // 
        this.miRightToLeft.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RightToLeft;
        this.miRightToLeft.Name = "miRightToLeft";
        this.miRightToLeft.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)));
        this.miRightToLeft.Size = new System.Drawing.Size(247, 22);
        this.miRightToLeft.Text = "Right to Left";
        // 
        // tsSeparator5
        // 
        this.tsSeparator5.Name = "tsSeparator5";
        this.tsSeparator5.Size = new System.Drawing.Size(244, 6);
        // 
        // miOnlyFitOversized
        // 
        this.miOnlyFitOversized.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Oversized;
        this.miOnlyFitOversized.Name = "miOnlyFitOversized";
        this.miOnlyFitOversized.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D0)));
        this.miOnlyFitOversized.Size = new System.Drawing.Size(247, 22);
        this.miOnlyFitOversized.Text = "&Only fit if oversized";
        // 
        // miZoom
        // 
        this.miZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miZoomIn,
        this.miZoomOut,
        this.miToggleZoom,
        this.tsSeparator6,
        this.miZoom100,
        this.miZoom125,
        this.miZoom150,
        this.miZoom200,
        this.miZoom400,
        this.tsSeparator7,
        this.miZoomCustom});
        this.miZoom.Name = "miZoom";
        this.miZoom.Size = new System.Drawing.Size(221, 22);
        this.miZoom.Text = "Zoom";
        // 
        // miZoomIn
        // 
        this.miZoomIn.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ZoomIn;
        this.miZoomIn.Name = "miZoomIn";
        this.miZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemplus)));
        this.miZoomIn.Size = new System.Drawing.Size(222, 22);
        this.miZoomIn.Text = "Zoom &In";
        // 
        // miZoomOut
        // 
        this.miZoomOut.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.ZoomOut;
        this.miZoomOut.Name = "miZoomOut";
        this.miZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.OemMinus)));
        this.miZoomOut.Size = new System.Drawing.Size(222, 22);
        this.miZoomOut.Text = "Zoom &Out";
        // 
        // miToggleZoom
        // 
        this.miToggleZoom.Name = "miToggleZoom";
        this.miToggleZoom.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
        | System.Windows.Forms.Keys.Z)));
        this.miToggleZoom.Size = new System.Drawing.Size(222, 22);
        this.miToggleZoom.Text = "Toggle Zoom";
        // 
        // tsSeparator6
        // 
        this.tsSeparator6.Name = "tsSeparator6";
        this.tsSeparator6.Size = new System.Drawing.Size(219, 6);
        // 
        // miZoom100
        // 
        this.miZoom100.Name = "miZoom100";
        this.miZoom100.Size = new System.Drawing.Size(222, 22);
        this.miZoom100.Text = "100%";
        // 
        // miZoom125
        // 
        this.miZoom125.Name = "miZoom125";
        this.miZoom125.Size = new System.Drawing.Size(222, 22);
        this.miZoom125.Text = "125%";
        // 
        // miZoom150
        // 
        this.miZoom150.Name = "miZoom150";
        this.miZoom150.Size = new System.Drawing.Size(222, 22);
        this.miZoom150.Text = "150%";
        // 
        // miZoom200
        // 
        this.miZoom200.Name = "miZoom200";
        this.miZoom200.Size = new System.Drawing.Size(222, 22);
        this.miZoom200.Text = "200%";
        // 
        // miZoom400
        // 
        this.miZoom400.Name = "miZoom400";
        this.miZoom400.Size = new System.Drawing.Size(222, 22);
        this.miZoom400.Text = "400%";
        // 
        // tsSeparator7
        // 
        this.tsSeparator7.Name = "tsSeparator7";
        this.tsSeparator7.Size = new System.Drawing.Size(219, 6);
        // 
        // miZoomCustom
        // 
        this.miZoomCustom.Name = "miZoomCustom";
        this.miZoomCustom.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.Z)));
        this.miZoomCustom.Size = new System.Drawing.Size(222, 22);
        this.miZoomCustom.Text = "&Custom...";
        // 
        // miRotation
        // 
        this.miRotation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miRotateLeft,
        this.miRotateRight,
        this.tsSeparator8,
        this.miRotate0,
        this.miRotate90,
        this.miRotate180,
        this.miRotate270,
        this.tsSeparator9,
        this.miAutoRotate});
        this.miRotation.Name = "miRotation";
        this.miRotation.Size = new System.Drawing.Size(221, 22);
        this.miRotation.Text = "&Rotation";
        // 
        // miRotateLeft
        // 
        this.miRotateLeft.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RotateLeft;
        this.miRotateLeft.Name = "miRotateLeft";
        this.miRotateLeft.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.OemMinus)));
        this.miRotateLeft.Size = new System.Drawing.Size(256, 22);
        this.miRotateLeft.Text = "Rotate Left";
        // 
        // miRotateRight
        // 
        this.miRotateRight.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.RotateRight;
        this.miRotateRight.Name = "miRotateRight";
        this.miRotateRight.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.Oemplus)));
        this.miRotateRight.Size = new System.Drawing.Size(256, 22);
        this.miRotateRight.Text = "Rotate Right";
        // 
        // tsSeparator8
        // 
        this.tsSeparator8.Name = "tsSeparator8";
        this.tsSeparator8.Size = new System.Drawing.Size(253, 6);
        // 
        // miRotate0
        // 
        this.miRotate0.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate0;
        this.miRotate0.Name = "miRotate0";
        this.miRotate0.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D7)));
        this.miRotate0.Size = new System.Drawing.Size(256, 22);
        this.miRotate0.Text = "&No Rotation";
        // 
        // miRotate90
        // 
        this.miRotate90.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate90;
        this.miRotate90.Name = "miRotate90";
        this.miRotate90.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D8)));
        this.miRotate90.Size = new System.Drawing.Size(256, 22);
        this.miRotate90.Text = "90°";
        // 
        // miRotate180
        // 
        this.miRotate180.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate180;
        this.miRotate180.Name = "miRotate180";
        this.miRotate180.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D9)));
        this.miRotate180.Size = new System.Drawing.Size(256, 22);
        this.miRotate180.Text = "180°";
        // 
        // miRotate270
        // 
        this.miRotate270.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Rotate270;
        this.miRotate270.Name = "miRotate270";
        this.miRotate270.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
        | System.Windows.Forms.Keys.D0)));
        this.miRotate270.Size = new System.Drawing.Size(256, 22);
        this.miRotate270.Text = "270°";
        // 
        // tsSeparator9
        // 
        this.tsSeparator9.Name = "tsSeparator9";
        this.tsSeparator9.Size = new System.Drawing.Size(253, 6);
        // 
        // miAutoRotate
        // 
        this.miAutoRotate.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.AutoRotate;
        this.miAutoRotate.Name = "miAutoRotate";
        this.miAutoRotate.Size = new System.Drawing.Size(256, 22);
        this.miAutoRotate.Text = "Autorotate Double Pages";
        // 
        // tsSeparator2
        // 
        this.tsSeparator2.Name = "tsSeparator2";
        this.tsSeparator2.Size = new System.Drawing.Size(218, 6);
        // 
        // miMinimalGui
        // 
        this.miMinimalGui.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.MenuToggle;
        this.miMinimalGui.Name = "miMinimalGui";
        this.miMinimalGui.ShortcutKeys = System.Windows.Forms.Keys.F10;
        this.miMinimalGui.Size = new System.Drawing.Size(221, 22);
        this.miMinimalGui.Text = "Minimal User Interface";
        // 
        // miFullScreen
        // 
        this.miFullScreen.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.FullScreen;
        this.miFullScreen.Name = "miFullScreen";
        this.miFullScreen.ShortcutKeyDisplayString = "";
        this.miFullScreen.ShortcutKeys = System.Windows.Forms.Keys.F11;
        this.miFullScreen.Size = new System.Drawing.Size(221, 22);
        this.miFullScreen.Text = "&Full Screen";
        // 
        // miReaderUndocked
        // 
        this.miReaderUndocked.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.UndockReader;
        this.miReaderUndocked.Name = "miReaderUndocked";
        this.miReaderUndocked.ShortcutKeys = System.Windows.Forms.Keys.F12;
        this.miReaderUndocked.Size = new System.Drawing.Size(221, 22);
        this.miReaderUndocked.Text = "Reader in &own Window";
        // 
        // tsSeparator3
        // 
        this.tsSeparator3.Name = "tsSeparator3";
        this.tsSeparator3.Size = new System.Drawing.Size(218, 6);
        // 
        // miMagnify
        // 
        this.miMagnify.Image = global::cYo.Projects.ComicRack.Viewer.Properties.Resources.Zoom;
        this.miMagnify.Name = "miMagnify";
        this.miMagnify.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
        this.miMagnify.Size = new System.Drawing.Size(221, 22);
        this.miMagnify.Text = "&Magnifier";

        // 
        // menu
        // 
        this.menu.Items.Add(displayMenuItem);
        this.menu.Location = new System.Drawing.Point(0, 0);
        this.menu.Name = "menu";
        this.menu.Size = new System.Drawing.Size(744, 24);
        this.menu.TabIndex = 0;

        //
        // DisplayMenu
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
    private ToolStripMenuItem displayMenuItem;
    private ToolStripMenuItem miComicDisplaySettings;
    private ToolStripMenuItem miPageLayout;
    private ToolStripMenuItem miFitAll;
    private ToolStripMenuItem miFitWidth;
    private ToolStripMenuItem miFitHeight;
    private ToolStripMenuItem miBestFit;
    private ToolStripMenuItem miFitWidthAdaptive;
    private ToolStripMenuItem miOriginal;
    private ToolStripMenuItem miTwoPagesAdaptive;
    private ToolStripMenuItem miTwoPages;
    private ToolStripMenuItem miSinglePage;
    private ToolStripMenuItem miZoom;
    private ToolStripMenuItem miRotation;
    private ToolStripMenuItem miRotate0;
    private ToolStripMenuItem miRotate90;
    private ToolStripMenuItem miRotate180;
    private ToolStripMenuItem miRotate270;
    private ToolStripMenuItem miRightToLeft;
    private ToolStripMenuItem miOnlyFitOversized;

    private ToolStripMenuItem miZoomIn;
    private ToolStripMenuItem miZoomOut;
    private ToolStripMenuItem miToggleZoom;

    private ToolStripMenuItem miZoom100;
    private ToolStripMenuItem miZoom125;
    private ToolStripMenuItem miZoom150;
    private ToolStripMenuItem miZoom200;
    private ToolStripMenuItem miZoom400;
    private ToolStripMenuItem miZoomCustom;
    private ToolStripMenuItem miRotateLeft;
    private ToolStripMenuItem miRotateRight;
    private ToolStripMenuItem miAutoRotate;

    private ToolStripMenuItem miMinimalGui;
    private ToolStripMenuItem miFullScreen;
    private ToolStripMenuItem miReaderUndocked;

    private ToolStripMenuItem miMagnify;

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
