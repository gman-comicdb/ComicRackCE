using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;

using cYo.Common;
using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.Mathematics;
using cYo.Common.Threading;
using cYo.Common.Xml;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Backup;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Engine.IO.Network;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Viewer.Views;

namespace cYo.Projects.ComicRack.Viewer.Config;

[Serializable]
public class Settings : ICacheSettings, IComicUpdateSettings, ISharesSettings, IVirtualTagSettings
{
    [Serializable]
    public class PasswordCacheEntry
    {
        [XmlAttribute]
        [DefaultValue(0)]
        public int RemoteId;

        [XmlAttribute]
        [DefaultValue(null)]
        public string Password;

        public PasswordCacheEntry()
        {
        }

        public PasswordCacheEntry(string remote, string password)
        {
            RemoteId = remote.GetHashCode();
            Password = password;
        }
    }

    [Serializable]
    public class RemoteViewConfig : IIdentity, IDisplayListConfig
    {
        [XmlAttribute]
        public Guid Id { get; set; }

        public DisplayListConfig Display { get; set; }

        public RemoteViewConfig()
        {
        }

        public RemoteViewConfig(Guid id, DisplayListConfig config)
        {
            Id = id;
            Display = config;
        }
    }

    public class RemoteExplorerViewSettings : IIdentity
    {
        [XmlAttribute]
        public Guid Id { get; set; }

        [DefaultValue(null)]
        public ComicExplorerViewSettings Settings { get; set; }

        public RemoteExplorerViewSettings()
        {
        }

        public RemoteExplorerViewSettings(Guid id, ComicExplorerViewSettings settings)
        {
            Id = id;
            Settings = settings;
        }
    }

    public const int RecentFileCount = 20;

    public const int MinimumMemoryPageCacheCount = 20;

    public const int MaximumMemoryPageCacheCount = 100;

    public const int DefaultMemoryPageCacheCount = 25;

    public const int MinimumMemoryThumbnailCacheMB = 5;

    public const int MaximumMemoryThumbnailCacheMB = 500;

    public const int DefaultMemoryThumbnailCacheMB = 50;

    public const int DefaultInternetCacheSizeMB = 1000;

    public const string DefaultHelpSystem = "ComicRack Online Manual";

    public const int MinimumSystemMemory = 64;

    public const int UnlimitedSystemMemory = 4096;

    private List<ListConfiguration> listConfigurations = new();

    private List<ExternalProgram> externalPrograms = new();

    private DisplayWorkspace currentWorkspace = new();

    private readonly List<DisplayWorkspace> workspaces = new();

    private string pasteProperties = "Series";

    private ComicPageType pageFilter = ComicPageType.All;

    private string lastExplorerFolder = string.Empty;

    private Guid lastLibraryItem = Guid.Empty;

    private int lastOpenFilterIndex = 1;

    private int lastSaveFilterIndex = 1;

    private int lastExportPageFilterIndex = 1;

    private readonly SmartList<string> favoriteFolders = new();

    private readonly MruList<RemoteShareItem> remoteShares = new();

    private readonly SmartList<ComicLibraryServerConfig> shares = new();

    private bool lookForShared = true;

    private bool autoConnectShares = true;

    private readonly SmartList<PasswordCacheEntry> passwordCache = new();

    private string extraWifiDeviceAddresses = string.Empty;

    private ImageDisplayOptions pageImageDisplayOptions = ImageDisplayOptions.HighQuality;

    private volatile int overlayScaling = 100;

    private BitmapAdjustment globalColorAdjustment = BitmapAdjustment.Empty;

    private Size magnifySize = new(300, 200);

    private float magnifyOpaque = 1f;

    private float magnifyZoom = 2f;

    private MagnifierStyle magnifyStyle;

    private bool autoMagnifiery = true;

    private bool hardwareAcceleration = true;

    private bool displayChangeAnimation = true;

    private bool flowingMouseScrolling = true;

    private bool softwareFiltering = true;

    private bool hardwareFiltering;

    private float mouseWheelSpeed = 2f;

    private readonly List<StringPair> readerKeyboardMapping = new();

    private string ignoredCoverImages;

    private bool autoScrolling;

    private HiddenMessageBoxes hiddenMessageBoxes;

    private bool updateComicFiles;

    private bool autoUpdateComicsFiles;

    private string helpSystem = DefaultHelpSystem;

    private bool scripting = true;

    private string scriptingLibraries = string.Empty;

    private bool showSplash = true;

    private bool openLastFile = true;

    private bool scanStartup;

    private bool updateWebComicsStartup;

    private bool newsStartup = true;

    private readonly List<string> lastOpenFiles = new();

    private bool openLastPage = true;

    private bool closeBrowserOnOpen;

    private bool addToLibraryOnOpen;

    private bool openInNewTab;

    private bool hideCursorFullScreen = true;

    private bool autoNavigateComics = true;

    private bool showCurrentPageOverlay = true;

    private bool showVisiblePagePartOverlay = true;

    private bool showStatusOverlay = true;

    private bool showNavigationOverlay = true;

    private bool navigationOverlayOnTop;

    private bool currentPageShowsName;

    private bool autoHideMagnifier = true;

    private bool pageChangeDelay = true;

    private bool scrollingDoesBrowse = true;

    private bool resetZoomOnPageChange;

    private bool zoomInOutOnPageChange = true;

    private bool smoothScrolling = true;

    private bool blendWhilePaging;

    private bool trackCurrentPage = true;

    private RightToLeftReadingMode rightToLeftReadingMode = RightToLeftReadingMode.FlipPages;

    private bool leftRightMovementReversed;

    private bool showToolTips;

    private bool showSearchLinks = true;

    private bool fadeInThumbnails = true;

    private bool dogEarThumbnails = true;

    private bool numericRatingThumbnails = true;

    private bool localQuickSearch = true;

    private bool coverThumbnailsSameSize;

    private bool commonListStackLayout;

    private bool showQuickOpen = true;

    private bool catalogOnlyForFileless = true;

    private bool showCustomBookFields;

    private bool minimizeToTray;

    private volatile bool closeMinimizeToTray = true;

    private volatile bool autoMinimalGui;

    private volatile bool animatePanels = true;

    private volatile bool alwaysDisplayBrowserDockingGrip;

    private bool autoHideMainMenu = true;

    private bool disableDragDrop = false;

    private bool showMainMenuNoComicOpen = true;

    private bool informationCover3D = true;

    private bool displayLibraryGauges = true;

    private LibraryGauges libraryGaugesFormat = LibraryGauges.Default;

    private bool newBooksChecked = true;

    private bool thumbCacheEnabled = true;

    private int thumbCacheSizeMB = 500;

    private bool pageCacheEnabled = true;

    private int pageCacheSizeMB = 500;

    private bool internetCacheEnabled = true;

    private int internetCacheSizeMB = DefaultInternetCacheSizeMB;

