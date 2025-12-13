using System;
using System.Collections.Generic;
using System.Windows.Forms;
using cYo.Common.Net;
using cYo.Common.Text;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Views;

namespace cYo.Projects.ComicRack.Viewer;

// only references are not from MainForm
public partial class MainForm
{
    public IEnumerable<string> LibraryPaths => Program.Settings.ScriptingLibraries
        .Replace("\n", "")
        .Replace("\r", "")
        .Split(';', StringSplitOptions.RemoveEmptyEntries);

    public void SelectComics(IEnumerable<ComicBook> books)
    {
        this.FindActiveService<IComicBrowser>()?.SelectComics(books);
    }

    public void ShowComic()
    {
        if (!ReaderUndocked && mainViewContainer.Dock == DockStyle.Fill)
            mainView.ShowView(books.CurrentSlot);
    }

    void IApplication.SynchronizeDevices()
    {
        StoreWorkspace(); // save workspace before sync, so sorted lists key are up to date
        Program.QueueManager.SynchronizeDevices();
    }

    public IDictionary<string, string> GetComicFields() => ComicBook.GetTranslatedWritableStringProperties();

    public string ReadInternet(string uri) => HttpAccess.ReadText(uri);
}
