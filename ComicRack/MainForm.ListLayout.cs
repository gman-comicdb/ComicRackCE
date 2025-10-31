using cYo.Common.Collections;
using cYo.Common.Localize;
using cYo.Common.Text;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Views;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm : FormEx, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    private void UpdateListConfigMenus()
    {
        UpdateListConfigMenus(miListLayouts.DropDownItems);
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
                SetListLayout(itemCfg.Config);
            });
            toolStripMenuItem.Tag = itemCfg;
            if (num < 6)
            {
                toolStripMenuItem.ShortcutKeys = (Keys)(0x50000 | (117 + num++));
            }
            items.Add(toolStripMenuItem);
        }
    }

    public void SetListLayout(DisplayListConfig cfg)
    {
        IComicBrowser comicBrowser = this.FindActiveService<IComicBrowser>();
        if (comicBrowser != null)
        {
            comicBrowser.ListConfig = cfg;
        }
    }

    public void SetListLayoutToAll(DisplayListConfig dlc = null)
    {
        if (!Program.AskQuestion(this, TR.Messages["AskSetAllLists", "Are you sure you want to set all Lists to the current layout?"], TR.Messages["Set", "Set"], HiddenMessageBoxes.SetAllListLayouts))
        {
            return;
        }
        if (dlc == null)
        {
            IComicBrowser comicBrowser = this.FindActiveService<IComicBrowser>();
            if (comicBrowser != null)
            {
                dlc = comicBrowser.ListConfig;
            }
        }
        if (dlc != null)
        {
            Program.Database.ResetDisplayConfigs(dlc);
        }
    }

    public void EditListLayout()
    {
        IComicBrowser cb = this.FindActiveService<IComicBrowser>();
        if (cb != null)
        {
            DisplayListConfig cfg = cb.ListConfig;
            if (ListLayoutDialog.Show(this, cfg, cfg.View.ItemViewMode, delegate
            {
                cb.ListConfig = cfg;
            }))
            {
                cb.ListConfig = cfg;
            }
        }
    }

    private ListConfiguration CreateListLayout()
    {
        IComicBrowser comicBrowser = this.FindActiveService<IComicBrowser>();
        if (comicBrowser == null)
        {
            return null;
        }
        string name = SelectItemDialog.GetName(this, TR.Messages["SaveListLayout", "Save List Layout"], TR.Default["Layout", "Layout"], Program.Settings.ListConfigurations);
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }
        return new ListConfiguration(name)
        {
            Config = comicBrowser.ListConfig
        };
    }

    public void SaveListLayout()
    {
        ListConfiguration cfg = CreateListLayout();
        if (cfg != null)
        {
            int num = Program.Settings.ListConfigurations.FindIndex((ListConfiguration c) => c.Name == cfg.Name);
            if (num != -1)
            {
                Program.Settings.ListConfigurations[num] = cfg;
                return;
            }
            Program.Settings.ListConfigurations.Add(cfg);
            UpdateListConfigMenus();
        }
    }

    public void EditListLayouts()
    {
        if (Program.Settings.ListConfigurations.Count != 0)
        {
            IList<ListConfiguration> list = ListEditorDialog.Show(Form.ActiveForm ?? this, TR.Messages["ListLayouts", "List Layouts"], Program.Settings.ListConfigurations, CreateListLayout, null, (ListConfiguration elc) =>
            {
                SetListLayout(elc.Config);
            }, (ListConfiguration elc) =>
            {
                SetListLayoutToAll(elc.Config);
            });
            if (list != null)
            {
                Program.Settings.ListConfigurations.Clear();
                Program.Settings.ListConfigurations.AddRange(list);
                UpdateListConfigMenus();
            }
        }
    }
}
