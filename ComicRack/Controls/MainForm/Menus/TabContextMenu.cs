using cYo.Projects.ComicRack.Engine.Display;
using System.ComponentModel;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class TabContextMenu : UserControl
{
    public TabContextMenu()
    {
        InitializeComponent();
    }

    private void tabContextMenu_Opening(object sender, CancelEventArgs e)
    {
        bool flag = ComicDisplay == null || ComicDisplay.Book == null || ComicDisplay.Book.Comic.EditMode.IsLocalComic();
        ToolStripSeparator toolStripSeparator = sepBeforeRevealInBrowser;
        ToolStripMenuItem toolStripMenuItem = cmRevealInExplorer;
        bool flag3 = (cmCopyPath.Visible = flag);
        bool visible = (toolStripMenuItem.Visible = flag3);
        toolStripSeparator.Visible = visible;
    }
}
