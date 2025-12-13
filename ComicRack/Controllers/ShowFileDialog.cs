using cYo.Common.Collections;
using cYo.Common.IO;
using cYo.Common.Localize;
using cYo.Common.Windows;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.IO.Provider;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Controls.MainForm;
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controllers;

internal static class ShowFileDialog
{
    public static IMain main;

    public static IMenuController menu;

    public static string Open(IWin32Window parent, string title, bool includeReadingLists)
    {
        IEnumerable<FileFormat> enumerable = from f in Providers.Readers.GetSourceFormats()
                                             orderby f
                                             select f;
        if (includeReadingLists)
            enumerable = enumerable.IncludeReadingList();

        using OpenFileDialog dialog = new()
        {
            Title = title,
            FilterIndex = Program.Settings.LastOpenFilterIndex,
            Filter = enumerable.GetDialogFilter(withAllFilter: true),
            CheckFileExists = true
        };

        //using CommonOpenFileDialog dialog = new()
        //{
        //    Title = title,
        //    EnsureFileExists = true,
        //    SetFile = enumerable.GetDialogFilter(withAllFilter: true)
        //    //FilterIndex = Program.Settings.LastOpenFilterIndex,
        //    //CheckFileExists = true,
        //    //Filter = enumerable.GetDialogFilter(withAllFilter: true)
        //};
        //foreach (string favoritePath in Program.GetFavoritePaths())
        //  dialog.AddPlace(favoritePath, FileDialogAddPlaceLocation.Bottom);
        //Program.Settings.LastOpenFilterIndex = dialog.SelectedFileTypeIndex;

        foreach (string favoritePath in Program.GetFavoritePaths())
            dialog.CustomPlaces.Add(favoritePath);
            
        string result = dialog.ShowDialog(parent) == DialogResult.OK ? dialog.FileName : null;
        Program.Settings.LastOpenFilterIndex = dialog.FilterIndex;
        return result;
    }

    public static string SaveImage(IWin32Window parent, string fileName)
    {
        using SaveFileDialog dialog = new()
        {
            // TODO : avoid Program.MainForm
            Title = LocalizeUtility.GetText(Program.MainForm.Control, "SavePageTitle", "Save Page as"),
            FileName = FileUtility.MakeValidFilename(fileName),
            FilterIndex = Program.Settings.LastExportPageFilterIndex,
            Filter = TR.Load("FileFilter")["PageImageSave", "JPEG Image|*.jpg|Windows Bitmap Image|*.bmp|PNG Image|*.png|GIF Image|*.gif|TIFF Image|*.tif"]
        };

        string result = dialog.ShowDialog(parent) == DialogResult.OK ? dialog.FileName : null;
        Program.Settings.LastExportPageFilterIndex = dialog.FilterIndex;
        return result;
    }
}
