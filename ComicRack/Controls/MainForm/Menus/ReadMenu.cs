using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class ReadMenu : UserControl
{
    public ToolStripMenuItem Item;

    public ReadMenu()
    {
        InitializeComponent();
        Item = readMenuItem;
    }

    public static implicit operator ToolStripMenuItem(ReadMenu menu)
        => menu.Item;
}
