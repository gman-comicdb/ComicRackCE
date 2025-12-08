using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class DisplayMenu : UserControl
{
    public ToolStripMenuItem Item;

    public DisplayMenu()
    {
        InitializeComponent();
        Item = displayMenuItem;
    }

    public static implicit operator ToolStripMenuItem(DisplayMenu menu)
        => menu.Item;
}
