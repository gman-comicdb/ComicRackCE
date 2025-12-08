using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class MainToolStrip : UserControl
{
    public ToolStrip Item;

    public DropDownHost<MagnifySetupControl> MagnifierDropDownHost
    {
        set
        {
            if (value != null)
            {
                tbMagnify.DropDown = value;
            }
        }
    }

    public MainToolStrip()
    {
        InitializeComponent();
        Item = mainToolStripItem;

        tbPrevPage.DropDownOpening += new System.EventHandler(this.tbPrevPage_DropDownOpening);
        tbNextPage.DropDownOpening += new System.EventHandler(this.tbNextPage_DropDownOpening);
        tbTools.DropDownOpening += new System.EventHandler(this.tbTools_DropDownOpening);
        tbBookmarks.DropDownOpening += new System.EventHandler(this.tbBookmarks_DropDownOpening);
    }

    public static implicit operator ToolStrip(MainToolStrip menu)
        => menu.Item;

    private void tbPrevPage_DropDownOpening(object sender, EventArgs e)
    {
        UpdateBookmarkMenu(tbPrevPage.DropDownItems, -1);
    }

    private void tbNextPage_DropDownOpening(object sender, EventArgs e)
    {
        UpdateBookmarkMenu(tbNextPage.DropDownItems, 1);
    }

    private void tbBookmarks_DropDownOpening(object sender, EventArgs e)
    {
        UpdateBookmarkMenu(tbBookmarks.DropDownItems, 0);
    }

    private void tbTools_DropDownOpening(object sender, EventArgs e)
    {
        tbUpdateWebComics.Visible = Program.Database.Books.FirstOrDefault((ComicBook cb) => cb.IsDynamicSource) != null;
    }
}
