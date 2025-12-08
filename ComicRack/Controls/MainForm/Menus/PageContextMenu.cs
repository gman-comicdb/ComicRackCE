using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class PageContextMenu : UserControl
{
    public ContextMenuStrip Item;

    public PageContextMenu()
    {
        InitializeComponent();
        Item = pageContextMenuItem;
    }

    public static implicit operator ContextMenuStrip(PageContextMenu menu)
        => menu.Item;

    private void pageContextMenu_Opening(object sender, CancelEventArgs e)
    {
        try
        {
            if (ComicDisplay == null)
            {
                e.Cancel = true;
                return;
            }
            if (ComicDisplay.SupressContextMenu)
            {
                ComicDisplay.SupressContextMenu = false;
                e.Cancel = true;
                return;
            }
            IEditPage pageEditor = GetPageEditor();
            EnumMenuUtility enumMenuUtility = pageTypeContextMenu;
            bool enabled = (pageRotationContextMenu.Enabled = pageEditor.IsValid);
            enumMenuUtility.Enabled = enabled;
            pageTypeContextMenu.Value = (int)pageEditor.PageType;
            pageRotationContextMenu.Value = (int)pageEditor.Rotation;
        }
        catch
        {
            e.Cancel = true;
        }
    }

    private void pageContextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
        StartMouseDisabledTimer();
    }

    private void cmBookmarks_DropDownOpening(object sender, EventArgs e)
    {
        UpdateBookmarkMenu(cmBookmarks.DropDownItems, 0);
    }
}
