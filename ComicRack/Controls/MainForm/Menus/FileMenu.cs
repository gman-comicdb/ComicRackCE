using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.IO;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.IO;
using System;
using System.IO;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class FileMenu : UserControl
{
    private string[] RecentFiles = [];

    // TODO : assign function. Or whatever, to make it functional
    public Func<string, bool, bool> OpenFile;

    public ToolStripMenuItem Item;

    public FileMenu()
    {
        InitializeComponent();
        Item = fileMenuItem;
    }

    public static implicit operator ToolStripMenuItem(FileMenu menu)
        => menu.Item;

    private void fileMenu_DropDownOpening(object sender, EventArgs e)
    {
        miUpdateAllComicFiles.Visible = !Program.Settings.AutoUpdateComicsFiles;
    }

    private void RecentFilesMenuOpening(object sender, EventArgs e)
    {
        int num = 0;
        foreach (ToolStripMenuItem dropDownItem in miOpenRecent.DropDownItems)
        {
            if (dropDownItem.Image != null)
            {
                dropDownItem.Image.Dispose();
            }
        }
        FormUtility.SafeToolStripClear(miOpenRecent.DropDownItems);

        foreach (string path in RecentFiles)
        {
            if (!File.Exists(path))
            {
                continue;
            }
            string displayPath = ++num + " - " + FormUtility.FixAmpersand(FileUtility.GetSafeFileName(path));
            using (IItemLock<ThumbnailImage> itemLock = Program.ImagePool.Thumbs.GetImage(Program.BookFactory.Create(path, CreateBookOption.DoNotAdd).GetFrontCoverThumbnailKey()))
            {
                try
                {
                    ToolStripMenuItem value = new(displayPath, (itemLock != null && itemLock.Item != null) ? itemLock.Item.Bitmap.Resize(16, 16) : null, OnOpenRecent);
                    miOpenRecent.DropDownItems.Add(value);
                }
                catch (Exception)
                {
                }
            }
        }
    }

    private void OnOpenRecent(object sender, EventArgs e)
    {
        string text = ((ToolStripMenuItem)sender).Text;
        int num = Convert.ToInt32(text.Substring(0, 2)) - 1;
        OpenFile(RecentFiles[num], Program.Settings.OpenInNewTab);
    }
}
