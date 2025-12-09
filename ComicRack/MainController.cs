using cYo.Common;
using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.IO;
using cYo.Common.Localize;
using cYo.Common.Mathematics;
using cYo.Common.Net;
using cYo.Common.Runtime;
using cYo.Common.Text;
using cYo.Common.Threading;
using cYo.Common.Win32;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Common.Windows.Forms.Theme.Resources;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Controls;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.Display.Forms;
using cYo.Projects.ComicRack.Engine.Drawing;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Engine.IO.Cache;
using cYo.Projects.ComicRack.Engine.IO.Network;
using cYo.Projects.ComicRack.Engine.IO.Provider;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Controls;
using cYo.Projects.ComicRack.Viewer.Controls.MainForm;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Menus;
using cYo.Projects.ComicRack.Viewer.Properties;
using cYo.Projects.ComicRack.Viewer.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Windows.Forms;
using static cYo.Projects.ComicRack.Viewer.MainForm;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace cYo.Projects.ComicRack.Viewer;

public class MainController : IDisposable
{
    //private readonly LibraryManager library;
    //private readonly ViewManager views;
    //private readonly ImagePool imagePool;
    //private readonly NetworkManager network;
    private readonly MainForm main;

    private MainMenuControl menu;

    public async Task CheckForUpdateAsync(bool alwaysCheck = false)
        => main.CheckForUpdateAsync(alwaysCheck);

    #region Moved from MainForm
    public string[] RecentFiles = [];

    private readonly CommandMapper commands = new CommandMapper();
    #endregion

    #region Exposing MainForm properties
    public ComicDisplay ComicDisplay => main.ComicDisplay;
    public NavigatorManager OpenBooks => main.OpenBooks;

    public IEditPage GetPageEditor() => main.GetPageEditor();
    public IEditRating GetRatingEditor() => main.GetRatingEditor();

    private BookmarkEditorWrapper GetBookmarkEditor() => main.GetBookmarkEditor();
    #endregion

    #region MenuControl
    public IEnumerable<ToolStripMenuItem> OpenNow => menu.OpenNow;
    #endregion

    private readonly Dictionary<int, Image> pageRotationImages = new()
    {
        [0] = Resources.Rotate0Permanent,
        [1] = Resources.Rotate90Permanent,
        [2] = Resources.Rotate180Permanent,
        [3] = Resources.Rotate270Permanent
    };

    public MainController(
        //LibraryManager library,
        //ViewManager views,
        //ImagePool imagePool,
        //NetworkManager network,
        MainForm mainForm)
    {
        //this.library = library;
        //this.views = views;
        //this.imagePool = imagePool;
        //this.network = network;
        this.main = mainForm;
        EventHandlers.main = mainForm;
        Commands.main = mainForm;
    }

    public void SetMenuControl(MainMenuControl menu)
    {
        this.menu = menu;
        EventHandlers.menu = menu;
        Commands.menu = menu;

        EventHandlers.controller = this;
        Commands.controller = this;
    }

    // this now runs before MainForm.InitializeComponent()
    public DropDownHost<MagnifySetupControl> GetMagnifierDropDown()
    {
        DropDownHost<MagnifySetupControl> dropDownHost = new DropDownHost<MagnifySetupControl>();
        main.ComicDisplay.MagnifierOpacity = (dropDownHost.Control.MagnifyOpaque = Program.Settings.MagnifyOpaque);
        main.ComicDisplay.MagnifierSize = (dropDownHost.Control.MagnifySize = Program.Settings.MagnifySize);
        main.ComicDisplay.MagnifierZoom = (dropDownHost.Control.MagnifyZoom = Program.Settings.MagnifyZoom);
        main.ComicDisplay.MagnifierStyle = (dropDownHost.Control.MagnifyStyle = Program.Settings.MagnifyStyle);
        main.ComicDisplay.AutoMagnifier = (dropDownHost.Control.AutoMagnifier = Program.Settings.AutoMagnifier);
        main.ComicDisplay.AutoHideMagnifier = (dropDownHost.Control.AutoHideMagnifier = Program.Settings.AutoHideMagnifier);
        dropDownHost.Control.ValuesChanged += OnMagnifierSetupChanged;
        return dropDownHost;
    }

