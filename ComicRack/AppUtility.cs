using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.IO;
using cYo.Common.Localize;
using cYo.Common.Text;
using cYo.Common.Win32;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Drawing;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Engine.IO.Provider;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

/// <summary>Methods that were in <see cref="Program"/> that had no business being there.</summary>
/// <remarks>This class shouldn't exist and will be phased out. It's here as a catch-all</remarks>
public static class AppUtility
{
    // PreferencesDialog.Apply
    public static void RefreshAllWindows()
    {
        ForAllForms((Form form) => form.Refresh());
    }

    // PreferencesDialog.Apply
    public static void ForAllForms(Action<Form> action)
    {
        if (action == null)
            return;

        foreach (Form openForm in Application.OpenForms)
            action(openForm);

    }

    // ComicBrowserControl.StartExternalProgram
    public static void StartProgram(string exe, string commandLine)
    {
        try
        {
            Process.Start(exe, commandLine);
        }
        catch (Exception)
        {
        }
    }

    // MainForm
    // ComicBookDialog
    public static string ShowComicOpenDialog(IWin32Window parent, string title, bool includeReadingLists)
    {
        string result = null;
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
            IEnumerable<FileFormat> enumerable = from f in Providers.Readers.GetSourceFormats()
                                                 orderby f
                                                 select f;
            if (includeReadingLists)
                enumerable = enumerable.AddLast(
                    new FileFormat(
                        TR.Load("FileFilter")["FormatReadingList", "ComicRack Reading List"],
                        10089,
                        ".cbl"
                    )
                );

            openFileDialog.Title = title;
            openFileDialog.Filter = enumerable.GetDialogFilter(withAllFilter: true);
            openFileDialog.FilterIndex = AppConfig.Settings.LastOpenFilterIndex;
            openFileDialog.CheckFileExists = true;

            foreach (string favoritePath in Views.ComicListLibraryBrowser.GetFavoritePaths())
                openFileDialog.CustomPlaces.Add(favoritePath);

            if (openFileDialog.ShowDialog(parent) == DialogResult.OK)
                result = openFileDialog.FileName;

            AppConfig.Settings.LastOpenFilterIndex = openFileDialog.FilterIndex;
            return result;
        }
    }

    public static bool ShowExplorer(string path)
    {
        if (string.IsNullOrEmpty(path))
            return false;

        try
        {
            if (File.Exists(path)) // Open explorer and select file
                return FileExplorer.OpenFolderAndSelect(path, AppConfig.ExtendedSettings.OpenExplorerUsingAPI);

            if (Directory.Exists(path)) // Open explorer at directory if path is a directory
                return FileExplorer.OpenFolder(path, AppConfig.ExtendedSettings.OpenExplorerUsingAPI);

            if (Path.GetDirectoryName(path) is string dir && Directory.Exists(dir)) //Open parent dir if file does not exist
                return FileExplorer.OpenFolder(dir, AppConfig.ExtendedSettings.OpenExplorerUsingAPI);
        }
        catch (Exception)
        {
        }
        return false;
    }

    // PreferencesDialog
    // ComicBookDialog
    // CoverViewItem
    public static void StartDocument(string document, string path = null)
    {
        try
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(document);

            if (path != null && Directory.Exists(path))
                processStartInfo.WorkingDirectory = path;

            Process.Start(processStartInfo);
        }
        catch (Exception)
        {
        }
    }

    // ComicListLibraryBrowser
    // FolderComicListProvider
    public static Image MakeBooksImage(IEnumerable<ComicBook> books, Size size, int maxImages, bool onlyMemory)
    {
        int nBooks = books.Count();
        int nImages = Math.Min(maxImages, nBooks);
        int width = size.Width / (nImages + 1);
        int height = size.Height - (nImages - 1) * 3;
        Bitmap bitmap = new Bitmap(size.Width, size.Height);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int nItem = 0;
            foreach (ComicBook item in books.Take(nImages))
            {
                int xLeft = width * nItem;
                int xRight = width * (nItem + 2);
                ThumbnailKey frontCoverThumbnailKey = item.GetFrontCoverThumbnailKey();
                using (IItemLock<ThumbnailImage> itemLock = AppServices.ImagePool.Thumbs.GetImage(frontCoverThumbnailKey, onlyMemory))
                {
                    Image image = itemLock?.Item.Bitmap;
                    if (image != null)
                    {
                        ThumbRenderer.DrawThumbnail(graphics, image, new Rectangle(xLeft, 3 * nItem, xRight - xLeft, height), ThumbnailDrawingOptions.Default | ThumbnailDrawingOptions.KeepAspect, item);
                    }
                }
                nItem++;
            }
            if (nImages != nBooks)
            {
                Color color = Color.FromArgb(192, SystemColors.Highlight);
                Font iconTitleFont = SystemFonts.IconTitleFont;
                string text = StringUtility.Format("{0} {1}", nBooks, TR.Default["Books", "books"]);
                Rectangle rectangle = new Rectangle(Point.Empty, graphics.MeasureString(text, iconTitleFont).ToSize());
                rectangle.Inflate(4, 4);
                rectangle = rectangle.Align(new Rectangle(Point.Empty, size), ContentAlignment.MiddleCenter);
                using (GraphicsPath path = rectangle.ConvertToPath(5, 5))
                {
                    using (Brush brush = new SolidBrush(color))
                    {
                        graphics.FillPath(brush, path);
                    }
                }
                using (StringFormat format = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                })
                {
                    graphics.DrawString(text, iconTitleFont, SystemBrushes.HighlightText, rectangle, format);
                    return bitmap;
                }
            }
            return bitmap;
        }
    }

    // TODO : move to IconManager? Or rename IconManager to something more generic so also move MakeBooksImage()
    // ComicBookDialog
    // ComicBrowserControl
    public static string LoadCustomThumbnail(string file, IWin32Window parent = null, string title = null)
    {
        string result = null;
        if (string.IsNullOrEmpty(file))
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (!string.IsNullOrEmpty(title))
                {
                    openFileDialog.Title = title;
                }
                openFileDialog.Filter = TR.Load("FileFilter")["LoadThumbnail", "JPEG Image|*.jpg|Windows Bitmap Image|*.bmp|PNG Image|*.png|GIF Image|*.gif|TIFF Image|*.tif|Icon Image|*.ico"];
                openFileDialog.CheckFileExists = true;
                if (openFileDialog.ShowDialog(parent) == DialogResult.OK)
                {
                    file = openFileDialog.FileName;
                }
            }
        }
        if (!string.IsNullOrEmpty(file))
        {
            string item = file;
            string text = null;
            try
            {
                if (Path.GetExtension(file).Equals(".ico", StringComparison.OrdinalIgnoreCase))
                {
                    using (Bitmap bitmap = BitmapExtensions.LoadIcon(file, Color.Transparent))
                    {
                        file = (text = Path.GetTempFileName());
                        bitmap.Save(text, ImageFormat.Png);
                    }
                }
                using (Bitmap bmp = BitmapExtensions.BitmapFromFile(file))
                {
                    result = AppServices.ImagePool.AddCustomThumbnail(bmp);
                }
                AppConfig.Settings.ThumbnailFiles.UpdateMostRecent(item);
                return result;
            }
            catch (Exception ex)
            {
                AppConfig.Settings.ThumbnailFiles.Remove(file);
                MessageBox.Show(parent, string.Format(TR.Messages["CouldNotLoadThumbnail", "Could not load thumbnail!\nReason: {0}"], ex.Message), TR.Messages["Attention", "Attention"], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return result;
            }
            finally
            {
                if (text != null)
                {
                    FileUtility.SafeDelete(text);
                }
            }
        }
        return result;
    }

    public static bool AskQuestion(IWin32Window parent, string question, string okButton, HiddenMessageBoxes hmb, string askAgainText = null, string cancelButton = null)
    {
        if ((AppConfig.Settings.HiddenMessageBoxes & hmb) != 0)
            return true;

        if (string.IsNullOrEmpty(askAgainText))
            askAgainText = TR.Messages["NotAskAgain", "&Do not ask me again"];

        switch (QuestionDialog.AskQuestion(parent, question, okButton, askAgainText, null, showCancel: true, cancelButton))
        {
            case var type when type.HasFlag(QuestionResult.Cancel):
                return false;
            case QuestionResult.OkWithOption:
                AppConfig.Settings.HiddenMessageBoxes |= hmb;
                break;
        }
        return true;
    }

    public static void NewSplashDialog(Splash splash, ManualResetEvent mre)
    {
        Rectangle bounds = Screen.FromPoint(AppConfig.Settings.CurrentWorkspace.FormBounds.Location).Bounds;
        splash = new Splash { Fade = true };
        splash.Location = splash
            .Bounds
            .Align(bounds, ContentAlignment.MiddleCenter)
            .Location;
        splash.VisibleChanged += (_, _) => mre.Set();
        splash.Closed += (_, _) => splash = null;
        splash.ShowDialog();
    }
}