    private int memoryThumbCacheSizeMB = DefaultMemoryThumbnailCacheMB;

    private int memoryPageCacheCount = DefaultMemoryPageCacheCount;

    private bool memoryThumbCacheOptimized = true;

    private bool memoryPageCacheOptimized = true;

    private bool removeMissingFilesOnFullScan;

    private bool dontAddRemoveFiles;

    private bool overwriteAssociations;

    private readonly List<RemoteViewConfig> remoteViewConfigList = new();

    private readonly List<RemoteExplorerViewSettings> remoteExplorerViewSettingsList = new();

    private readonly List<string> quickSearchList = new();

    private readonly List<string> libraryQuickSearchList = new();

    private readonly MruList<string> keyboardLayouts = new();

    private readonly MruList<string> thumbnailFiles = new();

    private readonly ExportSettingCollection exportUserPresets = new();

    private readonly SmartList<DeviceSyncSettings> devices = new();

    public List<ListConfiguration> ListConfigurations
    {
        get => listConfigurations;
        set => listConfigurations = value;
    }

    public List<ExternalProgram> ExternalPrograms
    {
        get => externalPrograms;
        set => externalPrograms = value;
    }

    public DisplayWorkspace CurrentWorkspace
    {
        get => currentWorkspace;
        set => currentWorkspace = value;
    }

    public List<DisplayWorkspace> Workspaces => workspaces;

    [Browsable(false)]
    [DefaultValue(0)]
    public int RunCount { get; set; }

    [Browsable(false)]
    [DefaultValue("")]
    public string PasteProperties
    {
        get => pasteProperties;
        set => pasteProperties = value;
    }

    [Browsable(false)]
    [DefaultValue(null)]
    public string SelectedBrowser { get; set; }

    [DefaultValue(ComicPageType.All)]
    [Browsable(false)]
    public ComicPageType PageFilter
    {
        get => pageFilter;
        set => pageFilter = value;
    }

    [Browsable(false)]
    [DefaultValue("")]
    public string LastExplorerFolder
    {
        get => lastExplorerFolder;
        set => lastExplorerFolder = value;
    }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool ExplorerIncludeSubFolders { get; set; }

    [Browsable(false)]
    [DefaultValue("")]
    public Guid LastLibraryItem
    {
        get => lastLibraryItem;
        set => lastLibraryItem = value;
    }

    [Browsable(false)]
    [DefaultValue(-1)]
    public int LastOpenFilterIndex
    {
        get => lastOpenFilterIndex;
        set => lastOpenFilterIndex = value;
    }

    [Browsable(false)]
    [DefaultValue(1)]
    public int LastSaveFilterIndex
    {
        get => lastSaveFilterIndex;
        set => lastSaveFilterIndex = value;
    }

    [Browsable(false)]
    [DefaultValue(1)]
    public int LastExportPageFilterIndex
    {
        get => lastExportPageFilterIndex;
        set => lastExportPageFilterIndex = value;
    }

    [Browsable(false)]
    public SmartList<string> FavoriteFolders => favoriteFolders;

    [Browsable(false)]
    [XmlArrayItem("Share")]
    public MruList<RemoteShareItem> RemoteShares => remoteShares;

    [Browsable(false)]
    [DefaultValue(null)]
    public string PluginsStates { get; set; }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool MoveFilesToRecycleBin { get; set; }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool AlsoRemoveFromLibrary { get; set; }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool AlsoRemoveFromLibraryFiltered { get; set; }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool RemoveFilesfromDatabase { get; set; }

    [Browsable(false)]
    [DefaultValue(TabLayouts.None)]
    public TabLayouts TabLayouts { get; set; }

    [DefaultValue(128)]
    public int QuickOpenThumbnailSize { get; set; }

    public SmartList<ComicLibraryServerConfig> Shares => shares;

    public bool IsSharing => shares.Any((ComicLibraryServerConfig sc) => sc.IsValidShare);

    [DefaultValue("")]
    public string ExternalServerAddress { get; set; }

    [DefaultValue("")]
    public string PrivateListingPassword { get; set; }

    [Category("Network")]
    [Description("Look for locally shared comic libraries on the network")]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool LookForShared
    {
        get => lookForShared;
        set
        {
            if (lookForShared != value)
            {
                lookForShared = value;
                FireEvent(this.LookForSharedChanged);
            }
        }
    }

    [Category("Network")]
    [Description("Autoconnect shares")]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool AutoConnectShares
    {
        get => autoConnectShares;
        set
        {
            if (autoConnectShares != value)
            {
                autoConnectShares = value;
                FireEvent(this.AutoConnectSharesChanged);
            }
        }
    }

    [Category("Network")]
    [Description("cache for remote passwords")]
    [XmlArrayItem("Item")]
    public SmartList<PasswordCacheEntry> PasswordCache => passwordCache;

    [DefaultValue("")]
    public string ExtraWifiDeviceAddresses
    {
        get => extraWifiDeviceAddresses;
        set
        {
            if (!(extraWifiDeviceAddresses == value))
            {
                extraWifiDeviceAddresses = value;
                FireEvent(this.ExtraWirelessIpAddressesChanged);
            }
        }
    }

    [Category("Display")]
    [Description("Set how the single pages are rendered")]
    [DefaultValue(ImageDisplayOptions.HighQuality)]
    [XmlElement("PageImageOptions")]
    public ImageDisplayOptions PageImageDisplayOptions
    {
        get => pageImageDisplayOptions;
        set
        {
            if (pageImageDisplayOptions != value)
            {
                pageImageDisplayOptions = value;
                FireEvent(this.PageImageDisplayOptionsChanged);
            }
        }
    }

    [Category("Display")]
    [Description("Scaling of the overlays")]
    [DefaultValue(100)]
    public int OverlayScaling
    {
        get => overlayScaling;
        set
        {
            if (overlayScaling != value)
            {
                overlayScaling = value;
                FireEvent(this.OverlayScalingChanged);
            }
        }
    }

    [DefaultValue(typeof(BitmapAdjustment), "0, 0, 0")]
    public BitmapAdjustment GlobalColorAdjustment
    {
        get
        {
            using (ItemMonitor.Lock(this))
            {
                return globalColorAdjustment;
            }
        }
        set
        {
            using (ItemMonitor.Lock(this))
            {
                if (globalColorAdjustment == value)
                {
                    return;
                }
                globalColorAdjustment = value;
            }
            FireEvent(this.ColorAdjustmentChanged);
        }
    }

    [DefaultValue(typeof(Size), "300, 200")]
    public Size MagnifySize
    {
        get => magnifySize;
        set
        {
            using (ItemMonitor.Lock(this))
            {
                if (magnifySize == value)
                {
                    return;
                }
                magnifySize = value;
            }
            FireEvent(this.MagnifySizeChanged);
        }
    }

