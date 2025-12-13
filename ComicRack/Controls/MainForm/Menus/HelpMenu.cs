using cYo.Common.Collections;
using cYo.Projects.ComicRack.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Command = cYo.Projects.ComicRack.Viewer.Controllers.Command;
using Menu = cYo.Projects.ComicRack.Viewer.Controllers.Menu;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class HelpMenu : UserControl
{
    public ToolStripMenuItem CheckUpdate => miCheckUpdate;

    private static readonly Command Separator = Command.None;

    public IList<Command> Commands =
    [
        Command.Help,
        Command.WebHelp,
        Separator,
        Command.Plugins,
        Separator,
        Command.ChooseHelp,
        Separator,
        Command.QuickIntro,
        Separator,
        Command.WebHomepage,
        Command.WebUserForum,
        Separator,
        Command.ShowNews,
        Command.CheckUpdate,
        Separator,
        Command.ShowAbout
    ];

    public HelpMenu()
    {
        Commands.ForEach(cmd => cmd.Menu = Menu.File);
        InitializeComponent();
        BindCommands();
        MainMenuControl.InitializeMenuState(this);

        InitializeHelp(Program.Settings.HelpSystem);
        Program.Settings.HelpSystemChanged += OnHelpSystemChanged;
        InitializePluginHelp();
    }

    private void BindCommands()
    {
        miHelp.Tag = Command.Help;
        miWebHelp.Tag = Command.WebHelp;
        miHelpPlugins.Tag = Command.Plugins;
        miChooseHelpSystem.Tag = Command.ChooseHelp;
        miHelpQuickIntro.Tag = Command.QuickIntro;

        miWebHome.Tag = Command.WebHomepage;
        miWebUserForum.Tag = Command.WebUserForum;

        miNews.Tag = Command.ShowNews;
        miCheckUpdate.Tag = Command.CheckUpdate;

        miAbout.Tag = Command.ShowAbout;
    }

    //protected override void OnLoad(EventArgs e)
    //{
    //    InitializeHelp(Program.Settings.HelpSystem);
    //    Program.Settings.HelpSystemChanged += OnHelpSystemChanged;
    //    InitializePluginHelp();
    //}

    public void InitializeHelp(string helpSystem)
    {
        Program.HelpSystem = helpSystem;
        miWebHelp.DropDownItems.Clear();
        miHelp.Visible = false;
        if (miHelp.DropDownItems.Contains(miWebHelp))
        {
            miHelp.DropDownItems.Remove(miWebHelp);
            helpMenuItem.DropDownItems.Insert(helpMenuItem.DropDownItems.IndexOf(miHelp) + 1, miWebHelp);
        }
        miHelp.DropDownItems.Clear();
        ToolStripItem[] array = Program.Help.GetCustomHelpMenu().ToArray();
        if (array.Length != 0)
        {
            helpMenuItem.DropDownItems.Remove(miWebHelp);
            miHelp.Visible = true;
            miHelp.DropDownItems.Add(miWebHelp);
            miHelp.DropDownItems.Add(new ToolStripSeparator());
            miHelp.DropDownItems.AddRange(array);
        }
        IEnumerable<string> helpSystems = Program.HelpSystems;
        miChooseHelpSystem.Visible = helpSystems.Count() > 1;
        miChooseHelpSystem.DropDownItems.Clear();
        foreach (string item in helpSystems)
        {
            string name = item;
            ((ToolStripMenuItem)miChooseHelpSystem.DropDownItems.Add(name, null, delegate
            {
                Program.Settings.HelpSystem = name;
            })).Checked = Program.HelpSystem == name;
        }
    }

    public void InitializePluginHelp()
    {
        IEnumerable<PackageManager.Package> packages = from package in Program.ScriptPackages.GetPackages()
                                                         where !string.IsNullOrEmpty(package.HelpLink)
                                                         select package;
        miHelpPlugins.Visible = packages.Count() > 0;
        foreach (PackageManager.Package package in packages)
        {
            miHelpPlugins.DropDownItems.Add(package.Name, package.Image, delegate
            {
                Program.StartDocument(package.HelpLink, package.PackagePath);
            });
        }
    }

    public void OnHelpSystemChanged(object sender, EventArgs e)
    {
        InitializeHelp(Program.Settings.HelpSystem);
    }

    public static implicit operator ToolStripMenuItem(HelpMenu menu) => menu.helpMenuItem;
}
