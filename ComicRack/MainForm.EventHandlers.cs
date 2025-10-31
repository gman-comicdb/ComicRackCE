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
using cYo.Common.Windows.Extensions;
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
        recentFiles = Program.Database.GetRecentFiles(Settings.RecentFileCount).ToArray();
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

    private void OnOpenRecent(object sender, EventArgs e)
    {
        string text = ((ToolStripMenuItem)sender).Text;
        int num = Convert.ToInt32(text.Substring(0, 2)) - 1;
        OpenSupportedFile(recentFiles[num], Program.Settings.OpenInNewTab);
    }

    private void RecentFilesMenuOpening(object sender, EventArgs e)
    {
        int num = 0;
        foreach (ToolStripMenuItem dropDownItem in miOpenRecent.DropDownItems)
        {
            if (dropDownItem.Image != null)
            {
                dropDownItem.Image.Dispose();
            }
        }
        FormUtility.SafeToolStripClear(miOpenRecent.DropDownItems);
        string[] array = recentFiles;
        foreach (string text in array)
        {
            if (!File.Exists(text))
            {
                continue;
            }
            string text2 = ++num + " - " + FormUtility.FixAmpersand(FileUtility.GetSafeFileName(text));
            using (IItemLock<ThumbnailImage> itemLock = Program.ImagePool.Thumbs.GetImage(Program.BookFactory.Create(text, CreateBookOption.DoNotAdd).GetFrontCoverThumbnailKey()))
            {
                try
                {
                    ToolStripMenuItem value = new ToolStripMenuItem(text2, (itemLock != null && itemLock.Item != null) ? itemLock.Item.Bitmap.Resize(16, 16) : null, OnOpenRecent);
                    miOpenRecent.DropDownItems.Add(value);
                }
                catch (Exception)
                {
                }
            }
        }
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
            FormUtility.SafeToolStripClear(miOpenNow.DropDownItems);
            FormUtility.SafeToolStripClear(cmComics.DropDownItems, cmComics.DropDownItems.IndexOf(cmComicsSep) + 1);
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
                miOpenNow.DropDownItems.Add(tmi);
                ToolStripMenuItem tmi2 = new ToolStripMenuItem(text);
                tmi2.Click += OpenBooks_Clicked;
                tmi2.Tag = i;
                tmi2.ShortcutKeys = tmi.ShortcutKeys;
                cmComics.DropDownItems.Add(tmi2);
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
            string text4 = miAddTab.Text.Replace("&", string.Empty);
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
        foreach (ToolStripMenuItem item2 in from tmi in miOpenNow.DropDownItems.OfType<ToolStripMenuItem>()
                                            where tmi.Tag is int
                                            select tmi)
        {
            item2.Checked = OpenBooks.CurrentSlot == (int)item2.Tag;
        }
        foreach (ToolStripMenuItem item3 in from tmi in cmComics.DropDownItems.OfType<ToolStripMenuItem>()
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

    private void tsCurrentPage_Click(object sender, EventArgs e)
    {
        Program.Settings.TrackCurrentPage = !Program.Settings.TrackCurrentPage;
    }

    private void tabContextMenu_Opening(object sender, CancelEventArgs e)
    {
        bool flag = ComicDisplay == null || ComicDisplay.Book == null || ComicDisplay.Book.Comic.EditMode.IsLocalComic();
        ToolStripSeparator toolStripSeparator = sepBeforeRevealInBrowser;
        ToolStripMenuItem toolStripMenuItem = cmRevealInExplorer;
        bool flag3 = (cmCopyPath.Visible = flag);
        bool visible = (toolStripMenuItem.Visible = flag3);
        toolStripSeparator.Visible = visible;
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

    private void MagnifySetupChanged(object sender, EventArgs e)
    {
        MagnifySetupControl magnifySetupControl = (MagnifySetupControl)sender;
        ComicDisplay.MagnifierOpacity = magnifySetupControl.MagnifyOpaque;
        ComicDisplay.MagnifierSize = magnifySetupControl.MagnifySize;
        ComicDisplay.MagnifierZoom = magnifySetupControl.MagnifyZoom;
        ComicDisplay.MagnifierStyle = magnifySetupControl.MagnifyStyle;
        ComicDisplay.AutoHideMagnifier = magnifySetupControl.AutoHideMagnifier;
        ComicDisplay.AutoMagnifier = magnifySetupControl.AutoMagnifier;
    }

    private void viewer_PageDisplayModeChanged(object sender, EventArgs e)
    {
        tbZoom.Text = $"{(int)(ComicDisplay.ImageZoom * 100f)}%";
        tbRotate.Text = TR.Translate(ComicDisplay.ImageRotation);
        tbRotate.Image = (ComicDisplay.ImageAutoRotate ? miAutoRotate.Image : miRotateRight.Image);
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

    private void fileMenu_DropDownOpening(object sender, EventArgs e)
    {
        miUpdateAllComicFiles.Visible = !Program.Settings.AutoUpdateComicsFiles;
    }

    private void editMenu_DropDownOpening(object sender, EventArgs e)
    {
        try
        {
            bool flag = ComicDisplay != null && ComicDisplay.Book != null;
            IEditPage pageEditor = GetPageEditor();
            EnumMenuUtility enumMenuUtility = pageTypeEditMenu;
            bool enabled = (pageRotationEditMenu.Enabled = pageEditor.IsValid);
            enumMenuUtility.Enabled = enabled;
            pageTypeEditMenu.Value = (int)pageEditor.PageType;
            pageRotationEditMenu.Value = (int)pageEditor.Rotation;
            if (miUndo.Tag == null)
            {
                miUndo.Tag = miUndo.Text;
            }
            string undoLabel = Program.Database.Undo.UndoLabel;
            miUndo.Text = (string)miUndo.Tag + (string.IsNullOrEmpty(undoLabel) ? string.Empty : (": " + undoLabel));
            if (miRedo.Tag == null)
            {
                miRedo.Tag = miRedo.Text;
            }
            string text = Program.Database.Undo.RedoEntries.FirstOrDefault();
            miRedo.Text = (string)miRedo.Tag + (string.IsNullOrEmpty(text) ? string.Empty : (": " + text));
        }
        catch (Exception)
        {
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

    private void tbBookmarks_DropDownOpening(object sender, EventArgs e)
    {
        UpdateBookmarkMenu(tbBookmarks.DropDownItems, 0);
    }

    private void cmBookmarks_DropDownOpening(object sender, EventArgs e)
    {
        UpdateBookmarkMenu(cmBookmarks.DropDownItems, 0);
    }

    private void tbPrevPage_DropDownOpening(object sender, EventArgs e)
    {
        UpdateBookmarkMenu(tbPrevPage.DropDownItems, -1);
    }

    private void tbNextPage_DropDownOpening(object sender, EventArgs e)
    {
        UpdateBookmarkMenu(tbNextPage.DropDownItems, 1);
    }

    private void miBookmarks_DropDownOpening(object sender, EventArgs e)
    {
        UpdateBookmarkMenu(miBookmarks.DropDownItems, 0);
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

    private void tbTools_DropDownOpening(object sender, EventArgs e)
    {
        tbUpdateWebComics.Visible = Program.Database.Books.FirstOrDefault((ComicBook cb) => cb.IsDynamicSource) != null;
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

    private void mainMenuStrip_MenuDeactivate(object sender, EventArgs e)
    {
        if (AutoHideMainMenu && enableAutoHideMenu)
        {
            mainMenuStripVisibility.Visible = false;
            menuAutoClosed = Machine.Ticks;
        }
    }

    private void UpdateActivityTimerTick(object sender, EventArgs e)
    {
        ToolStripStatusLabel[] array = new ToolStripStatusLabel[5]
        {
                tsReadInfoActivity,
                tsWriteInfoActivity,
                tsScanActivity,
                tsExportActivity,
                tsDeviceSyncActivity
        };
        int num = Numeric.BinaryHash(array.Select((ToolStripStatusLabel l) => l.Visible).ToArray());
        tsScanActivity.Visible = Program.Scanner.IsScanning;
        tsWriteInfoActivity.Visible = Program.QueueManager.IsInComicFileUpdate;
        tsReadInfoActivity.Visible = Program.QueueManager.IsInComicFileRefresh;
        tsPageActivity.Visible = Program.ImagePool.IsWorking;
        bool isInComicConversion = Program.QueueManager.IsInComicConversion;
        int pendingComicConversions = Program.QueueManager.PendingComicConversions;
        int count = Program.QueueManager.ExportErrors.Count;
        tsExportActivity.Visible = isInComicConversion || count > 0;
        if (tsExportActivity.Visible)
        {
            Image image = ((count <= 0) ? exportAnimation : ((pendingComicConversions == 0) ? exportError : exportErrorAnimation));
            tsExportActivity.Image = image;
            string text = StringUtility.Format(ExportingComics, pendingComicConversions);
            if (count > 0)
            {
                text += "\n";
                text += StringUtility.Format(ExportingErrors, count);
            }
            tsExportActivity.ToolTipText = text;
        }
        bool isInDeviceSync = Program.QueueManager.IsInDeviceSync;
        int pendingDeviceSyncs = Program.QueueManager.PendingDeviceSyncs;
        int count2 = Program.QueueManager.DeviceSyncErrors.Count;
        tsDeviceSyncActivity.Visible = isInDeviceSync || count2 > 0;
        if (tsDeviceSyncActivity.Visible)
        {
            Image image2 = ((count2 <= 0) ? deviceSyncAnimation : ((pendingDeviceSyncs == 0) ? deviceSyncError : deviceSyncErrorAnimation));
            tsDeviceSyncActivity.Image = image2;
            string text2 = StringUtility.Format(DeviceSyncing, pendingDeviceSyncs);
            if (count > 0)
            {
                text2 += "\n";
                text2 += StringUtility.Format(DeviceSyncingErrors, count2);
            }
            tsDeviceSyncActivity.ToolTipText = text2;
        }
        Image image3 = null;
        if (comicDisplay != null && comicDisplay.Book != null && !comicDisplay.Book.IsIndexRetrievalCompleted)
        {
            image3 = updatePages;
        }
        tsCurrentPage.Image = (Program.Settings.TrackCurrentPage ? null : trackPagesLockedImage);
        tsPageCount.Image = image3;
        int num2 = Numeric.BinaryHash(array.Select((ToolStripStatusLabel l) => l.Visible).ToArray());
        if (num2 != num)
        {
            int num3 = Numeric.HighestBit(num2);
            if (num3 == -1)
            {
                Win7.SetOverlayIcon(null, null);
            }
            else
            {
                Win7.SetOverlayIcon(array[num3].Image as Bitmap, null);
            }
        }
        tsServerActivity.Visible = Program.NetworkManager.HasActiveServers();
        if (tsServerActivity.Visible)
        {
            tsServerActivity.ToolTipText = string.Format(TR.Messages["ServerActivity", "{0} Server(s) running"], Program.NetworkManager.RunningServers.Count);
            tsServerActivity.Image = (Program.NetworkManager.RecentServerActivity() ? greenLight : grayLight);
        }
        bool flag = Program.Database != null && Program.Database.ComicStorage != null;
        tsDataSourceState.Visible = flag;
        if (flag)
        {
            tsDataSourceState.Image = (Program.Database.ComicStorage.IsConnected ? datasourceConnected : datasourceDisconnected);
            tsDataSourceState.ToolTipText = (Program.Database.ComicStorage.IsConnected ? TR.Messages["DataSourceConnected", "Connected to data source"] : TR.Messages["DataSourceDisconnected", "Disconnected from data source!"]);
        }
    }

    private void NotifyIconMouseDoubleClick(object sender, MouseEventArgs e)
    {
        RestoreFromTray();
    }

    private void StartMouseDisabledTimer()
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
        ShowOpenDialog();
    }
}
