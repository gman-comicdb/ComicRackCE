using cYo.Common.Collections;
using cYo.Common.Drawing;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Controls;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Command = cYo.Projects.ComicRack.Viewer.Controllers.Command;
using Menu = cYo.Projects.ComicRack.Viewer.Controllers.Menu;

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class EditMenu : UserControl
{
    private static readonly Command Separator = Command.None;

    public IList<Command> Commands =
    [
        Command.ShowInfo,
        Separator,
        Command.Undo,
        Command.Redo,
        Separator,
        Command.MyRating,
        Command.Rate0,
        Command.Rate1,
        Command.Rate2,
        Command.Rate3,
        Command.Rate4,
        Command.Rate5,
        Command.QuickRating,
        Separator,
        Command.PageType,
        Separator,
        Command.PageRotation,
        Separator,
        Command.Bookmarks,
        Command.SetBookmark,
        Command.RemoveBookmark,
        Command.PreviousBookmark,
        Command.NextBookmark,
        Command.LastPageRead,
        Separator,
        Command.CopyPage,
        Command.ExportPage,
        Separator,
        Command.RefreshView,
        Separator,
        Command.ShowDevices,
        Command.ShowPreferences
    ];

    private EnumMenuUtility pageType;

    private EnumMenuUtility pageRotation;

    public EditMenu()
    {
        Commands.ForEach(cmd => cmd.Menu = Menu.Edit);
        InitializeComponent();
        BindCommands();
        MainMenuControl.InitializeMenuState(this);

        pageType = MainMenuControl.GetPageType(miPageType);
        pageRotation = MainMenuControl.GetPageRotation(miPageRotate);

        pageType.ValueChanged += MainController.EventHandlers.OnPageTypeChanged;
        pageRotation.ValueChanged += MainController.EventHandlers.OnPageRotationChanged;

        contextRating.Renderer = new MenuRenderer(Resources.StarYellow);
        contextRating.Items.Insert(contextRating.Items.Count - 2, new ToolStripSeparator());
        RatingControl.InsertRatingControl(
            contextRating,
            contextRating.Items.Count - 2,
            Resources.StarYellow,
            MC.GetRatingEditor);

        editMenuItem.DropDownOpening += MainMenuControl.OnToolStripMenuDropDownOpening;
        editMenuItem.DropDownOpening += OnMenuDropDownOpening;
    }

    private void BindCommands()
    {
        miShowInfo.Tag = Command.ShowInfo;

        miUndo.Tag = Command.Undo;
        miRedo.Tag = Command.Redo;

        miRating.Tag = Command.MyRating;
        miRate0.Tag = Command.Rate0;
        miRate1.Tag = Command.Rate1;
        miRate2.Tag = Command.Rate2;
        miRate3.Tag = Command.Rate3;
        miRate4.Tag = Command.Rate4;
        miRate5.Tag = Command.Rate5;

        miPageType.Tag = Command.PageType;

        miPageRotate.Tag = Command.PageRotation;

        miBookmarks.Tag = Command.Bookmarks;
        miSetBookmark.Tag = Command.SetBookmark;
        miRemoveBookmark.Tag = Command.RemoveBookmark;
        miPrevBookmark.Tag = Command.PreviousBookmark;
        miNextBookmark.Tag = Command.NextBookmark;
        miLastPageRead.Tag = Command.LastPageRead;

        miCopyPage.Tag = Command.CopyPage;
        miExportPage.Tag = Command.ExportPage;
        miViewRefresh.Tag = Command.RefreshView;
        miDevices.Tag = Command.ShowDevices;
        miPreferences.Tag = Command.ShowPreferences;
    }

    public void OnMenuDropDownOpening(object sender, EventArgs e)
    {
        try
        {
            IEditPage pageEditor = MC.GetPageEditor();
            pageType.Value = (int)pageEditor.PageType;
            pageRotation.Value = (int)pageEditor.Rotation;

            miUndo.Tag ??= miUndo.Text;
            string undoLabel = Program.Database.Undo.UndoLabel;
            miUndo.Text = (string)miUndo.Tag + (string.IsNullOrEmpty(undoLabel) ? string.Empty : (": " + undoLabel));

            miRedo.Tag ??= miRedo.Text;
            string redoLabel = Program.Database.Undo.RedoEntries.FirstOrDefault();
            miRedo.Text = (string)miRedo.Tag + (string.IsNullOrEmpty(redoLabel) ? string.Empty : (": " + redoLabel));
        }
        catch (Exception)
        {
        }
    }

    public static implicit operator ToolStripMenuItem(EditMenu menu) => menu.editMenuItem;
}
