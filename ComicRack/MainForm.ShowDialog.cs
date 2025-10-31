using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using cYo.Common.Collections;
using cYo.Common.Drawing;
using cYo.Common.Localize;
using cYo.Common.Threading;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Views;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm : FormEx, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    // ExportImage - SaveFileDialog

    public void ShowComicInfo(IEnumerable<ComicBook> books)
    {
        books = (books ?? Enumerable.Empty<ComicBook>()).Where((ComicBook cb) => cb.EditMode.CanEditProperties());
        if (books.IsEmpty())
        {
            return;
        }
        if (books.Count() > 1)
        {
            Program.Database.Undo.SetMarker(TR.Messages["UndoEditMultipleComics", "Edit multiple Books"]);
            using (MultipleComicBooksDialog multipleComicBooksDialog = new MultipleComicBooksDialog(books))
            {
                multipleComicBooksDialog.ShowDialog(this);
            }
        }
        else
        {
            Program.Database.Undo.SetMarker(TR.Messages["UndoShowInfo", "Show Info"]);
            ComicBookDialog.Show(Form.ActiveForm ?? this, books.FirstOrDefault(), null, null);
        }
    }

    public void ShowNews()
    {
        ShowNews(always: true);
    }

    public void ShowNews(bool always)
    {
        if (always)
        {
            AutomaticProgressDialog.Process(this, TR.Messages["RetrieveNews", "Retrieving News"], TR.Messages["RetrieveNewsText", "Refreshing subscribed News Channels"], 1000, UpdateFeeds, AutomaticProgressDialogOptions.EnableCancel);
            NewsDialog.ShowNews(this, Program.News);
            return;
        }
        ThreadUtility.RunInBackground("Read News", delegate
        {
            UpdateFeeds();
            if (Program.News.HasUnread)
            {
                this.BeginInvoke(delegate
                {
                    NewsDialog.ShowNews(this, Program.News);
                });
            }
        });
    }

    public void ShowInfo()
    {
        IGetBookList getBookList = FormUtility.FindActiveService<IGetBookList>();
        if (getBookList == null)
        {
            return;
        }
        IEnumerable<ComicBook> bookList = getBookList.GetBookList(ComicBookFilterType.Selected);
        if (bookList.Count() > 1 && bookList.All((ComicBook cb) => cb.EditMode.CanEditProperties()))
        {
            Program.Database.Undo.SetMarker(TR.Messages["UndoEditMultipleComics", "Edit multiple Books"]);
            using (MultipleComicBooksDialog multipleComicBooksDialog = new MultipleComicBooksDialog(bookList))
            {
                multipleComicBooksDialog.ShowDialog(this);
            }
        }
        else if (!bookList.IsEmpty())
        {
            IComicBrowser comicBrowser = FormUtility.FindActiveService<IComicBrowser>();
            Program.Database.Undo.SetMarker(TR.Messages["UndoShowInfo", "Show Info"]);
            ComicBookDialog.Show(Form.ActiveForm ?? this, bookList.FirstOrDefault(), getBookList.GetBookList(ComicBookFilterType.All).ToArray(), (comicBrowser != null) ? new Func<ComicBook, bool>(comicBrowser.SelectComic) : null);
        }
    }

    public void ShowPortableDevices(DeviceSyncSettings dss = null, Guid? guid = null)
    {
        DevicesEditDialog.Show(Form.ActiveForm ?? this, Program.Settings.Devices, dss, guid);
    }

    public void ShowPreferences(string autoInstallplugin = null)
    {
        KeyboardShortcuts keyboardMap = new KeyboardShortcuts(ComicDisplay.KeyboardMap);
        if (PreferencesDialog.Show(Form.ActiveForm ?? this, keyboardMap, ScriptUtility.Scripts, autoInstallplugin))
        {
            ComicDisplay.KeyboardMap = keyboardMap;
        }
    }

    private void ShowPendingTasks(int tab = 0)
    {
        if (taskDialog != null && !taskDialog.IsDisposed)
        {
            taskDialog.SelectedTab = tab;
            taskDialog.Activate();
        }
        else
        {
            taskDialog = TasksDialog.Show(this, Program.QueueManager.GetQueues(), tab);
        }
    }

    public void MenuSynchronizeDevices()
    {
        StoreWorkspace(); // save workspace before sync, so sorted lists key are up to date
        if (!Program.QueueManager.SynchronizeDevices())
        {
            ShowPortableDevices();
        }
    }

    public void ShowAboutDialog()
    {
        using (Splash splash = new Splash())
        {
            splash.Fade = true;
            splash.Location = splash.Bounds.Align(Screen.FromPoint(base.Location).Bounds, ContentAlignment.MiddleCenter).Location;
            splash.ShowDialog(this);
        }
    }

    public void ShowOpenDialog()
    {
        string text = Program.ShowComicOpenDialog(Form.ActiveForm ?? this, miOpenComic.Text.Replace("&", ""), includeReadingLists: true);
        if (text != null)
        {
            OpenSupportedFile(text, Program.Settings.OpenInNewTab);
        }
    }

    public int AskQuestion(string question, string buttonText, string optionText)
    {
        switch (QuestionDialog.AskQuestion(this, question, buttonText, optionText))
        {
            default:
                return 0;
            case QuestionResult.Ok:
                return 1;
            case QuestionResult.OkWithOption:
                return 2;
        }
    }

    #region Helpers
    public void UpdateFeeds()
    {
        using (ItemMonitor.Lock(Program.News))
        {
            Program.News.UpdateFeeds(Program.NewsIntervalMinutes);
        }
    }
    #endregion
}
