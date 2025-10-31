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

public partial class MainForm : Form, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    public void UpdateWebComics(bool refresh = false)
    {
        Program.Database.Books.Concat(Program.BookFactory.TemporaryBooks).ForEach((ComicBook cb) =>
        {
            UpdateWebComic(cb, refresh);
        });
    }

    public void UpdateWebComics()
    {
        UpdateWebComics(refresh: false);
    }

    public void UpdateWebComic(ComicBook cb, bool refresh)
    {
        Program.QueueManager.AddBookToDynamicUpdate(cb, refresh);
    }

    public bool OpenNextComic(int relative)
    {
        return OpenNextComic(relative, OpenComicOptions.None);
    }

    public bool OpenNextComic()
    {
        return OpenNextComic(1);
    }

    public bool OpenPrevComic()
    {
        return OpenNextComic(-1);
    }

    public bool OpenRandomComic()
    {
        return OpenNextComic(0);
    }

    public bool OpenNextComic(int relative, OpenComicOptions openOptions)
    {
        if (ComicDisplay == null || ComicDisplay.Book == null)
        {
            return false;
        }
        ComicBook comic = ComicDisplay.Book.Comic;
        if (comic == null)
        {
            return false;
        }
        IComicBrowser comicBrowser = this.FindServices<IComicBrowser>().FirstOrDefault((IComicBrowser cb) => cb.Library == comic.Container);
        if (comicBrowser == null)
        {
            return false;
        }
        if (comicBrowser.Library != null && comic.LastOpenedFromListId != Guid.Empty)
        {
            ComicListItem comicListItem = comicBrowser.Library.ComicLists.GetItems<ComicListItem>().FirstOrDefault((ComicListItem li) => li.Id == comic.LastOpenedFromListId);
            if (comicListItem != null)
            {
                ShowBookInList(comicBrowser.Library, comicListItem, comic, switchToList: false);
            }
        }
        ComicBook[] array = comicBrowser.GetBookList(ComicBookFilterType.IsNotFileless).ToArray();
        if (array.Length == 0)
        {
            return false;
        }
        int num = array.FindIndex((ComicBook cb) => cb.Id == comic.Id);
        ComicBook comicBook;
        if (relative != 0)
        {
            if (num == -1)
            {
                return false;
            }
            num += relative;
            if (num < 0 || num >= array.Length)
            {
                return false;
            }
            comicBook = array[num];
        }
        else
        {
            if (!lastRandomList.SequenceEqual(array))
            {
                lastRandomList = array;
                randomSelectedComics = new List<ComicBook>();
            }
            if (lastRandomList.Length == randomSelectedComics.Count)
            {
                randomSelectedComics.Clear();
            }
            ComicBook[] array2 = lastRandomList.Except(randomSelectedComics).ToArray();
            int num2 = new Random().Next(0, array2.Length);
            comicBook = array2[num2];
            randomSelectedComics.Add(comicBook);
        }
        if (comicBook == null)
        {
            return false;
        }
        comicBook.LastOpenedFromListId = comicBrowser.GetBookListId();
        return books.Open(comicBook, openOptions);
    }
}
