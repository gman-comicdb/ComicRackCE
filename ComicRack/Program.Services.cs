using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.IO.Cache;
using cYo.Projects.ComicRack.Viewer.Dialogs;

namespace cYo.Projects.ComicRack.Viewer;

/// <summary>
/// To my untrained eye, they look like App Services. Not sure if they actually are.
/// </summary>
public static partial class Program
{
    // Program.StartupProgress
    // Program.StartNew
    private static Splash splash;

    /// <summary>
    /// The GUI. All of it.
    /// </summary>
    public static MainForm MainForm { get; private set; }
    public static ScriptOutputForm ScriptConsole { get; set; }
    public static NewsStorage News { get; private set; }

    // NetWorkManager
    public static NetworkManager NetworkManager { get; private set; }

    // CacheManager
    public static CacheManager CacheManager { get; private set; }
    public static ImagePool ImagePool => CacheManager.ImagePool;
    public static FileCache InternetCache => CacheManager.InternetCache;

    // DatabaseManager
    public static readonly DatabaseManager DatabaseManager = new(); // field
    public static ComicBookFactory BookFactory => DatabaseManager.BookFactory;
    public static ComicDatabase Database => DatabaseManager.Database;

    // QueueManager
    public static QueueManager QueueManager { get; private set; }
    public static ComicScanner Scanner => QueueManager.Scanner;
}
