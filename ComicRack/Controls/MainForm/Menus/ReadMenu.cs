using System;
using cYo.Common.Windows;
using cYo.Projects.ComicRack.Engine.Display;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class ReadMenu : UserControl
{
    private MainController controller;

    public ReadMenu()
    {
        InitializeComponent();
        //this.controller = controller;
    }

    public void SetController(MainController controller)
    {
        this.controller = controller;
    }

    public static implicit operator ToolStripMenuItem(ReadMenu menu)
        => menu.readMenuItem;

    public void InitializeCommands()
    {
        MainController.Commands.Add(() => MainController.Commands.OpenNextComic(), miNextFromList);
        MainController.Commands.Add(() => MainController.Commands.OpenPrevComic(), miPrevFromList);
        MainController.Commands.Add(() => MainController.Commands.OpenRandomComic(), miRandomFromList);
        MainController.Commands.Add(() => MainController.Commands.SyncBrowser(), () => controller.ComicDisplay.Book != null, miSyncBrowser);

        MainController.Commands.Add(controller.ComicDisplay.DisplayLastPage, () => controller.ComicDisplay.Book != null && controller.ComicDisplay.Book.CanNavigate(1), miLastPage);
        MainController.Commands.Add(controller.OpenBooks.PreviousSlot, () => controller.OpenBooks.Slots.Count > 1, miPrevTab);
        MainController.Commands.Add(controller.OpenBooks.NextSlot, () => controller.OpenBooks.Slots.Count > 1, miNextTab);
    }

    public void InitializeKeyboard()
    {
        string group = "Library";
        #region Library
        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miNextFromList.Image,
                "NextComic",
                group,
                "Next Book",
                () => MainController.Commands.OpenNextComic(),
                [CommandKey.N]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miPrevFromList.Image,
                "PrevComic",
                group,
                "Previous Book",
                () => MainController.Commands.OpenPrevComic(),
                [CommandKey.P]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miRandomFromList.Image,
                "RandomComic",
                group,
                "Random Book",
                () => MainController.Commands.OpenRandomComic(),
                [CommandKey.L]
                )
            );

        group = "Browse";

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miFirstPage.Image,
                "MoveToFirstPage",
                group,
                "First Page",
                controller.ComicDisplay.DisplayFirstPage,
                CommandKey.Home | CommandKey.Ctrl, CommandKey.GestureDouble1
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miPrevPage.Image,
                "MoveToPreviousPage",
                group,
                "Previous Page",
                () => controller.ComicDisplay.DisplayPreviousPage(ComicDisplay.PagingMode.Double),
                [
                    CommandKey.PageUp,
                    CommandKey.Left | CommandKey.Alt,
                    CommandKey.Gesture1,
                    CommandKey.FlickRight
                ]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miNextPage.Image,
                "MoveToNextPage",
                group,
                "Next Page",
                () => controller.ComicDisplay.DisplayNextPage(ComicDisplay.PagingMode.Double),
                [
                    CommandKey.PageDown,
                    CommandKey.Right | CommandKey.Alt,
                    CommandKey.Gesture3,
                    CommandKey.FlickLeft
                ]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miLastPage.Image,
                "MoveToLastPage",
                group,
                "Last Page",
                controller.ComicDisplay.DisplayLastPage,
                [
                    CommandKey.End | CommandKey.Ctrl,
                    CommandKey.GestureDouble3
                ]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miPrevTab.Image,
                "PrevTab",
                group,
                "Previous Tab",
                controller.OpenBooks.PreviousSlot,
                CommandKey.Tab | CommandKey.Shift
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miNextTab.Image,
                "NextTab",
                group,
                "Next Tab",
                controller.OpenBooks.NextSlot,
                CommandKey.Tab
                )
            );
        #endregion

        group = "Scroll";
        #region Scroll
        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miAutoScroll.Image,
                "ToggleAutoScrolling",
                group,
                "Toggle Auto Scrolling",
                () => Program.Settings.AutoScrolling = !Program.Settings.AutoScrolling,
                CommandKey.S
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                "DoublePageAutoScroll",
                group,
                "Double Page Auto Scroll",
                () => controller.ComicDisplay.TwoPageNavigation = !controller.ComicDisplay.TwoPageNavigation,
                CommandKey.S | CommandKey.Shift
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                "MoveUp",
                group,
                "Up",
                controller.ComicDisplay.ScrollUp,
                CommandKey.Up,
                CommandKey.MouseWheelUp
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                "MoveDown",
                group,
                "Down",
                controller.ComicDisplay.ScrollDown,
                CommandKey.Down,
                CommandKey.MouseWheelDown
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                "MoveLeft",
                group,
                "Left",
                controller.ComicDisplay.ScrollLeft,
                CommandKey.Left,
                CommandKey.MouseTiltLeft
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                "MoveRight",
                group,
                "Right",
                controller.ComicDisplay.ScrollRight,
                CommandKey.Right,
                CommandKey.MouseTiltRight
                )
            );

        #endregion
    }
}
