using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.IO;
using cYo.Common.Localize;
using cYo.Common.Runtime;
using cYo.Common.Text;
using cYo.Common.Win32;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Drawing;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Engine.IO.Cache;
using cYo.Projects.ComicRack.Engine.IO.Provider;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace cYo.Projects.ComicRack.Viewer;

/// <summary>
/// Methods called by <see cref="Dialogs"/> or <see cref="Views"/>
/// </summary>
public static partial class Program
{
    public static bool AskQuestion(IWin32Window parent, string question, string okButton, HiddenMessageBoxes hmb, string askAgainText = null, string cancelButton = null)
    {
        if ((Settings.HiddenMessageBoxes & hmb) != 0)
        {
            return true;
        }
        if (string.IsNullOrEmpty(askAgainText))
        {
            askAgainText = TR.Messages["NotAskAgain", "&Do not ask me again"];
        }
        switch (QuestionDialog.AskQuestion(parent, question, okButton, askAgainText, null, showCancel: true, cancelButton))
        {
            case var type when type.HasFlag(QuestionResult.Cancel):
                return false;
            case QuestionResult.OkWithOption:
                Settings.HiddenMessageBoxes |= hmb;
                break;
        }
        return true;
    }

    // Program.StartNew
    // MainForm
    public static void StartupProgress(string message, int progress)
    {
        Splash splash = Program.splash;
        if (splash != null)
        {
            splash.Message = splash.Message.AppendWithSeparator("\n", message);
            if (progress >= 0)
            {
                splash.Progress = progress;
            }
        }
    }

    // Program.ShowComicOpenDialog
    // ComicListLibraryBrowser
    public static IEnumerable<string> GetFavoritePaths()
    {
        return Settings.FavoriteFolders.Concat(Database.WatchFolders.Select((WatchFolder wf) => wf.Folder)).Distinct();
    }

    // MainForm
    // PreferencesDialog
    // ComicBrowserControl
    public static bool ShowExplorer(string path)
    {
        if (string.IsNullOrEmpty(path))
            return false;

        try
        {
            if (File.Exists(path)) // Open explorer and select file
                return FileExplorer.OpenFolderAndSelect(path, Program.ExtendedSettings.OpenExplorerUsingAPI);

            if (Directory.Exists(path)) // Open explorer at directory if path is a directory
                return FileExplorer.OpenFolder(path, Program.ExtendedSettings.OpenExplorerUsingAPI);

            if (Path.GetDirectoryName(path) is string dir && Directory.Exists(dir)) //Open parent dir if file does not exist
                return FileExplorer.OpenFolder(dir, Program.ExtendedSettings.OpenExplorerUsingAPI);
        }
        catch (Exception)
        {
        }
        return false;
    }

