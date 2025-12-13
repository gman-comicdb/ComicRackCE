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
using cYo.Projects.ComicRack.Viewer.Controllers;
using cYo.Projects.ComicRack.Viewer.Controls;
using cYo.Projects.ComicRack.Viewer.Controls.MainForm;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Manager;
using cYo.Projects.ComicRack.Viewer.Menus;
using cYo.Projects.ComicRack.Viewer.Properties;
using cYo.Projects.ComicRack.Viewer.Views;
using Microsoft.Win32;
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

    const string NightlyDownloadLinkEXE = @"https://github.com/maforget/ComicRackCE/releases/download/nightly/ComicRackCESetup_nightly.exe";
    const string NightlyDownloadLinkZIP = @"https://github.com/maforget/ComicRackCE/releases/download/nightly/ComicRackCE_nightly.zip";
    #endregion

    #region Image
    private Image addTabImage = Resources.AddTab;

    private Image emptyTabImage = Resources.Original;


    #endregion
    #endregion

    #region Complex Properties
    private readonly CommandMapper commands = new CommandMapper();

	public void AddCommand(CommandHandler clickHandler, UpdateHandler enabledHandler, UpdateHandler checkedHandler, params object[] senders)
		=> commands.Add(clickHandler, enabledHandler, checkedHandler, senders);

	public void AddCommand(CommandHandler clickHandler, bool isCheckedHandler, UpdateHandler updateHandler, params object[] senders)
		=> commands.Add(clickHandler, isCheckedHandler, updateHandler, senders);

    public void AddCommand(CommandHandler clickHandler, UpdateHandler enableHandler, params object[] senders)
        => commands.Add(clickHandler, enableHandler, null, senders);

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

	private ReaderForm readerForm;

	private DockStyle savedBrowserDockStyle;

	private Rectangle undockedReaderBounds;

	private FormWindowState undockedReaderState;

	private TasksDialog taskDialog;

	private IEnumerable<ShareableComicListItem> defaultQuickOpenLists;

	private FormWindowState oldState = FormWindowState.Normal;

    [DefaultValue(false)]
	public bool TaskDialogWindow => taskDialog != null && !taskDialog.IsDisposed;

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

    private MainController controller;

    private MainMenuControl menu;

	public void SetController(MainController controller)
	{
		this.controller = controller;
		menu.SetController(controller);
    }

    public MainForm()
	{
		LocalizeUtility.UpdateRightToLeft(this);
		//controller = new MainController(this);
        menu = new MainMenuControl();
		
        InitializeComponent();
		base.Size = base.Size.ScaleDpi();
		
		SystemEvents.DisplaySettingsChanging += (_, _) => StoreWorkspace();
		SystemEvents.DisplaySettingsChanged += (_, _) => WorkspaceManager.SetWorkspaceDisplayOptions(Program.Settings.CurrentWorkspace);

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
		mainMenuStripVisibility = new VisibilityAnimator(components, mainMenuStrip);
		fileTabsVisibility = new VisibilityAnimator(components, fileTabs);
		statusStripVisibility = new VisibilityAnimator(components, statusStrip);
		LocalizeUtility.Localize(this, components);
		quickOpenView.Caption = TR.Load(base.Name)[quickOpenView.Name, quickOpenView.Caption];
		Program.StartupProgress(TR.Messages["InitScripts", "Initializing Scripts"], 70);
		if (ScriptUtility.Initialize(this, this, this, ComicDisplay, this, OpenBooks))
			menu.CreatePluginMenuItems();
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
        pageDisplay.PageDisplayModeChanged += menu.OnPageDisplayModeChanged;
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

	public bool OpenFile(string file, bool newSlot = false, int page = 0, bool fromShell = false)
		=> OpenSupportedFile(file, newSlot, page, fromShell);

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
