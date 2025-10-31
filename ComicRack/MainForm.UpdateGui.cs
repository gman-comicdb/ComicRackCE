using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Localize;
using cYo.Common.Text;
using cYo.Common.Threading;
using cYo.Common.Windows;
using cYo.Common.Windows.Extensions;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Controls;
using cYo.Projects.ComicRack.Viewer.Views;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm : FormEx, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    #region Toggle
    public void ToggleBrowser(bool alwaysShow, IComicBrowser cb = null)
    {
        BrowserVisible = !BrowserVisible || alwaysShow;
        if (ReaderUndocked)
        {
            if (!BrowserVisible)
            {
                readerForm.Focus();
                return;
            }
            mainView.Focus();
            if (cb != null)
            {
                mainView.ShowLibrary(cb.Library);
            }
        }
        else if (!BrowserVisible)
        {
            UpdateBrowserVisibility();
        }
        else if (cb == null)
        {
            mainView.ShowLast();
        }
        else
        {
            mainView.ShowLibrary(cb.Library);
        }
    }

    public void ToggleBrowser()
    {
        ToggleBrowser(alwaysShow: false);
    }

    public void ToggleBrowserFromReader()
    {
        if (!Program.ExtendedSettings.MouseSwitchesToFullLibrary && (ReaderUndocked || mainViewContainer.Dock == DockStyle.Fill))
        {
            MinimalGui = !MinimalGui;
        }
        else
        {
            ToggleBrowser(alwaysShow: false);
        }
    }

    private void ToggleZoom(CommandKey key)
    {
        float num;
        if (ComicDisplay.ImageZoom < 1.05f)
        {
            num = lastZoom;
        }
        else
        {
            lastZoom = ComicDisplay.ImageZoom;
            num = 1f;
        }
        if (key.IsMouseButton())
        {
            ComicDisplay.ZoomTo(Point.Empty, num);
        }
        else
        {
            ComicDisplay.ImageZoom = num;
        }
    }

    public void ToggleSidebar()
    {
        ISidebar sidebar = this.FindActiveService<ISidebar>();
        if (sidebar != null)
        {
            sidebar.Visible = !sidebar.Visible;
        }
    }

    public void ToggleSmallPreview()
    {
        ISidebar sidebar = this.FindActiveService<ISidebar>();
        if (sidebar != null)
        {
            sidebar.Preview = !sidebar.Preview;
        }
    }

    public void ToggleInfoPanel()
    {
        ISidebar sidebar = this.FindActiveService<ISidebar>();
        if (sidebar != null)
        {
            sidebar.Info = !sidebar.Info;
        }
    }

    public void ToggleSearchBrowser()
    {
        ISearchOptions searchOptions = this.FindActiveService<ISearchOptions>();
        if (searchOptions != null)
        {
            searchOptions.SearchBrowserVisible = !searchOptions.SearchBrowserVisible;
        }
    }

    private void ToggleUndockReader()
    {
        if (!ReaderUndocked)
        {
            ComicDisplay.FullScreen = false;
        }
        ReaderUndocked = !ReaderUndocked;
    }
    #endregion

    #region Check
    private bool CheckSidebarAvailable()
    {
        return this.FindActiveService<ISidebar>() != null;
    }

    private bool CheckInfoPanelAvailable()
    {
        return this.FindActiveService<ISidebar>()?.HasInfoPanels ?? false;
    }

    private bool CheckSidebarEnabled()
    {
        return this.FindActiveService<ISidebar>()?.Visible ?? false;
    }

    private bool CheckInfoPanelEnabled()
    {
        return this.FindActiveService<ISidebar>()?.Info ?? false;
    }

    private bool CheckSmallPreviewEnabled()
    {
        return this.FindActiveService<ISidebar>()?.Preview ?? false;
    }

    private bool CheckSearchBrowserEnabled()
    {
        return this.FindActiveService<ISearchOptions>()?.SearchBrowserVisible ?? false;
    }

    private bool CheckSearchAvailable()
    {
        return this.FindActiveService<ISearchOptions>() != null;
    }

    private bool CheckViewOptionsAvailable()
    {
        return this.FindActiveService<IComicBrowser>() != null;
    }
    #endregion

    private void OnGuiVisibilities()
    {
        bool flag = !MinimalGui;
        bool flag2 = books.OpenCount > 0;
        miOpenNow.Enabled = miOpenNow.DropDownItems.Count > 0;
        cmComicsSep.Visible = miOpenNow.DropDownItems.Count > 0;
        if (ReaderUndocked)
        {
            fileTabsVisibility.Visible = flag;
            fileTabs.TopPadding = 2;
            mainView.TabBar.TopPadding = 6;
            mainView.TabBar.BottomPadding = 0;
            VisibilityAnimator visibilityAnimator = statusStripVisibility;
            bool visible = (MainToolStripVisible = true);
            visibilityAnimator.Visible = visible;
            enableAutoHideMenu = false;
            mainView.TabBarVisible = true;
            mainMenuStripVisibility.Visible = true;
        }
        else
        {
            bool expanded = mainViewContainer.Expanded;
            if (mainViewContainer.Dock == DockStyle.Fill)
            {
                fileTabsVisibility.Visible = false;
                MainToolStripVisible = false;
                bool flag4 = flag || !mainView.IsComicViewer || (ShowMainMenuNoComicOpen && !flag2);
                VisibilityAnimator visibilityAnimator2 = statusStripVisibility;
                bool visible = (mainView.TabBarVisible = flag4);
                visibilityAnimator2.Visible = visible;
                mainMenuStripVisibility.Visible = flag4 && (!AutoHideMainMenu || (ShowMainMenuNoComicOpen && !flag2));
                enableAutoHideMenu = !mainMenuStripVisibility.Visible && flag;
                mainView.TabBar.TopPadding = (mainMenuStripVisibility.Visible ? 2 : 6);
                mainView.TabBar.BottomPadding = (mainView.IsComicViewer ? 4 : 0);
            }
            else
            {
                flag = flag || expanded;
                MainToolStripVisible = true;
                bool flag6 = flag || (ShowMainMenuNoComicOpen && !flag2);
                VisibilityAnimator visibilityAnimator3 = statusStripVisibility;
                bool visible = (fileTabsVisibility.Visible = flag6);
                visibilityAnimator3.Visible = visible;
                mainMenuStripVisibility.Visible = flag6 && (!AutoHideMainMenu || (ShowMainMenuNoComicOpen && !flag2));
                enableAutoHideMenu = !mainMenuStripVisibility.Visible && flag;
                fileTabs.TopPadding = (mainMenuStripVisibility.Visible ? 2 : 6);
                fileTabs.BottomPadding = 2;
                mainView.TabBarVisible = true;
                mainView.TabBar.TopPadding = ((mainViewContainer.Dock != DockStyle.Bottom) ? fileTabs.TopPadding : 0);
                mainView.TabBar.BottomPadding = ((mainViewContainer.Dock != DockStyle.Bottom) ? fileTabs.BottomPadding : 0);
            }
        }
        fileTabs.PerformLayout();
        mainView.TabBar.PerformLayout();
        if (base.Visible)
        {
            mainViewContainer.Visible = mainViewContainer.Expanded || Program.Settings.AlwaysDisplayBrowserDockingGrip;
        }
    }

    private void UpdateBookmarkMenu(ToolStripItemCollection items, int direction)
    {
        for (int num = items.Count - 1; num >= 0; num--)
        {
            if ("bm".Equals(items[num].Tag))
            {
                items.RemoveAt(num);
            }
        }
        ToolStripItem toolStripItem = items.OfType<ToolStripItem>().FirstOrDefault((ToolStripItem ti) => "bms".Equals(ti.Tag));
        int num2 = items.IndexOf(toolStripItem) + 1;
        if (toolStripItem != null)
        {
            toolStripItem.Visible = false;
        }
        if (books.CurrentBook == null)
        {
            return;
        }
        int i = 0;
        int currentPage = books.CurrentBook.CurrentPage;
        var enumerable = from p in books.CurrentBook.Comic.Pages
                         select new
                         {
                             Page = i++,
                             Info = p
                         } into pi
                         where pi.Info.IsBookmark
                         select pi;
        if (direction < 0)
        {
            enumerable = enumerable.Reverse();
        }
        try
        {
            foreach (var item in enumerable)
            {
                var cpi = item;
                if ((direction >= 0 || cpi.Page >= currentPage) && (direction <= 0 || cpi.Page <= currentPage) && direction != 0)
                {
                    continue;
                }
                ToolStripMenuItem value = new ToolStripMenuItem(string.Format("{0} ({1} {2})", FormUtility.FixAmpersand(cpi.Info.Bookmark), TR.Default["Page", "Page"], cpi.Page + 1), null, delegate
                {
                    try
                    {
                        ComicDisplay.Book.Navigate(cpi.Page, PageSeekOrigin.Beginning, noFilter: true);
                    }
                    catch
                    {
                    }
                })
                {
                    Tag = "bm",
                    Enabled = (cpi.Page != currentPage)
                };
                items.Insert(num2++, value);
                if (toolStripItem != null)
                {
                    toolStripItem.Visible = true;
                }
            }
        }
        catch
        {
        }
    }

    private void OnUpdateGui()
    {
        UpdateQuickList();
        miOpenRecent.Enabled = recentFiles.Length != 0;
        string text = ((ComicDisplay.Book == null) ? null : ComicDisplay.Book.Caption.Ellipsis(60, "..."));
        tsBook.Text = (string.IsNullOrEmpty(text) ? None : text);
        if (readerForm != null && !MinimizedToTray)
        {
            readerForm.Visible = books.OpenCount > 0;
            readerForm.Text = tsBook.Text;
        }
        if (ComicDisplay.Book == null || string.IsNullOrEmpty(text))
        {
            Text = Application.ProductName;
        }
        else
        {
            Text = Application.ProductName + " - " + (ComicDisplay.Book.Comic.IsInContainer ? text : ComicDisplay.Book.Comic.FileName);
        }
        tsCurrentPage.Text = ((ComicDisplay.Book == null) ? NotAvailable : (ComicDisplay.Book.CurrentPage + 1).ToString());
        tsPageCount.Text = TotalPageInformation(ComicDisplay.Book);
        IComicBrowser comicBrowser = mainView.FindActiveService<IComicBrowser>();
        tsText.Text = FormUtility.FixAmpersand((comicBrowser != null) ? comicBrowser.SelectionInfo : string.Empty);
        tbFit.Image = GetFitModeImage();
        tbPageLayout.Image = GetLayoutImage();
        ToolStripMenuItem toolStripMenuItem = miMagnify;
        ToolStripMenuItem toolStripMenuItem2 = cmMagnify;
        Image image2 = (tbMagnify.Image = (ComicDisplay.MagnifierVisible ? zoomImage : zoomClearImage));
        Image image5 = (toolStripMenuItem.Image = (toolStripMenuItem2.Image = image2));
        ItemSizeInfo itemSizeInfo = this.FindActiveService<IItemSize>()?.GetItemSize();
        thumbSize.Visible = mainViewContainer.Expanded && itemSizeInfo != null;
        if (itemSizeInfo != null)
        {
            thumbSize.SetSlider(itemSizeInfo.Minimum, itemSizeInfo.Maximum, itemSizeInfo.Value);
        }
        ToolStripMenuItem toolStripMenuItem3 = miSynchronizeDevices;
        bool visible = (tsSynchronizeDevices.Visible = Program.Settings.Devices.Count > 0);
        toolStripMenuItem3.Visible = visible;
        ToolStripMenuItem toolStripMenuItem4 = readMenu;
        ToolStripSplitButton toolStripSplitButton = tbPrevPage;
        ToolStripSplitButton toolStripSplitButton2 = tbNextPage;
        ToolStripSeparator toolStripSeparator = toolStripSeparator5;
        ToolStripSplitButton toolStripSplitButton3 = tbPageLayout;
        ToolStripSplitButton toolStripSplitButton4 = tbFit;
        ToolStripSplitButton toolStripSplitButton5 = tbZoom;
        ToolStripSplitButton toolStripSplitButton6 = tbRotate;
        ToolStripSeparator toolStripSeparator2 = toolStripSeparator7;
        bool flag3 = (tbMagnify.Visible = IsComicVisible || ComicDisplay.Book != null);
        bool flag5 = (toolStripSeparator2.Visible = flag3);
        bool flag7 = (toolStripSplitButton6.Visible = flag5);
        bool flag9 = (toolStripSplitButton5.Visible = flag7);
        bool flag11 = (toolStripSplitButton4.Visible = flag9);
        bool flag13 = (toolStripSplitButton3.Visible = flag11);
        bool flag15 = (toolStripSeparator.Visible = flag13);
        bool flag17 = (toolStripSplitButton2.Visible = flag15);
        visible = (toolStripSplitButton.Visible = flag17);
        toolStripMenuItem4.Visible = visible;
    }

    private void UpdateQuickList()
    {
        quickOpenView.Visible = quickOpenView.Parent.Visible && Program.Settings.ShowQuickOpen && OpenBooks.CurrentBook == null && Program.Database.Books.Count > 0;
        if (!quickOpenView.Visible)
        {
            return;
        }
        if (!quickUpdateRegistered)
        {
            Program.Database.ComicListsChanged += (object s, ComicListItemChangedEventArgs e) =>
            {
                if (e.Change != ComicListItemChange.Statistic)
                {
                    quickListDirty = true;
                }
            };
            Program.Database.Books.Changed += QuickOpenBooksChanged;
            mainView.ViewAdded += delegate
            {
                quickListDirty = true;
            };
            mainView.ViewRemoved += delegate
            {
                quickListDirty = true;
            };
            quickUpdateRegistered = true;
        }
        while (quickListDirty)
        {
            quickListDirty = false;
            FillWithQuickOpenBooks();
        }
    }

    private void FillWithQuickOpenBooks()
    {
        if (this.InvokeIfRequired(FillWithQuickOpenBooks))
        {
            return;
        }
        List<ShareableComicListItem> list = (from cli in Program.Database.ComicLists.GetItems<ShareableComicListItem>()
                                             where cli.QuickOpen
                                             select cli.Clone() as ShareableComicListItem).ToList();
        if (list.Count == 0 || !Program.ExtendedSettings.ReplaceDefaultListsInQuickOpen)
        {
            defaultQuickOpenLists = defaultQuickOpenLists ?? new ShareableComicListItem[3]
            {
                    ComicLibrary.DefaultReadingList(Program.Database),
                    ComicLibrary.DefaultRecentlyReadList(Program.Database),
                    ComicLibrary.DefaultRecentlyAddedList(Program.Database)
            };
            list.AddRange(defaultQuickOpenLists);
        }
        quickOpenView.BeginUpdate();
        try
        {
            int num = 0;
            foreach (ShareableComicListItem item in list)
            {
                HashSet<ComicBook> list2 = new HashSet<ComicBook>(ComicBook.GuidEquality);
                using (IEnumerator<ComicLibrary> enumerator2 = mainView.GetLibraries(Program.ExtendedSettings.RemoteLibrariesInQuickOpen, Program.ExtendedSettings.OnlyLocalRemoteLibrariesInQuickOpen).GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        ComicLibrary comicLibrary = (item.Library = enumerator2.Current);
                        list2.AddRange(item.GetBooks());
                    }
                }
                quickOpenView.AddGroup(new GroupInfo(item.Name, num++), list2, Program.ExtendedSettings.QuickOpenListSize);
            }
        }
        finally
        {
            quickOpenView.EndUpdate();
        }
    }

    // Command
    public void StartFullScan()
    {
        Program.QueueManager.StartScan(all: true, Program.Settings.RemoveMissingFilesOnFullScan);
    }

    private void UpdateTabCaptions()
    {
        using (ItemMonitor.Lock(OpenBooks.Slots.SyncRoot))
        {
            foreach (TabBar.TabBarItem item in fileTabs.Items.Where((TabBar.TabBarItem t) => t.Tag is int && (int)t.Tag >= 0))
            {
                item.Text = OpenBooks.GetSlotCaption((int)item.Tag);
            }
            foreach (TabBar.TabBarItem fileTab in mainView.GetFileTabs())
            {
                fileTab.Text = OpenBooks.GetSlotCaption((int)fileTab.Tag);
            }
            foreach (ToolStripMenuItem dropDownItem in miOpenNow.DropDownItems)
            {
                if (dropDownItem.Tag is int)
                {
                    dropDownItem.Text = OpenBooks.GetSlotCaption((int)dropDownItem.Tag);
                }
            }
        }
    }

    private void UpdateBrowserVisibility()
    {
        if (!ReaderUndocked)
        {
            if (BrowserDock == DockStyle.Fill)
            {
                mainView.ShowView(books.CurrentSlot);
            }
            else if (Program.Settings.CloseBrowserOnOpen)
            {
                BrowserVisible = false;
            }
        }
    }

    #region ToolStrip Helpers
    private Image GetLayoutImage()
    {
        switch (ComicDisplay.PageLayout)
        {
            default:
                if (!ComicDisplay.RightToLeftReading)
                {
                    return miSinglePage.Image;
                }
                return SinglePageRtl;
            case PageLayoutMode.Double:
                if (!ComicDisplay.RightToLeftReading)
                {
                    return miTwoPages.Image;
                }
                return TwoPagesRtl;
            case PageLayoutMode.DoubleAdaptive:
                if (!ComicDisplay.RightToLeftReading)
                {
                    return miTwoPagesAdaptive.Image;
                }
                return TwoPagesAdaptiveRtl;
        }
    }

    private Image GetFitModeImage()
    {
        try
        {
            int imageFitMode = (int)ComicDisplay.ImageFitMode;
            foreach (ToolStripItem dropDownItem in tbFit.DropDownItems)
            {
                if (dropDownItem.Image != null && imageFitMode-- == 0)
                {
                    return dropDownItem.Image;
                }
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    private static string TotalPageInformation(ComicBookNavigator nav)
    {
        if (nav == null)
        {
            return NotAvailable;
        }
        if (nav.IsIndexRetrievalCompleted || nav.IndexPagesRetrieved == nav.Comic.PageCount)
        {
            return nav.Comic.PagesAsText;
        }
        return $"{nav.Comic.PagesAsText} ({nav.IndexPagesRetrieved})";
    }
    #endregion

    #region DesktopWindow
    private void MinimizeToTray()
    {
        if (shieldTray)
        {
            return;
        }
        shieldTray = true;
        try
        {
            if (readerForm != null)
            {
                readerForm.Visible = false;
            }
            base.Visible = false;
            notifyIcon.Visible = true;
        }
        finally
        {
            shieldTray = false;
        }
    }

    private void RestoreFromTray()
    {
        if (shieldTray)
        {
            return;
        }
        shieldTray = true;
        try
        {
            notifyIcon.Visible = false;
            if (readerForm != null)
            {
                readerForm.Visible = books.OpenCount > 0;
            }
            base.Visible = true;
            base.Bounds = SafeBounds;
            base.WindowState = oldState;
        }
        finally
        {
            shieldTray = false;
        }
    }

    public void RestoreToFront()
    {
        if (MinimizedToTray)
        {
            RestoreFromTray();
        }
        else if (base.WindowState == FormWindowState.Minimized)
        {
            base.WindowState = FormWindowState.Normal;
        }
        BringToFront();
        Activate();
    }
    #endregion

    #region App LifeCycle
    public void MenuClose()
    {
        menuClose = true;
        Close();
    }

    public void MenuRestart()
    {
        Program.Restart = true;
        MenuClose();
    }

    private void ControlExit()
    {
        if (Program.Settings.CloseMinimizesToTray)
        {
            MinimizeToTray();
        }
        else
        {
            Close();
        }
    }

    void IApplication.Restart()
    {
        MenuRestart();
    }

    void IApplication.ScanFolders()
    {
        StartFullScan();
    }
    #endregion
}
