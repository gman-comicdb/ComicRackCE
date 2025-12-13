using cYo.Common.Collections;
using cYo.Common.Windows;
using cYo.Projects.ComicRack.Engine.Display;
using System.Collections.Generic;
using System.Windows.Forms;
using Command = cYo.Projects.ComicRack.Viewer.Controllers.Command;
using Menu = cYo.Projects.ComicRack.Viewer.Controllers.Menu;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class BrowseMenu : UserControl
{
    private static readonly Command Separator = Command.None;

    public IList<Command> Commands =
    [
     Command.ToggleBrowser,
        Separator,
        Command.ShowDisplaySettings,
        Command.ViewLibrary,
        Command.ViewFolders,
        Command.ViewPages,
        Separator,
        Command.ToggleSidebar,
        Command.TogglePreview,
        Command.ToggleSearchFilter,
        Separator,
        Command.ToggleInfoPanel,
        Separator,
        Command.PreviousList,
        Command.NextList,
        Separator,
        Command.Workspaces,
        Command.SaveWorkspace,
        Command.EditWorkspaces,
        Separator,
        Command.ListLayout,
        Command.EditListLayout,
        Command.SaveListLayout,
        Command.EditAllListLayout,
        Command.SetAllListLayout
    ];

    public BrowseMenu()
    {
        Commands.ForEach(cmd => cmd.Menu = Menu.Edit);
        InitializeComponent();
        BindCommands();
        MainMenuControl.InitializeMenuState(this);
    }

    private void BindCommands()
    {
        miToggleBrowser.Tag = Command.ToggleBrowser;

        miViewLibrary.Tag = Command.ViewLibrary;
        miViewFolders.Tag = Command.ViewFolders;
        miViewPages.Tag = Command.ViewPages;

        miSidebar.Tag = Command.ToggleSidebar;
        miSmallPreview.Tag = Command.TogglePreview;
        miSearchBrowser.Tag = Command.ToggleSearchFilter;

        miInfoPanel.Tag = Command.ToggleInfoPanel;

        miPreviousList.Tag = Command.PreviousList;
        miNextList.Tag = Command.NextList;

        miWorkspaces.Tag = Command.Workspaces;
        miListLayouts.Tag = Command.ListLayout;
        
        miSaveWorkspace.Tag = Command.SaveWorkspace;
        miEditWorkspaces.Tag = Command.EditWorkspaces;

        miEditListLayout.Tag = Command.EditListLayout;
        miSaveListLayout.Tag = Command.SaveListLayout;
        miEditLayouts.Tag = Command.EditAllListLayout;
        miSetAllListsSame.Tag = Command.SetAllListLayout;
    }

    public static implicit operator ToolStripMenuItem(BrowseMenu menu) => menu.browseMenuItem;
}
