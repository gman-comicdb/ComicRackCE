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
using cYo.Projects.ComicRack.Viewer.Controllers;
using cYo.Projects.ComicRack.Viewer.Controls;
using cYo.Projects.ComicRack.Viewer.Controls.MainForm;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Manager;
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
using System.Windows.Forms;
using System.Windows.Input;
using CommandMapper = cYo.Projects.ComicRack.Viewer.Controllers.CommandMapper;

namespace cYo.Projects.ComicRack.Viewer;

public class MainController : IDisposable
{
    //private readonly LibraryManager library;
    //private readonly ViewManager views;
    //private readonly ImagePool imagePool;
    //private readonly NetworkManager network;
    // TODO : replace with interface
    private static MainForm MainForm => Program.MainForm;

    //private static ComicDisplay ComicDisplay => MainForm.ComicDisplay;

    private MainMenuControl menu;

    #region Moved from MainForm
    public static string[] RecentFiles = [];

    private readonly CommandMapper commands = new CommandMapper();
    #endregion

    #region Exposing MainForm properties
    public static ComicDisplay ComicDisplay => MainForm.ComicDisplay;

    public static IEditPage GetPageEditor() => MainForm.GetPageEditor();

    public static IEditRating GetRatingEditor() => MainForm.GetRatingEditor();

    private static IEditBookmark GetBookmarkEditor() => MainForm.GetBookmarkEditor();
    #endregion

    #region MenuControl
    public IEnumerable<ToolStripMenuItem> OpenNow => menu.OpenNow;
    #endregion


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
        //MainForm = mainForm;
        EventHandlers.main = mainForm;
        Commands.main = mainForm;

        new Controllers.CommandMapper(
            typeof(CommandAction),
            typeof(CommandAvailable),
            typeof(CommandVisible),
            typeof(CommandUpdate));
    }

    public void SetMenuControl(MainMenuControl menu)
    {
        this.menu = menu;
        EventHandlers.menu = menu;
        Commands.menu = menu;

        EventHandlers.controller = this;
        Commands.controller = this;
    }

    #region Main Menu Strip
    public void OnMainMenuStripMenuDeactivate(object sender, EventArgs e)
        => MainForm.OnMainMenuStripMenuDeactivate(sender, e);
    #endregion

    #region File Menu
    
    #endregion

    #region Edit Menu
    public void OnEditMenuBookmarksDropDownOpening(object sender, EventArgs e)
    {
        MainForm.UpdateBookmarkMenu((sender as ToolStripMenuItem).DropDownItems, 0);
    }
    #endregion

    public static class EventHandlers
    {
        public static MainController controller;

        public static MainForm main;

        public static MainMenuControl menu;

        #region Edit Menu + Context Menu
        public static void OnPageTypeChanged(object sender, EventArgs e)
        {
            MC.GetPageEditor().PageType = (ComicPageType)(sender as EnumMenuUtility).Value;
        }

        public static void OnPageRotationChanged(object sender, EventArgs e)
        {
            MC.GetPageEditor().Rotation = (ImageRotation)(sender as EnumMenuUtility).Value;
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

        public static void ToggleBrowserFromReader()
        {
            if (!Program.ExtendedSettings.MouseSwitchesToFullLibrary && (main.ReaderUndocked || main.ViewDock == DockStyle.Fill))
                main.MinimalGui = !main.MinimalGui;
            else
                main.ToggleBrowser(alwaysShow: false);
        }

        public static void ShowOpenDialog()
            => main.ShowOpenDialog(menu.ComicTitle);

        public static void StartFullScan()
        {
            Program.QueueManager.StartScan(all: true, Program.Settings.RemoveMissingFilesOnFullScan);
        }

        public static void StartMouseDisabledTimer()
            => main.StartMouseDisabledTimer();

        public static void SetWorkspace(DisplayWorkspace workspace, bool remember)
            => main.SetWorkspace(workspace, remember);

        public static void SetListLayout(DisplayListConfig cfg)
            => main.SetListLayout(cfg);

        public static bool AddRemoteLibrary(ShareInformation info, MainView.AddRemoteLibraryOptions options)
            => main.AddRemoteLibrary(info, options);
    }

    public void Dispose()
    {
        if (MainForm != null)
        {
            MainForm.Dispose();
            //main = null;
        }
        if (menu != null)
        {
            menu.Dispose();
            menu = null;
        }
    }
}
