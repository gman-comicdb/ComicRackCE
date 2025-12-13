using cYo.Common.Collections;
using cYo.Common.Localize;
using cYo.Common.Text;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Controls.MainForm;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Manager;

internal static class ListManager
{
    public static IMain MainForm => Program.MainForm;

    //private static IMenuController Menu;

    public static void SetListLayout(DisplayListConfig listConfig)
    {
        IComicBrowser comicBrowser = Program.MainForm.FindActiveService<IComicBrowser>();
        if (comicBrowser != null)
            comicBrowser.ListConfig = listConfig;
    }

    public static void SetListLayoutToAll(DisplayListConfig listConfig = null)
    {
        if (!Program.AskQuestion(MainForm,
                TR.Messages["AskSetAllLists", "Are you sure you want to set all Lists to the current layout?"],
                TR.Messages["Set", "Set"],
                HiddenMessageBoxes.SetAllListLayouts))
            return;

        if (listConfig == null)
        {
            IComicBrowser comicBrowser = Program.MainForm.FindActiveService<IComicBrowser>();
            if (comicBrowser != null)
                listConfig = comicBrowser.ListConfig;
        }

        if (listConfig != null)
            Program.Database.ResetDisplayConfigs(listConfig);
    }

    public static void EditListLayout()
    {
        IComicBrowser comicBrowser = Program.MainForm.FindActiveService<IComicBrowser>();
        if (comicBrowser != null)
        {
            DisplayListConfig listConfig = comicBrowser.ListConfig;
            if (ListLayoutDialog.Show(MainForm,
                    listConfig,
                    listConfig.View.ItemViewMode,
                    (listConfig) => comicBrowser.ListConfig = listConfig))
                comicBrowser.ListConfig = listConfig;
        }
    }

    private static ListConfiguration CreateListLayout()
    {
        IComicBrowser comicBrowser = Program.MainForm.FindActiveService<IComicBrowser>();
        if (comicBrowser == null)
            return null;

        string name = SelectItemDialog.GetName(MainForm,
            TR.Messages["SaveListLayout", "Save List Layout"],
            TR.Default["Layout", "Layout"],
            Program.Settings.ListConfigurations);

        if (string.IsNullOrEmpty(name))
            return null;

        return new ListConfiguration(name)
        {
            Config = comicBrowser.ListConfig
        };
    }

    public static void SaveListLayout()
    {
        ListConfiguration listConfig = CreateListLayout();
        if (listConfig != null)
        {
            int index = Program.Settings.ListConfigurations.FindIndex(config => config.Name == listConfig.Name);
            if (index != -1)
            {
                Program.Settings.ListConfigurations[index] = listConfig;
                return;
            }
            Program.Settings.ListConfigurations.Add(listConfig);
            //UpdateListConfigMenus();
        }
    }

    public static void EditListLayouts()
    {
        if (Program.Settings.ListConfigurations.Count != 0)
        {
            IList<ListConfiguration> list = ListEditorDialog.Show(
                MainForm,
                TR.Messages["ListLayouts", "List Layouts"],
                Program.Settings.ListConfigurations, CreateListLayout,
                null,
                (ListConfiguration listConfig) => SetListLayout(listConfig.Config),
                (ListConfiguration listConfig) => SetListLayoutToAll(listConfig.Config));

            if (list != null)
            {
                Program.Settings.ListConfigurations.Clear();
                Program.Settings.ListConfigurations.AddRange(list);
                //UpdateListConfigMenus();
            }
        }
    }

    public static void UpdateListConfigMenus(ToolStripItemCollection items)
    {
        items.RemoveAll((ToolStripItem c) => c.Tag is ListConfiguration);
        ToolStripSeparator toolStripSeparator = items.OfType<ToolStripSeparator>().LastOrDefault();
        if (toolStripSeparator != null)
        {
            toolStripSeparator.Visible = Program.Settings.ListConfigurations.Count > 0;
        }
        int num = 0;
        //TR tR = TR.Load(base.Name);
        TR tR = TR.Load("");
        foreach (ListConfiguration listConfiguration in Program.Settings.ListConfigurations)
        {
            ListConfiguration itemCfg = listConfiguration;
            ToolStripMenuItem toolStripMenuItem = new(
                StringUtility.Format(tR["SetLayoutMenu", "Set '{0}' Layout"],
                FormUtility.FixAmpersand(listConfiguration.Name)),
                null,
                (_,_) => SetListLayout(itemCfg.Config));
            toolStripMenuItem.Tag = itemCfg;
            if (num < 6)
                toolStripMenuItem.ShortcutKeys = (Keys)(0x50000 | (117 + num++));

            items.Add(toolStripMenuItem);
        }
    }
}
