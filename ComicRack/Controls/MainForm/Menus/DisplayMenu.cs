using cYo.Common.Collections;
using cYo.Common.Drawing;
using cYo.Common.Mathematics;
using cYo.Common.Windows;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Command = cYo.Projects.ComicRack.Viewer.Controllers.Command;
using Menu = cYo.Projects.ComicRack.Viewer.Controllers.Menu;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class DisplayMenu : UserControl
{
    private static readonly Command Separator = Command.None;

    public IList<Command> Commands =
    [
        Command.ShowDisplaySettings,
        Separator,
        Command.PageLayout,
        Command.OriginalSize,
        Command.FitAll,
        Command.FitWidth,
        Command.FitWidthAdaptive,
        Command.FitHeight,
        Command.FitBest,
        Command.SinglePage,
        Command.TwoPages,
        Command.TwoPagesAdaptive,
        Command.ToggleRtL,
        Command.ToggleOversizeFit,
        Separator,
        Command.Zoom,
        Command.ZoomIn,
        Command.ZoomOut,
        Command.ToggleZoom,
        Command.Zoom100,
        Command.Zoom125,
        Command.Zoom150,
        Command.Zoom200,
        Command.Zoom400,
        Command.ShowZoomCustom,
        Separator,
        Command.Rotation,
        Command.RotateDisplayLeft,
        Command.RotateDisplayRight,
        Command.RotateDisplay0,
        Command.RotateDisplay90,
        Command.RotateDisplay180,
        Command.RotateDisplay270,
        Command.ToggleAutoRotate,
        Separator,
        Command.ToggleMinimalGui,
        Command.ToggleFullScreen,
        Command.ToggleReaderWindow,
        Command.ToggleMagnifier
    ];

    public DisplayMenu()
    {
        Commands.ForEach(cmd => cmd.Menu = Menu.Display);
        InitializeComponent();
        BindCommands();
        MainMenuControl.InitializeMenuState(this);
    }

    private void BindCommands()
    {
        miComicDisplaySettings.Tag = Command.ShowDisplaySettings;
        miPageLayout.Tag = Command.PageLayout;
        miZoom.Tag = Command.Zoom;
        miRotation.Tag = Command.Rotation;
        miMinimalGui.Tag = Command.ToggleMinimalGui;
        miFullScreen.Tag = Command.ToggleFullScreen;
        miReaderUndocked.Tag = Command.ToggleReaderWindow;
        miMagnify.Tag = Command.ToggleMagnifier;

        miOriginal.Tag = Command.OriginalSize;
        miFitAll.Tag = Command.FitAll;
        miFitWidth.Tag = Command.FitWidth;
        miFitWidthAdaptive.Tag = Command.FitWidthAdaptive;
        miFitHeight.Tag = Command.FitHeight;
        miBestFit.Tag = Command.FitBest;
        miSinglePage.Tag = Command.SinglePage;
        miTwoPages.Tag = Command.TwoPages;
        miTwoPagesAdaptive.Tag = Command.TwoPagesAdaptive;
        miRightToLeft.Tag = Command.ToggleRtL;
        miOnlyFitOversized.Tag = Command.ToggleOversizeFit;

        miZoomIn.Tag = Command.ZoomIn;
        miZoomOut.Tag = Command.ZoomOut;
        miToggleZoom.Tag = Command.ToggleZoom;
        miZoom100.Tag = Command.Zoom100;
        miZoom125.Tag = Command.Zoom125;
        miZoom150.Tag = Command.Zoom150;
        miZoom200.Tag = Command.Zoom200;
        miZoom400.Tag = Command.Zoom400;
        miZoomCustom.Tag = Command.ShowZoomCustom;

        miRotateLeft.Tag = Command.RotateDisplayLeft;
        miRotateRight.Tag = Command.RotateDisplayRight;
        miRotate0.Tag = Command.RotateDisplay0;
        miRotate90.Tag = Command.RotateDisplay90;
        miRotate180.Tag = Command.RotateDisplay180;
        miRotate270.Tag = Command.RotateDisplay270;
        miAutoRotate.Tag = Command.ToggleAutoRotate;
    }

    public static implicit operator ToolStripMenuItem(DisplayMenu menu) => menu.displayMenuItem;
}
