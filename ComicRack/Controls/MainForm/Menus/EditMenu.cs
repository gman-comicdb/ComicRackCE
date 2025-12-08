using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
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

public partial class EditMenu : UserControl
{
    public ToolStripMenuItem Item;

    public EditMenu()
    {
        InitializeComponent();
        Item = editMenuItem;
    }

    public static implicit operator ToolStripMenuItem(EditMenu menu)
        => menu.Item;

    private void miBookmarks_DropDownOpening(object sender, EventArgs e)
    {
        UpdateBookmarkMenu(miBookmarks.DropDownItems, 0);
    }

    private void editMenu_DropDownOpening(object sender, EventArgs e)
    {
        try
        {
            bool flag = ComicDisplay != null && ComicDisplay.Book != null;
            IEditPage pageEditor = GetPageEditor();
            EnumMenuUtility enumMenuUtility = pageTypeEditMenu;
            bool enabled = (pageRotationEditMenu.Enabled = pageEditor.IsValid);
            enumMenuUtility.Enabled = enabled;
            pageTypeEditMenu.Value = (int)pageEditor.PageType;
            pageRotationEditMenu.Value = (int)pageEditor.Rotation;
            if (miUndo.Tag == null)
            {
                miUndo.Tag = miUndo.Text;
            }
            string undoLabel = Program.Database.Undo.UndoLabel;
            miUndo.Text = (string)miUndo.Tag + (string.IsNullOrEmpty(undoLabel) ? string.Empty : (": " + undoLabel));
            if (miRedo.Tag == null)
            {
                miRedo.Tag = miRedo.Text;
            }
            string text = Program.Database.Undo.RedoEntries.FirstOrDefault();
            miRedo.Text = (string)miRedo.Tag + (string.IsNullOrEmpty(text) ? string.Empty : (": " + text));
        }
        catch (Exception)
        {
        }
    }
}