    private void OnMagnifierSetupChanged(object sender, EventArgs e)
    {
        MagnifySetupControl magnifySetupControl = (MagnifySetupControl)sender;
        main.ComicDisplay.MagnifierOpacity = magnifySetupControl.MagnifyOpaque;
        main.ComicDisplay.MagnifierSize = magnifySetupControl.MagnifySize;
        main.ComicDisplay.MagnifierZoom = magnifySetupControl.MagnifyZoom;
        main.ComicDisplay.MagnifierStyle = magnifySetupControl.MagnifyStyle;
        main.ComicDisplay.AutoHideMagnifier = magnifySetupControl.AutoHideMagnifier;
        main.ComicDisplay.AutoMagnifier = magnifySetupControl.AutoMagnifier;
    }

    #region Main Menu Strip
    public void OnMainMenuStripMenuDeactivate(object sender, EventArgs e)
        => main.OnMainMenuStripMenuDeactivate(sender, e);
    #endregion

    #region File Menu
    public void OnOpenRecent(object sender, EventArgs e)
    {
        string text = ((ToolStripMenuItem)sender).Text;
        int num = Convert.ToInt32(text.Substring(0, 2)) - 1;
        main.OpenSupportedFile(RecentFiles[num], Program.Settings.OpenInNewTab);
    }
    #endregion

    #region Edit Menu
    public void OnEditMenuBookmarksDropDownOpening(object sender, EventArgs e)
    {
        main.UpdateBookmarkMenu((sender as ToolStripMenuItem).DropDownItems, 0);
    }
    #endregion

    public EnumMenuUtility GetPageType(ToolStripDropDownItem tsdItem)
    {
        return new EnumMenuUtility(
            tsdItem,
            typeof(ComicPageType),
            flagsMode: false,
            images: null,
            Keys.A | Keys.Shift | Keys.Alt);
    }

    public EnumMenuUtility GetPageRotation(ToolStripDropDownItem tsdItem)
    {
        return new EnumMenuUtility(
            tsdItem,
            typeof(ImageRotation),
            flagsMode: false,
            images: pageRotationImages,
            Keys.D6 | Keys.Shift | Keys.Alt);
    }

    public static class EventHandlers
    {
        public static MainController controller;

        public static MainForm main;

        public static MainMenuControl menu;

        #region Edit Menu + Context Menu
        public static void OnPageTypeChanged(object sender, EventArgs e)
        {
            controller.GetPageEditor().PageType = (ComicPageType)(sender as EnumMenuUtility).Value;
        }

        public static void OnPageRotationChanged(object sender, EventArgs e)
        {
            controller.GetPageEditor().Rotation = (ImageRotation)(sender as EnumMenuUtility).Value;
        }
        #endregion

        #region Status Strip
        public static void OnDeviceSyncActivityClick(object sender, EventArgs e)
        {
            if (Program.QueueManager.DeviceSyncErrors.Count != 0)
                ShowErrorsDialog.ShowErrors(
                    main,
                    Program.QueueManager.DeviceSyncErrors,
                    ShowErrorsDialog.DeviceSyncErrorConverter);
            else
                main.ShowPendingTasks();
        }

        public static void OnExportActivityClick(object sender, EventArgs e)
        {
            if (Program.QueueManager.ExportErrors.Count != 0)
                ShowErrorsDialog.ShowErrors(
                    main,
                    Program.QueueManager.ExportErrors,
                    ShowErrorsDialog.ComicExporterConverter);
            else
                main.ShowPendingTasks();
        }

        public static void OnGenericActivityClick(object sender, EventArgs e)
            => main.ShowPendingTasks((int?)(sender as ToolStripStatusLabel).Tag ?? 0);

        public static void OnCurrentPageClick(object sender, EventArgs e)
        {
            Program.Settings.TrackCurrentPage = !Program.Settings.TrackCurrentPage;
        }
        #endregion

        #region MainToolStrip
        public static void OnToolStripPrevPageDropDownOpening(object sender, EventArgs e)
        {
            main.UpdateBookmarkMenu((sender as ToolStripSplitButton).DropDownItems, -1);
        }

        public static void OnToolStripNextPageDropDownOpening(object sender, EventArgs e)
        {
            main.UpdateBookmarkMenu((sender as ToolStripSplitButton).DropDownItems, 1);
        }

        public static void OnToolStripBookmarksDropDownOpening(object sender, EventArgs e)
        {
            main.UpdateBookmarkMenu((sender as ToolStripMenuItem).DropDownItems, 0);
        }
        #endregion

        #region Page Context Menu
        public static void OnPageContextMenuBookmarksDropDownOpening(object sender, EventArgs e)
        {
            main.UpdateBookmarkMenu((sender as ToolStripMenuItem).DropDownItems, 0);
        }
        #endregion
    }

    public static class Commands
    {
        public static MainController controller;

        public static MainForm main;

        public static MainMenuControl menu;

