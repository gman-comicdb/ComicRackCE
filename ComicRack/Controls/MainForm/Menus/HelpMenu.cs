using cYo.Common.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class HelpMenu : UserControl
{
    public ToolStripMenuItem Item;

    public HelpMenu()
    {
        InitializeComponent();
        Item = helpMenuItem;
    }

    public static implicit operator ToolStripMenuItem(HelpMenu menu)
        => menu.Item;

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
}
