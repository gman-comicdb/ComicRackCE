using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.IO;
using cYo.Common.Localize;
using cYo.Common.Mathematics;
using cYo.Common.Runtime;
using cYo.Common.Text;
using cYo.Common.Threading;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Controls;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Views;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm : FormEx, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    private void SettingsChanged(object sender, EventArgs e)
    {
        UpdateSettings();
    }

    private void OnBookOpened(object sender, BookEventArgs e)
    {
        if (Program.Settings.TrackCurrentPage)
        {
            e.Book.OpenedTime = DateTime.Now;
        }
        e.Book.NewPages = 0;
        controller.RecentFiles = Program.Database.GetRecentFiles(Settings.RecentFileCount).ToArray();
        if (e.Book.EditMode.IsLocalComic())
        {
            Win7.UpdateRecent(e.Book.FilePath);
        }
        UpdateBrowserVisibility();
        ScriptUtility.Invoke(PluginEngine.ScriptTypeBookOpened, e.Book);
        string url = e.Book.FilePath;
        Win7.AddTabbedThumbnail(this, e.Book.FilePath, delegate
        {
            books.CurrentSlot = books.Slots.FindIndex((ComicBookNavigator s) => s.Comic.FilePath == url);
        }, delegate
        {
            books.Close(books.Slots.FindIndex((ComicBookNavigator s) => s.Comic.FilePath == url));
        }, delegate
        {
            ComicBookNavigator comicBookNavigator = books.Slots.FirstOrDefault((ComicBookNavigator s) => s.Comic.FilePath == url);
            return (comicBookNavigator == books.CurrentBook) ? ComicDisplay.CreateThumbnail() : comicBookNavigator.Thumbnail;
        });
    }

    private void OnBookClosing(object sender, BookEventArgs e)
    {
        if (Program.Settings.AutoShowQuickReview && e.Book != null && e.Book.HasBeenRead && e.Book.Rating == 0f)
        {
            new RatingEditor(Form.ActiveForm ?? this, ListExtensions.AsEnumerable<ComicBook>(e.Book)).QuickRatingAndReview();
        }
    }

    private void OnBookClosed(object sender, BookEventArgs e)
    {
        Program.ImagePool.SlowPageQueue.RemoveItems((PageKey k) => k.Location == e.Book.FilePath);
        Program.ImagePool.FastPageQueue.RemoveItems((PageKey k) => k.Location == e.Book.FilePath);
        Program.QueueManager.AddBookToFileUpdate(e.Book);
        Win7.RemoveThumbnail(e.Book.FilePath);
    }

    private void ComicDisplay_PageChanged(object sender, BookPageEventArgs e)
    {
        Win7.InvalidateThumbnail(OpenBooks.CurrentBook.Comic.FilePath);
    }

    internal void MouseDownHandler(object sender, MouseEventArgs e)
    {
        if (!mainKeys.HandleKey(e.Button, false, false))
        {
            base.OnMouseDown(e);
        }
    }

    private void BookDragEnter(object sender, DragEventArgs e)
    {
        string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
        e.Effect = ((array != null && array.Length == 1) ? DragDropEffects.Copy : DragDropEffects.None);
    }

    private void BookDragDrop(object sender, DragEventArgs e)
    {
        string[] array = (string[])e.Data.GetData(DataFormats.FileDrop);
        OpenSupportedFile(array[0]);
    }

    private void ReaderFormFormClosing(object sender, FormClosingEventArgs e)
    {
        if (!shieldReaderFormClosing)
        {
            try
            {
                shieldReaderFormClosing = true;
                ReaderUndocked = false;
            }
            finally
            {
                shieldReaderFormClosing = false;
            }
        }
    }

    private void ReaderFormKeyDown(object sender, KeyEventArgs e)
    {
        e.Handled = commands.InvokeKey(e.KeyData);
    }

    private void RebuildBookTabs()
    {
        using (ItemMonitor.Lock(OpenBooks.Slots.SyncRoot))
        {
            mainMenuStrip.SuspendLayout();
            fileTabs.SuspendLayout();
            for (int num = fileTabs.Items.Count - 1; num >= 0; num--)
            {
                if (fileTabs.Items[num].Tag != null)
                {
                    fileTabs.Items.RemoveAt(num);
                }
            }
            menu.ClearComicsList();
            mainView.ClearFileTabs();
            Bitmap thumb = default(Bitmap);
            for (int i = 0; i < OpenBooks.Slots.Count; i++)
            {
                string text = FormUtility.FixAmpersand(OpenBooks.GetSlotCaption(i));
                ComicBookNavigator nav = OpenBooks.Slots[i];
                string text2 = text;
                string text3 = null;
                KeysConverter keysConverter = new KeysConverter();
                ToolStripMenuItem tmi = new ToolStripMenuItem(text);
                tmi.Click += OpenBooks_Clicked;
                tmi.Tag = i;
                if (i < 12)
                {
                    tmi.ShortcutKeys = (Keys)(0x60000 | (112 + i));
                    text3 = keysConverter.ConvertToString(tmi.ShortcutKeys);
                    text2 = text2 + "\r\n(" + text3 + ")";
                }
                menu.AddOpenNow(tmi);
                ToolStripMenuItem tmi2 = new ToolStripMenuItem(text);
                tmi2.Click += OpenBooks_Clicked;
                tmi2.Tag = i;
                tmi2.ShortcutKeys = tmi.ShortcutKeys;
                menu.AddComic(tmi2);
                TabBar.TabBarItem tbi = new ComicReaderTab(text, nav, Font, text3)
                {
                    Tag = i,
                    MinimumWidth = 100,
                    CanClose = true,
                    ToolTipText = text2,
                    ContextMenu = tabContextMenu
                };
                if (nav == null)
                {
                    tbi.Image = emptyTabImage;
                }
                tbi.Selected += OpenBooks_Selected;
                tbi.CloseClick += btn_CloseClick;
                tbi.CaptionClick += tbi_CaptionClick;
                fileTabs.Items.Add(tbi);
                TabBar.TabBarItem tbi2 = new ComicReaderTab(text, nav, Font, text3)
                {
                    Image = emptyTabImage,
                    Tag = i,
                    MinimumWidth = 100,
                    CanClose = true,
                    ToolTipText = text2,
                    ContextMenu = tabContextMenu,
                    Visible = (ViewDock == DockStyle.Fill)
                };
                if (nav == null)
                {
                    tbi2.Image = emptyTabImage;
                }
                tbi2.Selected += OpenBooks_Selected;
                tbi2.CloseClick += btn_CloseClick;
                tbi2.CaptionClick += tbi_CaptionClick;
                mainView.AddFileTab(tbi2);
                ThreadUtility.RunInBackground("Create tab thumbnails", delegate
                {
                    try
                    {
                        using (IItemLock<ThumbnailImage> itemLock = Program.ImagePool.GetThumbnail(nav.Comic))
                        {
                            thumb = itemLock.Item.Bitmap.Resize(16, 16);
                        }
                        this.Invoke(delegate
                        {
                            if (!tmi.IsDisposed)
                            {
                                tmi.Image = thumb;
                            }
                            if (!tmi2.IsDisposed)
                            {
                                tmi2.Image = thumb;
                            }
                            tbi.Image = thumb;
                            tbi2.Image = thumb;
                        });
                    }
                    catch
                    {
                    }
                });
            }
            string text4 = menu.TabTitle;
            TabBar.TabBarItem tabBarItem = new TabBar.TabBarItem(text4)
            {
                Tag = -1,
                Image = addTabImage,
                MinimumWidth = 32,
                ShowText = false,
                ToolTipText = text4,
                AdjustWidth = false
            };
            tabBarItem.Click += delegate
            {
                OpenBooks.AddSlot();
                OpenBooks.CurrentSlot = OpenBooks.Slots.Count - 1;
            };
            fileTabs.Items.Add(tabBarItem);
            tabBarItem = new TabBar.TabBarItem(text4)
            {
                Tag = -1,
                Image = addTabImage,
                MinimumWidth = 32,
                AdjustWidth = false,
                ShowText = false,
                ToolTipText = text4,
                Visible = (ViewDock == DockStyle.Fill)
            };
            tabBarItem.Click += delegate
            {
                OpenBooks.AddSlot();
                OpenBooks.CurrentSlot = OpenBooks.Slots.Count - 1;
            };
            mainView.AddFileTab(tabBarItem);
            fileTabs.ResumeLayout(performLayout: false);
            fileTabs.PerformLayout();
            mainMenuStrip.ResumeLayout(performLayout: false);
            mainMenuStrip.PerformLayout();
            OnGuiVisibilities();
            if (books.OpenCount == 0 && !Program.Settings.ShowQuickOpen)
            {
                BrowserVisible = true;
                mainView.ShowLast();
            }
        }
    }

    private void btn_CloseClick(object sender, EventArgs e)
    {
        OpenBooks.Close((int)((TabBar.TabBarItem)sender).Tag);
    }

    private void tbi_CaptionClick(object sender, CancelEventArgs e)
    {
        TabBar.TabBarItem tabBarItem = sender as TabBar.TabBarItem;
        if (tabBarItem.IsSelected)
        {
            ToggleBrowser();
            e.Cancel = true;
        }
    }

    private void OpenBooks_Clicked(object sender, EventArgs e)
    {
        object obj = ((sender is ToolStripItem) ? ((ToolStripItem)sender).Tag : ((TabBar.TabBarItem)sender).Tag);
        OpenBooks.CurrentSlot = (int)obj;
    }

    private void OpenBooks_Selected(object sender, CancelEventArgs e)
    {
        object obj = ((sender is ToolStripItem) ? ((ToolStripItem)sender).Tag : ((TabBar.TabBarItem)sender).Tag);
        OpenBooks.CurrentSlot = (int)obj;
    }

    private void OpenBooks_SlotsChanged(object sender, SmartListChangedEventArgs<ComicBookNavigator> e)
    {
        RebuildBookTabs();
    }

    private void OpenBooks_CurrentSlotChanged(object sender, EventArgs e)
    {
        foreach (TabBar.TabBarItem item in fileTabs.Items)
        {
            if (item.Tag is int && OpenBooks.CurrentSlot == (int)item.Tag)
            {
                fileTabs.SelectedTab = item;
            }
        }
        foreach (ToolStripMenuItem item2 in from tmi in menu.OpenNow
                                            where tmi.Tag is int
                                            select tmi)
        {
            item2.Checked = OpenBooks.CurrentSlot == (int)item2.Tag;
        }
        foreach (ToolStripMenuItem item3 in from tmi in menu.Comics
                                            where tmi.Tag is int
                                            select tmi)
        {
            item3.Checked = OpenBooks.CurrentSlot == (int)item3.Tag;
        }
        mainView.ShowView(OpenBooks.CurrentSlot);
        UpdateTabCaptions();
        if (OpenBooks.CurrentBook != null)
        {
            Win7.SetActiveThumbnail(OpenBooks.CurrentBook.Comic.FilePath);
        }
    }

    private void OpenBooks_CaptionsChanged(object sender, EventArgs e)
    {
        UpdateTabCaptions();
    }

    private void readerContainer_Paint(object sender, PaintEventArgs e)
    {
        try
        {
            if (EngineConfiguration.Default.AeroFullScreenWorkaround)
            {
                e.Graphics.Clear(Color.Black);
            }
        }
        catch (Exception)
        {
        }
    }

    private void Application_Idle(object sender, EventArgs e)
    {
        OnUpdateGui();
    }

    private void viewer_BookChanged(object sender, EventArgs e)
    {
        if (!Program.ExtendedSettings.DoNotResetZoomOnBookOpen)
        {
            ComicDisplay.ImageZoom = 1f;
        }
        if (ComicDisplay.Book != null)
        {
            ComicDisplay.Focus();
        }
    }

    private void WatchedBookHasChanged(object sender, ContainerBookChangedEventArgs e)
    {
        if (e.IsComicInfo && e.Book.EditMode.IsLocalComic() && e.Book.FileInfoRetrieved)
        {
            e.Book.ComicInfoIsDirty = true;
            if (!books.IsOpen(e.Book))
            {
                Program.QueueManager.AddBookToFileUpdate(e.Book);
            }
        }
    }

    private void viewer_FirstPageReached(object sender, EventArgs e)
    {
        if (Program.Settings.AutoNavigateComics)
        {
            OpenPrevComic();
        }
    }

    private void viewer_LastPageReached(object sender, EventArgs e)
    {
        if (Program.Settings.AutoNavigateComics)
        {
            OpenNextComic(1, OpenComicOptions.NoMoveToLastPage);
        }
    }

    private void backgroundSaveTimer_Tick(object sender, EventArgs e)
    {
        Program.DatabaseManager.SaveInBackground();
    }

    private void tsExportActivity_Click(object sender, EventArgs e)
    {
        if (Program.QueueManager.ExportErrors.Count != 0)
        {
            ShowErrorsDialog.ShowErrors(this, Program.QueueManager.ExportErrors, ShowErrorsDialog.ComicExporterConverter);
        }
        else
        {
            ShowPendingTasks();
        }
    }

    private void tsDeviceSyncActivity_Click(object sender, EventArgs e)
    {
        if (Program.QueueManager.DeviceSyncErrors.Count != 0)
        {
            ShowErrorsDialog.ShowErrors(this, Program.QueueManager.DeviceSyncErrors, ShowErrorsDialog.DeviceSyncErrorConverter);
        }
        else
        {
            ShowPendingTasks();
        }
    }

    private void tsPageActivity_Click(object sender, EventArgs e)
    {
        ShowPendingTasks();
    }

    private void tsReadInfoActivity_Click(object sender, EventArgs e)
    {
        ShowPendingTasks();
    }

    private void tsUpdateInfoActivity_Click(object sender, EventArgs e)
    {
        ShowPendingTasks();
    }

    private void tsScanActivity_Click(object sender, EventArgs e)
    {
        ShowPendingTasks();
    }

    private void tsServerActivity_Click(object sender, EventArgs e)
    {
        ShowPendingTasks(1);
    }

    private void pageContextMenu_Opening(object sender, CancelEventArgs e)
    {
        try
        {
            if (ComicDisplay == null)
            {
                e.Cancel = true;
                return;
            }
            if (ComicDisplay.SupressContextMenu)
            {
                ComicDisplay.SupressContextMenu = false;
                e.Cancel = true;
                return;
            }
            IEditPage pageEditor = GetPageEditor();
            EnumMenuUtility enumMenuUtility = pageTypeContextMenu;
            bool enabled = (pageRotationContextMenu.Enabled = pageEditor.IsValid);
            enumMenuUtility.Enabled = enabled;
            pageTypeContextMenu.Value = (int)pageEditor.PageType;
            pageRotationContextMenu.Value = (int)pageEditor.Rotation;
        }
        catch
        {
            e.Cancel = true;
        }
    }

    private void mainViewContainer_ExpandedChanged(object sender, EventArgs e)
    {
        OnGuiVisibilities();
        if (base.Visible)
        {
            if (!mainViewContainer.Expanded)
            {
                ComicDisplay.Focus();
            }
            if (mainViewContainer.Expanded && mainViewContainer.Dock == DockStyle.Fill)
            {
                mainView.Focus();
            }
        }
    }

    private void ViewerFullScreenChanged(object sender, EventArgs e)
    {
        if (Program.Settings.AutoMinimalGui)
        {
            MinimalGui = ComicDisplay.FullScreen;
        }
        OnGuiVisibilities();
    }

    private void mainViewContainer_DockChanged(object sender, EventArgs e)
    {
        OnGuiVisibilities();
        if (ReaderUndocked)
        {
            return;
        }
        if (mainViewContainer.Dock == DockStyle.Fill)
        {
            mainViewContainer.BringToFront();
            if (base.Controls.Contains(viewContainer))
            {
                base.Controls.Remove(viewContainer);
                mainView.SetComicViewer(viewContainer);
                mainView.ShowView(books.CurrentSlot);
            }
        }
        else
        {
            if (!base.Controls.Contains(viewContainer))
            {
                mainView.SetComicViewer(null);
                base.Controls.Add(viewContainer);
            }
            viewContainer.Visible = true;
            viewContainer.BringToFront();
        }
    }

    private void TrackBar_Scroll(object sender, EventArgs e)
    {
        this.FindActiveService<IItemSize>()?.SetItemSize(thumbSize.TrackBar.Value);
    }

    private void mainView_TabChanged(object sender, EventArgs e)
    {
        if (!ReaderUndocked && BrowserDock == DockStyle.Fill)
        {
            BrowserVisible = !mainView.IsComicVisible;
        }
        OnGuiVisibilities();
    }

    private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
    {
        if (notifyIcon.Tag is HiddenMessageBoxes)
        {
            Program.Settings.HiddenMessageBoxes |= (HiddenMessageBoxes)notifyIcon.Tag;
            notifyIcon.Tag = null;
        }
    }

    private void trimTimer_Tick(object sender, EventArgs e)
    {
        Program.ImagePool.Thumbs.MemoryCache.Trim();
        Program.ImagePool.Pages.MemoryCache.Trim();
        int val = ((Program.ExtendedSettings.LimitMemory == 0) ? Settings.UnlimitedSystemMemory : Program.ExtendedSettings.LimitMemory);
        val = Math.Min(val, Program.Settings.MaximumMemoryMB);
        if (val == Settings.UnlimitedSystemMemory)
        {
            return;
        }
        try
        {
            using (Process process = Process.GetCurrentProcess())
            {
                process.MaxWorkingSet = new IntPtr(Convert.ToInt64(val.Clamp(50, Settings.UnlimitedSystemMemory)) * 1024 * 1024);
            }
        }
        catch
        {
        }
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
        if (AutoHideMainMenu && enableAutoHideMenu && menuDown && e.KeyCode == Keys.Menu && Machine.Ticks - menuAutoClosed > 500)
        {
            mainMenuStripVisibility.Visible = !mainMenuStripVisibility.Visible;
            if (mainMenuStripVisibility.Visible)
            {
                mainMenuStrip.Items[0].Select();
            }
            else
            {
                menuAutoClosed = Machine.Ticks;
            }
        }
        menuDown = false;
        base.OnKeyDown(e);
    }

    public void OnMainMenuStripMenuDeactivate(object sender, EventArgs e)
    {
        if (AutoHideMainMenu && enableAutoHideMenu)
        {
            mainMenuStripVisibility.Visible = false;
            menuAutoClosed = Machine.Ticks;
        }
    }

    private void NotifyIconMouseDoubleClick(object sender, MouseEventArgs e)
    {
        RestoreFromTray();
    }

    public void StartMouseDisabledTimer()
    {
        ComicDisplay.MouseClickEnabled = false;
        mouseDisableTimer.Stop();
        mouseDisableTimer.Start();
    }

    private void pageContextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
        StartMouseDisabledTimer();
    }

    private void showDisableTimer_Tick(object sender, EventArgs e)
    {
        ComicDisplay.MouseClickEnabled = true;
    }

    private void pageDisplay_SizeChanged(object sender, EventArgs e)
    {
        Control control = sender as Control;
        Screen screen = Screen.FromControl(control);
        if (control.Size == screen.Bounds.Size)
        {
            control.Height--;
        }
    }

    private void QuickOpenVisibleChanged(object sender, EventArgs e)
    {
        if (quickOpenView.Visible)
        {
            quickListDirty = true;
        }
    }

    private void QuickOpenBookActivated(object sender, EventArgs e)
    {
        ComicBook selectedBook = quickOpenView.SelectedBook;
        if (selectedBook != null)
        {
            OpenBooks.Open(selectedBook, inNewSlot: false);
        }
    }

    private void QuickOpenBooksChanged(object sender, SmartListChangedEventArgs<ComicBook> e)
    {
        if (e.Action == SmartListAction.Remove)
        {
            quickListDirty = true;
        }
    }

    private void quickOpenView_ShowBrowser(object sender, EventArgs e)
    {
        ToggleBrowser();
    }

    private void quickOpenView_OpenFile(object sender, EventArgs e)
    {
        MainController.Commands.ShowOpenDialog();
    }
}
