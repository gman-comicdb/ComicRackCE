using cYo.Common;
using cYo.Common.Drawing;
using cYo.Common.Localize;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm : FormEx, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    #region Properties
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle ScriptOutputBounds
    {
        get
        {
            return ScriptConsole?.SafeBounds ?? Rectangle.Empty;
        }
        set
        {
            if (ScriptConsole != null)
            {
                if (value.IsEmpty)
                {
                    ScriptConsole.StartPosition = FormStartPosition.WindowsDefaultLocation;
                }
                else
                {
                    ScriptConsole.SafeBounds = value;
                }
            }
        }
    }
    #endregion

    private void EditWorkspaceDisplaySettings()
    {
        DisplayWorkspace ws = new DisplayWorkspace();
        StoreWorkspace(ws);
        ComicDisplaySettingsDialog.Show(this, ComicDisplay.IsHardwareRenderer, ws, delegate
        {
            SetWorkspaceDisplayOptions(ws);
        });
    }

    private DisplayWorkspace CreateNewWorkspace()
    {
        DisplayWorkspace displayWorkspace = new DisplayWorkspace();
        StoreWorkspace(displayWorkspace);
        displayWorkspace.Name = lastWorkspaceName ?? TR.Default["Workspace", "Workspace"];
        displayWorkspace.Type = lastWorkspaceType;
        if (SaveWorkspaceDialog.Show(this, displayWorkspace))
        {
            return displayWorkspace;
        }
        return null;
    }

    private void SaveWorkspace()
    {
        DisplayWorkspace newWs = CreateNewWorkspace();
        if (newWs != null)
        {
            lastWorkspaceName = newWs.Name;
            lastWorkspaceType = newWs.Type;
            int num = Program.Settings.Workspaces.FindIndex((DisplayWorkspace ws) => ws.Name == newWs.Name);
            if (num != -1)
            {
                Program.Settings.Workspaces[num] = newWs;
                return;
            }
            Program.Settings.Workspaces.Add(newWs);
            UpdateWorkspaceMenus();
        }
    }

    private void EditWorkspace()
    {
        if (Program.Settings.Workspaces.Count != 0)
        {
            IList<DisplayWorkspace> list = ListEditorDialog.Show(Form.ActiveForm ?? this, TR.Default["Workspaces"], Program.Settings.Workspaces, CreateNewWorkspace, null, (DisplayWorkspace w) =>
            {
                SetWorkspace(w, remember: true);
            });
            if (list != null)
            {
                Program.Settings.Workspaces.Clear();
                Program.Settings.Workspaces.AddRange(list);
                UpdateWorkspaceMenus();
            }
        }
    }

    private void UpdateWorkspaceMenus()
    {
        UpdateWorkspaceMenus(tsWorkspaces.DropDownItems);
        UpdateWorkspaceMenus(miWorkspaces.DropDownItems);
    }

    private void UpdateWorkspaceMenus(ToolStripItemCollection items)
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
            toolStripSeparator.Visible = Program.Settings.Workspaces.Count > 0;
        }
        int num2 = 0;
        foreach (DisplayWorkspace workspace in Program.Settings.Workspaces)
        {
            DisplayWorkspace itemWs = workspace;
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(FormUtility.FixAmpersand(workspace.Name), null, delegate
            {
                SetWorkspace(CloneUtility.Clone(itemWs), remember: true);
            });
            if (num2 < 6)
            {
                toolStripMenuItem.ShortcutKeys = (Keys)(0x50000 | (112 + num2++));
            }
            items.Add(toolStripMenuItem);
        }
    }

    private void SetWorkspace(DisplayWorkspace workspace, bool remember)
    {
        if (ComicDisplay == null)
        {
            return;
        }
        SuspendLayout();
        bool enableAnimation = SizableContainer.EnableAnimation;
        VisibilityAnimator.EnableAnimation = (SizableContainer.EnableAnimation = false);
        try
        {
            if (remember)
            {
                lastWorkspaceName = workspace.Name;
                lastWorkspaceType = workspace.Type;
            }
            ComicDisplay.FullScreen = false;
            if (workspace.IsWindowLayout)
            {
                if (!workspace.FormBounds.IsEmpty)
                    base.Bounds = GetOnScreenBounds(workspace.FormBounds);

                BrowserVisible = workspace.PanelVisible || (!ComicDisplay.IsValid && Program.Settings.ShowQuickOpen);
                mainViewContainer.DockSize = workspace.PanelSize;
                BrowserDock = workspace.PanelDock;
                ReaderUndocked = workspace.ReaderUndocked;
                UndockedReaderBounds = GetOnScreenBounds(workspace.UndockedReaderBounds);
                UndockedReaderState = workspace.UndockedReaderState;
                ScriptOutputBounds = workspace.ScriptOutputBounds;
            }
            if (workspace.IsViewsSetup)
            {
                foreach (IDisplayWorkspace item in this.FindServices<IDisplayWorkspace>())
                {
                    item.SetWorkspace(workspace);
                }
            }
            if (workspace.IsWindowLayout)
            {
                oldState = Program.ExtendedSettings.StartHidden ? workspace.FormState : workspace.PreviousFormState;
                base.WindowState = Program.ExtendedSettings.StartHidden ? FormWindowState.Minimized : workspace.FormState;
                ComicDisplay.FullScreen = workspace.FullScreen;
                MinimalGui = workspace.MinimalGui;
                ComicBookDialog.PagesConfig = workspace.ComicBookDialogPagesConfig;
                ComicBookDialog.SafeSize = workspace.ComicBookDialogOutputSize;
                PreferencesDialog.SafeSize = workspace.PreferencesOutputSize;
                Program.ExtendedSettings.StartHidden = false; //Sets it false so it respects normal setting after the first load
            }
            SetWorkspaceDisplayOptions(workspace);
        }
        finally
        {
            ResumeLayout();
            VisibilityAnimator.EnableAnimation = (SizableContainer.EnableAnimation = enableAnimation);
        }
    }

    private void SetWorkspaceDisplayOptions(DisplayWorkspace workspace)
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

    private void StoreWorkspace(DisplayWorkspace workspace)
    {
        workspace.FormState = base.WindowState;
        workspace.PreviousFormState = oldState;
        workspace.FormBounds = SafeBounds;
        workspace.MinimalGui = MinimalGui;
        workspace.PanelDock = BrowserDock;
        workspace.PanelVisible = BrowserVisible;
        workspace.PanelSize = mainViewContainer.DockSize;
        workspace.RightToLeftReading = ComicDisplay.RightToLeftReading;
        workspace.FullScreen = ComicDisplay.FullScreen;
        workspace.Layout.PageLayout = ComicDisplay.PageLayout;
        workspace.Layout.TwoPageAutoScroll = ComicDisplay.TwoPageNavigation;
        workspace.Layout.FitOnlyIfOversized = ComicDisplay.ImageFitOnlyIfOversized;
        workspace.Layout.PageZoom = ComicDisplay.ImageZoom;
        workspace.Layout.PageDisplayMode = ComicDisplay.ImageFitMode;
        workspace.Layout.PageImageRotation = ComicDisplay.ImageRotation;
        workspace.Layout.AutoRotate = ComicDisplay.ImageAutoRotate;
        workspace.DrawRealisticPages = ComicDisplay.RealisticPages;
        workspace.BackColor = ComicDisplay.BackColor;
        workspace.BackgroundTexture = ComicDisplay.BackgroundTexture;
        workspace.PaperTexture = ComicDisplay.PaperTexture;
        workspace.PaperTextureStrength = ComicDisplay.PaperTextureStrength;
        workspace.PageImageBackgroundMode = ComicDisplay.ImageBackgroundMode;
        workspace.PaperTextureLayout = ComicDisplay.PaperTextureLayout;
        workspace.BackgroundImageLayout = ComicDisplay.BackgroundImageLayout;
        workspace.PageMargin = ComicDisplay.PageMargin;
        workspace.PageMarginPercentWidth = ComicDisplay.PageMarginPercentWidth;
        workspace.PageTransitionEffect = ComicDisplay.PageTransitionEffect;
        workspace.ReaderUndocked = ReaderUndocked;
        if (ScriptConsole != null)
        {
            workspace.ScriptOutputBounds = ScriptOutputBounds;
        }
        if (workspace.ReaderUndocked)
        {
            workspace.UndockedReaderBounds = UndockedReaderBounds;
            workspace.UndockedReaderState = UndockedReaderState;
        }
        foreach (IDisplayWorkspace item in this.FindServices<IDisplayWorkspace>())
        {
            item.StoreWorkspace(workspace);
        }
        workspace.ComicBookDialogPagesConfig = ComicBookDialog.PagesConfig;
        if (workspace.ComicBookDialogPagesConfig != null)
        {
            workspace.ComicBookDialogPagesConfig.GroupsStatus = null;
        }
        workspace.PreferencesOutputSize = PreferencesDialog.SafeSize;
        workspace.ComicBookDialogOutputSize = ComicBookDialog.SafeSize;
    }

    public void StoreWorkspace()
    {
        StoreWorkspace(Program.Settings.CurrentWorkspace);
    }

    private Rectangle GetOnScreenBounds(Rectangle formBounds)
    {
        if (formBounds.IsEmpty)
            return Rectangle.Empty;

        Rectangle b = formBounds;
        Screen screen = Screen.AllScreens.Where((Screen scr) => scr.Bounds.IntersectsWith(b)).FirstOrDefault();
        if (screen == null)
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            b.Width = Math.Min(b.Width, bounds.Width);
            b.Height = Math.Min(b.Height, bounds.Height);
            b = b.Center(bounds);
        }
        return b;
    }
}
