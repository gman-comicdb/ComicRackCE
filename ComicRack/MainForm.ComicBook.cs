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

// either has ComicBook parameter or returns ComicBook
public partial class MainForm : Form, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    public ComicBook GetBook(string file)
    {
        return Program.BookFactory.Create(file, CreateBookOption.AddToTemporary);
    }

    public ComicBook AddNewBook(bool showDialog = true)
    {
        ComicBook comicBook = new ComicBook
        {
            AddedTime = DateTime.Now
        };
        if (showDialog && !ComicBookDialog.Show(Form.ActiveForm ?? this, comicBook, null, null))
        {
            return null;
        }
        Program.Database.Add(comicBook);
        return comicBook;
    }

    public bool RemoveBook(ComicBook cb)
    {
        return Program.Database.Remove(cb);
    }

    public void ConvertComic(IEnumerable<ComicBook> books, ExportSetting setting)
    {
        ExportSetting exportSetting = setting ?? ExportComicsDialog.Show(this, Program.ExportComicRackPresets, Program.Settings.ExportUserPresets, Program.Settings.CurrentExportSetting ?? new ExportSetting());
        if (exportSetting == null)
        {
            return;
        }
        bool flag = books.All((ComicBook b) => b.EditMode.IsLocalComic());
        Program.Settings.CurrentExportSetting = exportSetting;
        if (flag && (exportSetting.Target == ExportTarget.ReplaceSource || exportSetting.DeleteOriginal) && !Program.AskQuestion(this, TR.Messages["AskExport", "You have chosen to delete or replace existing files during export. Are you sure you want to continue?\nThe deleted files will be moved to the Recycle Bin during export. Please make sure there is enough disk space available and the eComics are not located on a network drive!"], TR.Messages["Export", "Export"], HiddenMessageBoxes.ConvertComics))
        {
            return;
        }
        exportSetting = CloneUtility.Clone(exportSetting);
        if (!flag)
        {
            exportSetting.DeleteOriginal = false;
            if (exportSetting.Target == ExportTarget.ReplaceSource || exportSetting.Target == ExportTarget.SameAsSource)
            {
                if (exportSetting.Target == ExportTarget.ReplaceSource)
                {
                    exportSetting.AddToLibrary = true;
                }
                exportSetting.Target = ExportTarget.NewFolder;
                using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
                {
                    folderBrowserDialog.Description = TR.Messages["SelectLocalFolder", "Select a local folder to store the remote Books"];
                    folderBrowserDialog.ShowNewFolderButton = true;
                    if (folderBrowserDialog.ShowDialog(this) == DialogResult.Cancel || string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                    {
                        return;
                    }
                    exportSetting.TargetFolder = folderBrowserDialog.SelectedPath;
                }
            }
        }
        if (exportSetting.Combine)
        {
            if (exportSetting.Target != ExportTarget.Ask)
            {
                Program.QueueManager.ExportComic(books, exportSetting, 0);
                return;
            }
            ComicBook comicBook = books.FirstOrDefault();
            if (comicBook != null)
            {
                ExportSetting exportSetting2 = FileSaveDialog(comicBook, exportSetting);
                if (exportSetting2 != null)
                {
                    Program.QueueManager.ExportComic(books, exportSetting2, 0);
                }
            }
            return;
        }
        int num = 0;
        foreach (ComicBook book in books)
        {
            ExportSetting exportSetting3 = ((exportSetting.Target == ExportTarget.Ask) ? FileSaveDialog(book, exportSetting) : exportSetting);
            if (exportSetting3 == null)
            {
                break;
            }
            Program.QueueManager.ExportComic(book, exportSetting3, num++);
        }
    }

    private ExportSetting FileSaveDialog(ComicBook cb, ExportSetting cs)
    {
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            FileFormat fileFormat = cs.GetFileFormat(cb);
            saveFileDialog.Title = TR.Messages["ExportComicTitle", "Export Book to"];
            saveFileDialog.Filter = new FileFormat[1]
            {
                    fileFormat
            }.GetDialogFilter(withAllFilter: false);
            saveFileDialog.FileName = cs.GetTargetFileName(cb, 0);
            saveFileDialog.DefaultExt = fileFormat.MainExtension;
            if (saveFileDialog.ShowDialog(this) == DialogResult.Cancel)
            {
                return null;
            }
            ExportSetting exportSetting = CloneUtility.Clone(cs);
            exportSetting.Target = ExportTarget.NewFolder;
            exportSetting.Naming = ExportNaming.Custom;
            exportSetting.CustomNamingStart = 0;
            exportSetting.TargetFolder = Path.GetDirectoryName(saveFileDialog.FileName);
            exportSetting.CustomName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
            return exportSetting;
        }
    }

    public IEnumerable<ComicBook> GetLibraryBooks()
    {
        return Program.Database.Books.ToArray();
    }

    public bool SetCustomBookThumbnail(ComicBook cb, Bitmap bmp)
    {
        if (cb.IsLinked)
        {
            return false;
        }
        cb.CustomThumbnailKey = Program.ImagePool.AddCustomThumbnail(bmp);
        return true;
    }

    public IEnumerable<ComicBook> ReadDatabaseBooks(string file)
    {
        throw new Exception("The method or operation is not implemented.");
    }

    #region Bitmap
    public Bitmap GetComicPage(ComicBook cb, int page)
    {
        try
        {
            using (IItemLock<PageImage> itemLock = Program.ImagePool.GetPage(cb.GetPageKey(page, BitmapAdjustment.Empty), cb))
            {
                if (itemLock == null || itemLock.Item == null || itemLock.Item.Bitmap == null)
                {
                    return null;
                }
                return itemLock.Item.Bitmap.Clone() as Bitmap;
            }
        }
        catch
        {
            return null;
        }
    }

    public Bitmap GetComicThumbnail(ComicBook cb, int page)
    {
        try
        {
            using (IItemLock<ThumbnailImage> itemLock = Program.ImagePool.GetThumbnail(cb.GetThumbnailKey(page), cb))
            {
                if (itemLock == null || itemLock.Item == null || itemLock.Item.Bitmap == null)
                {
                    return null;
                }
                return itemLock.Item.Bitmap.Clone() as Bitmap;
            }
        }
        catch
        {
            return null;
        }
    }

    public Bitmap GetComicPublisherIcon(ComicBook cb)
    {
        Image image = ComicBook.PublisherIcons.GetImage(cb.GetPublisherIconKey()) ?? ComicBook.PublisherIcons.GetImage(cb.Publisher);
        return image.CreateCopy(alwaysTrueCopy: true);
    }

    public Bitmap GetComicImprintIcon(ComicBook cb)
    {
        Image image = ComicBook.PublisherIcons.GetImage(cb.GetImprintIconKey()) ?? ComicBook.PublisherIcons.GetImage(cb.Imprint);
        return image.CreateCopy(alwaysTrueCopy: true);
    }

    public Bitmap GetComicAgeRatingIcon(ComicBook cb)
    {
        return ComicBook.AgeRatingIcons.GetImage(cb.AgeRating).CreateCopy(alwaysTrueCopy: true);
    }

    public Bitmap GetComicFormatIcon(ComicBook cb)
    {
        return ComicBook.FormatIcons.GetImage(cb.Format).CreateCopy(alwaysTrueCopy: true);
    }
    #endregion

}