    // MainForm
    public static void Collect()
    {
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
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
            {
                enumerable = enumerable.AddLast(new FileFormat(TR.Load("FileFilter")["FormatReadingList", "ComicRack Reading List"], 10089, ".cbl"));
            }
            openFileDialog.Title = title;
            openFileDialog.Filter = enumerable.GetDialogFilter(withAllFilter: true);
            openFileDialog.FilterIndex = Settings.LastOpenFilterIndex;
            openFileDialog.CheckFileExists = true;
            foreach (string favoritePath in GetFavoritePaths())
            {
                openFileDialog.CustomPlaces.Add(favoritePath);
            }
            if (openFileDialog.ShowDialog(parent) == DialogResult.OK)
            {
                result = openFileDialog.FileName;
            }
            Settings.LastOpenFilterIndex = openFileDialog.FilterIndex;
            return result;
        }
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
            {
                processStartInfo.WorkingDirectory = path;
            }
            Process.Start(processStartInfo);
        }
        catch (Exception)
        {
        }
    }

    // PreferencesDialog
    public static void RefreshAllWindows()
    {
        ForAllForms((Form f) =>
        {
            f.Refresh();
        });
    }

    // PreferencesDialog
    public static void ForAllForms(Action<Form> action)
    {
        if (action == null)
        {
            return;
        }
        foreach (Form openForm in Application.OpenForms)
        {
            action(openForm);
        }
    }

    // MatcherEditor
    public static ContextMenuStrip CreateComicBookMatchersMenu(Action<ComicBookValueMatcher> action, int minUsage = 20)
    {
        ContextMenuBuilder contextMenuBuilder = new ContextMenuBuilder();
        Type[] source = (from m in GetUsedComicBookMatchers(5)
                         select m.GetType()).ToArray();
        foreach (ComicBookValueMatcher availableComicBookMatcher in GetAvailableComicBookMatchers())
        {
            ComicBookValueMatcher i = availableComicBookMatcher;
            contextMenuBuilder.Add(availableComicBookMatcher.Description, topLevel: false, chk: false, delegate
            {
                action(i);
            }, null, source.Contains(availableComicBookMatcher.GetType()) ? DateTime.MaxValue : DateTime.MinValue);
        }
        ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
        contextMenuStrip.Items.AddRange(contextMenuBuilder.Create(20));
        return contextMenuStrip;
    }

    // ComicDisplaySettingsDialog
    public static string[] LoadDefaultBackgroundTextures()
    {
        return (from s in FileUtility.GetFiles(IniFile.GetDefaultLocations(DefaultBackgroundTexturesPath), SearchOption.AllDirectories, ".jpg", ".gif", ".png")
                orderby s
                select s).ToArray();
    }

    // ComicDisplaySettingsDialog
    public static string[] LoadDefaultPaperTextures()
    {
        return (from s in FileUtility.GetFiles(IniFile.GetDefaultLocations(DefaultPaperTexturesPath), SearchOption.AllDirectories, ".jpg", ".gif", ".png")
                orderby s
                select s).ToArray();
    }

    // ComicBrowserControl
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

    // ComicListLibraryBrowser
    // FolderComicListProvider
    public static Image MakeBooksImage(IEnumerable<ComicBook> books, Size size, int maxImages, bool onlyMemory)
    {
        int num = books.Count();
        int num2 = Math.Min(maxImages, num);
        int num3 = size.Width / (num2 + 1);
        int height = size.Height - (num2 - 1) * 3;
        Bitmap bitmap = new Bitmap(size.Width, size.Height);
        using (Graphics graphics = Graphics.FromImage(bitmap))
        {
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            int num4 = 0;
            foreach (ComicBook item in books.Take(num2))
            {
                int num5 = num3 * num4;
                int num6 = num3 * (num4 + 2);
                ThumbnailKey frontCoverThumbnailKey = item.GetFrontCoverThumbnailKey();
                using (IItemLock<ThumbnailImage> itemLock = ImagePool.Thumbs.GetImage(frontCoverThumbnailKey, onlyMemory))
                {
                    Image image = itemLock?.Item.Bitmap;
                    if (image != null)
                    {
                        ThumbRenderer.DrawThumbnail(graphics, image, new Rectangle(num5, 3 * num4, num6 - num5, height), ThumbnailDrawingOptions.Default | ThumbnailDrawingOptions.KeepAspect, item);
                    }
                }
                num4++;
            }
            if (num2 != num)
            {
                Color color = Color.FromArgb(192, SystemColors.Highlight);
                Font iconTitleFont = SystemFonts.IconTitleFont;
                string text = StringUtility.Format("{0} {1}", num, TR.Default["Books", "books"]);
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
                    result = ImagePool.AddCustomThumbnail(bmp);
                }
                Settings.ThumbnailFiles.UpdateMostRecent(item);
                return result;
            }
            catch (Exception ex)
            {
                Settings.ThumbnailFiles.Remove(file);
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

    #region Helpers
    public static IEnumerable<ComicBookValueMatcher> GetAvailableComicBookMatchers()
    {
        return ComicBookValueMatcher.GetAvailableMatchers().OfType<ComicBookValueMatcher>();
    }

    public static IEnumerable<ComicBookValueMatcher> GetUsedComicBookMatchers(int minUsage)
    {
        return from n in Database.ComicLists.GetItems<ComicSmartListItem>().SelectMany((ComicSmartListItem n) => n.Matchers.Recurse<ComicBookValueMatcher>((object o) => (!(o is ComicBookGroupMatcher)) ? null : ((ComicBookGroupMatcher)o).Matchers))
               select n.GetType() into n
               group n by n into g
               where g.Count() >= minUsage
               select g into t
               select Activator.CreateInstance(t.First()) as ComicBookValueMatcher into n
               orderby n.Description
               select n;
    }
    #endregion
}
