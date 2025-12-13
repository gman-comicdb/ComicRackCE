using cYo.Common.Collections;
using cYo.Common.Windows;
using cYo.Projects.ComicRack.Engine.Display;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Command = cYo.Projects.ComicRack.Viewer.Controllers.Command;
using Menu = cYo.Projects.ComicRack.Viewer.Controllers.Menu;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class ReadMenu : UserControl
{
    private static readonly Command Separator = Command.None;

    public IList<Command> Commands =
    [
        Command.Open,
        Command.Close,
        Command.CloseAll,
        Separator,
        Command.AddTab,
        Separator,
        Command.AddFolder,
        Command.ScanFolders,
        Command.UpdateAllBooks,
        Command.UpdateWebComics,
        Command.SynchronizeDevices,
        Command.GenerateCovers,
        Command.ShowTasks,
        Command.Automation,
        Separator,
        Command.NewComic,
        Separator,
        Command.OpenRemoteLibrary,
        Separator,
        Command.OpenBooks,
        Command.RecentBooks,
        Separator,
        Command.Restart,
        Separator,
        Command.Exit
    ];

    public ReadMenu()
    {
        Commands.ForEach(cmd => cmd.Menu = Menu.File);
        InitializeComponent();
        BindCommands();
        MainMenuControl.InitializeMenuState(this);
    }

    private void BindCommands()
    {
        miFirstPage.Tag = Command.GoFirstPage;
        miPrevPage.Tag = Command.GoPreviousPage;
        miNextPage.Tag = Command.GoNextPage;
        miLastPage.Tag = Command.GoLastPage;

        miPrevFromList.Tag = Command.PreviousBook;
        miNextFromList.Tag = Command.NextBook;
        miRandomFromList.Tag = Command.RandomBook;
        miSyncBrowser.Tag = Command.BrowseToBook;

        miPrevTab.Tag = Command.PreviousTab;
        miNextTab.Tag = Command.NextTab;

        miAutoScroll.Tag = Command.ToggleAutoScroll;
        miDoublePageAutoScroll.Tag = Command.ToggleDoublePageAutoScroll;

        miTrackCurrentPage.Tag = Command.ToggleTrackCurrentPage;
    }

    public static implicit operator ToolStripMenuItem(ReadMenu menu) => menu.readMenuItem;
}
