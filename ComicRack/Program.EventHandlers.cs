using cYo.Common.Localize;
using cYo.Common.Text;
using cYo.Common.Threading;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Config;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

/// <summary>
/// <see cref="EventHandler"/> methods, regardless of provider.
/// Basically anything with <see cref="object"/> <paramref name="sender"/> and <see cref="EventArgs"/> parameters.
/// </summary>
public static partial class Program
{
    private static void RemoteServerStarted(object sender, NetworkManager.RemoteServerStartedEventArgs e)
    {
        CallMainForm("Remote Server Started", () => MainForm.OnRemoteServerStarted(e.Information));
    }

    private static void RemoteServerStopped(object sender, NetworkManager.RemoteServerStoppedEventArgs e)
    {
        CallMainForm("Remote Server Stopped", () => MainForm.OnRemoteServerStopped(e.Address));
    }

    private static void ScannerCheckFileIgnore(object sender, ComicScanNotifyEventArgs e)
    {
        if (Settings.DontAddRemoveFiles && Database.IsBlacklisted(e.File))
            e.IgnoreFile = true;

        if (ExtendedSettings.MacCompatibleScanning && Path.GetFileName(e.File).StartsWith("._"))
            e.IgnoreFile = true;
    }

    private static void IgnoredCoverImagesChanged(object sender, EventArgs e)
    {
        ComicInfo.CoverKeyFilter = Settings.IgnoredCoverImages;
    }

    private static void SystemEventsPowerModeChanged(object sender, PowerModeChangedEventArgs e)
    {
        switch (e.Mode)
        {
            case PowerModes.Resume:
                NetworkManager.Start();
                break;
            case PowerModes.Suspend:
                NetworkManager.Stop();
                break;
            case PowerModes.StatusChange:
                break;
        }
    }

    private static void MainFormFormClosing(object sender, FormClosingEventArgs e)
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

    private static void MainFormFormClosed(object sender, FormClosedEventArgs e)
    {
        AutomaticProgressDialog.Process(
            null,
            TR.Messages["ClosingComicRack", "Closing ComicRack"],
            TR.Messages["SaveAllData", "Saving all Data to Disk..."],
            1500,
            CleanUp,
            AutomaticProgressDialogOptions.None
        );
    }

    #region Helpers
    private static void CallMainForm(string actionName, Action action)
    {
        ThreadUtility.RunInBackground(actionName, delegate
        {
            while (MainForm == null || !MainForm.IsInitialized)
            {
                if (MainForm != null && MainForm.IsDisposed)
                    return;
                Thread.Sleep(1000);
            }
            if (!MainForm.InvokeIfRequired(action))
                action();
        });
    }

    private static void CleanUp()
    {
        try
        {
            NetworkManager.Dispose();
            SystemEvents.PowerModeChanged -= SystemEventsPowerModeChanged;
            QueueManager.Dispose();
            News.Save(defaultNewsFile);
            Settings.Save(defaultSettingsFile);
            ImagePool.Dispose();
            DatabaseManager.Dispose();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                StringUtility.Format(
                    TR.Messages["ErrorShutDown", "There was an error shutting down the application:\r\n{0}"],
                    ex.Message
                ),
                TR.Messages["Error", "Error"],
                MessageBoxButtons.OK,
                MessageBoxIcon.Hand
            );
        }
    }
    #endregion
}
