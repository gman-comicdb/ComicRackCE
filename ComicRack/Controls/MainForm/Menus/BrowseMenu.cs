using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class BrowseMenu : UserControl
{
    public ToolStripMenuItem Item;

    public BrowseMenu()
    {
        InitializeComponent();
        Item = browseMenuItem;
    }

    public static implicit operator ToolStripMenuItem(BrowseMenu menu)
        => menu.Item;
}
