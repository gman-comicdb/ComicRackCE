using cYo.Projects.ComicRack.Engine;
using System.ComponentModel;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class TabContextMenu : UserControl
{
    private MainController controller;

    public TabContextMenu()
    {
        InitializeComponent();

        tabContextMenuItem.Opening += OnContextMenuOpening;
        //this.controller = controller;
    }

    public void SetController(MainController controller)
    {
        this.controller = controller;
    }

    public static implicit operator ContextMenuStrip(TabContextMenu menu)
        => menu.tabContextMenuItem;

    private void OnContextMenuOpening(object sender, CancelEventArgs e)
    {
        bool comicIsNullOrLocal = controller.ComicDisplay?.Book == null || controller.ComicDisplay.Book.Comic.EditMode.IsLocalComic();
        tsSeparatorReveal.Visible = comicIsNullOrLocal;
        cmRevealInExplorer.Visible = comicIsNullOrLocal;
    }
}