        public static void ToggleUndockReader()
            => main.ToggleUndockReader();

        public static bool OpenNextComic(int relative)
            => main.OpenNextComic(relative, OpenComicOptions.None);

        public static bool OpenNextComic()
            => OpenNextComic(1);

        public static bool OpenPrevComic()
            => OpenNextComic(-1);

        public static bool OpenRandomComic()
            => OpenNextComic(0);

        public static void ToggleBrowserFromReader()
        {
            if (!Program.ExtendedSettings.MouseSwitchesToFullLibrary && (main.ReaderUndocked || main.ViewDock == DockStyle.Fill))
                main.MinimalGui = !main.MinimalGui;
            else
                main.ToggleBrowser(alwaysShow: false);
        }

        public static void ToggleMinimalGui()
            => main.MinimalGui = !main.MinimalGui;

        public static void ToogleZoom(CommandKey key)
            => main.ToggleZoom(key);

        public static void ShowOpenDialog()
            => main.ShowOpenDialog(menu.ComicTitle);

        public static ComicBook AddNewBook(bool showDialog = true)
            => main.AddNewBook(showDialog);

        public static void AddFolderToLibrary()
            => main.AddFolderToLibrary();

        public static void ExportCurrentImage()
            => main.ExportCurrentImage();

        public static void UpdateComics()
            => main.UpdateComics();

        public static void GenerateFrontCoverCache()
        {
            Program.Database.Books.Concat(Program.BookFactory.TemporaryBooks).ForEach((ComicBook cb) =>
            {
                Program.ImagePool.GenerateFrontCoverThumbnail(cb);
            });
        }

        public static void SetBookmark()
            => main.SetBookmark();

        public static bool SetBookmarkAvailable()
        {
            return controller.GetBookmarkEditor().CanBookmark;
        }

        public static void RemoveBookmark()
        {
            controller.GetBookmarkEditor().Bookmark = string.Empty;
        }

        public static bool RemoveBookmarkAvailable()
        {
            return !string.IsNullOrEmpty(controller.GetBookmarkEditor().Bookmark);
        }

        public static void SyncBrowser()
            => main.SyncBrowser();

        public static void ShowPendingTasks()
            => main.ShowPendingTasks();

        public static void StartFullScan()
        {
            Program.QueueManager.StartScan(all: true, Program.Settings.RemoveMissingFilesOnFullScan);
        }

        public static void MenuSynchronizeDevices()
            => main.MenuSynchronizeDevices();

        public static void ShowAboutDialog()
            => main.ShowAboutDialog();

        public static void ShowInfo()
            => main.ShowInfo();

        public static void ShowNews()
            => main.ShowNews(always: true);

        public static void OpenRemoteLibrary()
            => main.OpenRemoteLibrary();

        public static void UpdateWebComics()
            => main.UpdateWebComics();

        public static void MenuRestart()
            => main.MenuRestart();

        public static void MenuClose()
            => main.MenuClose();

        public static void StartMouseDisabledTimer()
            => main.StartMouseDisabledTimer();

        public static void UpdateWorkspaceMenus(ToolStripItemCollection items)
            => menu.UpdateWorkspaceMenus(items);

        public static void UpdateWorkspaceMenus()
            => menu.UpdateWorkspaceMenus();

        public static void UpdateListConfigMenus(ToolStripItemCollection items)
            => menu.UpdateListConfigMenus(items);

        public static void UpdateListConfigMenus()
            => menu.UpdateListConfigMenus();

        public static void SetWorkspace(DisplayWorkspace workspace, bool remember)
            => main.SetWorkspace(workspace, remember);

        public static void SetListLayout(DisplayListConfig cfg)
            => main.SetListLayout(cfg);

        public static void Add(CommandHandler clickHandler, UpdateHandler enabledHandler, UpdateHandler checkedHandler, params object[] senders)
            => main.AddCommand(clickHandler, enabledHandler, checkedHandler, senders);

        public static void Add(CommandHandler clickHandler, bool isCheckedHandler, UpdateHandler updateHandler, params object[] senders)
            => main.AddCommand(clickHandler, isCheckedHandler, updateHandler, senders);

        public static void Add(CommandHandler clickHandler, UpdateHandler enableHandler, params object[] senders)
            => main.AddCommand(clickHandler, enableHandler, null, senders);

        public static void Add(CommandHandler ch, params object[] senders)
            => main.AddCommand(ch, null, senders);
    }

    public void Dispose()
    {
        if (main != null)
        {
            main.Dispose();
            //main = null;
        }
        if (menu != null)
        {
            menu.Dispose();
            menu = null;
        }
    }
}
