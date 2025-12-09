using cYo.Common.Localize;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class MainToolStrip : UserControl
{
    private MainController controller;

    public MainToolStrip()
    {
        //this.controller = controller;
        InitializeComponent();
    }

    public void SetController(MainController controller)
    {
        this.controller = controller;

        tbMagnify.DropDown = controller.GetMagnifierDropDown();

        tbPrevPage.DropDownOpening += MainController.EventHandlers.OnToolStripPrevPageDropDownOpening;
        tbNextPage.DropDownOpening += MainController.EventHandlers.OnToolStripNextPageDropDownOpening;
        tbBookmarks.DropDownOpening += MainController.EventHandlers.OnToolStripBookmarksDropDownOpening;

        tbTools.DropDownOpening += OnToolStripToolsDropDownOpening;
        tbTools.DropDownClosed += OnToolStripToolsDropDownClosed;
    }

    public static implicit operator ToolStrip(MainToolStrip menu)
        => menu.mainToolStripItem;

    public void OnToolStripToolsDropDownOpening(object sender, EventArgs e)
    {
        tbUpdateWebComics.Visible = Program.Database.Books.FirstOrDefault((ComicBook cb) => cb.IsDynamicSource) != null;
        mainToolStripItem.DefaultDropDownDirection = ToolStripDropDownDirection.BelowLeft;
    }

    public void OnToolStripToolsDropDownClosed(object sender, EventArgs e)
    {
        mainToolStripItem.DefaultDropDownDirection = ToolStripDropDownDirection.Default;
    }

    public void UpdateWorkspaceMenus()
        => MainController.Commands.UpdateWorkspaceMenus(tsWorkspaces.DropDownItems);

    public void OnPageDisplayModeChanged(object sender, EventArgs e)
    {
        tbZoom.Text = $"{(int)(controller.ComicDisplay.ImageZoom * 100f)}%";
        tbRotate.Text = TR.Translate(controller.ComicDisplay.ImageRotation);
        tbRotate.Image = controller.ComicDisplay.ImageAutoRotate ? Resources.AutoRotate : Resources.RotateRight;
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
    private Image GetLayoutImage() => controller.ComicDisplay.PageLayout switch
    {
        PageLayoutMode.Double
            => controller.ComicDisplay.RightToLeftReading ? Resources.TwoPageForcedRtl : Resources.TwoPageForced,
        PageLayoutMode.DoubleAdaptive
            => controller.ComicDisplay.RightToLeftReading ? Resources.TwoPageRtl : Resources.TwoPage,
        _
            => controller.ComicDisplay.RightToLeftReading ? Resources.SinglePageRtl : Resources.SinglePage
    };

    private Image GetFitModeImage()
    {
        try
        {
            int imageFitMode = (int)controller.ComicDisplay.ImageFitMode;
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

}
