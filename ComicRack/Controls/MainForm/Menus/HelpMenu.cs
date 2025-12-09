using cYo.Projects.ComicRack.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static cYo.Projects.ComicRack.Viewer.MainController;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class HelpMenu : UserControl
{
    private MainController controller;

    public ToolStripMenuItem CheckUpdate => miCheckUpdate;

    public HelpMenu()
    {
        //this.controller = controller;
        InitializeComponent();
    }

    public void SetController(MainController controller)
    {
        this.controller = controller;
        InitializeHelp(Program.Settings.HelpSystem);
        Program.Settings.HelpSystemChanged += OnHelpSystemChanged;
        InitializePluginHelp();
    }

    public static implicit operator ToolStripMenuItem(HelpMenu menu)
        => menu.helpMenuItem;

    //protected override void OnLoad(EventArgs e)
    //{
    //    InitializeHelp(Program.Settings.HelpSystem);
    //    Program.Settings.HelpSystemChanged += OnHelpSystemChanged;
    //    InitializePluginHelp();
    //}

    public void InitializeCommands()
    {

    }

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
        IEnumerable<PackageManager.Package> enumerable = from p in Program.ScriptPackages.GetPackages()
                                                         where !string.IsNullOrEmpty(p.HelpLink)
                                                         select p;
        miHelpPlugins.Visible = enumerable.Count() > 0;
        foreach (PackageManager.Package p2 in enumerable)
        {
            miHelpPlugins.DropDownItems.Add(p2.Name, p2.Image, delegate
            {
                Program.StartDocument(p2.HelpLink, p2.PackagePath);
            });
        }
    }

    public void OnHelpSystemChanged(object sender, EventArgs e)
    {
        InitializeHelp(Program.Settings.HelpSystem);
    }
}
