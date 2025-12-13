using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.IO;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Controllers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Command = cYo.Projects.ComicRack.Viewer.Controllers.Command;
using Menu = cYo.Projects.ComicRack.Viewer.Controllers.Menu;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class FileMenu : UserControl
{
    public string ComicTitle => miOpenComic.Text.Replace("&", "");

    public string TabTitle => miAddTab.Text.Replace("&", string.Empty);

    public IEnumerable<ToolStripMenuItem> OpenNow => miOpenNow.DropDownItems.OfType<ToolStripMenuItem>();

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

    public Dictionary<ToolStripItem, Command> Items = [];

    public FileMenu()
    {
        Commands.ForEach(cmd => cmd.Menu = Menu.File);
        InitializeComponent();
        BindCommands();
        MainMenuControl.InitializeMenuState(this);

        miOpenRecent.DropDownOpening += OnRecentFilesDropDownOpening;
        miOpenRecent.DropDownItems.Add(new ToolStripMenuItem("dummy"));

        fileMenu.DropDownOpening += MainMenuControl.OnToolStripMenuDropDownOpening;
    }

    private void BindCommands()
    {
        miOpenComic.Tag = Command.Open;
        miCloseComic.Tag = Command.Close;
        miCloseAllComics.Tag = Command.CloseAll;
        miAddTab.Tag = Command.AddTab;
        miAddFolderToLibrary.Tag = Command.AddFolder;
        miScan.Tag = Command.ScanFolders;
        miUpdateAllComicFiles.Tag = Command.UpdateAllBooks;
        miUpdateWebComics.Tag = Command.UpdateWebComics;
        miCacheThumbnails.Tag = Command.GenerateCovers;
        miSynchronizeDevices.Tag = Command.SynchronizeDevices;
        miTasks.Tag = Command.ShowTasks;
        miFileAutomation.Tag = Command.Automation;
        miNewComic.Tag = Command.NewComic;
        miOpenRemoteLibrary.Tag = Command.OpenRemoteLibrary;
        miOpenNow.Tag = Command.OpenBooks;
        miOpenRecent.Tag = Command.RecentBooks;
        miRestart.Tag = Command.Restart;
        miExit.Tag = Command.Exit;
    }

    //protected override void OnLoad(EventArgs e)
    //{
    //    base.OnLoad(e);
    //    miOpenRecent.DropDownItems.Add(new ToolStripMenuItem("dummy"));
    //}

    public void CreatePluginMenuItems()
        => CreatePluginMenuItems(fileMenu, miFileAutomation, fileMenu.DropDownItems.IndexOf(miNewComic));

    public void CreatePluginMenuItems(ToolStripMenuItem menu, ToolStripMenuItem subMenu, int menuIndex)
    {
        subMenu.DropDownItems.AddRange(
            ScriptUtility.CreateToolItems<ToolStripMenuItem>(
                this, 
                PluginEngine.ScriptTypeLibrary,
                () => Program.Database.Books)
            .ToArray());

        ToolStripMenuItem[] newBookPlugins = ScriptUtility.CreateToolItems<ToolStripMenuItem>(
            this,
            PluginEngine.ScriptTypeNewBooks,
            () => Program.Database.Books)
            .ToArray();

        foreach (ToolStripMenuItem plugin in newBookPlugins)
            menu.DropDownItems.Insert(++menuIndex, plugin);

        foreach (Plugins.Command command in ScriptUtility.Scripts.GetCommands(PluginEngine.ScriptTypeDrawThumbnailOverlay))
        {
            command.PreCompile();
            CoverViewItem.DrawCustomThumbnailOverlay += (ComicBook comic, Graphics graphics, Rectangle bounds, int flags) =>
            {
                command.Invoke(new object[4]
                {
                        comic,
                        graphics,
                        bounds,
                        flags
                }, catchErrors: true);
            };
        }
    }

    public void OnRecentFilesDropDownOpening(object sender, EventArgs e)
    {
        int num = 0;
        foreach (ToolStripMenuItem dropDownItem in miOpenRecent.DropDownItems)
            if (dropDownItem.Image != null)
                dropDownItem.Image.Dispose();
        FormUtility.SafeToolStripClear(miOpenRecent.DropDownItems);

        foreach (string path in MC.RecentFiles)
        {
            if (!File.Exists(path))
                continue;

            string displayPath = ++num + " - " + FormUtility.FixAmpersand(FileUtility.GetSafeFileName(path));
            using (IItemLock<ThumbnailImage> itemLock = Program.ImagePool.Thumbs.GetImage(Program.BookFactory.Create(path, CreateBookOption.DoNotAdd).GetFrontCoverThumbnailKey()))
            {
                try
                {
                    ToolStripMenuItem value = new(
                        displayPath,
                        (itemLock != null && itemLock.Item != null) ? itemLock.Item.Bitmap.Resize(16, 16) : null,
                        OnOpenRecent
                        );
                    miOpenRecent.DropDownItems.Add(value);
                }
                catch (Exception)
                {
                }
            }
        }
    }

    public void OnOpenRecent(object sender, EventArgs e)
    {
        string displayPath = ((ToolStripMenuItem)sender).Text;
        int index = Convert.ToInt32(displayPath.Substring(0, 2)) - 1;
        Program.MainForm.OpenSupportedFile(MC.RecentFiles[index], Program.Settings.OpenInNewTab);
    }

    public void ClearOpenNow()
    {
        FormUtility.SafeToolStripClear(miOpenNow.DropDownItems);
    }

    public void AddOpenNow(ToolStripMenuItem item)
    {
        miOpenNow.DropDownItems.Add(item);
    }

    public static implicit operator ToolStripMenuItem(FileMenu menu) => menu.fileMenu;
}