    [DefaultValue(1f)]
    public float MagnifyOpaque
    {
        get => magnifyOpaque;
        set
        {
            if (magnifyOpaque != value)
            {
                magnifyOpaque = value;
                FireEvent(this.MagnifyOpaqueChanged);
            }
        }
    }

    [DefaultValue(2f)]
    public float MagnifyZoom
    {
        get => magnifyZoom;
        set
        {
            if (magnifyZoom != value)
            {
                magnifyZoom = value;
                FireEvent(this.MagnifyZoomChanged);
            }
        }
    }

    [DefaultValue(MagnifierStyle.Glass)]
    public MagnifierStyle MagnifyStyle
    {
        get => magnifyStyle;
        set
        {
            if (magnifyStyle != value)
            {
                magnifyStyle = value;
                FireEvent(this.MagnifyStyleChanged);
            }
        }
    }

    [DefaultValue(true)]
    public bool AutoMagnifier
    {
        get => autoMagnifiery;
        set
        {
            if (autoMagnifiery != value)
            {
                autoMagnifiery = value;
                FireEvent(this.AutoMagnifierChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool HardwareAcceleration
    {
        get => hardwareAcceleration;
        set
        {
            if (hardwareAcceleration != value)
            {
                hardwareAcceleration = value;
                FireEvent(this.HardwareAccelerationChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool DisplayChangeAnimation
    {
        get => displayChangeAnimation;
        set
        {
            if (displayChangeAnimation != value)
            {
                displayChangeAnimation = value;
                FireEvent(this.DisplayChangeAnimationChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool FlowingMouseScrolling
    {
        get => flowingMouseScrolling;
        set
        {
            if (flowingMouseScrolling != value)
            {
                flowingMouseScrolling = value;
                FireEvent(this.FlowingMouseScrollingChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool SoftwareFiltering
    {
        get => softwareFiltering;
        set
        {
            if (softwareFiltering != value)
            {
                softwareFiltering = value;
                FireEvent(this.SoftwareFilteringChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool HardwareFiltering
    {
        get => hardwareFiltering;
        set
        {
            if (hardwareFiltering != value)
            {
                hardwareFiltering = value;
                FireEvent(this.HardwareFilteringChanged);
            }
        }
    }

    [Category("Behavior")]
    [Description("Lines per mouse scrolling")]
    [DefaultValue(2f)]
    public float MouseWheelSpeed
    {
        get => mouseWheelSpeed;
        set
        {
            if (mouseWheelSpeed != value)
            {
                mouseWheelSpeed = value;
                FireEvent(this.MouseWheelSpeedChanged);
            }
        }
    }

    [Category("Behavior")]
    [Description("Shortcuts for the reader")]
    [XmlArray("ReaderKeyboardV3")]
    [XmlArrayItem("Action")]
    public List<StringPair> ReaderKeyboardMapping => readerKeyboardMapping;

    [Category("Behavior")]
    [Description("Images not to use as cover images")]
    [DefaultValue(null)]
    public string IgnoredCoverImages
    {
        get => ignoredCoverImages;
        set
        {
            if (!(ignoredCoverImages == value))
            {
                ignoredCoverImages = value;
                FireEvent(this.IgnoredCoverImagesChanged);
            }
        }
    }

    [Category("Behavior")]
    [Description("Turns autoscrolling on")]
    [DefaultValue(false)]
    [Browsable(false)]
    public bool AutoScrolling
    {
        get => autoScrolling;
        set
        {
            if (autoScrolling != value)
            {
                autoScrolling = value;
                FireEvent(this.AutoScrollingChanged);
            }
        }
    }

    [Category("Behavior")]
    [Description("Turned off message boxes")]
    [DefaultValue(HiddenMessageBoxes.None)]
    public HiddenMessageBoxes HiddenMessageBoxes
    {
        get => hiddenMessageBoxes;
        set => hiddenMessageBoxes = value;
    }

    [Category("Behavior")]
    [Description("Update Book Files with new information")]
    [DefaultValue(false)]
    [Browsable(false)]
    public bool UpdateComicFiles
    {
        get => updateComicFiles;
        set
        {
            if (updateComicFiles != value)
            {
                updateComicFiles = value;
                FireEvent(this.UpdateComicFilesChanged);
            }
        }
    }

    [Category("Behavior")]
    [Description("Auto update of Book files")]
    [DefaultValue(false)]
    [Browsable(false)]
    public bool AutoUpdateComicsFiles
    {
        get => autoUpdateComicsFiles;
        set
        {
            if (autoUpdateComicsFiles != value)
            {
                autoUpdateComicsFiles = value;
                FireEvent(this.AutoUpdateComicFilesChanged);
            }
        }
    }

    [DefaultValue(DefaultHelpSystem)]
    public string HelpSystem
    {
        get => helpSystem;
        set
        {
            if (!(helpSystem == value))
            {
                helpSystem = value;
                FireEvent(this.HelpSystemChanged);
            }
        }
    }

    [Category("Scripting")]
    [Description("Enable or disable Scripting")]
    [Browsable(false)]
    [DefaultValue(true)]
    public bool Scripting
    {
        get => scripting;
        set
        {
            if (scripting != value)
            {
                scripting = value;
                FireEvent(this.ScriptingChanged);
            }
        }
    }

    [Category("Scripting")]
    [Description("Enable or disable Scripting")]
    [DefaultValue("")]
    public string ScriptingLibraries
    {
        get => scriptingLibraries;
        set
        {
            if (!(scriptingLibraries == value))
            {
                scriptingLibraries = value;
                FireEvent(this.ScriptingLibrariesChanged);
            }
        }
    }

    [DefaultValue(false)]
    public bool HideSampleScripts { get; set; }

    [Category("Starting ComicRack")]
    [Description("Show Splash Screen")]
    [DefaultValue(true)]
    public bool ShowSplash
    {
        get => showSplash;
        set
        {
            if (showSplash != value)
            {
                showSplash = value;
                FireEvent(this.ShowSplashChanged);
            }
        }
    }

    [Category("Starting ComicRack")]
    [Description("Reopen Books from last session")]
    [DefaultValue(true)]
    public bool OpenLastFile
    {
        get => openLastFile;
        set
        {
            if (openLastFile != value)
            {
                openLastFile = value;
                FireEvent(this.OpenLastFileChanged);
            }
        }
    }

    [Category("Starting ComicRack")]
    [Description("Rescan the Book Folders for new Books")]
    [DefaultValue(false)]
    public bool ScanStartup
    {
        get => scanStartup;
        set
        {
            if (scanStartup != value)
            {
                scanStartup = value;
                FireEvent(this.ScanStartupChanged);
            }
        }
    }

    [Category("Starting ComicRack")]
    [Description("Update Web Comics")]
    [DefaultValue(false)]
    public bool UpdateWebComicsStartup
    {
        get => updateWebComicsStartup;
        set
        {
            if (updateWebComicsStartup != value)
            {
                updateWebComicsStartup = value;
                FireEvent(this.CheckWebComicsStartupChanged);
            }
        }
    }

    [Category("Starting ComicRack")]
    [Description("Check for latest news on ComicRack")]
    [DefaultValue(true)]
    public bool NewsStartup
    {
        get => newsStartup;
        set
        {
            if (newsStartup != value)
            {
                newsStartup = value;
                FireEvent(this.NewsStartupChanged);
            }
        }
    }

    [Category("Starting ComicRack")]
    [Description("Lasts file opened")]
    public List<string> LastOpenFiles => lastOpenFiles;

    [DefaultValue(true)]
    public bool ShowQuickManual { get; set; }

    [Category("Opening a Book")]
    [Description("Open the Book at the page where it was closed")]
    [DefaultValue(true)]
    public bool OpenLastPage
    {
        get => openLastPage;
        set
        {
            if (openLastPage != value)
            {
                openLastPage = value;
                FireEvent(this.OpenLastPageChanged);
            }
        }
    }

    [Category("Opening a Book")]
    [Description("Close the Browser when a new Book is opened")]
    [DefaultValue(false)]
    public bool CloseBrowserOnOpen
    {
        get => closeBrowserOnOpen;
        set
        {
            if (closeBrowserOnOpen != value)
            {
                closeBrowserOnOpen = value;
                FireEvent(this.CloseBrowserOnOpenChanged);
            }
        }
    }

    [Category("Opening a Book")]
    [Description("Opened Files are added to the Library")]
    [DefaultValue(false)]
    public bool AddToLibraryOnOpen
    {
        get => addToLibraryOnOpen;
        set
        {
            if (addToLibraryOnOpen != value)
            {
                addToLibraryOnOpen = value;
                FireEvent(this.AddToLibraryOnOpenChanged);
            }
        }
    }

    [Category("Opening a Book")]
    [Description("Open in new Tab")]
    [DefaultValue(false)]
    public bool OpenInNewTab
    {
        get => openInNewTab;
        set
        {
            if (openInNewTab != value)
            {
                openInNewTab = value;
                FireEvent(this.OpenInNewTabChanged);
            }
        }
    }

    [Category("Reading")]
    [Description("Hide the mouse cursor when reading in Full Screen Mode")]
    [DefaultValue(true)]
    public bool HideCursorFullScreen
    {
        get => hideCursorFullScreen;
        set
        {
            if (hideCursorFullScreen != value)
            {
                hideCursorFullScreen = value;
                FireEvent(this.HideCursorFullScreenChanged);
            }
        }
    }

    [Category("Reading")]
    [Description("Reading beyond the start or end opens the next Book")]
    [DefaultValue(true)]
    public bool AutoNavigateComics
    {
        get => autoNavigateComics;
        set
        {
            if (autoNavigateComics != value)
            {
                autoNavigateComics = value;
                FireEvent(this.AutoNavigateComicsChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool ShowCurrentPageOverlay
    {
        get => showCurrentPageOverlay;
        set
        {
            if (showCurrentPageOverlay != value)
            {
                showCurrentPageOverlay = value;
                FireEvent(this.ShowOverlaysChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool ShowVisiblePagePartOverlay
    {
        get => showVisiblePagePartOverlay;
        set
        {
            if (showVisiblePagePartOverlay != value)
            {
                showVisiblePagePartOverlay = value;
                FireEvent(this.ShowOverlaysChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool ShowStatusOverlay
    {
        get => showStatusOverlay;
        set
        {
            if (showStatusOverlay != value)
            {
                showStatusOverlay = value;
                FireEvent(this.ShowOverlaysChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool ShowNavigationOverlay
    {
        get => showNavigationOverlay;
        set
        {
            if (showNavigationOverlay != value)
            {
                showNavigationOverlay = value;
                FireEvent(this.ShowOverlaysChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool NavigationOverlayOnTop
    {
        get => navigationOverlayOnTop;
        set
        {
            if (navigationOverlayOnTop != value)
            {
                navigationOverlayOnTop = value;
                FireEvent(this.ShowOverlaysChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(false)]
    public bool CurrentPageShowsName
    {
        get => currentPageShowsName;
        set
        {
            if (currentPageShowsName != value)
            {
                currentPageShowsName = value;
                FireEvent(this.ShowOverlaysChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool AutoHideMagnifier
    {
        get => autoHideMagnifier;
        set
        {
            if (autoHideMagnifier != value)
            {
                autoHideMagnifier = value;
                FireEvent(this.AutoHideMagnifierChanged);
            }
        }
    }

    [Category("Reading")]
    [Description("Mouse Wheel and Cursor Keys delay on page transitions")]
    [DefaultValue(true)]
    public bool PageChangeDelay
    {
        get => pageChangeDelay;
        set
        {
            if (pageChangeDelay != value)
            {
                pageChangeDelay = value;
                FireEvent(this.PageChangeDelayChanged);
            }
        }
    }

    [Category("Reading")]
    [Description("Scrolling to page margin browses to new pages")]
    [DefaultValue(true)]
    public bool ScrollingDoesBrowse
    {
        get => scrollingDoesBrowse;
        set
        {
            if (scrollingDoesBrowse != value)
            {
                scrollingDoesBrowse = value;
                FireEvent(this.ScrollingDoesBrowseChanged);
            }
        }
    }

    [Category("Reading")]
    [Description("Zoom is reset to 100% on page change")]
    [DefaultValue(false)]
    public bool ResetZoomOnPageChange
    {
        get => resetZoomOnPageChange;
        set
        {
            if (resetZoomOnPageChange != value)
            {
                resetZoomOnPageChange = value;
                FireEvent(this.ResetZoomOnPageChangeChanged);
            }
        }
    }

    [Category("Reading")]
    [Description("During page change a zoom out is done")]
    [DefaultValue(true)]
    public bool ZoomInOutOnPageChange
    {
        get => zoomInOutOnPageChange;
        set
        {
            if (zoomInOutOnPageChange != value)
            {
                zoomInOutOnPageChange = value;
                FireEvent(this.ZoomInOutOnPageChangeChanged);
            }
        }
    }

    [Browsable(false)]
    [DefaultValue(true)]
    public bool SmoothScrolling
    {
        get => smoothScrolling;
        set
        {
            if (smoothScrolling != value)
            {
                smoothScrolling = value;
                FireEvent(this.SmoothScrollingChanged);
            }
        }
    }

    [Category("Reading")]
    [Description("Blend animation while fast paging")]
    [DefaultValue(false)]
    public bool BlendWhilePaging
    {
        get => blendWhilePaging;
        set
        {
            if (blendWhilePaging != value)
            {
                blendWhilePaging = value;
                FireEvent(this.BlendWhilePagingChanged);
            }
        }
    }

    [DefaultValue(true)]
    public bool TrackCurrentPage
    {
        get => trackCurrentPage;
        set
        {
            if (trackCurrentPage != value)
            {
                trackCurrentPage = value;
                FireEvent(this.TrackCurrentPageChanged);
            }
        }
    }

    [DefaultValue(RightToLeftReadingMode.FlipPages)]
    [Browsable(false)]
    public RightToLeftReadingMode RightToLeftReadingMode
    {
        get => rightToLeftReadingMode;
        set
        {
            if (rightToLeftReadingMode != value)
            {
                rightToLeftReadingMode = value;
                FireEvent(this.RightToLeftReadingModeChanged);
            }
        }
    }

    [Category("Right to Left")]
    [Description("True right to left reading")]
    [DefaultValue(false)]
    public bool TrueRightToLeftReading
    {
        get => RightToLeftReadingMode == RightToLeftReadingMode.FlipParts;
        set => RightToLeftReadingMode = ((!value) ? RightToLeftReadingMode.FlipPages : RightToLeftReadingMode.FlipParts);
    }

    [Category("Right to Left")]
    [Description("Left/right movement is also reversed")]
    [DefaultValue(false)]
    public bool LeftRightMovementReversed
    {
        get => leftRightMovementReversed;
        set
        {
            if (leftRightMovementReversed != value)
            {
                leftRightMovementReversed = value;
                FireEvent(this.LeftRightMovementReversedChanged);
            }
        }
    }

    [Category("Browser")]
    [Description("Show Tooltips for Books in the Browser")]
    [DefaultValue(false)]
    public bool ShowToolTips
    {
        get => showToolTips;
        set
        {
            if (showToolTips != value)
            {
                showToolTips = value;
                FireEvent(this.ShowToolTipsChanged);
            }
        }
    }

    [Category("Browser")]
    [Description("Show Search Links")]
    [DefaultValue(true)]
    public bool ShowSearchLinks
    {
        get => showSearchLinks;
        set
        {
            if (showSearchLinks != value)
            {
                showSearchLinks = value;
                FireEvent(this.ShowSearchLinksChanged);
            }
        }
    }

    [Category("Browser")]
    [Description("New loaded Thumbnails slowly fade in")]
    [DefaultValue(true)]
    public bool FadeInThumbnails
    {
        get => fadeInThumbnails;
        set
        {
            if (fadeInThumbnails != value)
            {
                fadeInThumbnails = value;
                FireEvent(this.FadeInThumbnailsChanged);
            }
        }
    }

    [Category("Browser")]
    [Description("Selected Thumbnails have a dog-ear")]
    [DefaultValue(true)]
    public bool DogEarThumbnails
    {
        get => dogEarThumbnails;
        set
        {
            if (dogEarThumbnails != value)
            {
                dogEarThumbnails = value;
                FireEvent(this.DogEarThumbnailsChanged);
            }
        }
    }

    [Category("Browser")]
    [Description("Thumbnails display numeric ratings")]
    [DefaultValue(true)]
    public bool NumericRatingThumbnails
    {
        get => numericRatingThumbnails;
        set
        {
            if (numericRatingThumbnails != value)
            {
                numericRatingThumbnails = value;
                FireEvent(this.NumericRatingThumbnailsChanged);
            }
        }
    }

    [Category("Browser")]
    [Description("Each List has own Quick Search settings")]
    [DefaultValue(true)]
    public bool LocalQuickSearch
    {
        get => localQuickSearch;
        set
        {
            if (localQuickSearch != value)
            {
                localQuickSearch = value;
                FireEvent(this.LocalQuickSearchChanged);
            }
        }
    }

    [Category("Browser")]
    [Description("All Cover Thumbnails have the same Size")]
    [DefaultValue(false)]
    public bool CoverThumbnailsSameSize
    {
        get => coverThumbnailsSameSize;
        set
        {
            if (coverThumbnailsSameSize != value)
            {
                coverThumbnailsSameSize = value;
                FireEvent(this.CoverThumbnailsSameSizeChanged);
            }
        }
    }

    [Category("Browser")]
    [Description("All Stacks in a List have the same Layout")]
    [DefaultValue(false)]
    public bool CommonListStackLayout
    {
        get => commonListStackLayout;
        set
        {
            if (commonListStackLayout != value)
            {
                commonListStackLayout = value;
                FireEvent(this.CommonListStackLayoutChanged);
            }
        }
    }

    [Category("Application")]
    [Description("Show Quick Open when no book is open")]
    [DefaultValue(true)]
    public bool ShowQuickOpen
    {
        get => showQuickOpen;
        set
        {
            if (showQuickOpen != value)
            {
                showQuickOpen = value;
                FireEvent(this.ShowQuickOpenChanged);
            }
        }
    }

    [Category("Application")]
    [Description("Show Catalog fields only for fileless Books")]
    [DefaultValue(true)]
    public bool CatalogOnlyForFileless
    {
        get => catalogOnlyForFileless;
        set
        {
            if (catalogOnlyForFileless != value)
            {
                catalogOnlyForFileless = value;
                FireEvent(this.CatalogOnlyForFilelessChanged);
            }
        }
    }

    [Category("Application")]
    [Description("Show custom Book fields")]
    [DefaultValue(false)]
    public bool ShowCustomBookFields
    {
        get => showCustomBookFields;
        set
        {
            if (showCustomBookFields != value)
            {
                showCustomBookFields = value;
                FireEvent(this.ShowCustomBookFieldsChanged);
            }
        }
    }

    [Category("Application")]
    [Description("Minimize moves ComicRack into the Notification Area")]
    [DefaultValue(false)]
    public bool MinimizeToTray
    {
        get => minimizeToTray;
        set
        {
            if (minimizeToTray != value)
            {
                minimizeToTray = value;
                FireEvent(this.MinimizeToTrayChanged);
            }
        }
    }

    [Category("Application")]
    [Description("Close moves ComicRack into the Notification Area")]
    [DefaultValue(true)]
    public bool CloseMinimizesToTray
    {
        get => closeMinimizeToTray;
        set
        {
            if (closeMinimizeToTray != value)
            {
                closeMinimizeToTray = value;
                FireEvent(this.CloseMinimizesToTrayChanged);
            }
        }
    }

    [Category("Reading")]
    [Description("Fullscreen also toggles Minimal User Interface")]
    [DefaultValue(false)]
    public bool AutoMinimalGui
    {
        get => autoMinimalGui;
        set
        {
            if (autoMinimalGui != value)
            {
                autoMinimalGui = value;
                FireEvent(this.AutoMinimalGuiChanged);
            }
        }
    }

    [Category("Application")]
    [Description("Animate expanding and collapsing Panels")]
    [DefaultValue(true)]
    public bool AnimatePanels
    {
        get => animatePanels;
        set
        {
            if (animatePanels != value)
            {
                animatePanels = value;
                FireEvent(this.AnimatePanelsChanged);
            }
        }
    }

    [Category("Browser")]
    [Description("Always display Browser Docking Grip")]
    [DefaultValue(false)]
    public bool AlwaysDisplayBrowserDockingGrip
    {
        get => alwaysDisplayBrowserDockingGrip;
        set
        {
            if (alwaysDisplayBrowserDockingGrip != value)
            {
                alwaysDisplayBrowserDockingGrip = value;
                FireEvent(this.AlwaysDisplayBrowserDockingGripChanged);
            }
        }
    }

    [Category("Browser")]
    [Description("Disable opening files via drag-and-drop")]
    [DefaultValue(false)]
    public bool DisableDragDrop
    {
        get => disableDragDrop;
        set
        {
            if (disableDragDrop != value)
            {
                disableDragDrop = value;
                FireEvent(this.DisableDragDropChanged);
            }
        }
    }

    [DefaultValue(true)]
    [Browsable(false)]
    public bool AutoHideMainMenu
    {
        get => autoHideMainMenu;
        set
        {
            if (autoHideMainMenu != value)
            {
                autoHideMainMenu = value;
                FireEvent(this.AutoHideMainMenuChanged);
            }
        }
    }

    [Category("Application")]
    [Description("Show Main Menu if no Book is open")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool ShowMainMenuNoComicOpen
    {
        get => showMainMenuNoComicOpen;
        set
        {
            if (showMainMenuNoComicOpen != value)
            {
                showMainMenuNoComicOpen = value;
                FireEvent(this.ShowMainMenuNoComicOpenChanged);
            }
        }
    }

    [Category("Application")]
    [Description("3D display of covers in Book Info Dialog")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool InformationCover3D
    {
        get => informationCover3D;
        set
        {
            if (informationCover3D != value)
            {
                informationCover3D = value;
                FireEvent(this.InformationCover3DChanged);
            }
        }
    }

    [Category("Application")]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool DisplayLibraryGauges
    {
        get => displayLibraryGauges;
        set
        {
            if (displayLibraryGauges != value)
            {
                displayLibraryGauges = value;
                FireEvent(this.DisplayLibraryGaugesChanged);
            }
        }
    }

    [Category("Application")]
    [Description("Gauge Format in Library Browser")]
    [DefaultValue(LibraryGauges.Default)]
    [Browsable(true)]
    public LibraryGauges LibraryGaugesFormat
    {
        get => libraryGaugesFormat;
        set
        {
            if (libraryGaugesFormat != value)
            {
                libraryGaugesFormat = value;
                FireEvent(this.DisplayLibraryGaugesChanged);
            }
        }
    }

    [Category("Application")]
    [Description("Newly added Books are checked")]
    [DefaultValue(true)]
    [Browsable(true)]
    public bool NewBooksChecked
    {
        get => newBooksChecked;
        set
        {
            if (newBooksChecked != value)
            {
                newBooksChecked = value;
                FireEvent(this.NewBooksCheckedChanged);
            }
        }
    }

    [Category("Caching")]
    [Description("Turn thumbnail caching on or off")]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool ThumbCacheEnabled
    {
        get => thumbCacheEnabled;
        set
        {
            if (thumbCacheEnabled != value)
            {
                thumbCacheEnabled = value;
                FireEvent(this.CacheSettingsChanged);
            }
        }
    }

    [Category("Caching")]
    [Description("Size of the thumbnail cache")]
    [DefaultValue(500)]
    [Browsable(false)]
    public int ThumbCacheSizeMB
    {
        get => thumbCacheSizeMB;
        set
        {
            if (thumbCacheSizeMB != value)
            {
                thumbCacheSizeMB = value;
                FireEvent(this.CacheSettingsChanged);
            }
        }
    }

    [Category("Caching")]
    [Description("Turn page caching on or off")]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool PageCacheEnabled
    {
        get => pageCacheEnabled;
        set
        {
            if (pageCacheEnabled != value)
            {
                pageCacheEnabled = value;
                FireEvent(this.CacheSettingsChanged);
            }
        }
    }

    [Category("Caching")]
    [Description("Size of the page cache")]
    [DefaultValue(500)]
    [Browsable(false)]
    public int PageCacheSizeMB
    {
        get => pageCacheSizeMB;
        set
        {
            if (pageCacheSizeMB != value)
            {
                pageCacheSizeMB = value;
                FireEvent(this.CacheSettingsChanged);
            }
        }
    }

    [Category("Caching")]
    [Description("Turn Internet caching on or off")]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool InternetCacheEnabled
    {
        get => internetCacheEnabled;
        set
        {
            if (internetCacheEnabled != value)
            {
                internetCacheEnabled = value;
                FireEvent(this.CacheSettingsChanged);
            }
        }
    }

    [Category("Caching")]
    [Description("Size of the internet cache")]
    [DefaultValue(DefaultInternetCacheSizeMB)]
    [Browsable(false)]
    public int InternetCacheSizeMB
    {
        get => internetCacheSizeMB;
        set
        {
            if (internetCacheSizeMB != value)
            {
                internetCacheSizeMB = value;
                FireEvent(this.CacheSettingsChanged);
            }
        }
    }

    [Category("Caching")]
    [Description("Size of the memory thumbnail cache")]
    [DefaultValue(DefaultMemoryThumbnailCacheMB)]
    [Browsable(false)]
    public int MemoryThumbCacheSizeMB
    {
        get => memoryThumbCacheSizeMB;
        set
        {
            value = value.Clamp(MinimumMemoryThumbnailCacheMB, MaximumMemoryThumbnailCacheMB);
            if (memoryThumbCacheSizeMB != value)
            {
                memoryThumbCacheSizeMB = value;
                FireEvent(this.CacheSettingsChanged);
            }
        }
    }

    [Category("Caching")]
    [Description("Pages to cache in memory")]
    [DefaultValue(DefaultMemoryPageCacheCount)]
    [Browsable(false)]
    public int MemoryPageCacheCount
    {
        get => memoryPageCacheCount;
        set
        {
            value = value.Clamp(MinimumMemoryPageCacheCount, MaximumMemoryPageCacheCount);
            if (memoryPageCacheCount != value)
            {
                memoryPageCacheCount = value;
                FireEvent(this.CacheSettingsChanged);
            }
        }
    }

    [Description("Optimize Memory Thumbnail cache")]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool MemoryThumbCacheOptimized
    {
        get => memoryThumbCacheOptimized;
        set
        {
            if (memoryThumbCacheOptimized != value)
            {
                memoryThumbCacheOptimized = value;
                FireEvent(this.CacheSettingsChanged);
            }
        }
    }

    [Description("Optimize Memory Page cache")]
    [DefaultValue(true)]
    [Browsable(false)]
    public bool MemoryPageCacheOptimized
    {
        get => memoryPageCacheOptimized;
        set
        {
            if (memoryPageCacheOptimized != value)
            {
                memoryPageCacheOptimized = value;
                FireEvent(this.CacheSettingsChanged);
            }
        }
    }

    [DefaultValue(UnlimitedSystemMemory)]
    [Browsable(false)]
    public int MaximumMemoryMB { get; set; }

    [DefaultValue(false)]
    public bool RemoveMissingFilesOnFullScan
    {
        get => removeMissingFilesOnFullScan;
        set
        {
            if (removeMissingFilesOnFullScan != value)
            {
                removeMissingFilesOnFullScan = value;
                FireEvent(this.ScanOptionsChanged);
            }
        }
    }

    [DefaultValue(false)]
    public bool DontAddRemoveFiles
    {
        get => dontAddRemoveFiles;
        set
        {
            if (dontAddRemoveFiles != value)
            {
                dontAddRemoveFiles = value;
                FireEvent(this.ScanOptionsChanged);
            }
        }
    }

    [DefaultValue(false)]
    public bool OverwriteAssociations
    {
        get => overwriteAssociations;
        set
        {
            if (overwriteAssociations != value)
            {
                overwriteAssociations = value;
                FireEvent(this.OverwriteAssociationsChanged);
            }
        }
    }

    public List<RemoteViewConfig> RemoteViewConfigList => remoteViewConfigList;

    public List<RemoteExplorerViewSettings> RemoteExplorerViewSettingsList => remoteExplorerViewSettingsList;

    [DefaultValue(null)]
    public string CultureName { get; set; }

    [Category("Import & Export")]
    [Description("Exported Book Lists contain filenames")]
    [DefaultValue(false)]
    public bool ExportedListsContainFilenames { get; set; }

    public List<string> QuickSearchList => quickSearchList;

    public List<string> LibraryQuickSearchList => libraryQuickSearchList;

    public MruList<string> KeyboardLayouts => keyboardLayouts;

    public MruList<string> ThumbnailFiles => thumbnailFiles;

    [DefaultValue(null)]
    public ExportSetting CurrentExportSetting { get; set; }

    public ExportSettingCollection ExportUserPresets => exportUserPresets;

    public SmartList<DeviceSyncSettings> Devices => devices;

    [DefaultValue(null)]
    public string UserEmail { get; set; }

    [DefaultValue(null)]
    [XmlElement("VK")]
    public string ValidationKey { get; set; }

    [DefaultValue(typeof(DateTime), "01.01.0001")]
    public DateTime ValidationDate { get; set; }

    [DefaultValue(null)]
    public string DonationShown { get; set; }

    public string OpenRemoteFilter { get; set; }

    public string OpenRemotePassword { get; set; }

    [Category("Reading")]
    [Description("Show Quick Review Dialog after finishing Book")]
    [DefaultValue(false)]
    public bool AutoShowQuickReview { get; set; }

    private List<VirtualTag> virtualTags = new();
    public List<VirtualTag> VirtualTags => virtualTags;

    private BackupManagerOptions backupManager = new();
    public BackupManagerOptions BackupManager
    {
        get => backupManager;
        set
        {
            if (backupManager != value)
            {
                backupManager = value;
                FireEvent(this.BackupManagerChanged);
            }
        }
    }

    [field: NonSerialized]
    public event EventHandler BackupManagerChanged;

    [field: NonSerialized]
    public event EventHandler SettingsChanged;

    [field: NonSerialized]
    public event EventHandler CacheSettingsChanged;

    [field: NonSerialized]
    public event EventHandler PageImageDisplayOptionsChanged;

    [field: NonSerialized]
    public event EventHandler FadeInThumbnailsChanged;

    [field: NonSerialized]
    public event EventHandler DogEarThumbnailsChanged;

    [field: NonSerialized]
    public event EventHandler NumericRatingThumbnailsChanged;

    [field: NonSerialized]
    public event EventHandler CoverThumbnailsSameSizeChanged;

    [field: NonSerialized]
    public event EventHandler CommonListStackLayoutChanged;

    [field: NonSerialized]
    public event EventHandler LocalQuickSearchChanged;

    [field: NonSerialized]
    public event EventHandler AlwaysDisplayBrowserDockingGripChanged;

    [field: NonSerialized]
    public event EventHandler ShowOverlaysChanged;

    [field: NonSerialized]
    public event EventHandler OverlayScalingChanged;

    [field: NonSerialized]
    public event EventHandler HideCursorFullScreenChanged;

    [field: NonSerialized]
    public event EventHandler ShowSplashChanged;

    [field: NonSerialized]
    public event EventHandler OpenLastFileChanged;

    [field: NonSerialized]
    public event EventHandler OpenLastPageChanged;

    [field: NonSerialized]
    public event EventHandler ScanStartupChanged;

    [field: NonSerialized]
    public event EventHandler CheckWebComicsStartupChanged;

    [field: NonSerialized]
    public event EventHandler ScanOptionsChanged;

    [field: NonSerialized]
    public event EventHandler OverwriteAssociationsChanged;

    [field: NonSerialized]
    public event EventHandler ShowToolTipsChanged;

    [field: NonSerialized]
    public event EventHandler ShowSearchLinksChanged;

    [field: NonSerialized]
    public event EventHandler LookForSharedChanged;

    [field: NonSerialized]
    public event EventHandler AutoConnectSharesChanged;

    [field: NonSerialized]
    public event EventHandler MinimizeToTrayChanged;

    [field: NonSerialized]
    public event EventHandler CatalogOnlyForFilelessChanged;

    [field: NonSerialized]
    public event EventHandler ShowCustomBookFieldsChanged;

    [field: NonSerialized]
    public event EventHandler ShowQuickOpenChanged;

    [field: NonSerialized]
    public event EventHandler CloseMinimizesToTrayChanged;

    [field: NonSerialized]
    public event EventHandler CloseBrowserOnOpenChanged;

    [field: NonSerialized]
    public event EventHandler AddToLibraryOnOpenChanged;

    [field: NonSerialized]
    public event EventHandler OpenInNewTabChanged;

    [field: NonSerialized]
    public event EventHandler AutoUpdateComicFilesChanged;

    [field: NonSerialized]
    public event EventHandler UpdateComicFilesChanged;

    [field: NonSerialized]
    public event EventHandler BlendWhilePagingChanged;

    [field: NonSerialized]
    public event EventHandler TrackCurrentPageChanged;

    [field: NonSerialized]
    public event EventHandler AutoNavigateComicsChanged;

    [field: NonSerialized]
    public event EventHandler NewsStartupChanged;

    [field: NonSerialized]
    public event EventHandler AutoScrollingChanged;

    [field: NonSerialized]
    public event EventHandler ColorAdjustmentChanged;

    [field: NonSerialized]
    public event EventHandler IgnoredCoverImagesChanged;

    [field: NonSerialized]
    public event EventHandler ScriptingChanged;

    [field: NonSerialized]
    public event EventHandler ScriptingLibrariesChanged;

    [field: NonSerialized]
    public event EventHandler MagnifySizeChanged;

    [field: NonSerialized]
    public event EventHandler MagnifyOpaqueChanged;

    [field: NonSerialized]
    public event EventHandler MagnifyZoomChanged;

    [field: NonSerialized]
    public event EventHandler MagnifyStyleChanged;

    [field: NonSerialized]
    public event EventHandler AutoMagnifierChanged;

    [field: NonSerialized]
    public event EventHandler AnimatePanelsChanged;

    [field: NonSerialized]
    public event EventHandler AutoHideMagnifierChanged;

    [field: NonSerialized]
    public event EventHandler AutoMinimalGuiChanged;

    [field: NonSerialized]
    public event EventHandler PageChangeDelayChanged;

    [field: NonSerialized]
    public event EventHandler ScrollingDoesBrowseChanged;

    [field: NonSerialized]
    public event EventHandler ResetZoomOnPageChangeChanged;

    [field: NonSerialized]
    public event EventHandler ZoomInOutOnPageChangeChanged;

    [field: NonSerialized]
    public event EventHandler SmoothScrollingChanged;

    [field: NonSerialized]
    public event EventHandler LeftRightMovementReversedChanged;

    [field: NonSerialized]
    public event EventHandler RightToLeftReadingModeChanged;

    [field: NonSerialized]
    public event EventHandler HardwareAccelerationChanged;

    [field: NonSerialized]
    public event EventHandler DisplayChangeAnimationChanged;

    [field: NonSerialized]
    public event EventHandler FlowingMouseScrollingChanged;

    [field: NonSerialized]
    public event EventHandler SoftwareFilteringChanged;

    [field: NonSerialized]
    public event EventHandler HardwareFilteringChanged;

    [field: NonSerialized]
    public event EventHandler MouseWheelSpeedChanged;

    [field: NonSerialized]
    public event EventHandler AutoHideMainMenuChanged;

    [field: NonSerialized]
    public event EventHandler DisableDragDropChanged;

    [field: NonSerialized]
    public event EventHandler ShowMainMenuNoComicOpenChanged;

    [field: NonSerialized]
    public event EventHandler InformationCover3DChanged;

    [field: NonSerialized]
    public event EventHandler DisplayLibraryGaugesChanged;

    [field: NonSerialized]
    public event EventHandler HelpSystemChanged;

    [field: NonSerialized]
    public event EventHandler NewBooksCheckedChanged;

    [field: NonSerialized]
    public event EventHandler ExtraWirelessIpAddressesChanged;

    public Settings()
    {
        ExternalServerAddress = string.Empty;
        PrivateListingPassword = string.Empty;
        QuickOpenThumbnailSize = 128;
        MaximumMemoryMB = UnlimitedSystemMemory;
        ShowQuickManual = true;
        ValidationDate = DateTime.MinValue;
    }

    public DisplayWorkspace GetWorkspace(string name)
    {
        return string.IsNullOrEmpty(name)
            ? null
            : workspaces.FirstOrDefault((DisplayWorkspace ws) => string.Equals(ws.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    public void AddPasswordToCache(string remote, string password)
    {
        int hash = remote.GetHashCode();
        PasswordCacheEntry passwordCacheEntry = passwordCache.Find((PasswordCacheEntry e) => e.RemoteId == hash);
        if (passwordCacheEntry == null)
        {
            passwordCache.Add(new PasswordCacheEntry(remote, password));
        }
        else
        {
            passwordCacheEntry.Password = password;
        }
    }

    public string GetPasswordFromCache(string remote)
    {
        int hash = remote.GetHashCode();
        PasswordCacheEntry passwordCacheEntry = passwordCache.Find((PasswordCacheEntry e) => e.RemoteId == hash);
        return passwordCacheEntry == null ? string.Empty : passwordCacheEntry.Password;
    }

    public DisplayListConfig GetRemoteViewConfig(Guid id, DisplayListConfig defaultConfig)
    {
        RemoteViewConfig remoteViewConfig = remoteViewConfigList.Find((RemoteViewConfig item) => item.Id == id);
        DisplayListConfig displayListConfig = null;
        if (remoteViewConfig != null)
        {
            displayListConfig = remoteViewConfig.Display;
        }
        return displayListConfig ?? defaultConfig;
    }

    public void UpdateRemoteViewConfig(Guid id, DisplayListConfig config)
    {
        RemoteViewConfig remoteViewConfig = remoteViewConfigList.Find((RemoteViewConfig item) => item.Id == id);
        if (remoteViewConfig != null)
        {
            remoteViewConfig.Display = config;
        }
        else
        {
            remoteViewConfigList.Add(new RemoteViewConfig(id, config));
        }
    }

    public ComicExplorerViewSettings GetRemoteExplorerViewSetting(Guid id)
    {
        return RemoteExplorerViewSettingsList.FirstOrDefault((RemoteExplorerViewSettings s) => s.Id == id)?.Settings;
    }

    public void UpdateExplorerViewSetting(Guid id, ComicExplorerViewSettings setting)
    {
        RemoteExplorerViewSettingsList.RemoveAll((RemoteExplorerViewSettings s) => s.Id == id);
        RemoteExplorerViewSettingsList.Add(new RemoteExplorerViewSettings(id, setting));
    }

    private void FireEvent(EventHandler eh)
    {
        eh?.Invoke(this, EventArgs.Empty);
        this.SettingsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Fix()
    {
        Devices.ForEach(delegate (DeviceSyncSettings d)
        {
            ((ICollection<DeviceSyncSettings.SharedList>)d.Lists).RemoveAll((Predicate<DeviceSyncSettings.SharedList>)((DeviceSyncSettings.SharedList sl) => sl == null));
        });
    }

    //public static Settings LoadBinary(string file)
    //{
    //	try
    //	{
    //		BinaryFormatter binaryFormatter = new BinaryFormatter
    //		{
    //			AssemblyFormat = FormatterAssemblyStyle.Simple
    //		};
    //		using (Stream serializationStream = File.OpenRead(file))
    //		{
    //			return (Settings)binaryFormatter.Deserialize(serializationStream);
    //		}
    //	}
    //	catch (Exception)
    //	{
    //		return new Settings();
    //	}
    //}

    public static Settings Load(string file)
    {
        try
        {
            Settings settings = XmlUtility.Load<Settings>(file);
            settings.Fix();
            return settings;
        }
        catch (Exception)
        {
            return new Settings();
        }
    }

    //public void SaveBinary(string file)
    //{
    //	BinaryFormatter binaryFormatter = new BinaryFormatter
    //	{
    //		TypeFormat = FormatterTypeStyle.TypesWhenNeeded,
    //		AssemblyFormat = FormatterAssemblyStyle.Simple
    //	};
    //	using (Stream serializationStream = File.Create(file))
    //	{
    //		binaryFormatter.Serialize(serializationStream, this);
    //	}
    //}

    public void Save(string file)
    {
        XmlUtility.Store(file, this);
    }
}
