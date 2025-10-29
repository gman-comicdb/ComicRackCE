using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using cYo.Common;
using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Compression;
using cYo.Common.Drawing;
using cYo.Common.IO;
using cYo.Common.Localize;
using cYo.Common.Mathematics;
using cYo.Common.Net;
using cYo.Common.Presentation.Tao;
using cYo.Common.Runtime;
using cYo.Common.Text;
using cYo.Common.Threading;
using cYo.Common.Win32;
using cYo.Common.Windows;
using cYo.Common.Windows.Extensions;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Database;
using cYo.Projects.ComicRack.Engine.Display.Forms;
using cYo.Projects.ComicRack.Engine.Drawing;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Engine.IO.Cache;
using cYo.Projects.ComicRack.Engine.IO.Provider;
using cYo.Projects.ComicRack.Engine.Sync;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Viewer.Config;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using cYo.Projects.ComicRack.Viewer.Properties;
using Microsoft.Win32;
using static IronPython.Modules._ast;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace cYo.Projects.ComicRack.Viewer;

public static partial class Program
{
    // Program.Main
    // MainForm
    /// <summary>
    /// Set by <see cref="MainForm.MenuRestart"/> when a restart is required.<br/>
    /// </summary>
    /// <remarks>
    /// When <paramref name="true"/>, <see cref="Program.Main(string[])"/> calls <see cref="Process.Start()"/> to re-launch the application instead of exiting.
    /// </remarks>
    public static bool Restart { get; set; }

    [STAThread]
	private static int Main(string[] args)
	{
		SetProcessDPIAware();
		ServicePointManager.Expect100Continue = false;
		if (ExtendedSettings.WaitPid != 0)
		{
			try
			{
				Process.GetProcessById(ExtendedSettings.WaitPid).WaitForExit(30000);
			}
			catch
			{
			}
		}
		if (!string.IsNullOrEmpty(ExtendedSettings.RegisterFormats))
		{
			if (!RegisterFormats(ExtendedSettings.RegisterFormats))
			{
				return 1;
			}
			return 0;
		}
		TR.ResourceFolder = new PackedLocalize(TR.ResourceFolder);
        NativeLibraryHelper.RegisterDirectory(); //Add the resources directory to the search path for natives dll's
        Control.CheckForIllegalCrossThreadCalls = false;
		ItemMonitor.CatchThreadInterruptException = true;
		SingleInstance singleInstance = new SingleInstance("ComicRackSingleInstance", StartNew, StartLast);
		singleInstance.Run(args);
		if (Restart)
		{
			Application.Exit();
			Process.Start(Application.ExecutablePath, "-restart -waitpid " + Process.GetCurrentProcess().Id + " " + Environment.CommandLine);
		}
		return 0;
	}

    #region Helpers
    [DllImport("user32.dll")]
    private static extern bool SetProcessDPIAware();

    public static bool RegisterFormats(string formats)
    {
        try
        {
            bool overwrite = formats.Contains("!");
            foreach (string item in formats.Remove("!").Split(',').RemoveEmpty())
            {
                bool shouldRegister = !item.Contains("-");
                string name = item.Remove("-");
                FileFormat fileFormat = Providers.Readers.GetSourceFormats().FirstOrDefault((FileFormat sf) => sf.Name == name);

                if (fileFormat != null)
                    if (shouldRegister)
                        fileFormat.RegisterShell(Program.ComicRackTypeId, Program.ComicRackDocumentName, overwrite);
                    else
                        fileFormat.UnregisterShell(Program.ComicRackTypeId);
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            ShellRegister.RefreshShell();
        }
    }

    /// <summary>
    /// Executed when an instance of ComicRackCE is already running.<br/>
    /// For example, when double-clicking a Comic or Plugin in File Explorer. (Assuming extension is associated with ComicRackCE)
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    private static void StartLast(string[] args)
    {
        ExtendedSettings sw = default(ExtendedSettings);
        MainForm.BeginInvoke(delegate
        {
            MainForm.RestoreToFront();
            try
            {
                sw = new ExtendedSettings();
                IEnumerable<string> enumerable = CommandLineParser.Parse(sw, args);
                if (!string.IsNullOrEmpty(sw.ImportList))
                {
                    MainForm.ImportComicList(sw.ImportList);
                }
                if (!string.IsNullOrEmpty(sw.InstallPlugin))
                {
                    MainForm.ShowPreferences(sw.InstallPlugin);
                }
                if (enumerable.Any())
                {
                    enumerable.ForEach((string file) =>
                    {
                        MainForm.OpenSupportedFile(file, newSlot: true, sw.Page, fromShell: true);
                    });
                }
            }
            catch (Exception)
            {
            }
        });
    }
    #endregion
}
