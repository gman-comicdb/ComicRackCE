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
    private EnumMenuUtility pageType;

    private EnumMenuUtility pageRotation;

    public IEnumerable<ToolStripMenuItem> Comics => cmComics.DropDownItems.OfType<ToolStripMenuItem>();

    public PageContextMenu()
    {
        //this.controller = controller;
        InitializeComponent();
        pageContextMenuItem.Closed += OnContextMenuClosed;
        pageContextMenuItem.Opening += OnContextMenuOpening;
        cmBookmarks.DropDownOpening += MC.EventHandlers.OnPageContextMenuBookmarksDropDownOpening;

        pageType = MainMenuControl.GetPageType(cmPageType);
        pageRotation = MainMenuControl.GetPageRotation(cmPageRotate);

        pageType.ValueChanged += MC.EventHandlers.OnPageTypeChanged;
        pageRotation.ValueChanged += MC.EventHandlers.OnPageRotationChanged;

        contextRating.Items.Insert(contextRating.Items.Count - 2, new ToolStripSeparator());
        RatingControl.InsertRatingControl(
            contextRating,
            contextRating.Items.Count - 2,
            Resources.StarYellow,
            MC.GetRatingEditor);
        contextRating.Renderer = new MenuRenderer(Resources.StarYellow);
    }

    //protected override void OnLoad(EventArgs e)
    //{
    //    base.OnLoad(e);

    //    pageType = MC.GetPageType(cmPageType);
    //    pageRotation = MC.GetPageRotation(cmPageRotate);

    //    pageType.ValueChanged += MainMC.EventHandlers.OnPageTypeChanged;
    //    pageRotation.ValueChanged += MainMC.EventHandlers.OnPageRotationChanged;

    //    contextRating.Items.Insert(contextRating.Items.Count - 2, new ToolStripSeparator());
    //    RatingControl.InsertRatingControl(
    //        contextRating,
    //        contextRating.Items.Count - 2,
    //        Resources.StarYellow,
    //        MC.GetRatingEditor);
    //    contextRating.Renderer = new MenuRenderer(Resources.StarYellow);
    //}

    public static implicit operator ContextMenuStrip(PageContextMenu menu)
        => menu.pageContextMenuItem;

    public void OnContextMenuOpening(object sender, CancelEventArgs e)
    {
        try
        {
            if (MC.ComicDisplay == null)
            {
                e.Cancel = true;
                return;
            }
            if (MC.ComicDisplay.SupressContextMenu)
            {
                MC.ComicDisplay.SupressContextMenu = false;
                e.Cancel = true;
                return;
            }
            IEditPage pageEditor = MC.GetPageEditor();
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
        MC.Commands.StartMouseDisabledTimer();
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
        cmMagnify.Image = MC.ComicDisplay.MagnifierVisible ? Resources.Zoom : Resources.ZoomClear;
    }
}
