using cYo.Common;
using cYo.Common.Drawing;
using cYo.Common.Localize;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm
{
    #region Properties
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Rectangle ScriptOutputBounds
    {
        get => ScriptConsole?.SafeBounds ?? Rectangle.Empty;
        set
        {
            if (ScriptConsole != null && value.IsEmpty)
                ScriptConsole.StartPosition = FormStartPosition.WindowsDefaultLocation;
            else if (!value.IsEmpty)
                    ScriptConsole.SafeBounds = value;
        }
    }
    #endregion

    public DisplayWorkspace Workspace
    {
        get
        {
            DisplayWorkspace workspace = new();
            StoreWorkspace(workspace);
            return workspace;
        }
    }

    public void StoreWorkspace()
        => StoreWorkspace(Program.Settings.CurrentWorkspace);

    public void SetWorkspace(DisplayWorkspace workspace, bool remember)
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
                WorkspaceManager.LastWorkspaceName = workspace.Name;
                WorkspaceManager.LastWorkspaceType = workspace.Type;
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
            WorkspaceManager.SetWorkspaceDisplayOptions(workspace);
        }
        finally
        {
            ResumeLayout();
            VisibilityAnimator.EnableAnimation = (SizableContainer.EnableAnimation = enableAnimation);
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

    // TODO : review if this belongs here or somewhere else, like FormUtility
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
