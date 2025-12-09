using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Controls;
using cYo.Projects.ComicRack.Viewer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class PageContextMenu : UserControl
{
    private MainController controller;

    private EnumMenuUtility pageType;

    private EnumMenuUtility pageRotation;

    public PageContextMenu()
    {
        //this.controller = controller;
        InitializeComponent();
    }

    public void SetController(MainController controller)
    {
        this.controller = controller;
        pageContextMenuItem.Closed += OnContextMenuClosed;
        pageContextMenuItem.Opening += OnContextMenuOpening;
        cmBookmarks.DropDownOpening += MainController.EventHandlers.OnPageContextMenuBookmarksDropDownOpening;

        pageType = controller.GetPageType(cmPageType);
        pageRotation = controller.GetPageRotation(cmPageRotate);

        pageType.ValueChanged += MainController.EventHandlers.OnPageTypeChanged;
        pageRotation.ValueChanged += MainController.EventHandlers.OnPageRotationChanged;

        contextRating.Items.Insert(contextRating.Items.Count - 2, new ToolStripSeparator());
        RatingControl.InsertRatingControl(
            contextRating,
            contextRating.Items.Count - 2,
            Resources.StarYellow,
            controller.GetRatingEditor);
        contextRating.Renderer = new MenuRenderer(Resources.StarYellow);
    }

    //protected override void OnLoad(EventArgs e)
    //{
    //    base.OnLoad(e);

    //    pageType = controller.GetPageType(cmPageType);
    //    pageRotation = controller.GetPageRotation(cmPageRotate);

    //    pageType.ValueChanged += MainController.EventHandlers.OnPageTypeChanged;
    //    pageRotation.ValueChanged += MainController.EventHandlers.OnPageRotationChanged;

    //    contextRating.Items.Insert(contextRating.Items.Count - 2, new ToolStripSeparator());
    //    RatingControl.InsertRatingControl(
    //        contextRating,
    //        contextRating.Items.Count - 2,
    //        Resources.StarYellow,
    //        controller.GetRatingEditor);
    //    contextRating.Renderer = new MenuRenderer(Resources.StarYellow);
    //}

    public static implicit operator ContextMenuStrip(PageContextMenu menu)
        => menu.pageContextMenuItem;

    public void OnContextMenuOpening(object sender, CancelEventArgs e)
    {
        tsSeparatorComics.Visible = controller.OpenNow.Count() > 0;
        try
        {
            if (controller.ComicDisplay == null)
            {
                e.Cancel = true;
                return;
            }
            if (controller.ComicDisplay.SupressContextMenu)
            {
                controller.ComicDisplay.SupressContextMenu = false;
                e.Cancel = true;
                return;
            }
            IEditPage pageEditor = controller.GetPageEditor();
            pageType.Enabled = pageEditor.IsValid;
            pageType.Value = (int)pageEditor.PageType;
            pageRotation.Value = (int)pageEditor.Rotation;
        }
        catch
        {
            e.Cancel = true;
        }
    }

    public void OnContextMenuClosed(object sender, ToolStripDropDownClosedEventArgs e)
    {
        MainController.Commands.StartMouseDisabledTimer();
    }

    public void ClearComics()
    {
        FormUtility.SafeToolStripClear(cmComics.DropDownItems, cmComics.DropDownItems.IndexOf(tsSeparatorComics) + 1);
    }

    public void AddComic(ToolStripMenuItem item)
    {
        cmComics.DropDownItems.Add(item);
    }

    public void UpdateMenu()
    {
        cmMagnify.Image = controller.ComicDisplay.MagnifierVisible ? Resources.Zoom : Resources.ZoomClear;
    }
}
