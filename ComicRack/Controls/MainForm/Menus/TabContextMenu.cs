using cYo.Projects.ComicRack.Engine;
using System.ComponentModel;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class TabContextMenu : UserControl
{
    public TabContextMenu()
    {
        InitializeComponent();

        tabContextMenuItem.Opening += OnContextMenuOpening;
    }

    public static implicit operator ContextMenuStrip(TabContextMenu menu)
        => menu.tabContextMenuItem;

    private void OnContextMenuOpening(object sender, CancelEventArgs e)
    {
        bool comicIsNullOrLocal = MC.ComicDisplay?.Book == null || MC.ComicDisplay.Book.Comic.EditMode.IsLocalComic();
        tsSeparatorReveal.Visible = comicIsNullOrLocal;
        cmRevealInExplorer.Visible = comicIsNullOrLocal;
    }
}
