using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
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
using cYo.Common.Windows.Extensions;
using cYo.Common.Windows.Forms;
using cYo.Common.Windows.Rendering;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Controls;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.Display.Forms;
using cYo.Projects.ComicRack.Engine.Drawing;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Engine.IO.Network;
using cYo.Projects.ComicRack.Engine.IO.Provider;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Controls;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Menus;
using cYo.Projects.ComicRack.Viewer.Properties;
using cYo.Projects.ComicRack.Viewer.Views;
using Microsoft.Win32;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace cYo.Projects.ComicRack.Viewer;

// only references are not from MainForm
public partial class MainForm : Form, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    public IEnumerable<string> LibraryPaths => Program.Settings.ScriptingLibraries.Replace("\n", "").Replace("\r", "").Split(';', StringSplitOptions.RemoveEmptyEntries);

    public void SelectComics(IEnumerable<ComicBook> books)
    {
        this.FindActiveService<IComicBrowser>()?.SelectComics(books);
    }

    public void ShowComic()
    {
        if (!ReaderUndocked && mainViewContainer.Dock == DockStyle.Fill)
        {
            mainView.ShowView(books.CurrentSlot);
        }
    }

    void IApplication.SynchronizeDevices()
    {
        StoreWorkspace(); // save workspace before sync, so sorted lists key are up to date
        Program.QueueManager.SynchronizeDevices();
    }

    public IDictionary<string, string> GetComicFields()
    {
        return ComicBook.GetTranslatedWritableStringProperties();
    }

    public string ReadInternet(string text)
    {
        return HttpAccess.ReadText(text);
    }
}
