using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using cYo.Common.Collections;
using cYo.Common.Drawing;
using cYo.Common.Localize;
using cYo.Common.Mathematics;
using cYo.Common.Win32;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Controls;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.Drawing;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Properties;
using cYo.Projects.ComicRack.Viewer.Views;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm : FormEx, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        Program.Database.BookChanged += WatchedBookHasChanged;
        Program.BookFactory.TemporaryBookChanged += WatchedBookHasChanged;
        Program.Database.Books.ForEach(Program.QueueManager.AddBookToFileUpdate);
        if (Program.Settings.ScanStartup)
        {
            Program.Scanner.ScanFilesOrFolders(Program.Database.WatchFolders.Folders, all: true, Program.Settings.RemoveMissingFilesOnFullScan);
        }
        if (Program.Settings.UpdateWebComicsStartup)
        {
            UpdateWebComics();
        }
        SuspendLayout();
        base.Icon = Resources.ComicRackAppSmall;
        notifyIcon.Icon = base.Icon;
        notifyIcon.Text = LocalizeUtility.GetText(this, "NotifyIconText", notifyIcon.Text);
        miFileAutomation.Visible = miFileAutomation.DropDownItems.Count != 0;
        mainMenuStrip.SendToBack();
        statusStrip.Items.Insert(statusStrip.Items.Count - 1, thumbSize);
        thumbSize.TrackBar.Scroll += TrackBar_Scroll;
        ThumbRenderer.DefaultRatingImage1 = Resources.StarYellow.ToOptimized();
        ThumbRenderer.DefaultRatingImage2 = Resources.StarBlue.ToOptimized();
        ThumbRenderer.DefaultTagRatingImage1 = Resources.RatingYellow.ToOptimized();
        ThumbRenderer.DefaultTagRatingImage2 = Resources.RatingBlue.ToOptimized();
        ComicDisplay.PagePool = Program.ImagePool;
        ComicDisplay.ThumbnailPool = Program.ImagePool;
        ComicDisplay.PageFilter = Program.Settings.PageFilter;
        mainView.Main = this;
        miOpenRecent.DropDownItems.Add(new ToolStripMenuItem("dummy"));
        IdleProcess.Idle += Application_Idle;
        Program.Settings.SettingsChanged += SettingsChanged;
        recentFiles = Program.Database.GetRecentFiles(Settings.RecentFileCount).ToArray();
        InitializeCommands();
        InitializeKeyboard();
        InitializeHelp(Program.Settings.HelpSystem);
        Program.Settings.HelpSystemChanged += delegate
        {
            InitializeHelp(Program.Settings.HelpSystem);
        };
        InitializePluginHelp();
        UpdateSettings();
        UpdateWorkspaceMenus();
        SetWorkspace(Program.Settings.GetWorkspace(Program.ExtendedSettings.Workspace) ?? Program.Settings.CurrentWorkspace, remember: false);
        UpdateListConfigMenus();
        pageTypeContextMenu = new EnumMenuUtility(cmPageType, typeof(ComicPageType), flagsMode: false, null, Keys.A | Keys.Shift | Keys.Alt);
        pageTypeEditMenu = new EnumMenuUtility(miPageType, typeof(ComicPageType), flagsMode: false, null, Keys.A | Keys.Shift | Keys.Alt);
        pageTypeContextMenu.ValueChanged += delegate
        {
            GetPageEditor().PageType = (ComicPageType)pageTypeContextMenu.Value;
        };
        pageTypeEditMenu.ValueChanged += delegate
        {
            GetPageEditor().PageType = (ComicPageType)pageTypeEditMenu.Value;
        };
        Dictionary<int, Image> images = new Dictionary<int, Image>
            {
                {
                    0,
                    Resources.Rotate0Permanent
                },
                {
                    1,
                    Resources.Rotate90Permanent
                },
                {
                    2,
                    Resources.Rotate180Permanent
                },
                {
                    3,
                    Resources.Rotate270Permanent
                }
            };
        pageRotationContextMenu = new EnumMenuUtility(cmPageRotate, typeof(ImageRotation), flagsMode: false, images, Keys.D6 | Keys.Shift | Keys.Alt);
        pageRotationEditMenu = new EnumMenuUtility(miPageRotate, typeof(ImageRotation), flagsMode: false, images, Keys.D6 | Keys.Shift | Keys.Alt);
        pageRotationContextMenu.ValueChanged += delegate
        {
            GetPageEditor().Rotation = (ImageRotation)pageRotationContextMenu.Value;
        };
        pageRotationEditMenu.ValueChanged += delegate
        {
            GetPageEditor().Rotation = (ImageRotation)pageRotationEditMenu.Value;
        };
        ResumeLayout(performLayout: true);
        contextRating.Items.Insert(contextRating.Items.Count - 2, new ToolStripSeparator());
        RatingControl.InsertRatingControl(contextRating, contextRating.Items.Count - 2, Resources.StarYellow, GetRatingEditor);
        contextRating2.Items.Insert(contextRating2.Items.Count - 2, new ToolStripSeparator());
        RatingControl.InsertRatingControl(contextRating2, contextRating2.Items.Count - 2, Resources.StarYellow, GetRatingEditor);
        contextRating.Renderer = new MenuRenderer(Resources.StarYellow);
        contextRating2.Renderer = new MenuRenderer(Resources.StarYellow);
        IdleProcess.CancelIdle += (object a, CancelEventArgs b) =>
        {
            b.Cancel = !IdleProcess.ShouldProcess(this) && !IdleProcess.ShouldProcess(readerForm);
        };
        Program.StartupProgress(TR.Messages["LoadComic", "Opening Files"], 90);
        Refresh();
        foreach (string commandLineFile in Program.CommandLineFiles)
        {
            if (File.Exists(commandLineFile))
            {
                OpenSupportedFile(commandLineFile, newSlot: false, 0, fromShell: true);
            }
        }
        if (books.OpenCount == 0 && Program.Settings.OpenLastFile)
        {
            List<string> files = new List<string>(Program.Settings.LastOpenFiles);
            books.Open(files, OpenComicOptions.NoIncreaseOpenedCount | OpenComicOptions.AppendNewSlots);
        }
        if (Program.Settings.ShowQuickManual)
        {
            Program.Settings.ShowQuickManual = false;
            books.Open(Program.QuickHelpManualFile, OpenComicOptions.OpenInNewSlot | OpenComicOptions.NoFileUpdate);
        }
        if (!string.IsNullOrEmpty(Program.ExtendedSettings.ImportList))
        {
            ImportComicList(Program.ExtendedSettings.ImportList);
        }
        Program.NetworkManager.BroadcastStart();
        VisibilityAnimator.EnableAnimation = Program.Settings.AnimatePanels && !Program.ExtendedSettings.DisableMenuHideShowAnimation;
        SizableContainer.EnableAnimation = Program.Settings.AnimatePanels;
        Program.Settings.AnimatePanelsChanged += delegate
        {
            SizableContainer.EnableAnimation = Program.Settings.AnimatePanels;
        };
        if (books.Slots.Count == 0)
        {
            RebuildBookTabs();
        }
        OnUpdateGui();
        IsInitialized = true;
        this.BeginInvoke(delegate
        {
            ScriptUtility.Invoke(PluginEngine.ScriptTypeStartup);
        });
        _ = Task.Run(() => CheckForUpdateAsync());
    }

    private void InitializeCommands()
    {
        tbTools.DropDownOpening += delegate
        {
            mainToolStrip.DefaultDropDownDirection = ToolStripDropDownDirection.BelowLeft;
        };
        tbTools.DropDownClosed += delegate
        {
            mainToolStrip.DefaultDropDownDirection = ToolStripDropDownDirection.Default;
        };
        commands.Add(ShowOpenDialog, miOpenComic, cmOpenComic, tbOpenComic);
        commands.Add(OpenBooks.Close, () => OpenBooks.Slots.Count > 0, miCloseComic, cmClose, cmCloseComic);
        commands.Add(OpenBooks.CloseAll, () => OpenBooks.Slots.Count > 0, miCloseAllComics);
        commands.Add(OpenBooks.AddSlot, miAddTab);
        commands.Add(delegate
        {
            AddNewBook();
        }, miNewComic);
        commands.Add(delegate
        {
            OpenNextComic();
        }, miNextFromList, tbNextFromList, cmNextFromList);
        commands.Add(delegate
        {
            OpenPrevComic();
        }, miPrevFromList, tbPrevFromList, cmPrevFromList);
        commands.Add(delegate
        {
            OpenRandomComic();
        }, miRandomFromList, tbRandomFromList, cmRandomFromList);
        commands.Add(delegate
        {
            SyncBrowser();
        }, () => ComicDisplay.Book != null, miSyncBrowser, cmSyncBrowser);
        commands.Add(AddFolderToLibrary, miAddFolderToLibrary);
        commands.Add(StartFullScan, miScan, tbScan);
        commands.Add(UpdateComics, miUpdateAllComicFiles, tbUpdateAllComicFiles);
        commands.Add(GenerateFrontCoverCache, miCacheThumbnails, tbCacheThumbnails);
        commands.Add(MenuSynchronizeDevices, miSynchronizeDevices, tsSynchronizeDevices);
        commands.Add(UpdateWebComics, miUpdateWebComics, tbUpdateWebComics);
        commands.Add(delegate
        {
            ShowPendingTasks();
        }, miTasks);
        commands.Add(MenuRestart, miRestart);
        commands.Add(MenuClose, miExit, cmNotifyExit, tbExit);
        commands.Add(OpenRemoteLibrary, miOpenRemoteLibrary, tbOpenRemoteLibrary);
        commands.Add(ComicDisplay.DisplayFirstPage, () => ComicDisplay.Book != null && ComicDisplay.Book.CanNavigate(-1), miFirstPage, tbFirstPage);
        commands.Add(delegate
        {
            ComicDisplay.DisplayPreviousPage(ComicDisplay.PagingMode.Double);
        }, () => ComicDisplay.Book != null, miPrevPage, tbPrevPage);
        commands.Add(delegate
        {
            ComicDisplay.DisplayNextPage(ComicDisplay.PagingMode.Double);
        }, () => ComicDisplay.Book != null, miNextPage, tbNextPage);
        commands.Add(ComicDisplay.DisplayLastPage, () => ComicDisplay.Book != null && ComicDisplay.Book.CanNavigate(1), miLastPage, tbLastPage);
        commands.Add(ComicDisplay.DisplayPreviousBookmarkedPage, () => ComicDisplay.Book != null && ComicDisplay.Book.CanNavigateBookmark(-1), miPrevBookmark, tbPrevBookmark, cmPrevBookmark);
        commands.Add(ComicDisplay.DisplayNextBookmarkedPage, () => ComicDisplay.Book != null && ComicDisplay.Book.CanNavigateBookmark(1), miNextBookmark, tbNextBookmark, cmNextBookmark);
        commands.Add(SetBookmark, SetBookmarkAvailable, miSetBookmark, tbSetBookmark, cmSetBookmark);
        commands.Add(RemoveBookmark, RemoveBookmarkAvailable, miRemoveBookmark, tbRemoveBookmark, cmRemoveBookmark);
        commands.Add(ComicDisplay.DisplayLastPageRead, () => ComicDisplay.Book != null && ComicDisplay.Book.CurrentPage != ComicDisplay.Book.Comic.LastPageRead, miLastPageRead, tbLastPageRead, cmLastPageRead);
        commands.Add(OpenBooks.PreviousSlot, () => OpenBooks.Slots.Count > 1, miPrevTab);
        commands.Add(OpenBooks.NextSlot, () => OpenBooks.Slots.Count > 1, miNextTab);
        commands.AddService(this, (ILibraryBrowser s) =>
        {
            s.BrowseNext();
        }, (ILibraryBrowser s) => s.CanBrowseNext(), miNextList);
        commands.AddService(this, (ILibraryBrowser s) =>
        {
            s.BrowsePrevious();
        }, (ILibraryBrowser s) => s.CanBrowsePrevious(), miPreviousList);
        commands.Add(delegate
        {
            Program.Settings.AutoScrolling = !Program.Settings.AutoScrolling;
        }, true, () => Program.Settings.AutoScrolling, miAutoScroll, tbAutoScroll);
        commands.Add(delegate
        {
            ComicDisplay.TwoPageNavigation = !ComicDisplay.TwoPageNavigation;
        }, true, () => ComicDisplay.TwoPageNavigation, miDoublePageAutoScroll);
        commands.Add(delegate
        {
            ComicDisplay.RightToLeftReading = !ComicDisplay.RightToLeftReading;
        }, true, () => ComicDisplay.RightToLeftReading, miRightToLeft, tbRightToLeft, cmRightToLeft);
        commands.Add(ShowInfo, () => this.InvokeActiveService((IGetBookList bl) => !bl.GetBookList(ComicBookFilterType.Selected).IsEmpty(), defaultReturn: false), miShowInfo, tbShowInfo, cmShowInfo);
        commands.Add(Program.Database.Undo.Undo, () => Program.Database.Undo.CanUndo, miUndo);
        commands.Add(Program.Database.Undo.Redo, () => Program.Database.Undo.CanRedo, miRedo);
        commands.Add(ComicDisplay.CopyPageToClipboard, () => ComicDisplay.Book != null, miCopyPage, cmCopyPage);
        commands.Add(ExportCurrentImage, () => ComicDisplay.Book != null, miExportPage, cmExportPage);
        commands.Add(ToggleUndockReader, true, () => ReaderUndocked, miReaderUndocked, tbReaderUndocked);
        commands.Add(delegate
        {
            MinimalGui = !MinimalGui;
        }, true, () => MinimalGui, miMinimalGui, cmMinimalGui, tbMinimalGui);
        commands.Add(ComicDisplay.ToggleFullScreen, true, () => ComicDisplay.FullScreen, miFullScreen, tbFullScreen);
        commands.Add(ComicDisplay.TogglePageLayout, tbPageLayout);
        commands.Add(delegate
        {
            ComicDisplay.PageLayout = PageLayoutMode.Single;
        }, true, () => ComicDisplay.PageLayout == PageLayoutMode.Single, miSinglePage, tbSinglePage, cmSinglePage);
        commands.Add(delegate
        {
            ComicDisplay.PageLayout = PageLayoutMode.Double;
        }, true, () => ComicDisplay.PageLayout == PageLayoutMode.Double, miTwoPages, tbTwoPages, cmTwoPages);
        commands.Add(delegate
        {
            ComicDisplay.PageLayout = PageLayoutMode.DoubleAdaptive;
        }, true, () => ComicDisplay.PageLayout == PageLayoutMode.DoubleAdaptive, miTwoPagesAdaptive, tbTwoPagesAdaptive, cmTwoPagesAdaptive);
        commands.Add(ComicDisplay.TogglePageFit, tbFit);
        commands.Add(ComicDisplay.SetPageOriginal, true, () => ComicDisplay.ImageFitMode == ImageFitMode.Original, miOriginal, cmOriginal, tbOriginal);
        commands.Add(ComicDisplay.SetPageFitAll, true, () => ComicDisplay.ImageFitMode == ImageFitMode.Fit, miFitAll, tbFitAll, cmFitAll);
        commands.Add(ComicDisplay.SetPageFitWidth, true, ComicDisplay.IsPageFitWidth, miFitWidth, tbFitWidth, cmFitWidth);
        commands.Add(ComicDisplay.SetPageFitWidthAdaptive, true, ComicDisplay.IsPageFitWidthAdaptive, miFitWidthAdaptive, tbFitWidthAdaptive, cmFitWidthAdaptive);
        commands.Add(ComicDisplay.SetPageFitHeight, true, ComicDisplay.IsPageFitHeight, miFitHeight, tbFitHeight, cmFitHeight);
        commands.Add(ComicDisplay.SetPageBestFit, true, ComicDisplay.IsPageFitBest, miBestFit, tbBestFit, cmFitBest);
        commands.Add(ComicDisplay.ToggleFitOnlyIfOversized, true, () => ComicDisplay.ImageFitOnlyIfOversized, miOnlyFitOversized, tbOnlyFitOversized, cmOnlyFitOversized);
        commands.Add(delegate
        {
            ComicDisplay.ImageZoom = Numeric.Select(ComicDisplay.ImageZoom, new float[4]
            {
                    1f,
                    1.25f,
                    1.5f,
                    2f
            }, wrap: true);
        }, tbZoom);
        commands.Add(delegate
        {
            ComicDisplay.ImageZoom = (ComicDisplay.ImageZoom + 0.1f).Clamp(1f, 8f);
        }, () => ComicDisplay.ImageZoom < 8f, miZoomIn, tbZoomIn);
        commands.Add(delegate
        {
            ComicDisplay.ImageZoom = (ComicDisplay.ImageZoom - 0.1f).Clamp(1f, 8f);
        }, () => ComicDisplay.ImageZoom > 1f, miZoomOut, tbZoomOut);
        commands.Add(delegate
        {
            ToggleZoom(CommandKey.None);
        }, miToggleZoom);
        commands.Add(delegate
        {
            ComicDisplay.ImageZoom = 1f;
        }, miZoom100, tbZoom100);
        commands.Add(delegate
        {
            ComicDisplay.ImageZoom = 1.25f;
        }, miZoom125, tbZoom125);
        commands.Add(delegate
        {
            ComicDisplay.ImageZoom = 1.5f;
        }, miZoom150, tbZoom150);
        commands.Add(delegate
        {
            ComicDisplay.ImageZoom = 2f;
        }, miZoom200, tbZoom200);
        commands.Add(delegate
        {
            ComicDisplay.ImageZoom = 4f;
        }, miZoom400, tbZoom400);
        commands.Add(delegate
        {
            ComicDisplay.ImageZoom = ZoomDialog.Show(this, ComicDisplay.ImageZoom);
        }, miZoomCustom, tbZoomCustom);
        commands.Add(delegate
        {
            ComicDisplay.ImageRotation = ImageRotation.None;
        }, true, () => ComicDisplay.ImageRotation == ImageRotation.None, miRotate0, tbRotate0, cmRotate0);
        commands.Add(delegate
        {
            ComicDisplay.ImageRotation = ImageRotation.Rotate90;
        }, true, () => ComicDisplay.ImageRotation == ImageRotation.Rotate90, miRotate90, tbRotate90, cmRotate90);
        commands.Add(delegate
        {
            ComicDisplay.ImageRotation = ImageRotation.Rotate180;
        }, true, () => ComicDisplay.ImageRotation == ImageRotation.Rotate180, miRotate180, tbRotate180, cmRotate180);
        commands.Add(delegate
        {
            ComicDisplay.ImageRotation = ImageRotation.Rotate270;
        }, true, () => ComicDisplay.ImageRotation == ImageRotation.Rotate270, miRotate270, tbRotate270, cmRotate270);
        commands.Add(delegate
        {
            ComicDisplay.ImageRotation = ComicDisplay.ImageRotation.RotateLeft();
        }, miRotateLeft, tbRotateLeft);
        commands.Add(delegate
        {
            ComicDisplay.ImageRotation = ComicDisplay.ImageRotation.RotateRight();
        }, miRotateRight, tbRotateRight, tbRotate);
        commands.Add(delegate
        {
            ComicDisplay.ImageAutoRotate = !ComicDisplay.ImageAutoRotate;
        }, true, () => ComicDisplay.ImageAutoRotate, miAutoRotate, tbAutoRotate);
        commands.Add(ComicDisplay.ToggleMagnifier, true, () => ComicDisplay.MagnifierVisible, miMagnify, tbMagnify, cmMagnify);
        commands.Add(delegate
        {
            ShowPortableDevices();
        }, miDevices);
        commands.Add(delegate
        {
            ShowPreferences();
        }, miPreferences, tbPreferences);
        commands.Add(delegate
        {
            Program.Settings.AutoHideMainMenu = !Program.Settings.AutoHideMainMenu;
        }, true, () => !Program.Settings.AutoHideMainMenu, tbShowMainMenu);
        commands.Add(delegate
        {
            BrowserVisible = true;
            mainView.ShowLibrary();
        }, miViewLibrary);
        commands.Add(delegate
        {
            BrowserVisible = true;
            mainView.ShowFolders();
        }, miViewFolders);
        commands.Add(delegate
        {
            BrowserVisible = true;
            mainView.ShowPages();
        }, () => OpenBooks.CurrentBook != null, miViewPages);
        commands.Add(ToggleBrowser, true, () => BrowserVisible, miToggleBrowser);
        commands.Add(ToggleSidebar, CheckSidebarAvailable, CheckSidebarEnabled, miSidebar);
        commands.Add(ToggleSmallPreview, CheckSidebarAvailable, CheckSmallPreviewEnabled, miSmallPreview);
        commands.Add(ToggleSearchBrowser, CheckSearchAvailable, CheckSearchBrowserEnabled, miSearchBrowser);
        commands.Add(ToggleInfoPanel, CheckInfoPanelAvailable, CheckInfoPanelEnabled, miInfoPanel);
        commands.AddService(this, (IRefreshDisplay c) =>
        {
            c.RefreshDisplay();
        }, miViewRefresh);
        commands.Add(delegate
        {
            GetRatingEditor().SetRating(0f);
        }, () => GetRatingEditor().IsValid(), () => Math.Round(GetRatingEditor().GetRating()) == 0.0, miRate0, cmRate0);
        commands.Add(delegate
        {
            GetRatingEditor().SetRating(1f);
        }, () => GetRatingEditor().IsValid(), () => Math.Round(GetRatingEditor().GetRating()) == 1.0, miRate1, cmRate1);
        commands.Add(delegate
        {
            GetRatingEditor().SetRating(2f);
        }, () => GetRatingEditor().IsValid(), () => Math.Round(GetRatingEditor().GetRating()) == 2.0, miRate2, cmRate2);
        commands.Add(delegate
        {
            GetRatingEditor().SetRating(3f);
        }, () => GetRatingEditor().IsValid(), () => Math.Round(GetRatingEditor().GetRating()) == 3.0, miRate3, cmRate3);
        commands.Add(delegate
        {
            GetRatingEditor().SetRating(4f);
        }, () => GetRatingEditor().IsValid(), () => Math.Round(GetRatingEditor().GetRating()) == 4.0, miRate4, cmRate4);
        commands.Add(delegate
        {
            GetRatingEditor().SetRating(5f);
        }, () => GetRatingEditor().IsValid(), () => Math.Round(GetRatingEditor().GetRating()) == 5.0, miRate5, cmRate5);
        commands.Add(delegate
        {
            GetRatingEditor().QuickRatingAndReview();
        }, () => GetRatingEditor().IsValid(), miQuickRating, cmQuickRating);
        commands.Add(delegate
        {
            if (!Program.Help.Execute("HelpMain"))
            {
                Program.StartDocument(Program.DefaultWiki);
            }
        }, miWebHelp);
        commands.Add(delegate
        {
            books.Open(Program.QuickHelpManualFile, OpenComicOptions.OpenInNewSlot | OpenComicOptions.NoFileUpdate);
        }, miHelpQuickIntro);
        commands.Add(delegate
        {
            Program.StartDocument(Program.DefaultWebSite);
        }, miWebHome);
        commands.Add(delegate
        {
            Program.StartDocument(Program.DefaultUserForm);
        }, miWebUserForum);
        commands.Add(ShowAboutDialog, miAbout, tbAbout);
        commands.Add(ShowNews, miNews);
        commands.Add(SaveWorkspace, tsSaveWorkspace, miSaveWorkspace);
        commands.Add(EditWorkspace, () => Program.Settings.Workspaces.Count > 0, tsEditWorkspaces, miEditWorkspaces);
        commands.Add(EditWorkspaceDisplaySettings, miComicDisplaySettings, tbComicDisplaySettings);
        commands.Add(EditListLayout, CheckViewOptionsAvailable, miEditListLayout);
        commands.Add(SaveListLayout, miSaveListLayout);
        commands.Add(EditListLayouts, () => Program.Settings.ListConfigurations.Count > 0, miEditLayouts);
        commands.Add(delegate
        {
            SetListLayoutToAll();
        }, miSetAllListsSame);
        commands.Add(delegate
        {
            Program.Settings.TrackCurrentPage = !Program.Settings.TrackCurrentPage;
        }, true, () => Program.Settings.TrackCurrentPage, miTrackCurrentPage);
        commands.Add(ComicDisplay.RefreshDisplay, ComicDisplay.IsValid, cmRefreshPage);
        commands.Add(RestoreFromTray, cmNotifyRestore);
        commands.Add(OpenBooks.CloseAllButCurrent, () => OpenBooks.Slots.Count > 0, cmCloseAllButThis);
        commands.Add(OpenBooks.CloseAllToTheRight, () => OpenBooks.CurrentSlot < OpenBooks.Slots.Count - 1, cmCloseAllToTheRight);
        commands.Add(delegate
        {
            Clipboard.SetText(ComicDisplay.Book.Comic.FilePath);
        }, () => ComicDisplay.Book != null && ComicDisplay.Book.Comic.EditMode.IsLocalComic(), cmCopyPath);
        commands.Add(delegate
        {
            Program.ShowExplorer(ComicDisplay.Book.Comic.FilePath);
        }, () => ComicDisplay.Book != null && ComicDisplay.Book.Comic.EditMode.IsLocalComic(), cmRevealInExplorer);
        commands.Add(() => _ = CheckForUpdateAsync(true), miCheckUpdate);
    }

    private void InitializeKeyboard()
    {
        string group = "Library";
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miNextFromList.Image, "NextComic", group, "Next Book", (Action)delegate
        {
            OpenNextComic();
        }, new CommandKey[1]
        {
                CommandKey.N
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miPrevFromList.Image, "PrevComic", group, "Previous Book", (Action)delegate
        {
            OpenPrevComic();
        }, new CommandKey[1]
        {
                CommandKey.P
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miRandomFromList.Image, "RandomComic", group, "Random Book", (Action)delegate
        {
            OpenRandomComic();
        }, new CommandKey[1]
        {
                CommandKey.L
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miToggleBrowser.Image, "ShowBrowser", group, "Show Browser", ToggleBrowserFromReader, CommandKey.MouseLeft, CommandKey.Escape));
        group = "Browse";
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miFirstPage.Image, "MoveToFirstPage", group, "First Page", ComicDisplay.DisplayFirstPage, CommandKey.Home | CommandKey.Ctrl, CommandKey.GestureDouble1));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miPrevPage.Image, "MoveToPreviousPage", group, "Previous Page", (Action)delegate
        {
            ComicDisplay.DisplayPreviousPage(ComicDisplay.PagingMode.Double);
        }, new CommandKey[4]
        {
                CommandKey.PageUp,
                CommandKey.Left | CommandKey.Alt,
                CommandKey.Gesture1,
                CommandKey.FlickRight
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miNextPage.Image, "MoveToNextPage", group, "Next Page", (Action)delegate
        {
            ComicDisplay.DisplayNextPage(ComicDisplay.PagingMode.Double);
        }, new CommandKey[4]
        {
                CommandKey.PageDown,
                CommandKey.Right | CommandKey.Alt,
                CommandKey.Gesture3,
                CommandKey.FlickLeft
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miLastPage.Image, "MoveToLastPage", group, "Last Page", ComicDisplay.DisplayLastPage, CommandKey.End | CommandKey.Ctrl, CommandKey.GestureDouble3));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miPrevBookmark.Image, "MoveToPrevBookmark", group, "Previous Bookmark", ComicDisplay.DisplayPreviousBookmarkedPage, CommandKey.PageUp | CommandKey.Ctrl));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miNextBookmark.Image, "MoveToNextBookmark", group, "Next Bookmark", ComicDisplay.DisplayNextBookmarkedPage, CommandKey.PageDown | CommandKey.Ctrl));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miPrevTab.Image, "PrevTab", group, "Previous Tab", OpenBooks.PreviousSlot, CommandKey.Tab | CommandKey.Shift));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miNextTab.Image, "NextTab", group, "Next Tab", OpenBooks.NextSlot, CommandKey.Tab));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MoveToPrevPageSingle", group, "Single Page Back", delegate
        {
            ComicDisplay.DisplayPreviousPage(ComicDisplay.PagingMode.None);
        }, CommandKey.PageUp | CommandKey.Shift));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MoveToNextPageSingle", group, "Single Page Forward", delegate
        {
            ComicDisplay.DisplayNextPage(ComicDisplay.PagingMode.None);
        }, CommandKey.PageDown | CommandKey.Shift));
        group = "Auto Scroll";
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MovePrevPart", group, "Previous Part", delegate
        {
            ComicDisplay.DisplayPreviousPageOrPart();
        }, CommandKey.Space | CommandKey.Shift, CommandKey.Gesture7));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MoveNextPart", group, "Next Part", delegate
        {
            ComicDisplay.DisplayNextPageOrPart();
        }, CommandKey.Space, CommandKey.Gesture9));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MoveFirstPart", group, "Page Start", delegate
        {
            ComicDisplay.DisplayPart(PartPageToDisplay.First);
        }, CommandKey.Home));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MoveLastPart", group, "Page End", delegate
        {
            ComicDisplay.DisplayPart(PartPageToDisplay.Last);
        }, CommandKey.End));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MovePartDown10", group, "Move Part 10% down", delegate
        {
            ComicDisplay.MovePartDown(0.1f);
        }, CommandKey.V, CommandKey.Down | CommandKey.Ctrl));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MovePartUp10", group, "Move Part 10% up", delegate
        {
            ComicDisplay.MovePartDown(-0.1f);
        }, CommandKey.B, CommandKey.Up | CommandKey.Ctrl));
        group = "Scroll";
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miAutoScroll.Image, "ToggleAutoScrolling", group, "Toggle Auto Scrolling", (Action)delegate
        {
            Program.Settings.AutoScrolling = !Program.Settings.AutoScrolling;
        }, new CommandKey[1]
        {
                CommandKey.S
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("DoublePageAutoScroll", group, "Double Page Auto Scroll", delegate
        {
            ComicDisplay.TwoPageNavigation = !ComicDisplay.TwoPageNavigation;
        }, CommandKey.S | CommandKey.Shift));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MoveUp", group, "Up", ComicDisplay.ScrollUp, CommandKey.Up, CommandKey.MouseWheelUp));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MoveDown", group, "Down", ComicDisplay.ScrollDown, CommandKey.Down, CommandKey.MouseWheelDown));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MoveLeft", group, "Left", ComicDisplay.ScrollLeft, CommandKey.Left, CommandKey.MouseTiltLeft));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("MoveRight", group, "Right", ComicDisplay.ScrollRight, CommandKey.Right, CommandKey.MouseTiltRight));
        group = "Display Options";
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miReaderUndocked.Image, "ToggleUndockReader", group, "Toggle Undock Reader", ToggleUndockReader, CommandKey.D));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miFullScreen.Image, "ToggleFullScreen", group, "Toggle Full Screen", ComicDisplay.ToggleFullScreen, CommandKey.F, CommandKey.MouseDoubleLeft, CommandKey.Gesture2));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miTwoPages.Image, "ToggleTwoPages", group, "Toggle Two Pages", ComicDisplay.TogglePageLayout, CommandKey.T));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("ToggleRealisticPages", group, "Toggle Realistic Display", ComicDisplay.ToogleRealisticPages, CommandKey.D | CommandKey.Shift));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miMagnify.Image, "ToggleMagnify", group, "Toggle Magnifier", (Action)delegate
        {
            ComicDisplay.MagnifierVisible = !ComicDisplay.MagnifierVisible;
        }, new CommandKey[2]
        {
                CommandKey.M,
                CommandKey.TouchPressAndTap
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("ToggleMenu", group, "Toggle Menu", delegate
        {
            MinimalGui = !MinimalGui;
        }, CommandKey.K));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(null, "ToggleNavigationOverlay", group, "Toggle Navigation Overlay", ComicDisplay.ToggleNavigationOverlay, CommandKey.Gesture8, CommandKey.TouchTwoFingerTap));
        group = "Page Display";
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miOriginal.Image, "Original", group, "Original Size", ComicDisplay.SetPageOriginal, CommandKey.D1));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miFitAll.Image, "FitAll", group, "Fit All", ComicDisplay.SetPageFitAll, CommandKey.D2));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miFitWidth.Image, "FitWidth", group, "Fit Width", ComicDisplay.SetPageFitWidth, CommandKey.D3));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miFitWidthAdaptive.Image, "FitWidthAdaptive", group, "Fit Width (adaptive)", ComicDisplay.SetPageFitWidthAdaptive, CommandKey.D4));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miFitHeight.Image, "FitHeight", group, "Fit Height", ComicDisplay.SetPageFitHeight, CommandKey.D5));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miBestFit.Image, "FitBest", group, "Best Fit", ComicDisplay.SetPageBestFit, CommandKey.D6));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miSinglePage.Image, "SinglePage", group, "Single Page", (Action)delegate
        {
            ComicDisplay.PageLayout = PageLayoutMode.Single;
        }, new CommandKey[1]
        {
                CommandKey.D7
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miTwoPages.Image, "TwoPages", group, "Two Pages", (Action)delegate
        {
            ComicDisplay.PageLayout = PageLayoutMode.Double;
        }, new CommandKey[1]
        {
                CommandKey.D8
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miTwoPagesAdaptive.Image, "TwoPagesAdaptive", group, "Two Pages (adaptive)", (Action)delegate
        {
            ComicDisplay.PageLayout = PageLayoutMode.DoubleAdaptive;
        }, new CommandKey[1]
        {
                CommandKey.D9
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miRightToLeft.Image, "RightToLeft", group, "Right to Left", (Action)delegate
        {
            ComicDisplay.RightToLeftReading = !ComicDisplay.RightToLeftReading;
        }, new CommandKey[1]
        {
                CommandKey.D0
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miOnlyFitOversized.Image, "OnlyFitIfOversized", group, "Only Fit if oversized", ComicDisplay.ToggleFitOnlyIfOversized, CommandKey.O));
        group = "ZoomAndRotate";
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miRotateRight.Image, "RotateC", group, "Rotate Right", (Action)delegate
        {
            ComicDisplay.ImageRotation = ComicDisplay.ImageRotation.RotateRight();
        }, new CommandKey[1]
        {
                CommandKey.R
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miRotateLeft.Image, "RotateCC", group, "Rotate Left", (Action)delegate
        {
            ComicDisplay.ImageRotation = ComicDisplay.ImageRotation.RotateLeft();
        }, new CommandKey[1]
        {
                CommandKey.R | CommandKey.Shift
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miAutoRotate.Image, "AutoRotate", group, "Autorotate Double Pages", (Action)delegate
        {
            ComicDisplay.ImageAutoRotate = !ComicDisplay.ImageAutoRotate;
        }, new CommandKey[1]
        {
                CommandKey.A
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miZoomIn.Image, "ZoomIn", group, "Zoom In", (Action)delegate
        {
            ComicDisplay.ImageZoom = (ComicDisplay.ImageZoom + 0.1f).Clamp(1f, 8f);
        }, new CommandKey[1]
        {
                CommandKey.MouseWheelUp | CommandKey.Ctrl
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miZoomOut.Image, "ZoomOut", group, "Zoom Out", (Action)delegate
        {
            ComicDisplay.ImageZoom = (ComicDisplay.ImageZoom - 0.1f).Clamp(1f, 8f);
        }, new CommandKey[1]
        {
                CommandKey.MouseWheelDown | CommandKey.Ctrl
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miZoomIn.Image, "StepZoomIn", group, "Step Zoom In", (Action)delegate
        {
            ComicDisplay.ImageZoom = (ComicDisplay.ImageZoom + Program.ExtendedSettings.KeyboardZoomStepping).Clamp(1f, 4f);
        }, new CommandKey[1]
        {
                CommandKey.Z
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miZoomOut.Image, "StepZoomOut", group, "Step Zoom Out", (Action)delegate
        {
            ComicDisplay.ImageZoom = (ComicDisplay.ImageZoom - Program.ExtendedSettings.KeyboardZoomStepping).Clamp(1f, 4f);
        }, new CommandKey[1]
        {
                CommandKey.Z | CommandKey.Shift
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand(miToggleZoom.Image, "ToggleZoom", group, "Toggle Zoom", ToggleZoom, CommandKey.TouchDoubleTap));
        group = "Edit";
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand((Image)Resources.Rotate90Permanent, "PageRotateC", group, "Rotate Page Right", (Action)delegate
        {
            GetPageEditor().Rotation = GetPageEditor().Rotation.RotateRight();
        }, new CommandKey[1]
        {
                CommandKey.Y
        }));
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand((Image)Resources.Rotate270Permanent, "PageRotateCC", group, "Rotate Page Left", (Action)delegate
        {
            GetPageEditor().Rotation = GetPageEditor().Rotation.RotateLeft();
        }, new CommandKey[1]
        {
                CommandKey.Y | CommandKey.Shift
        }));
        group = "Other";
        ComicDisplay.KeyboardMap.Commands.Add(new KeyboardCommand("Exit", group, "Exit", ControlExit, CommandKey.Q));
        Program.DefaultKeyboardMapping = ComicDisplay.KeyboardMap.GetKeyMapping().ToArray();
        ComicDisplay.KeyboardMap.SetKeyMapping(Program.Settings.ReaderKeyboardMapping);
        mainKeys.Commands.Add(new KeyboardCommand("FocusQuickSearch", "General", "FQS", FocusQuickSearch, CommandKey.F | CommandKey.Ctrl));
        mainKeys.Commands.Add(new KeyboardCommand("BrowsePrevious", "General", "Previous List",
            () =>
            {
                if (this.FindActiveService<ILibraryBrowser>()?.CanBrowsePrevious() == true)
                    this.FindActiveService<ILibraryBrowser>()?.BrowsePrevious();
            }, [CommandKey.MouseButton4]));
        mainKeys.Commands.Add(new KeyboardCommand("BrowseNext", "General", "Next List",
            () =>
            {
                if (this.FindActiveService<ILibraryBrowser>()?.CanBrowseNext() == true)
                    this.FindActiveService<ILibraryBrowser>()?.BrowseNext();
            }, [CommandKey.MouseButton5]));
        mainKeys.Commands.Add(new KeyboardCommand("ToggleQuickSearch", "General", "Quick Search",
            () => this.FindActiveService<ComicListLibraryBrowser>()?.ToggleQuickSearch(), [CommandKey.F | CommandKey.Ctrl | CommandKey.Alt]));
    }
}
