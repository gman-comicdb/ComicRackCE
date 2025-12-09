using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.IO;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class FileMenu : UserControl
{
    private MainController controller;

    public string ComicTitle => miOpenComic.Text.Replace("&", "");

    public string TabTitle => miAddTab.Text.Replace("&", string.Empty);

    public IEnumerable<ToolStripMenuItem> OpenNow => miOpenNow.DropDownItems.OfType<ToolStripMenuItem>();

    //public FileMenu(MainController controller)
    public FileMenu()
    {
        //this.controller = controller;
        InitializeComponent();
    }

    public void SetController(MainController controller)
    {
        this.controller = controller;
        fileMenuItem.DropDownOpening += OnMenuDropDownOpening;
        miOpenRecent.DropDownOpening += OnRecentFilesDropDownOpening;
        miOpenRecent.DropDownItems.Add(new ToolStripMenuItem("dummy"));
    }

    public static implicit operator ToolStripMenuItem(FileMenu menu)
        => menu.fileMenuItem;

    //protected override void OnLoad(EventArgs e)
    //{
    //    base.OnLoad(e);
    //    miOpenRecent.DropDownItems.Add(new ToolStripMenuItem("dummy"));
    //}

    public void CreatePluginMenuItems()
    {
        miFileAutomation.DropDownItems.AddRange(ScriptUtility.CreateToolItems<ToolStripMenuItem>(this, PluginEngine.ScriptTypeLibrary, () => Program.Database.Books).ToArray());
        miFileAutomation.Visible = miFileAutomation.DropDownItems.Count != 0;
        int num = fileMenuItem.DropDownItems.IndexOf(miNewComic);
        ToolStripMenuItem[] array = ScriptUtility.CreateToolItems<ToolStripMenuItem>(this, PluginEngine.ScriptTypeNewBooks, () => Program.Database.Books).ToArray();
        foreach (ToolStripMenuItem value in array)
        {
            fileMenuItem.DropDownItems.Insert(++num, value);
        }
        foreach (Command sc in ScriptUtility.Scripts.GetCommands(PluginEngine.ScriptTypeDrawThumbnailOverlay))
        {
            sc.PreCompile();
            CoverViewItem.DrawCustomThumbnailOverlay += (ComicBook comic, Graphics graphics, Rectangle bounds, int flags) =>
            {
                sc.Invoke(new object[4]
                {
                        comic,
                        graphics,
                        bounds,
                        flags
                }, catchErrors: true);
            };
        }
    }

    public void OnMenuDropDownOpening(object sender, EventArgs e)
    {
        miOpenRecent.Enabled = controller.RecentFiles.Length != 0;
        miOpenNow.Enabled = miOpenNow.DropDownItems.Count > 0;

        miUpdateAllComicFiles.Visible = !Program.Settings.AutoUpdateComicsFiles;
        miFileAutomation.Visible = miFileAutomation.DropDownItems.Count != 0;
        miSynchronizeDevices.Visible = Program.Settings.Devices.Count > 0;
    }

    public void OnRecentFilesDropDownOpening(object sender, EventArgs e)
    {
        int num = 0;
        foreach (ToolStripMenuItem dropDownItem in miOpenRecent.DropDownItems)
            if (dropDownItem.Image != null)
                dropDownItem.Image.Dispose();
        FormUtility.SafeToolStripClear(miOpenRecent.DropDownItems);

        foreach (string path in controller.RecentFiles)
        {
            if (!File.Exists(path))
                continue;

            string displayPath = ++num + " - " + FormUtility.FixAmpersand(FileUtility.GetSafeFileName(path));
            using (IItemLock<ThumbnailImage> itemLock = Program.ImagePool.Thumbs.GetImage(Program.BookFactory.Create(path, CreateBookOption.DoNotAdd).GetFrontCoverThumbnailKey()))
            {
                try
                {
                    ToolStripMenuItem value = new(displayPath, (itemLock != null && itemLock.Item != null) ? itemLock.Item.Bitmap.Resize(16, 16) : null, controller.OnOpenRecent);
                    miOpenRecent.DropDownItems.Add(value);
                }
                catch (Exception)
                {
                }
            }
        }
    }

    public void InitializeCommands()
    {
        MainController.Commands.Add(MainController.Commands.ShowOpenDialog, miOpenComic);
        MainController.Commands.Add(controller.OpenBooks.Close, () => controller.OpenBooks.Slots.Count > 0, miCloseComic);
        MainController.Commands.Add(controller.OpenBooks.CloseAll, () => controller.OpenBooks.Slots.Count > 0, miCloseAllComics);
        MainController.Commands.Add(controller.OpenBooks.AddSlot, miAddTab);
        MainController.Commands.Add(() => MainController.Commands.AddNewBook(), miNewComic);

        MainController.Commands.Add(MainController.Commands.AddFolderToLibrary, miAddFolderToLibrary);
        MainController.Commands.Add(MainController.Commands.StartFullScan, miScan);
        MainController.Commands.Add(MainController.Commands.UpdateComics, miUpdateAllComicFiles);
        MainController.Commands.Add(MainController.Commands.GenerateFrontCoverCache, miCacheThumbnails);
        MainController.Commands.Add(MainController.Commands.MenuSynchronizeDevices, miSynchronizeDevices);
        MainController.Commands.Add(MainController.Commands.UpdateWebComics, miUpdateWebComics);
        MainController.Commands.Add(() => MainController.Commands.ShowPendingTasks(), miTasks);
        MainController.Commands.Add(MainController.Commands.MenuRestart, miRestart);
        MainController.Commands.Add(MainController.Commands.MenuClose, miExit);
        MainController.Commands.Add(MainController.Commands.OpenRemoteLibrary, miOpenRemoteLibrary);
    }

    public void ClearOpenNow()
    {
        FormUtility.SafeToolStripClear(miOpenNow.DropDownItems);
    }

    public void AddOpenNow(ToolStripMenuItem item)
    {
        miOpenNow.DropDownItems.Add(item);
    }
}
