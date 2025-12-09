using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using cYo.Common.Localize;
using cYo.Common.Net;
using cYo.Common.Runtime;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Controls;
using cYo.Projects.ComicRack.Viewer.Properties;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm : FormEx, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    protected override void WndProc(ref Message m)
    {
        //Disables fullscreen mode when the application is deactivated
        if (m.Msg == Native.WM_ACTIVATEAPP && m.WParam.ToInt32() == 0)
        {
            ComicDisplay.FullScreen = false;
        }
        base.WndProc(ref m);
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        Win7.Initialize();
        if (!string.IsNullOrEmpty(Program.ExtendedSettings.InstallPlugin))
        {
            ShowPreferences(Program.ExtendedSettings.InstallPlugin);
        }
    }

    protected override void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
        if (!mainViewContainer.Expanded)
        {
            ComicDisplay.Focus();
        }
        else
        {
            mainView.Focus();
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (e.CloseReason == CloseReason.UserClosing && Program.Settings.CloseMinimizesToTray && !menuClose)
        {
            MinimizeToTray();
            if ((Program.Settings.HiddenMessageBoxes & HiddenMessageBoxes.ComicRackMinimized) == 0)
            {
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.BalloonTipText = TR.Messages["ComicRackMinimized", "You either close ComicRack with File/Exit or you can change this behavior in the Preferences Dialog.\nClick here to not show this message again"];
                notifyIcon.BalloonTipTitle = TR.Messages["ComicRackMinimizedTitle", "ComicRack is still running"];
                notifyIcon.Tag = HiddenMessageBoxes.ComicRackMinimized;
                notifyIcon.ShowBalloonTip(5000);
            }
            e.Cancel = true;
            return;
        }
        if (Program.Settings.UpdateComicFiles)
        {
            IEnumerable<ComicBook> dirtyTempList = Program.BookFactory.TemporaryBooks.Where((ComicBook cb) => cb.ComicInfoIsDirty);
            int dirtyCount = dirtyTempList.Count();
            if (dirtyCount != 0 && Program.AskQuestion(this, TR.Messages["AskDirtyItems", "Save changed information for Books that are not in the database?\nAll changes not saved now will be lost!"], TR.Default["Save", "Save"], HiddenMessageBoxes.AskDirtyItems, TR.Messages["AlwaysSaveDirty", "Always save changes"], TR.Default["No", "No"]))
            {
                AutomaticProgressDialog.Process(this, TR.Messages["SaveInfo", "Saving Book Information"], TR.Messages["SaveInfoText", "Please wait while all unsaved information is stored!"], 5000, delegate
                {
                    int num = 0;
                    foreach (ComicBook item in dirtyTempList)
                    {
                        if (AutomaticProgressDialog.ShouldAbort)
                        {
                            break;
                        }
                        AutomaticProgressDialog.Value = num++ * 100 / dirtyCount;
                        Program.QueueManager.WriteInfoToFileWithCacheUpdate(item);
                    }
                }, AutomaticProgressDialogOptions.EnableCancel);
            }
        }
        if (Program.QueueManager.IsActive && !QuestionDialog.Ask(this, TR.Messages["BackgroundConvert", "Files are still being updated/converted/synchronized in the background. If you close now, some information will not be written!"], TR.Messages["CloseComicRack", "Close ComicRack"]))
        {
            e.Cancel = true;
            return;
        }
        if (ScriptUtility.Enabled)
        {
            Program.Settings.PluginsStates = ScriptUtility.Scripts.CommandStates;
        }
        Program.Settings.LastOpenFiles.Clear();
        Program.Settings.LastOpenFiles.AddRange(books.OpenFiles);
        StoreWorkspace();
        Program.Settings.QuickOpenThumbnailSize = quickOpenView.ThumbnailSize;
        Program.Settings.MagnifySize = ComicDisplay.MagnifierSize;
        Program.Settings.MagnifyOpaque = ComicDisplay.MagnifierOpacity;
        Program.Settings.MagnifyZoom = ComicDisplay.MagnifierZoom;
        Program.Settings.MagnifyStyle = ComicDisplay.MagnifierStyle;
        Program.Settings.AutoHideMagnifier = ComicDisplay.AutoHideMagnifier;
        Program.Settings.AutoMagnifier = ComicDisplay.AutoMagnifier;
        Program.Settings.ThumbCacheEnabled = Program.ImagePool.Thumbs.DiskCache.Enabled;
        Program.Settings.PageFilter = ComicDisplay.PageFilter;
        Program.Settings.ReaderKeyboardMapping.Clear();
        Program.Settings.ReaderKeyboardMapping.AddRange(ComicDisplay.KeyboardMap.GetKeyMapping());
        Program.NetworkManager.BroadcastStop();
        if (readerForm != null)
        {
            readerForm.Dispose();
            readerForm = null;
        }
        if (menu != null) { menu.Dispose(); menu = null; }
        if (controller != null) { controller = null; }
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
        base.OnLayout(levent);
        ConstraintMainView(always: false);
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
        base.OnVisibleChanged(e);
        if (MinimizedToTray)
        {
            base.Visible = false;
        }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        menuDown = e.KeyCode == Keys.Menu;
        if (!mainKeys.HandleKey(e.KeyCode | e.Modifiers))
        {
            base.OnKeyDown(e);
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        if (!base.IsHandleCreated || !base.Visible)
        {
            return;
        }
        switch (base.WindowState)
        {
            case FormWindowState.Maximized:
                oldState = FormWindowState.Maximized;
                break;
            case FormWindowState.Minimized:
                if (Program.ExtendedSettings.StartHidden || Program.Settings.MinimizeToTray)
                    MinimizeToTray();

                Program.Collect();
                break;
            case FormWindowState.Normal:
                UpdateSafeBounds();
                oldState = FormWindowState.Normal;
                break;
        }
    }

    protected override void OnMove(EventArgs e)
    {
        base.OnMove(e);
        UpdateSafeBounds();
    }

    #region Helpers [Commands, so can be manually triggered]
    public async Task CheckForUpdateAsync(bool alwaysCheck = false)
    {
        bool doNotCheckForUpdate = Program.Settings.HiddenMessageBoxes.HasFlag(HiddenMessageBoxes.DoNotCheckForUpdate);

        // Proceed if opened from the menu; otherwise, abort while developping or update checks are disabled.
        if (!alwaysCheck && (GitVersion.IsDirty || doNotCheckForUpdate))
            return;

        GithubAPI gh = new GithubAPI();
        await gh.ExecuteAsync();
        bool isUpdateAvailable = gh.IsUpdateAvailable;

        if (alwaysCheck || isUpdateAvailable)
        {
            string message = isUpdateAvailable ?
                TR.Messages["UpdateAvailable", "An update is available download it?"] :
                TR.Messages["UpdateNotAvailable", "There are no updates available"];
            string okButtonText = isUpdateAvailable ? TR.Default["Download", "Download"] : TR.Default["OK", "OK"];

            QuestionResult qr = QuestionDialog.AskQuestion(this, message, okButtonText, (QuestionDialog qd) =>
            {
                qd.Option2Independent = true;
                qd.OptionText = $"{(doNotCheckForUpdate ? "!" : "")}{TR.Messages["NeverCheckForUpdate", "&Never check for updates on startup"]}";
                qd.Option2Text = isUpdateAvailable ? TR.Messages["ZipFileOption", "&Download zip file instead of installer"] : string.Empty;
                qd.ShowCancel = isUpdateAvailable;
                qd.CancelButtonText = TR.Default["No", "No"];
            });

            // The update checks option was enabled but now isn't, remove the setting. Happens only when opening from the menu.
            if (doNotCheckForUpdate && !qr.HasFlag(QuestionResult.Option))
                Program.Settings.HiddenMessageBoxes &= ~HiddenMessageBoxes.DoNotCheckForUpdate;

            // The update checks option is enabled, update the setting
            if (qr.HasFlag(QuestionResult.Option))
                Program.Settings.HiddenMessageBoxes |= HiddenMessageBoxes.DoNotCheckForUpdate;

            if (qr.HasFlag(QuestionResult.Cancel) || !isUpdateAvailable)
                return;

            if (qr.HasFlag(QuestionResult.Ok) && qr.HasFlag(QuestionResult.Option2))
                Program.StartDocument(NightlyDownloadLinkZIP);
            else if (qr.HasFlag(QuestionResult.Ok))
                Program.StartDocument(NightlyDownloadLinkEXE);
        }
    }
    #endregion

    #region Helpers [also called by EventHandlers]
    private void UpdateSettings()
    {
        ComicDisplay.MouseWheelSpeed = Program.Settings.MouseWheelSpeed;
        ComicDisplay.ImageDisplayOptions = Program.Settings.PageImageDisplayOptions;
        ComicDisplay.SmoothScrolling = Program.Settings.SmoothScrolling;
        ComicDisplay.BlendWhilePaging = Program.Settings.BlendWhilePaging;
        ComicDisplay.InfoOverlayScaling = (float)Program.Settings.OverlayScaling / 100f;
        ComicDisplay.SetInfoOverlays(InfoOverlays.PartInfo, Program.Settings.ShowVisiblePagePartOverlay);
        ComicDisplay.SetInfoOverlays(InfoOverlays.CurrentPage, Program.Settings.ShowCurrentPageOverlay);
        ComicDisplay.SetInfoOverlays(InfoOverlays.LoadPage, Program.Settings.ShowStatusOverlay);
        ComicDisplay.SetInfoOverlays(InfoOverlays.PageBrowser, Program.Settings.ShowNavigationOverlay);
        ComicDisplay.SetInfoOverlays(InfoOverlays.PageBrowserOnTop, Program.Settings.NavigationOverlayOnTop);
        ComicDisplay.SetInfoOverlays(InfoOverlays.CurrentPageShowsName, Program.Settings.CurrentPageShowsName);
        ComicDisplay.HideCursorFullScreen = Program.Settings.HideCursorFullScreen;
        ComicDisplay.AutoScrolling = Program.Settings.AutoScrolling;
        ComicDisplay.PageWallTicks = (Program.Settings.PageChangeDelay ? 300 : 0);
        ComicDisplay.ScrollingDoesBrowse = Program.Settings.ScrollingDoesBrowse;
        ComicDisplay.ResetZoomOnPageChange = Program.Settings.ResetZoomOnPageChange;
        ComicDisplay.ZoomInOutOnPageChange = Program.Settings.ZoomInOutOnPageChange;
        ComicDisplay.RightToLeftReadingMode = Program.Settings.RightToLeftReadingMode;
        ComicDisplay.LeftRightMovementReversed = Program.Settings.LeftRightMovementReversed;
        ComicDisplay.DisplayChangeAnimation = Program.Settings.DisplayChangeAnimation;
        ComicDisplay.FlowingMouseScrolling = Program.Settings.FlowingMouseScrolling;
        ComicDisplay.SoftwareFiltering = Program.Settings.SoftwareFiltering;
        ComicDisplay.HardwareFiltering = Program.Settings.HardwareFiltering;
        ComicDisplay.SetRenderer(Program.Settings.HardwareAcceleration);
        foreach (ComicBookNavigator slot in OpenBooks.Slots)
        {
            if (slot != null)
            {
                slot.BaseColorAdjustment = Program.Settings.GlobalColorAdjustment;
            }
        }
        AutoHideMainMenu = Program.Settings.AutoHideMainMenu;
        ShowMainMenuNoComicOpen = Program.Settings.ShowMainMenuNoComicOpen;
        quickOpenView.ThumbnailSize = Program.Settings.QuickOpenThumbnailSize;
        ComicBookNavigator.TrackCurrentPage = Program.Settings.TrackCurrentPage;
        menu.UpdateCurrentPageImage();
        CoverViewItem.ThumbnailSizing = (Program.Settings.CoverThumbnailsSameSize ? CoverThumbnailSizing.Fit : CoverThumbnailSizing.None);
        ComicBook.NewBooksChecked = Program.Settings.NewBooksChecked;
        DeviceSyncFactory.SetExtraWifiDeviceAddresses(EngineConfiguration.Default.ExtraWifiDeviceAddresses + "," + Program.Settings.ExtraWifiDeviceAddresses);
    }
    #endregion

    #region Helpers [only called by LifeCycle methods]
    private void UpdateSafeBounds()
    {
        if (base.IsHandleCreated && base.WindowState == FormWindowState.Normal && base.FormBorderStyle != 0)
        {
            SafeBounds = base.Bounds;
        }
    }

    private void ConstraintMainView(bool always)
    {
        if ((base.Visible || always) && mainView.Dock != DockStyle.Fill && base.WindowState != FormWindowState.Minimized)
        {
            Rectangle displayRectangle = DisplayRectangle;
            if (fileTabsVisibility.Visible)
            {
                displayRectangle.Y = fileTabs.Bottom;
                displayRectangle.Height -= fileTabs.Bottom;
            }
            if (statusStrip.Visible)
            {
                displayRectangle.Height -= statusStrip.Height;
            }
            mainViewContainer.Bounds = Rectangle.Intersect(displayRectangle, mainViewContainer.Bounds);
        }
    }
    #endregion
}
