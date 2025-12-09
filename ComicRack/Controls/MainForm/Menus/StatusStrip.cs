using cYo.Common.Localize;
using cYo.Common.Mathematics;
using cYo.Common.Text;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Viewer.Properties;
using cYo.Projects.ComicRack.Viewer.Views;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinStatusStrip = System.Windows.Forms.StatusStrip;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class StatusStrip : UserControl
{
    private MainController controller;

    private static readonly string ExportingComics = TR.Load(typeof(Viewer.MainForm).Name)["ExportingComics", "Exporting Books: {0} queued"];

    private static readonly string ExportingErrors = TR.Load(typeof(Viewer.MainForm).Name)["ExportingErrors", "{0} errors. Click for details"];

    private static readonly string DeviceSyncing = TR.Load(typeof(Viewer.MainForm).Name)["DeviceSyncing", "Syncing Devices: {0} queued"];

    private static readonly string DeviceSyncingErrors = TR.Load(typeof(Viewer.MainForm).Name)["DeviceSyncingErrors", "{0} errors. Click for details"];

    private static readonly Image exportErrorAnimation = Resources.ExportAnimationWithError;

    private static readonly Image exportAnimation = Resources.ExportAnimation;

    private static readonly Image exportError = Resources.ExportError;

    private static readonly Image deviceSyncErrorAnimation = Resources.DeviceSyncAnimationWithError;

    private static readonly Image deviceSyncAnimation = Resources.DeviceSyncAnimation;

    private static readonly Image deviceSyncError = Resources.DeviceSyncError;

    private static readonly Image zoomImage = Resources.Zoom;

    private static readonly Image zoomClearImage = Resources.ZoomClear;

    private static readonly Image updatePages = Resources.UpdatePages;

    private static readonly Image greenLight = Resources.GreenLight;

    private static readonly Image grayLight = Resources.GrayLight;

    private static readonly Image trackPagesLockedImage = Resources.Locked;

    private static readonly Image datasourceConnected = Resources.DataSourceConnected;

    private static readonly Image datasourceDisconnected = Resources.DataSourceDisconnected;

    public void UpdateCurrentPageImage()
    {
        tsCurrentPage.Image = ComicBookNavigator.TrackCurrentPage ? null : Resources.Locked;
    }

    public StatusStrip()
    {
        //this.controller = controller;
        InitializeComponent();
    }

    public void SetController(MainController controller)
    {
        this.controller = controller;
        statusStripItem.Height = (int)tsText.Font.GetHeight() + FormUtility.ScaleDpiY(8);

        tsDeviceSyncActivity.Click += MainController.EventHandlers.OnDeviceSyncActivityClick;
        tsExportActivity.Click += MainController.EventHandlers.OnExportActivityClick;
        tsCurrentPage.Click += MainController.EventHandlers.OnCurrentPageClick;

        tsReadInfoActivity.Click += MainController.EventHandlers.OnGenericActivityClick;
        tsWriteInfoActivity.Click += MainController.EventHandlers.OnGenericActivityClick;
        tsPageActivity.Click += MainController.EventHandlers.OnGenericActivityClick;
        tsScanActivity.Click += MainController.EventHandlers.OnGenericActivityClick;
        tsServerActivity.Click += MainController.EventHandlers.OnGenericActivityClick;
    }

    public static implicit operator WinStatusStrip(StatusStrip menu)
        => menu.statusStripItem;

    public void UpdateActivityTimerTick(object sender, EventArgs e)
    {
        ToolStripStatusLabel[] labels =
        [
                tsReadInfoActivity,
                tsWriteInfoActivity,
                tsScanActivity,
                tsExportActivity,
                tsDeviceSyncActivity
        ];

        // initial hash + visibility
        int initialHash = Numeric.BinaryHash([.. labels.Select((ToolStripStatusLabel l) => l.Visible)]);
        tsScanActivity.Visible = Program.Scanner.IsScanning;
        tsWriteInfoActivity.Visible = Program.QueueManager.IsInComicFileUpdate;
        tsReadInfoActivity.Visible = Program.QueueManager.IsInComicFileRefresh;
        tsPageActivity.Visible = Program.ImagePool.IsWorking;

        // Export Activity
        bool isInComicConversion = Program.QueueManager.IsInComicConversion;
        int pendingComicConversions = Program.QueueManager.PendingComicConversions;
        int exportErrors = Program.QueueManager.ExportErrors.Count;
        tsExportActivity.Visible = isInComicConversion || exportErrors > 0;
        if (tsExportActivity.Visible)
        {
            Image image = ((exportErrors <= 0) ? exportAnimation : ((pendingComicConversions == 0) ? exportError : exportErrorAnimation));
            tsExportActivity.Image = image;
            string text = StringUtility.Format(ExportingComics, pendingComicConversions);
            if (exportErrors > 0)
            {
                text += "\n";
                text += StringUtility.Format(ExportingErrors, exportErrors);
            }
            tsExportActivity.ToolTipText = text;
        }

        // Device Sync
        bool isInDeviceSync = Program.QueueManager.IsInDeviceSync;
        int pendingDeviceSyncs = Program.QueueManager.PendingDeviceSyncs;
        int syncErrors = Program.QueueManager.DeviceSyncErrors.Count;
        tsDeviceSyncActivity.Visible = isInDeviceSync || syncErrors > 0;
        if (tsDeviceSyncActivity.Visible)
        {
            Image image2 = ((syncErrors <= 0) ? deviceSyncAnimation : ((pendingDeviceSyncs == 0) ? deviceSyncError : deviceSyncErrorAnimation));
            tsDeviceSyncActivity.Image = image2;
            string text2 = StringUtility.Format(DeviceSyncing, pendingDeviceSyncs);
            if (syncErrors > 0)
            {
                text2 += "\n";
                text2 += StringUtility.Format(DeviceSyncingErrors, syncErrors);
            }
            tsDeviceSyncActivity.ToolTipText = text2;
        }

        // Current Page + Page Count
        Image pageImage = null;
        if (controller.ComicDisplay?.Book != null && !controller.ComicDisplay.Book.IsIndexRetrievalCompleted)
            pageImage = updatePages;
        tsCurrentPage.Image = (Program.Settings.TrackCurrentPage ? null : trackPagesLockedImage);
        tsPageCount.Image = pageImage;

        // Updated hash
        int updatedHash = Numeric.BinaryHash([.. labels.Select((ToolStripStatusLabel l) => l.Visible)]);
        if (updatedHash != initialHash)
        {
            int index = Numeric.HighestBit(updatedHash);
            Win7.SetOverlayIcon(index == -1 ? null : labels[index].Image as Bitmap, null);
        }

        // Server Activity
        tsServerActivity.Visible = Program.NetworkManager.HasActiveServers();
        if (tsServerActivity.Visible)
        {
            tsServerActivity.ToolTipText = string.Format(TR.Messages["ServerActivity", "{0} Server(s) running"], Program.NetworkManager.RunningServers.Count);
            tsServerActivity.Image = (Program.NetworkManager.RecentServerActivity() ? greenLight : grayLight);
        }

        // Data Source State
        tsDataSourceState.Visible = Program.Database != null && Program.Database.ComicStorage != null;
        if (tsDataSourceState.Visible)
        {
            tsDataSourceState.Image = (Program.Database.ComicStorage.IsConnected ? datasourceConnected : datasourceDisconnected);
            tsDataSourceState.ToolTipText = (Program.Database.ComicStorage.IsConnected ? TR.Messages["DataSourceConnected", "Connected to data source"] : TR.Messages["DataSourceDisconnected", "Disconnected from data source!"]);
        }
    }

    public void UpdateMenu(string statusText)
    {
        string comicTitle = controller.ComicDisplay.Book?.Caption.Ellipsis(20, "...");
        tsBook.Text = string.IsNullOrEmpty(comicTitle) ? TR.Default["None", "None"] : comicTitle;

        tsCurrentPage.Text = controller.ComicDisplay.Book == null ? TR.Default["NotAvailable", "NA"]
            : (controller.ComicDisplay.Book.CurrentPage + 1).ToString();
        tsPageCount.Text = TotalPageInformation(controller.ComicDisplay.Book);
        tsText.Text = statusText;
    }

    private static string TotalPageInformation(ComicBookNavigator nav)
    {
        if (nav == null)
            return TR.Default["NotAvailable", "NA"];
        if (nav.IsIndexRetrievalCompleted || nav.IndexPagesRetrieved == nav.Comic.PageCount)
            return nav.Comic.PagesAsText;
        return $"{nav.Comic.PagesAsText} ({nav.IndexPagesRetrieved})";
    }
}
