using cYo.Common;
using cYo.Common.Collections;
using cYo.Common.Drawing;
using cYo.Common.Localize;
using cYo.Common.Runtime;
using cYo.Common.Text;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Controllers;
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
using Command = cYo.Projects.ComicRack.Viewer.Controllers.Command;
using Menu = cYo.Projects.ComicRack.Viewer.Controllers.Menu;
using StatusStrip = cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus.StatusStrip;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm;

public interface IMenuController
{
    MenuStrip MenuStrip { get; }

    //ToolStrip ToolStrip { get; }

    //System.Windows.Forms.StatusStrip StatusStrip { get; }

    //void SetController(MainController controller);

    //void InitializeCommands();

    //void InitializeKeyboard();

    //void UpdateMenuStrip();
}

public partial class MainMenuControl : UserControl, IMenuController
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

    public static MainController Controller { get; private set; }

    #region MenuItem Property Forwarders
    public string ComicTitle => FileMenu.ComicTitle;
    public string TabTitle => FileMenu.TabTitle;

    public IEnumerable<ToolStripMenuItem> OpenNow => FileMenu.OpenNow;
    public IEnumerable<ToolStripMenuItem> Comics => PageContextMenu.Comics;

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
        controller.SetMenuControl(this);
    }

    public static implicit operator MenuStrip(MainMenuControl menu)
        => menu.mainMenuStrip;

    public void UpdateMenu(bool readerItemsVisible, string statusText)
    {
        PageContextMenu.UpdateMenu();
        StatusStrip.UpdateMenu(statusText);
        ToolStrip.UpdateMenu(readerItemsVisible);
    }

    public static void InitializeMenuState(ToolStripMenuItem menu)
    {
        foreach (ToolStripItem item in menu.DropDownItems)
        {
            if (item.Tag is Command command)
                if (command.EventHandler != null)
                    item.Click += command.EventHandler;
                else
                    item.Click += (_, _) => command.Action();
        }
    }

    public static void UpdateMenuState(ToolStripMenuItem menu)
    {
        foreach (ToolStripItem item in menu.DropDownItems)
        {
            if (item.Tag is Command command)
            {
                item.Enabled = command.CanExecute();
                item.Visible = command.Show();

                command.UpdateHandler?.Invoke(item);
            }
        }
    }

    public static void OnToolStripMenuDropDownOpening(object sender, EventArgs e)
        => UpdateMenuState((ToolStripMenuItem)sender);
    

    public static EnumMenuUtility GetPageType(ToolStripDropDownItem tsItem)
    {
        return new EnumMenuUtility(
            tsItem,
            typeof(ComicPageType),
            flagsMode: false,
            images: null,
            Keys.A | Keys.Shift | Keys.Alt);
    }

    public static EnumMenuUtility GetPageRotation(ToolStripDropDownItem tsItem)
    {
        return new EnumMenuUtility(
            tsItem,
            typeof(ImageRotation),
            flagsMode: false,
            images: CommandIcons.PageRotationImages,
            Keys.D6 | Keys.Shift | Keys.Alt);
    }

    #region MenuItem Method Forwarders

    public void ClearComicsList()
    {
        FileMenu.ClearOpenNow();
        PageContextMenu.ClearComics();
    }

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
