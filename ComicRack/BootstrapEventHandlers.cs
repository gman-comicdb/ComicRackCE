using cYo.Common.Localize;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Plugins;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

internal class BootstrapEventHandlers
{
    public static void OnFirstDatabaseAccess(object s, EventArgs e)
        => Bootstrap.UpdateStartupProgress(
            TR.Messages["OpenDatabase", "Opening Database"],
            AppConfig.ExtendedSettings.LoadDatabaseInForeground ? 0 : -1);

    public static void OnClientSyncRequest(object s, WirelessSyncProvider.ClientSyncRequestArgs e)
    {
        if (AppServices.MainForm != null)
        {
            e.IsPaired = AppServices.QueueManager.Devices.Any((DeviceSyncSettings d) => d.DeviceKey == e.Key);
            if (e.IsPaired && s is IPAddress address)
                AppServices.MainForm.BeginInvoke(() => SyncClientRequest(e.Key, address));
        }
    }

    internal static void OnComicScannedIgnoreFile(object sender, ComicScanNotifyEventArgs e)
    {
        if (AppConfig.Settings.DontAddRemoveFiles && AppServices.Database.IsBlacklisted(e.File))
            e.IgnoreFile = true;

        if (AppConfig.ExtendedSettings.MacCompatibleScanning && Path.GetFileName(e.File).StartsWith("._"))
        {
            e.IgnoreFile = true;
        }
    }

    internal static void OnIgnoredCoverImagesChanged(object sender, EventArgs e)
        => ComicInfo.CoverKeyFilter = AppConfig.Settings.IgnoredCoverImages;

    internal static void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
    {
        switch (e.Mode)
        {
            case PowerModes.Resume:
                AppServices.NetworkManager.Start();
                break;
            case PowerModes.Suspend:
                AppServices.NetworkManager.Stop();
                break;
            case PowerModes.StatusChange:
                break;
        }
    }

    #region MainForm Event Handlers
    public static void OnMainFormFormClosing(object sender, FormClosingEventArgs e)
    {
        bool bcUserClosing = e.CloseReason == CloseReason.UserClosing;
        foreach (Command command in ScriptUtility.GetCommands(PluginEngine.ScriptTypeShutdown))
        {
            try
            {
                if (!(bool)command.Invoke(new object[1]
                {
                    bcUserClosing
                }) && bcUserClosing)
                {
                    e.Cancel = true;
                    return;
                }
            }
            catch (Exception)
            {
            }
        }
    }

    public static void OnMainFormFormClosed(object sender, FormClosedEventArgs e)
    {
        AutomaticProgressDialog.Process(
            null,
            TR.Messages["ClosingComicRack", "Closing ComicRack"],
            TR.Messages["SaveAllData", "Saving all Data to Disk..."],
            1500,
            AppServices.CleanUp,
            AutomaticProgressDialogOptions.None
        );
    }
    #endregion

    // Helpers
    private static void SyncClientRequest(string key, IPAddress address)
    {
        AppServices.MainForm.StoreWorkspace(); // save workspace before sync, so sorted lists key are up to date
        AppServices.QueueManager.SynchronizeDevice(key, address);
    }
}
