using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using cYo.Common.Collections;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Views;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm
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
