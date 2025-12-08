using cYo.Projects.ComicRack.Viewer.Dialogs;
using System;
using System.Windows.Forms;
using WinStatusStrip = System.Windows.Forms.StatusStrip;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class StatusStrip : UserControl
{
    public WinStatusStrip Item;

    public StatusStrip()
    {
        InitializeComponent();
        Item = statusStripItem;
    }

    public static implicit operator WinStatusStrip(StatusStrip menu)
        => menu.Item;

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

    private void tsCurrentPage_Click(object sender, EventArgs e)
    {
        Program.Settings.TrackCurrentPage = !Program.Settings.TrackCurrentPage;
    }
}
