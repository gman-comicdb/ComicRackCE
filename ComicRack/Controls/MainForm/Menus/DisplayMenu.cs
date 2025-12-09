using cYo.Common.Drawing;
using cYo.Common.Mathematics;
using cYo.Common.Windows;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Properties;
using System;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class DisplayMenu : UserControl
{
    private MainController controller;

    public DisplayMenu()
    {
        InitializeComponent();
        //this.controller = controller;
    }

    public void SetController(MainController controller)
    {
        this.controller = controller;
    }

    public static implicit operator ToolStripMenuItem(DisplayMenu menu)
        => menu.displayMenuItem;

    public void InitializeCommands()
    {
        //MainController.Commands.Add(ToggleUndockReader, true, () => ReaderUndocked, miReaderUndocked);
    }

    public void InitializeKeyboard()
    {
        string group = "Display Options";
        #region Display Options
        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miReaderUndocked.Image,
                "ToggleUndockReader",
                group,
                "Toggle Undock Reader",
                MainController.Commands.ToggleUndockReader,
                CommandKey.D
                )
            );
        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miFullScreen.Image,
                "ToggleFullScreen",
                group,
                "Toggle Full Screen",
                controller.ComicDisplay.ToggleFullScreen,
                CommandKey.F,
                CommandKey.MouseDoubleLeft,
                CommandKey.Gesture2
                )
            );
        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miTwoPages.Image,
                "ToggleTwoPages",
                group,
                "Toggle Two Pages",
                controller.ComicDisplay.TogglePageLayout,
                CommandKey.T
                )
            );
        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                "ToggleRealisticPages",
                group,
                "Toggle Realistic Display",
                controller.ComicDisplay.ToogleRealisticPages,
                CommandKey.D | CommandKey.Shift
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miMagnify.Image,
                "ToggleMagnify",
                group,
                "Toggle Magnifier",
                () => controller.ComicDisplay.MagnifierVisible = !controller.ComicDisplay.MagnifierVisible,
                [
                    CommandKey.M,
                    CommandKey.TouchPressAndTap
                ]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                "ToggleMenu",
                group,
                "Toggle Menu",
                MainController.Commands.ToggleMinimalGui,
                CommandKey.K
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                null,
                "ToggleNavigationOverlay",
                group,
                "Toggle Navigation Overlay",
                controller.ComicDisplay.ToggleNavigationOverlay,
                CommandKey.Gesture8, CommandKey.TouchTwoFingerTap
                )
            );
        #endregion

        group = "Page Display";
        #region Page Display
        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miOriginal.Image,
                "Original",
                group,
                "Original Size",
                controller.ComicDisplay.SetPageOriginal,
                CommandKey.D1
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miFitAll.Image,
                "FitAll",
                group,
                "Fit All",
                controller.ComicDisplay.SetPageFitAll,
                CommandKey.D2
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miFitWidth.Image,
                "FitWidth",
                group,
                "Fit Width",
                controller.ComicDisplay.SetPageFitWidth,
                CommandKey.D3
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miFitWidthAdaptive.Image,
                "FitWidthAdaptive",
                group,
                "Fit Width (adaptive)",
                controller.ComicDisplay.SetPageFitWidthAdaptive,
                CommandKey.D4
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miFitHeight.Image,
                "FitHeight",
                group,
                "Fit Height",
                controller.ComicDisplay.SetPageFitHeight,
                CommandKey.D5
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miBestFit.Image,
                "FitBest",
                group,
                "Best Fit",
                controller.ComicDisplay.SetPageBestFit,
                CommandKey.D6
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miSinglePage.Image,
                "SinglePage",
                group,
                "Single Page",
                (Action)delegate
                {
                    controller.ComicDisplay.PageLayout = PageLayoutMode.Single;
                },
                [CommandKey.D7]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miTwoPages.Image,
                "TwoPages",
                group,
                "Two Pages",
                (Action)delegate
                {
                    controller.ComicDisplay.PageLayout = PageLayoutMode.Double;
                },
                [CommandKey.D8]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miTwoPagesAdaptive.Image,
                "TwoPagesAdaptive",
                group,
                "Two Pages (adaptive)",
                (Action)delegate
                {
                    controller.ComicDisplay.PageLayout = PageLayoutMode.DoubleAdaptive;
                },
                [CommandKey.D9]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miRightToLeft.Image,
                "RightToLeft",
                group,
                "Right to Left",
                (Action)delegate
                {
                    controller.ComicDisplay.RightToLeftReading = !controller.ComicDisplay.RightToLeftReading;
                },
                [CommandKey.D0]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miOnlyFitOversized.Image,
                "OnlyFitIfOversized",
                group,
                "Only Fit if oversized",
                controller.ComicDisplay.ToggleFitOnlyIfOversized,
                CommandKey.O
                )
            );
        #endregion

        group = "ZoomAndRotate";
        #region Zoom and Rotate
        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miRotateRight.Image,
                "RotateC",
                group,
                "Rotate Right",
                (Action)delegate
                {
                    controller.ComicDisplay.ImageRotation = controller.ComicDisplay.ImageRotation.RotateRight();
                },
                [CommandKey.R]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miRotateLeft.Image,
                "RotateCC",
                group,
                "Rotate Left",
                (Action)delegate
                {
                    controller.ComicDisplay.ImageRotation = controller.ComicDisplay.ImageRotation.RotateLeft();
                },
                [CommandKey.R | CommandKey.Shift]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miAutoRotate.Image,
                "AutoRotate",
                group,
                "Autorotate Double Pages",
                () => controller.ComicDisplay.ImageAutoRotate = !controller.ComicDisplay.ImageAutoRotate,
                [CommandKey.A]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miZoomIn.Image,
                "ZoomIn",
                group,
                "Zoom In",
                () => controller.ComicDisplay.ImageZoom = (controller.ComicDisplay.ImageZoom + 0.1f).Clamp(1f, 8f),
                [CommandKey.MouseWheelUp | CommandKey.Ctrl]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miZoomOut.Image,
                "ZoomOut",
                group,
                "Zoom Out",
                () => controller.ComicDisplay.ImageZoom = (controller.ComicDisplay.ImageZoom - 0.1f).Clamp(1f, 8f),
                [CommandKey.MouseWheelDown | CommandKey.Ctrl]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miZoomIn.Image,
                "StepZoomIn",
                group,
                "Step Zoom In",
                () => controller.ComicDisplay.ImageZoom = (controller.ComicDisplay.ImageZoom + Program.ExtendedSettings.KeyboardZoomStepping).Clamp(1f, 4f),
                [CommandKey.Z]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miZoomOut.Image,
                "StepZoomOut",
                group,
                "Step Zoom Out",
                () => controller.ComicDisplay.ImageZoom = (controller.ComicDisplay.ImageZoom - Program.ExtendedSettings.KeyboardZoomStepping).Clamp(1f, 4f),
                [CommandKey.Z | CommandKey.Shift]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                miToggleZoom.Image,
                "ToggleZoom",
                group,
                "Toggle Zoom",
                MainController.Commands.ToogleZoom,
                CommandKey.TouchDoubleTap
                )
            );
        #endregion
    }

    public void UpdateMenu()
    {
        miMagnify.Image = controller.ComicDisplay.MagnifierVisible ? Resources.Zoom : Resources.ZoomClear;
    }
}
