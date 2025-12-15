global using SystemBrushes = cYo.Common.Drawing.ExtendedColors.SystemBrushesEx;
global using SystemColors = cYo.Common.Drawing.ExtendedColors.SystemColorsEx;
global using SystemPens = cYo.Common.Drawing.ExtendedColors.SystemPensEx;
using cYo.Common.Threading;
using cYo.Common.Windows;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.IO.Cache;
using cYo.Projects.ComicRack.Viewer.Config;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

public static class Program
{
    public static Settings Settings => AppConfig.Settings;

    public static ExtendedSettings ExtendedSettings => AppConfig.ExtendedSettings;

    public static DefaultLists Lists => AppConfig.Lists;

    /// <summary>The GUI. All of it.</summary>
    public static MainForm MainForm => AppServices.MainForm;

    // NetworkManager
    public static NetworkManager NetworkManager => AppServices.NetworkManager;

    // CacheManager
    public static ImagePool ImagePool => AppServices.CacheManager.ImagePool;
    public static FileCache InternetCache => AppServices.CacheManager.InternetCache;

    // DatabaseManager
    public static ComicBookFactory BookFactory => AppServices.DatabaseManager.BookFactory;
    public static ComicDatabase Database => AppServices.DatabaseManager.Database;

    // QueueManager
    public static QueueManager QueueManager => AppServices.QueueManager;
    public static ComicScanner Scanner => AppServices.QueueManager.Scanner;

    [STAThread]
	private static int Main(string[] args)
	{
        // Complete Pre-Bootstrap and exit if it returns false (occurs when registering formats)
        if (!Bootstrap.PreBootstrap(out int exitCode))
            return exitCode;

        // Complete all the other crud that was in Program.Main + Program.StartNew/StartLast
        Bootstrap.RunBootstrap(args);
		
        if (AppConfig.Restart)
            RestartComicRack();
        return 0;
	}

    private static Process RestartComicRack()
    {
        Application.Exit();
        return Process.Start(
            Application.ExecutablePath,
            "-restart -waitpid " + Process.GetCurrentProcess().Id + " " + Environment.CommandLine);
    }

    public static void Collect()
    {
        GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
    }

    #region Unused?
    private static void RemoteServerStarted(object sender, NetworkManager.RemoteServerStartedEventArgs e)
    {
        CallMainForm("Remote Server Started", () => MainForm.OnRemoteServerStarted(e.Information));
    }

    private static void RemoteServerStopped(object sender, NetworkManager.RemoteServerStoppedEventArgs e)
    {
        CallMainForm("Remote Server Stopped", () => MainForm.OnRemoteServerStopped(e.Address));
    }

    private static void CallMainForm(string actionName, Action action)
    {
        ThreadUtility.RunInBackground(actionName, delegate
        {
            while (MainForm == null || !MainForm.IsInitialized)
            {
                if (MainForm != null && MainForm.IsDisposed)
                    return;
                Thread.Sleep(1000);
            }
            if (!MainForm.InvokeIfRequired(action))
                action();
        });
    }
    #endregion
}
