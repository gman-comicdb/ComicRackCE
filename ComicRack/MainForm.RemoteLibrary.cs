using System.Windows.Forms;
using cYo.Common.Localize;
using cYo.Common.Text;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine.IO.Network;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Views;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm
{
    public bool AddRemoteLibrary(ShareInformation info, MainView.AddRemoteLibraryOptions options)
    {
        if (info == null)
        {
            return false;
        }
        if (mainView.IsRemoteConnected(info.Uri))
        {
            return true;
        }
        mainView.AddRemoteLibrary(ComicLibraryClient.Connect(info), options);
        return true;
    }

    public void OnRemoteServerStarted(ShareInformation info)
    {
        MainView.AddRemoteLibraryOptions addRemoteLibraryOptions = MainView.AddRemoteLibraryOptions.Auto;
        if (Program.Settings.AutoConnectShares && info.IsLocal)
        {
            addRemoteLibraryOptions |= MainView.AddRemoteLibraryOptions.Open;
        }
        AddRemoteLibrary(info, addRemoteLibraryOptions);
    }

    public void OnRemoteServerStopped(string address)
    {
        mainView.RemoveRemoteLibrary(address);
    }

    public void OpenRemoteLibrary()
    {
        RemoteShareItem share = OpenRemoteDialog.GetShare(this, Program.Settings.RemoteShares.First, Program.Settings.RemoteShares, showPublic: false);
        if (share != null && !string.IsNullOrEmpty(share.Uri))
        {
            string serverName = share.Uri;
            ShareInformation serverInfo = null;
            AutomaticProgressDialog.Process(this, TR.Messages["ConnectToServer", "Connecting to Server"], TR.Messages["GetShareInfoText", "Getting information about the shared Library"], 1000, delegate
            {
                serverInfo = ComicLibraryClient.GetServerInfo(serverName);
            }, AutomaticProgressDialogOptions.EnableCancel);
            if (serverInfo == null || !AddRemoteLibrary(serverInfo, MainView.AddRemoteLibraryOptions.Open | MainView.AddRemoteLibraryOptions.Select))
            {
                MessageBox.Show(this, StringUtility.Format(TR.Messages["ConnectRemoteError", "Failed to connect to remote Server"], share), TR.Messages["Error", "Error"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Program.Settings.RemoteShares.UpdateMostRecent(new RemoteShareItem(serverInfo));
            }
        }
    }
}
