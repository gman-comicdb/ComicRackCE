using cYo.Common.Localize;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Controls;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class MainToolStrip : UserControl
{
    public MainToolStrip()
    {
        //this.controller = controller;
        InitializeComponent();

        tbPrevPage.DropDownOpening += MainController.EventHandlers.OnToolStripPrevPageDropDownOpening;
        tbNextPage.DropDownOpening += MainController.EventHandlers.OnToolStripNextPageDropDownOpening;
        tbBookmarks.DropDownOpening += MainController.EventHandlers.OnToolStripBookmarksDropDownOpening;

        tbTools.DropDownOpening += OnToolStripToolsDropDownOpening;
        tbTools.DropDownClosed += OnToolStripToolsDropDownClosed;
    }

    public static implicit operator ToolStrip(MainToolStrip menu)
        => menu.mainToolStripItem;

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        tbMagnify.DropDown = GetMagnifierDropDown();
    }

    public void OnToolStripToolsDropDownOpening(object sender, EventArgs e)
    {
        tbUpdateWebComics.Visible = Program.Database.Books.FirstOrDefault((ComicBook cb) => cb.IsDynamicSource) != null;
        mainToolStripItem.DefaultDropDownDirection = ToolStripDropDownDirection.BelowLeft;
    }

    public void OnToolStripToolsDropDownClosed(object sender, EventArgs e)
    {
        mainToolStripItem.DefaultDropDownDirection = ToolStripDropDownDirection.Default;
    }

    public void OnPageDisplayModeChanged(object sender, EventArgs e)
    {
        tbZoom.Text = $"{(int)(MC.ComicDisplay.ImageZoom * 100f)}%";
        tbRotate.Text = TR.Translate(MC.ComicDisplay.ImageRotation);
        tbRotate.Image = MC.ComicDisplay.ImageAutoRotate ? Resources.AutoRotate : Resources.RotateRight;
    }

    public void UpdateMenu(bool readerButtonsVisible)
    {
        tbFit.Image = GetFitModeImage();
        //tbPageLayout.Image = GetLayoutImage();

        tsSynchronizeDevices.Visible = Program.Settings.Devices.Count > 0;
        SetReaderButtonVisibility(readerButtonsVisible);
    }

    private void SetReaderButtonVisibility(bool visible)
    {
        tbPrevPage.Visible = visible;
        tbNextPage.Visible = visible;
        tbPageLayout.Visible = visible;
        tbFit.Visible = visible;
        tbZoom.Visible = visible;
        tbRotate.Visible = visible;
        tbMagnify.Visible = visible;
        tsSeparatorRotateMagnify.Visible = visible;
        tsSeparatorPage.Visible = visible;
    }

    #region ToolStrip Helpers
    private Image GetLayoutImage() => MC.ComicDisplay.PageLayout switch
    {
        PageLayoutMode.Double
            => MC.ComicDisplay.RightToLeftReading ? Resources.TwoPageForcedRtl : Resources.TwoPageForced,
        PageLayoutMode.DoubleAdaptive
            => MC.ComicDisplay.RightToLeftReading ? Resources.TwoPageRtl : Resources.TwoPage,
        _
            => MC.ComicDisplay.RightToLeftReading ? Resources.SinglePageRtl : Resources.SinglePage
    };

    private Image GetFitModeImage()
    {
        try
        {
            int imageFitMode = (int)MC.ComicDisplay.ImageFitMode;
            foreach (ToolStripItem dropDownItem in tbFit.DropDownItems)
                if (dropDownItem.Image != null && imageFitMode-- == 0)
                    return dropDownItem.Image;
            return null;
        }
        catch
        {
            return null;
        }
    }
    #endregion

    // this now runs before MainForm.InitializeComponent()
    public static DropDownHost<MagnifySetupControl> GetMagnifierDropDown()
    {
        DropDownHost<MagnifySetupControl> dropDownHost = new DropDownHost<MagnifySetupControl>();
        MC.ComicDisplay.MagnifierOpacity = (dropDownHost.Control.MagnifyOpaque = Program.Settings.MagnifyOpaque);
        MC.ComicDisplay.MagnifierSize = (dropDownHost.Control.MagnifySize = Program.Settings.MagnifySize);
        MC.ComicDisplay.MagnifierZoom = (dropDownHost.Control.MagnifyZoom = Program.Settings.MagnifyZoom);
        MC.ComicDisplay.MagnifierStyle = (dropDownHost.Control.MagnifyStyle = Program.Settings.MagnifyStyle);
        MC.ComicDisplay.AutoMagnifier = (dropDownHost.Control.AutoMagnifier = Program.Settings.AutoMagnifier);
        MC.ComicDisplay.AutoHideMagnifier = (dropDownHost.Control.AutoHideMagnifier = Program.Settings.AutoHideMagnifier);
        dropDownHost.Control.ValuesChanged += OnMagnifierSetupChanged;
        return dropDownHost;
    }

    private static void OnMagnifierSetupChanged(object sender, EventArgs e)
    {
        MagnifySetupControl magnifySetupControl = (MagnifySetupControl)sender;
        MC.ComicDisplay.MagnifierOpacity = magnifySetupControl.MagnifyOpaque;
        MC.ComicDisplay.MagnifierSize = magnifySetupControl.MagnifySize;
        MC.ComicDisplay.MagnifierZoom = magnifySetupControl.MagnifyZoom;
        MC.ComicDisplay.MagnifierStyle = magnifySetupControl.MagnifyStyle;
        MC.ComicDisplay.AutoHideMagnifier = magnifySetupControl.AutoHideMagnifier;
        MC.ComicDisplay.AutoMagnifier = magnifySetupControl.AutoMagnifier;
    }
}
