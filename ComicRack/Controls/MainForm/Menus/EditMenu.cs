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

namespace cYo.Projects.ComicRack.Viewer.Controls.MainForm.Menus;

public partial class EditMenu : UserControl
{
    private MainController controller;

    private EnumMenuUtility pageType;

    private EnumMenuUtility pageRotation;

    public IEnumerable<ToolStripMenuItem> Comics => cmComics.DropDownItems.OfType<ToolStripMenuItem>();

    public EditMenu()
    {
        InitializeComponent();
        //this.controller = controller;
    }
    public void SetController(MainController controller)
    {
        this.controller = controller;
        pageType = controller.GetPageType(miPageType);
        pageRotation = controller.GetPageRotation(miPageRotate);

        pageType.ValueChanged += MainController.EventHandlers.OnPageTypeChanged;
        pageRotation.ValueChanged += MainController.EventHandlers.OnPageRotationChanged;

        contextRating.Items.Insert(contextRating.Items.Count - 2, new ToolStripSeparator());
        RatingControl.InsertRatingControl(
            contextRating,
            contextRating.Items.Count - 2,
            Resources.StarYellow,
            controller.GetRatingEditor);
        contextRating.Renderer = new MenuRenderer(Resources.StarYellow);

        editMenuItem.DropDownOpening += OnMenuDropDownOpening;
        miBookmarks.DropDownOpening += controller.OnEditMenuBookmarksDropDownOpening;
    }

    public static implicit operator ToolStripMenuItem(EditMenu menu)
        => menu.editMenuItem;

    //protected override void OnLoad(EventArgs e)
    //{
    //    base.OnLoad(e);

    //    pageType = controller.GetPageType(miPageType);
    //    pageRotation = controller.GetPageRotation(miPageRotate);

    //    pageType.ValueChanged += MainController.EventHandlers.OnPageTypeChanged;
    //    pageRotation.ValueChanged += MainController.EventHandlers.OnPageRotationChanged;

    //    contextRating.Items.Insert(contextRating.Items.Count - 2, new ToolStripSeparator());
    //    RatingControl.InsertRatingControl(
    //        contextRating,
    //        contextRating.Items.Count - 2,
    //        Resources.StarYellow,
    //        controller.GetRatingEditor);
    //    contextRating.Renderer = new MenuRenderer(Resources.StarYellow);
    //}

    public void OnMenuDropDownOpening(object sender, EventArgs e)
    {
        try
        {
            //bool flag = controller.ComicDisplay != null && controller.ComicDisplay.Book != null;  //unused
            IEditPage pageEditor = controller.GetPageEditor();
            pageRotation.Enabled = pageEditor.IsValid;
            pageType.Enabled = pageEditor.IsValid;
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

    public void InitializeCommands()
    {
        MainController.Commands.Add(
            controller.ComicDisplay.DisplayPreviousBookmarkedPage,
            () => controller.ComicDisplay.Book != null && controller.ComicDisplay.Book.CanNavigateBookmark(-1),
            miPrevBookmark);
        MainController.Commands.Add(
            controller.ComicDisplay.DisplayNextBookmarkedPage,
            () => controller.ComicDisplay.Book != null && controller.ComicDisplay.Book.CanNavigateBookmark(1),
            miNextBookmark);
        MainController.Commands.Add(
            MainController.Commands.SetBookmark,
            MainController.Commands.SetBookmarkAvailable,
            miSetBookmark);
        MainController.Commands.Add(
            MainController.Commands.RemoveBookmark,
            MainController.Commands.RemoveBookmarkAvailable,
            miRemoveBookmark);
        MainController.Commands.Add(
            controller.ComicDisplay.DisplayLastPageRead,
            () => controller.ComicDisplay.Book != null && controller.ComicDisplay.Book.CurrentPage != controller.ComicDisplay.Book.Comic.LastPageRead,
            miLastPageRead);

        MainController.Commands.Add(
            Program.Database.Undo.Undo,
            () => Program.Database.Undo.CanUndo,
            miUndo);
        MainController.Commands.Add(
            Program.Database.Undo.Redo,
            () => Program.Database.Undo.CanRedo,
            miRedo);
    }

    public void InitializeKeyboard()
    {
        string group = "Edit";
        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                Resources.Rotate90Permanent,
                "PageRotateC",
                group,
                "Rotate Page Right",
                () => controller.GetPageEditor().Rotation = controller.GetPageEditor().Rotation.RotateRight(),
                [CommandKey.Y]
                )
            );

        controller.ComicDisplay.KeyboardMap.Commands.Add(
            new KeyboardCommand(
                Resources.Rotate270Permanent,
                "PageRotateCC",
                group,
                "Rotate Page Left",
                () => controller.GetPageEditor().Rotation = controller.GetPageEditor().Rotation.RotateLeft(),
                [CommandKey.Y | CommandKey.Shift]
                )
            );
    }
}
