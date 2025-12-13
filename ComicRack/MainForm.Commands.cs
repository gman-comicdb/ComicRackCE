using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cYo.Common.Collections;
using cYo.Common.Drawing;
using cYo.Common.IO;
using cYo.Common.Localize;
using cYo.Common.Text;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Views;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm
{
    public void AddFolderToLibrary()
    {
        using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
        {
            folderBrowserDialog.Description = TR.Messages["AddFolderLibrary", "Books in this Folder and all sub Folders will be added to the library."];
            folderBrowserDialog.ShowNewFolderButton = true;
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
            {
                Program.Scanner.ScanFileOrFolder(folderBrowserDialog.SelectedPath, all: true, removeMissing: false);
            }
        }
    }

    public void ExportCurrentImage()
    {
        if (ComicDisplay.Book == null || ComicDisplay.Book.Comic == null)
            return;

        using Image image = ComicDisplay.CreatePageImage();
        if (image != null)
            ExportImage(
                StringUtility.Format("{0} - {1} {2}",
                ComicDisplay.Book.Comic.Caption,
                TR.Default["Page", "Page"],
                ComicDisplay.Book.CurrentPage + 1),
                image);
    }

    private void FocusQuickSearch()
    {
        this.FindActiveService<ISearchOptions>()?.FocusQuickSearch();
    }

    public void UpdateComics()
    {
        Program.Database.Books.Concat(Program.BookFactory.TemporaryBooks).ForEach((ComicBook cb) =>
        {
            Program.QueueManager.AddBookToFileUpdate(cb, alwaysWrite: true);
        });
    }

    public void SetBookmark()
    {
        BookmarkEditorWrapper bookmarkEditor = GetBookmarkEditor();
        if (bookmarkEditor.CanBookmark)
        {
            bookmarkEditor.Bookmark = SelectItemDialog.GetName<string>(Form.ActiveForm ?? this, TR.Default["Bookmark", "Bookmark"], bookmarkEditor.BookmarkProposal, null);
        }
    }

    public bool SyncBrowser()
    {
        if (ComicDisplay == null || ComicDisplay.Book == null || ComicDisplay.Book.Comic == null)
        {
            return false;
        }
        ComicBook comic = ComicDisplay.Book.Comic;
        IComicBrowser comicBrowser = this.FindServices<IComicBrowser>().FirstOrDefault((IComicBrowser b) => b.Library == comic.Container);
        if (comicBrowser == null)
        {
            return false;
        }
        ToggleBrowser(alwaysShow: true, comicBrowser);
        if (comicBrowser.SelectComic(ComicDisplay.Book.Comic))
        {
            return true;
        }
        if (comic.LastOpenedFromListId != Guid.Empty)
        {
            ComicListItem comicListItem = comicBrowser.Library.ComicLists.GetItems<ComicListItem>().FirstOrDefault((ComicListItem li) => li.Id == comic.LastOpenedFromListId);
            if (comicListItem != null && ShowBookInList(comicBrowser.Library, comicListItem, comic))
            {
                return true;
            }
        }
        return false;
    }

    #region Helpers
    public void ExportImage(string name, Image image)
    {
        using (SaveFileDialog saveFileDialog = new SaveFileDialog())
        {
            saveFileDialog.Title = LocalizeUtility.GetText(this, "SavePageTitle", "Save Page as");
            saveFileDialog.Filter = TR.Load("FileFilter")["PageImageSave", "JPEG Image|*.jpg|Windows Bitmap Image|*.bmp|PNG Image|*.png|GIF Image|*.gif|TIFF Image|*.tif"];
            saveFileDialog.FileName = FileUtility.MakeValidFilename(name);
            saveFileDialog.FilterIndex = Program.Settings.LastExportPageFilterIndex;
            IWin32Window owner = Form.ActiveForm ?? this;
            if (saveFileDialog.ShowDialog(owner) != DialogResult.OK)
            {
                return;
            }
            Program.Settings.LastExportPageFilterIndex = saveFileDialog.FilterIndex;
            name = saveFileDialog.FileName;
            try
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        image.SaveImage(AddExtension(name, ".jpg"), ImageFormat.Jpeg, 24);
                        break;
                    case 2:
                        image.SaveImage(AddExtension(name, ".bmp"), ImageFormat.Bmp, 24);
                        break;
                    case 3:
                        image.SaveImage(AddExtension(name, ".png"), ImageFormat.Png, 24);
                        break;
                    case 4:
                        image.SaveImage(AddExtension(name, ".gif"), ImageFormat.Gif, 8);
                        break;
                    case 5:
                        image.SaveImage(AddExtension(name, ".tif"), ImageFormat.Tiff, 24);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, StringUtility.Format(TR.Messages["CouldNotSaveImage", "Could not save the page image!\nReason: {0}"], ex.Message), TR.Messages["Error", "Error"], MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }

    private static string AddExtension(string file, string ext)
    {
        if (!Path.HasExtension(file))
            return file + ext;
        return file;
    }
    #endregion
}
