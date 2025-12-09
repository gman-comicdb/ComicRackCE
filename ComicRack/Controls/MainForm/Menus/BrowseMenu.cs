using cYo.Common.Windows;
using cYo.Projects.ComicRack.Engine.Display;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class BrowseMenu : UserControl
{
    private MainController controller;

    public ToolStripMenuItem NextList => miNextList;
    public ToolStripMenuItem PreviousList => miPreviousList;

    public BrowseMenu()
    {
        InitializeComponent();
        //this.controller = controller;
        if (Program.ExtendedSettings.DisableFoldersView)
            miViewFolders.GetCurrentParent().Items.Remove(miViewFolders);
    }
    public void SetController(MainController controller)
    {
        this.controller = controller;
    }

    public static implicit operator ToolStripMenuItem(BrowseMenu menu)
        => menu.browseMenuItem;

    public void InitializeKeyboard()
    {
        string group = "Library";

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miToggleBrowser.Image,
                "ShowBrowser",
                group,
                "Show Browser",
                MainController.Commands.ToggleBrowserFromReader,
                CommandKey.MouseLeft,
                CommandKey.Escape
                )
            );

        group = "Browse";

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                "MoveToPrevPageSingle",
                group,
                "Single Page Back",
                () => controller.ComicDisplay.DisplayPreviousPage(ComicDisplay.PagingMode.None),
                CommandKey.PageUp | CommandKey.Shift
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
           new KeyboardCommand(
               "MoveToNextPageSingle",
               group,
               "Single Page Forward",
               () => controller.ComicDisplay.DisplayNextPage(ComicDisplay.PagingMode.None),
               CommandKey.PageDown | CommandKey.Shift
               )
           );

        group = "Auto Scroll";

        controller.ComicDisplay.KeyboardMap.Commands.Add(
           new KeyboardCommand(
               "MovePrevPart",
               group,
               "Previous Part",
               () => controller.ComicDisplay.DisplayPreviousPageOrPart(),
               CommandKey.Space | CommandKey.Shift,
               CommandKey.Gesture7
               )
           );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
           new KeyboardCommand(
               "MoveNextPart",
               group,
               "Next Part",
               () => controller.ComicDisplay.DisplayNextPageOrPart(),
               CommandKey.Space,
               CommandKey.Gesture9
               )
           );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
           new KeyboardCommand(
               "MoveFirstPart",
               group,
               "Page Start",
               () => controller.ComicDisplay.DisplayPart(PartPageToDisplay.First),
               CommandKey.Home
               )
           );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
           new KeyboardCommand(
               "MoveLastPart",
               group,
               "Page End",
               () => controller.ComicDisplay.DisplayPart(PartPageToDisplay.Last),
               CommandKey.End
               )
           );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
           new KeyboardCommand(
               "MovePartDown10",
               group,
               "Move Part 10% down",
               () => controller.ComicDisplay.MovePartDown(0.1f),
               CommandKey.V,
               CommandKey.Down | CommandKey.Ctrl
               )
           );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
           new KeyboardCommand(
               "MovePartUp10",
               group,
               "Move Part 10% up",
               () => controller.ComicDisplay.MovePartDown(-0.1f),
               CommandKey.B,
               CommandKey.Up | CommandKey.Ctrl
               )
           );
    }

    public void UpdateWorkspaceMenus()
        => MainController.Commands.UpdateWorkspaceMenus(miWorkspaces.DropDownItems);

    public void UpdateListConfigMenus()
        => MainController.Commands.UpdateListConfigMenus(miListLayouts.DropDownItems);

}
