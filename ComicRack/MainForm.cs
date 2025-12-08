using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using cYo.Common;
using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.IO;
using cYo.Common.Localize;
using cYo.Common.Mathematics;
using cYo.Common.Net;
using cYo.Common.Runtime;
using cYo.Common.Text;
using cYo.Common.Threading;
using cYo.Common.Win32;
using cYo.Common.Windows;
using cYo.Common.Windows.Forms;
using cYo.Common.Windows.Forms.Theme.Resources;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Controls;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.Display.Forms;
using cYo.Projects.ComicRack.Engine.Drawing;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Engine.IO.Network;
using cYo.Projects.ComicRack.Engine.IO.Provider;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Controls;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Menus;
using cYo.Projects.ComicRack.Viewer.Properties;
using cYo.Projects.ComicRack.Viewer.Views;
using Microsoft.Win32;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace cYo.Projects.ComicRack.Viewer;

[ComVisible(true)]
public partial class MainForm : FormEx, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
	private static class Native
	{
		public const int WM_ACTIVATEAPP = 0x001C;
	}

    #region Simple Properties

    #region bool
    private bool menuDown;

    private bool autoHideMainMenu;

    private bool showMainMenuNoComicOpen = true;

    private bool menuClose;

    private bool savedBrowserVisible;

    private bool shieldReaderFormClosing;

    private bool enableAutoHideMenu = true;

    private bool shieldTray;

    private bool minimalGui;

    private bool quickUpdateRegistered;

    private bool quickListDirty;
    #endregion

    #region number
    private float lastZoom = 2f;

    private long menuAutoClosed;
    #endregion

    #region string
    private string[] recentFiles = [];

    private string lastWorkspaceName;

    private static readonly string None = TR.Default["None", "None"];

    private static readonly string NotAvailable = TR.Default["NotAvailable", "NA"];

    private static readonly string ExportingComics = TR.Load(typeof(MainForm).Name)["ExportingComics", "Exporting Books: {0} queued"];

    private static readonly string ExportingErrors = TR.Load(typeof(MainForm).Name)["ExportingErrors", "{0} errors. Click for details"];

    private static readonly string DeviceSyncing = TR.Load(typeof(MainForm).Name)["DeviceSyncing", "Syncing Devices: {0} queued"];

    private static readonly string DeviceSyncingErrors = TR.Load(typeof(MainForm).Name)["DeviceSyncingErrors", "{0} errors. Click for details"];

    const string NightlyDownloadLinkEXE = @"https://github.com/maforget/ComicRackCE/releases/download/nightly/ComicRackCESetup_nightly.exe";
    const string NightlyDownloadLinkZIP = @"https://github.com/maforget/ComicRackCE/releases/download/nightly/ComicRackCE_nightly.zip";
    #endregion

    #region Image
    private Image addTabImage = Resources.AddTab;

    private Image emptyTabImage = Resources.Original;

    private static Image SinglePageRtl = Resources.SinglePageRtl;

    private static Image TwoPagesRtl = Resources.TwoPageForcedRtl;

    private static Image TwoPagesAdaptiveRtl = Resources.TwoPageRtl;

    private static readonly Image exportErrorAnimation = Resources.ExportAnimationWithError;

    private static readonly Image exportAnimation = Resources.ExportAnimation;

    private static readonly Image exportError = Resources.ExportError;

    private static readonly Image deviceSyncErrorAnimation = Resources.DeviceSyncAnimationWithError;

    private static readonly Image deviceSyncAnimation = Resources.DeviceSyncAnimation;

    private static readonly Image deviceSyncError = Resources.DeviceSyncError;

    private static readonly Image zoomImage = Resources.Zoom;

    private static readonly Image zoomClearImage = Resources.ZoomClear;

    private static readonly Image updatePages = Resources.UpdatePages;

    private static readonly Image greenLight = Resources.GreenLight;

    private static readonly Image grayLight = Resources.GrayLight;

    private static readonly Image trackPagesLockedImage = Resources.Locked;

    private static readonly Image datasourceConnected = Resources.DataSourceConnected;

    private static readonly Image datasourceDisconnected = Resources.DataSourceDisconnected;
    #endregion
    #endregion

    #region Complex Properties
    private readonly CommandMapper commands = new CommandMapper();

	private readonly ToolStripThumbSize thumbSize = new ToolStripThumbSize();

	private readonly VisibilityAnimator mainMenuStripVisibility;

	private readonly VisibilityAnimator fileTabsVisibility;

	private readonly VisibilityAnimator statusStripVisibility;

	private EnumMenuUtility pageTypeContextMenu;

	private EnumMenuUtility pageTypeEditMenu;

	private EnumMenuUtility pageRotationContextMenu;

	private EnumMenuUtility pageRotationEditMenu;

	private readonly KeyboardShortcuts mainKeys = new KeyboardShortcuts();

	private ComicBook[] lastRandomList = new ComicBook[0];

	private List<ComicBook> randomSelectedComics;

	private WorkspaceType lastWorkspaceType = WorkspaceType.Default;

	private ReaderForm readerForm;

	private DockStyle savedBrowserDockStyle;

	private Rectangle undockedReaderBounds;

	private FormWindowState undockedReaderState;

	private TasksDialog taskDialog;

	private IEnumerable<ShareableComicListItem> defaultQuickOpenLists;

	private FormWindowState oldState = FormWindowState.Normal;

	[DefaultValue(null)]
	public ComicDisplay ComicDisplay
	{
		get
		{
			if (comicDisplay == null)
			{
				comicDisplay = CreateComicDisplay();
			}
			return comicDisplay;
		}
	}

	[DefaultValue(false)]
	public bool AutoHideMainMenu
	{
		get
		{
			return autoHideMainMenu;
		}
		set
		{
			if (autoHideMainMenu != value)
			{
				autoHideMainMenu = value;
				OnGuiVisibilities();
			}
		}
	}

	[DefaultValue(true)]
	public bool ShowMainMenuNoComicOpen
	{
		get
		{
			return showMainMenuNoComicOpen;
		}
		set
		{
			if (showMainMenuNoComicOpen != value)
			{
				showMainMenuNoComicOpen = value;
				OnGuiVisibilities();
			}
		}
	}

	[Browsable(false)]
	public bool IsInitialized
	{
		get;
		private set;
	}

	public bool ReaderUndocked
	{
		get
		{
			return readerForm != null;
		}
		set
		{
			if (value == ReaderUndocked)
			{
				return;
			}
			if (value)
			{
				savedBrowserDockStyle = BrowserDock;
				savedBrowserVisible = BrowserVisible;
				BrowserVisible = true;
				BrowserDock = DockStyle.Fill;
				panelReader.Controls.Remove(readerContainer);
				readerForm = new ReaderForm(ComicDisplay);
				readerForm.FormClosing += ReaderFormFormClosing;
				readerForm.KeyDown += ReaderFormKeyDown;
				readerForm.Controls.Add(readerContainer);
				readerForm.WindowState = undockedReaderState;
				if (undockedReaderBounds.IsEmpty)
				{
					readerForm.StartPosition = FormStartPosition.WindowsDefaultLocation;
				}
				else
				{
					readerForm.StartPosition = FormStartPosition.Manual;
					readerForm.SafeBounds = undockedReaderBounds;
				}
				mainView.ShowLibrary();
				if (OpenBooks.OpenCount > 0)
				{
					readerForm.Show();
				}
			}
			else
			{
				undockedReaderState = readerForm.WindowState;
				undockedReaderBounds = readerForm.SafeBounds;
				readerForm.Controls.Remove(readerContainer);
				readerForm.Dispose();
				panelReader.Controls.Add(readerContainer);
				readerForm = null;
				BrowserDock = savedBrowserDockStyle;
				BrowserVisible = savedBrowserVisible;
			}
			OnGuiVisibilities();
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Rectangle UndockedReaderBounds
	{
		get
		{
			if (!ReaderUndocked)
			{
				return undockedReaderBounds;
			}
			return readerForm.SafeBounds;
		}
		set
		{
			if (ReaderUndocked)
			{
				readerForm.SafeBounds = value;
			}
			else
			{
				undockedReaderBounds = value;
			}
		}
	}

	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public FormWindowState UndockedReaderState
	{
		get
		{
			if (!ReaderUndocked)
			{
				return undockedReaderState;
			}
			return readerForm.WindowState;
		}
		set
		{
			if (ReaderUndocked)
			{
				readerForm.WindowState = value;
			}
			else
			{
				undockedReaderState = value;
			}
		}
	}

	private bool MainToolStripVisible
	{
		get
		{
			return fileTabs.Controls.Contains(mainToolStrip);
		}
		set
		{
			bool mainToolStripVisible = MainToolStripVisible;
			if (value != mainToolStripVisible)
			{
				if (mainToolStripVisible)
				{
					mainView.TabBar.Controls.Remove(mainToolStrip);
				}
				else
				{
					fileTabs.Controls.Remove(mainToolStrip);
				}
				if (value)
				{
					fileTabs.Controls.Add(mainToolStrip);
					return;
				}
				mainView.TabBar.Controls.Add(mainToolStrip);
				mainView.TabBar.Controls.SetChildIndex(mainToolStrip, 0);
			}
		}
	}

	public DockStyle ViewDock
	{
		get
		{
			return mainViewContainer.Dock;
		}
		set
		{
			mainViewContainer.Dock = value;
		}
	}

	public Rectangle SafeBounds
	{
		get;
		set;
	}

	public bool MinimizedToTray => notifyIcon.Visible;

	public Control Control => this;

	public bool IsComicVisible
	{
		get
		{
			if (!ReaderUndocked && BrowserDock == DockStyle.Fill)
			{
				return mainView.IsComicVisible;
			}
			return true;
		}
	}

	public bool BrowserVisible
	{
		get
		{
			if (!ReaderUndocked && BrowserDock != DockStyle.Fill)
			{
				return mainViewContainer.Expanded;
			}
			return savedBrowserVisible;
		}
		set
		{
			if (!ReaderUndocked && BrowserDock != DockStyle.Fill)
			{
				mainViewContainer.Expanded = value;
				return;
			}
			savedBrowserVisible = value;
			mainViewContainer.Expanded = true;
		}
	}

	public DockStyle BrowserDock
	{
		get
		{
			if (!ReaderUndocked)
			{
				return mainViewContainer.Dock;
			}
			return savedBrowserDockStyle;
		}
		set
		{
			if (ReaderUndocked)
			{
				savedBrowserDockStyle = value;
				mainViewContainer.Dock = DockStyle.Fill;
			}
			else
			{
				mainViewContainer.Dock = value;
			}
		}
	}

	public NavigatorManager OpenBooks => books;

	[Browsable(false)]
	[DefaultValue(false)]
	public bool MinimalGui
	{
		get
		{
			return minimalGui;
		}
		set
		{
			if (minimalGui != value)
			{
				minimalGui = value;
				OnGuiVisibilities();
			}
		}
	}

	public static ScriptOutputForm ScriptConsole
	{
		get => Program.ScriptConsole != null ? Program.ScriptConsole : null;
	}

	#endregion

	public MainForm()
	{
		LocalizeUtility.UpdateRightToLeft(this);
		InitializeComponent();
		base.Size = base.Size.ScaleDpi();
		statusStrip.Height = (int)tsText.Font.GetHeight() + FormUtility.ScaleDpiY(8);
		SystemEvents.DisplaySettingsChanging += delegate
		{
			StoreWorkspace();
		};
		SystemEvents.DisplaySettingsChanged += delegate
		{
			SetWorkspaceDisplayOptions(Program.Settings.CurrentWorkspace);
		};
		if (Program.ExtendedSettings.DisableFoldersView)
		{
			miViewFolders.GetCurrentParent().Items.Remove(miViewFolders);
		}
		notifyIcon.MouseDoubleClick += NotifyIconMouseDoubleClick;
		FormUtility.EnableRightClickSplitButtons(mainToolStrip.Items);
		AllowDrop = !Program.Settings.DisableDragDrop;
		Program.Settings.DisableDragDropChanged += (sender, e) => AllowDrop = !Program.Settings.DisableDragDrop;
		base.DragDrop += BookDragDrop;
		base.DragEnter += BookDragEnter;
		ComicDisplay.FirstPageReached += viewer_FirstPageReached;
		ComicDisplay.LastPageReached += viewer_LastPageReached;
		ComicDisplay.FullScreenChanged += ViewerFullScreenChanged;
		ComicDisplay.PageChanged += ComicDisplay_PageChanged;
		books = new NavigatorManager(ComicDisplay);
		books.BookOpened += OnBookOpened;
		books.BookClosed += OnBookClosed;
		books.BookClosing += OnBookClosing;
		books.Slots.Changed += OpenBooks_SlotsChanged;
		books.CurrentSlotChanged += OpenBooks_CurrentSlotChanged;
		books.OpenComicsChanged += OpenBooks_CaptionsChanged;
		components.Add(commands);
		fileTabs.Visible = false;
		DropDownHost<MagnifySetupControl> dropDownHost = new DropDownHost<MagnifySetupControl>();
		ComicDisplay.MagnifierOpacity = (dropDownHost.Control.MagnifyOpaque = Program.Settings.MagnifyOpaque);
		ComicDisplay.MagnifierSize = (dropDownHost.Control.MagnifySize = Program.Settings.MagnifySize);
		ComicDisplay.MagnifierZoom = (dropDownHost.Control.MagnifyZoom = Program.Settings.MagnifyZoom);
		ComicDisplay.MagnifierStyle = (dropDownHost.Control.MagnifyStyle = Program.Settings.MagnifyStyle);
		ComicDisplay.AutoMagnifier = (dropDownHost.Control.AutoMagnifier = Program.Settings.AutoMagnifier);
		ComicDisplay.AutoHideMagnifier = (dropDownHost.Control.AutoHideMagnifier = Program.Settings.AutoHideMagnifier);
		dropDownHost.Control.ValuesChanged += MagnifySetupChanged;
		tbMagnify.DropDown = dropDownHost;
		mainMenuStripVisibility = new VisibilityAnimator(components, mainMenuStrip);
		fileTabsVisibility = new VisibilityAnimator(components, fileTabs);
		statusStripVisibility = new VisibilityAnimator(components, statusStrip);
		LocalizeUtility.Localize(this, components);
		quickOpenView.Caption = TR.Load(base.Name)[quickOpenView.Name, quickOpenView.Caption];
		Program.StartupProgress(TR.Messages["InitScripts", "Initializing Scripts"], 70);
		if (ScriptUtility.Initialize(this, this, this, ComicDisplay, this, OpenBooks))
		{
			miFileAutomation.DropDownItems.AddRange(ScriptUtility.CreateToolItems<ToolStripMenuItem>(this, PluginEngine.ScriptTypeLibrary, () => Program.Database.Books).ToArray());
			miFileAutomation.Visible = miFileAutomation.DropDownItems.Count != 0;
			int num = fileMenu.DropDownItems.IndexOf(miNewComic);
			ToolStripMenuItem[] array = ScriptUtility.CreateToolItems<ToolStripMenuItem>(this, PluginEngine.ScriptTypeNewBooks, () => Program.Database.Books).ToArray();
			foreach (ToolStripMenuItem value in array)
			{
				fileMenu.DropDownItems.Insert(++num, value);
			}
			foreach (Command sc in ScriptUtility.Scripts.GetCommands(PluginEngine.ScriptTypeDrawThumbnailOverlay))
			{
				sc.PreCompile();
				CoverViewItem.DrawCustomThumbnailOverlay += (ComicBook comic, Graphics graphics, Rectangle bounds, int flags) =>
				{
					sc.Invoke(new object[4]
					{
						comic,
						graphics,
						bounds,
						flags
					}, catchErrors: true);
				};
			}
		}
		Program.StartupProgress(TR.Messages["InitGUI", "Initializing User Interface"], 80);
	}

	// called the first time comicDisplay property get method is invoked
    private ComicDisplay CreateComicDisplay()
    {
        ComicDisplayControl pageDisplay = new ComicDisplayControl
        {
            AllowDrop = true,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, 0),
            MagnifierSize = new Size(400, 300),
            Name = "pageDisplay",
            Padding = new Padding(16),
            ShowStatusMessage = true,
            ContextMenuStrip = pageContextMenu,
            AnamorphicTolerance = Program.ExtendedSettings.AnamorphicScalingTolerance,
            AutoHideCursorDelay = Program.ExtendedSettings.AutoHideCursorDuration
        };
        pageDisplay.DragDrop += BookDragDrop;
        pageDisplay.DragEnter += BookDragEnter;
        pageDisplay.BookChanged += viewer_BookChanged;
        pageDisplay.PageDisplayModeChanged += viewer_PageDisplayModeChanged;
        pageDisplay.Resize += delegate
        {
            ScriptUtility.Invoke(PluginEngine.ScriptTypeReaderResized, pageDisplay.Width, pageDisplay.Height);
        };
        pageDisplay.VisibleInfoOverlaysChanged += delegate
        {
            Program.Settings.ShowVisiblePagePartOverlay = pageDisplay.VisibleInfoOverlays.HasFlag(InfoOverlays.PartInfo);
        };
        if (EngineConfiguration.Default.AeroFullScreenWorkaround)
        {
            pageDisplay.SizeChanged += pageDisplay_SizeChanged;
        }
        readerContainer.Controls.Add(pageDisplay);
        readerContainer.Controls.SetChildIndex(pageDisplay, 0);
        readerContainer.Controls.SetChildIndex(quickOpenView, 0);
        ComicDisplay comicDisplay = new ComicDisplay(pageDisplay);
        FormUtility.ServiceTranslation[pageDisplay] = comicDisplay;
        return comicDisplay;
    }

    #region Called by MainForm methods & external methods
    public ComicListItem ImportComicList(string file)
    {
        return this.FindFirstService<IImportComicList>()?.ImportList(file);
    }

    public bool OpenSupportedFile(string file, bool newSlot = false, int page = 0, bool fromShell = false)
    {
        if (Path.GetExtension(file).Equals(".crplugin", StringComparison.OrdinalIgnoreCase))
        {
            ShowPreferences(file);
            return true;
        }
        if (!Path.GetExtension(file).Equals(".cbl", StringComparison.OrdinalIgnoreCase))
        {
            bool result = books.Open(file, newSlot, Math.Max(0, page - 1));
            if (fromShell && Program.ExtendedSettings.HideBrowserIfShellOpen)
            {
                BrowserVisible = false;
            }
            return result;
        }
        ComicListItem comicListItem = ImportComicList(file);
        if (comicListItem == null)
        {
            return false;
        }
        ComicBook[] array = comicListItem.GetBooks().ToArray();
        if (array.Length == 0)
        {
            return false;
        }
        ComicBook cb = array.Aggregate((ComicBook a, ComicBook b) => (!(a.OpenedTime > b.OpenedTime)) ? b : a);
        return books.Open(cb, newSlot);
    }

    public bool ShowBookInList(ComicLibrary library, ComicListItem list, ComicBook cb, bool switchToList = true)
    {
        if (list == null || cb == null)
        {
            return false;
        }
        ILibraryBrowser libraryBrowser = this.FindFirstService<ILibraryBrowser>();
        if (libraryBrowser == null)
        {
            return false;
        }
        if (switchToList)
        {
            mainView.ShowLibrary(library);
        }
        if (!libraryBrowser.SelectList(list.Id))
        {
            return false;
        }
        return this.FindActiveService<IComicBrowser>()?.SelectComic(cb) ?? false;
    }
    #endregion
}
