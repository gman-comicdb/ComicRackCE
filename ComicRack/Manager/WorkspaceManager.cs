using cYo.Common;
using cYo.Common.Localize;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using System.Collections.Generic;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Manager;

internal static class WorkspaceManager
{
    public static IMain MainForm => Program.MainForm;

    private static IComicDisplay ComicDisplay => MainForm.ComicDisplay;

    // better this or using Workspaces = Program.Settings.Workspaces?
    // think this
    public static List<DisplayWorkspace> Workspaces => Program.Settings.Workspaces;

    public static string LastWorkspaceName { private get; set; }

    public static WorkspaceType LastWorkspaceType { private get; set; } = WorkspaceType.Default;

    public static void EditWorkspaceDisplaySettings()
    {
        DisplayWorkspace ws = MainForm.Workspace;
        ComicDisplaySettingsDialog.Show(
            MainForm,
            ComicDisplay.IsHardwareRenderer,
            ws,
            SetWorkspaceDisplayOptions
            );
    }

    private static DisplayWorkspace CreateNewWorkspace()
    {
        DisplayWorkspace displayWorkspace = MainForm.Workspace;
        displayWorkspace.Name = LastWorkspaceName ?? TR.Default["Workspace", "Workspace"];
        displayWorkspace.Type = LastWorkspaceType;
        if (SaveWorkspaceDialog.Show(MainForm, displayWorkspace))
        {
            return displayWorkspace;
        }
        return null;
    }

    public static void SaveWorkspace()
    {
        DisplayWorkspace workspace = CreateNewWorkspace();
        if (workspace != null)
        {
            // TODO : re-add
            LastWorkspaceName = workspace.Name;
            LastWorkspaceType = workspace.Type;
            int index = Workspaces.FindIndex((DisplayWorkspace ws) => ws.Name == workspace.Name);
            if (index != -1)
            {
                Workspaces[index] = workspace;
                return;
            }
            Workspaces.Add(workspace);
            //UpdateWorkspaceMenus();
        }
    }

    public static void EditWorkspace(IWin32Window window)
    {
        if (Workspaces.Count != 0)
        {
            IList<DisplayWorkspace> list = ListEditorDialog.Show(
                Form.ActiveForm ?? window,
                TR.Default["Workspaces"],
                Workspaces,
                CreateNewWorkspace,
                null,
                (DisplayWorkspace w) => MainForm.SetWorkspace(w, remember: true)
            );
            if (list != null)
            {
                Workspaces.Clear();
                Workspaces.AddRange(list);
                //UpdateWorkspaceMenus();
            }
        }
    }

    public static void SetWorkspaceDisplayOptions(DisplayWorkspace workspace)
    {
        if (workspace.IsComicPageLayout)
        {
            bool displayChangeAnimation = ComicDisplay.DisplayChangeAnimation;
            ComicDisplay.DisplayChangeAnimation = false;
            try
            {
                ComicDisplay.PageLayout = workspace.Layout.PageLayout;
                ComicDisplay.TwoPageNavigation = workspace.Layout.TwoPageAutoScroll;
                ComicDisplay.RightToLeftReading = workspace.RightToLeftReading;
                ComicDisplay.ImageFitMode = workspace.Layout.PageDisplayMode;
                ComicDisplay.ImageFitOnlyIfOversized = workspace.Layout.FitOnlyIfOversized;
                ComicDisplay.ImageRotation = workspace.Layout.PageImageRotation;
                ComicDisplay.ImageAutoRotate = workspace.Layout.AutoRotate;
                try
                {
                    ComicDisplay.ImageZoom = workspace.Layout.PageZoom;
                }
                catch
                {
                    ComicDisplay.DisplayChangeAnimation = displayChangeAnimation;
                }
            }
            finally
            {
                ComicDisplay.DisplayChangeAnimation = displayChangeAnimation;
            }
        }
        if (workspace.IsComicPageDisplay)
        {
            ComicDisplay.RealisticPages = workspace.DrawRealisticPages;
            ComicDisplay.BackColor = workspace.BackColor;
            ComicDisplay.BackgroundTexture = workspace.BackgroundTexture;
            ComicDisplay.PaperTexture = workspace.PaperTexture;
            ComicDisplay.PaperTextureStrength = workspace.PaperTextureStrength;
            ComicDisplay.ImageBackgroundMode = workspace.PageImageBackgroundMode;
            ComicDisplay.PaperTextureLayout = workspace.PaperTextureLayout;
            ComicDisplay.BackgroundImageLayout = workspace.BackgroundImageLayout;
            ComicDisplay.PageTransitionEffect = workspace.PageTransitionEffect;
            ComicDisplay.PageMargin = workspace.PageMargin;
            ComicDisplay.PageMarginPercentWidth = workspace.PageMarginPercentWidth;
        }
    }

    public static void UpdateWorkspaceMenus(ToolStripItemCollection items)
    {
        ToolStripSeparator toolStripSeparator = null;
        for (int num = items.Count - 1; num > 0; num--)
        {
            if (items[num] is ToolStripSeparator)
            {
                toolStripSeparator = items[num] as ToolStripSeparator;
                break;
            }
            items.RemoveAt(num);
        }
        if (toolStripSeparator != null)
        {
            toolStripSeparator.Visible = Workspaces.Count > 0;
        }
        int num2 = 0;
        foreach (DisplayWorkspace workspace in Workspaces)
        {
            DisplayWorkspace itemWs = workspace;
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(FormUtility.FixAmpersand(workspace.Name), null, delegate
            {
                MainController.Commands.SetWorkspace(CloneUtility.Clone(itemWs), remember: true);
            });
            if (num2 < 6)
            {
                toolStripMenuItem.ShortcutKeys = (Keys)(0x50000 | (112 + num2++));
            }
            items.Add(toolStripMenuItem);
        }
    }
}
