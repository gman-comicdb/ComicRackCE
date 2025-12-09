using cYo.Common;
using cYo.Common.Collections;
using cYo.Common.Localize;
using cYo.Common.Runtime;
using cYo.Common.Text;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StatusStrip = cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.StatusStrip;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm;

public partial class MainMenuControl : UserControl
{
    public MenuStrip MenuStrip { get; }

    public FileMenu FileMenu { get; }
    
    public EditMenu EditMenu { get; }
    
    public BrowseMenu BrowseMenu { get; }
    
    public ReadMenu ReadMenu { get; }
    
    public DisplayMenu DisplayMenu { get; }

    public HelpMenu HelpMenu { get; }

    public MainToolStrip ToolStrip { get; }

    public StatusStrip StatusStrip { get; }

    public PageContextMenu PageContextMenu { get; }

    public TabContextMenu TabContextMenu { get; }

    private MainController controller;

    #region MenuItem Property Forwarders
    public string ComicTitle => FileMenu.ComicTitle;
    public string TabTitle => FileMenu.TabTitle;

    public IEnumerable<ToolStripMenuItem> OpenNow => FileMenu.OpenNow;
    public IEnumerable<ToolStripMenuItem> Comics => EditMenu.Comics;

    public ToolStripMenuItem NextList => BrowseMenu.NextList;
    public ToolStripMenuItem PreviousList => BrowseMenu.PreviousList;

    public ToolStripMenuItem CheckUpdate => HelpMenu.CheckUpdate;
    #endregion

    public MainMenuControl()
    {
        //this.controller = controller;

        FileMenu = new FileMenu();
        EditMenu = new EditMenu();
        BrowseMenu = new BrowseMenu();
        ReadMenu = new ReadMenu();
        DisplayMenu = new DisplayMenu();
        HelpMenu = new HelpMenu();

        ToolStrip = new MainToolStrip();
        StatusStrip = new StatusStrip();
        PageContextMenu = new PageContextMenu();
        TabContextMenu = new TabContextMenu();

        InitializeComponent();

        MenuStrip = mainMenuStrip;
        //mainMenuStrip.MenuDeactivate += controller.OnMainMenuStripMenuDeactivate;
    }

    public void SetController(MainController controller)
    {
        this.controller = controller;
        FileMenu.SetController(controller);
        EditMenu.SetController(controller);
        BrowseMenu.SetController(controller);
        ReadMenu.SetController(controller);
        DisplayMenu.SetController(controller);
        HelpMenu.SetController(controller);

        ToolStrip.SetController(controller);
        StatusStrip.SetController(controller);
        PageContextMenu.SetController(controller);
        TabContextMenu.SetController(controller);
        controller.SetMenuControl(this);
    }

    public static implicit operator MenuStrip(MainMenuControl menu)
        => menu.mainMenuStrip;

    public void InitializeCommands()
    {
        FileMenu.InitializeCommands();
        EditMenu.InitializeCommands();
        ReadMenu.InitializeCommands();
    }

    public void InitializeKeyboard()
    {
        BrowseMenu.InitializeKeyboard();
        DisplayMenu.InitializeKeyboard();
        EditMenu.InitializeKeyboard();
        ReadMenu.InitializeKeyboard();
    }

    public void UpdateMenu(bool readerItemsVisible, string statusText)
    {
        DisplayMenu.UpdateMenu();
        PageContextMenu.UpdateMenu();
        StatusStrip.UpdateMenu(statusText);
        ToolStrip.UpdateMenu(readerItemsVisible);
    }

    public void UpdateWorkspaceMenus(ToolStripItemCollection items)
    {
        ToolStripSeparator toolStripSeparator = null;
        for (int num = items.Count - 1; num > 0; num--)
        {
            if (items[num] is ToolStripSeparator)
            {
                toolStripSeparator = items[num] as ToolStripSeparator;
                break;
            }
            items.RemoveAt(num);
        }
        if (toolStripSeparator != null)
        {
            toolStripSeparator.Visible = Program.Settings.Workspaces.Count > 0;
        }
        int num2 = 0;
        foreach (DisplayWorkspace workspace in Program.Settings.Workspaces)
        {
            DisplayWorkspace itemWs = workspace;
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(FormUtility.FixAmpersand(workspace.Name), null, delegate
            {
                MainController.Commands.SetWorkspace(CloneUtility.Clone(itemWs), remember: true);
            });
            if (num2 < 6)
            {
                toolStripMenuItem.ShortcutKeys = (Keys)(0x50000 | (112 + num2++));
            }
            items.Add(toolStripMenuItem);
        }
    }

    public void UpdateListConfigMenus(ToolStripItemCollection items)
    {
        items.RemoveAll((ToolStripItem c) => c.Tag is ListConfiguration);
        ToolStripSeparator toolStripSeparator = items.OfType<ToolStripSeparator>().LastOrDefault();
        if (toolStripSeparator != null)
        {
            toolStripSeparator.Visible = Program.Settings.ListConfigurations.Count > 0;
        }
        int num = 0;
        TR tR = TR.Load(base.Name);
        foreach (ListConfiguration listConfiguration in Program.Settings.ListConfigurations)
        {
            ListConfiguration itemCfg = listConfiguration;
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(StringUtility.Format(tR["SetLayoutMenu", "Set '{0}' Layout"], FormUtility.FixAmpersand(listConfiguration.Name)), null, delegate
            {
                MainController.Commands.SetListLayout(itemCfg.Config);
            });
            toolStripMenuItem.Tag = itemCfg;
            if (num < 6)
            {
                toolStripMenuItem.ShortcutKeys = (Keys)(0x50000 | (117 + num++));
            }
            items.Add(toolStripMenuItem);
        }
    }

    #region MenuItem Method Forwarders
    public void UpdateWorkspaceMenus()
    {
        BrowseMenu.UpdateWorkspaceMenus();
        ToolStrip.UpdateWorkspaceMenus();
    }

    public void ClearComicsList()
    {
        FileMenu.ClearOpenNow();
        PageContextMenu.ClearComics();
    }

    // BrowseMenu
    public void UpdateListConfigMenus()
        => BrowseMenu.UpdateWorkspaceMenus();

    // FileMenu
    public void CreatePluginMenuItems()
        => FileMenu.CreatePluginMenuItems();

    public void AddOpenNow(ToolStripMenuItem item)
        => FileMenu.AddOpenNow(item);

    // MainToolStrip
    public void OnPageDisplayModeChanged(object sender, EventArgs e)
        => ToolStrip.OnPageDisplayModeChanged(sender, e);

    // PageContextMenu
    public void AddComic(ToolStripMenuItem item)
        => PageContextMenu.AddComic(item);

    // StatusStrip
    public void UpdateCurrentPageImage()
        => StatusStrip.UpdateCurrentPageImage();

    public void UpdateActivityTimerTick(object sender, EventArgs e)
        => StatusStrip.UpdateActivityTimerTick(sender, e);
    #endregion

}
