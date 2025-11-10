using cYo.Common.Presentation.Tao.TaoOpenGL;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace cYo.Common.Presentation.Tao.TaoOpenGL;

public static class User
{
    [CLSCompliant(false)]
    public enum SHOWWINDOW : uint
    {
        SW_HIDE = 0u,
        SW_SHOWNORMAL = 1u,
        SW_NORMAL = 1u,
        SW_SHOWMINIMIZED = 2u,
        SW_SHOWMAXIMIZED = 3u,
        SW_MAXIMIZE = 3u,
        SW_SHOWNOACTIVATE = 4u,
        SW_SHOW = 5u,
        SW_MINIMIZE = 6u,
        SW_SHOWMINNOACTIVE = 7u,
        SW_SHOWNA = 8u,
        SW_RESTORE = 9u,
        SW_SHOWDEFAULT = 10u,
        SW_FORCEMINIMIZE = 11u,
        SW_MAX = 11u
    }

    private const string USER_NATIVE_LIBRARY = "user32.dll";

    private const CallingConvention CALLING_CONVENTION = CallingConvention.StdCall;

    public const int CS_VREDRAW = 1;

    public const int CS_HREDRAW = 2;

    public const int CS_DBLCLKS = 8;

    public const int CS_OWNDC = 32;

    public const int CS_CLASSDC = 64;

    public const int CS_PARENTDC = 128;

    public const int CS_NOCLOSE = 512;

    public const int CS_SAVEBITS = 2048;

    public const int CS_BYTEALIGNCLIENT = 4096;

    public const int CS_BYTEALIGNWINDOW = 8192;

    public const int CS_GLOBALCLASS = 16384;

    public const int CS_IME = 65536;

    public const int CS_DROPSHADOW = 131072;

    public const int CDS_UPDATEREGISTRY = 1;

    public const int CDS_TEST = 2;

    public const int CDS_FULLSCREEN = 4;

    public const int DISP_CHANGE_SUCCESSFUL = 0;

    public const int DISP_CHANGE_RESTART = 1;

    public const int DISP_CHANGE_FAILED = -1;

    public const int ENUM_CURRENT_SETTINGS = -1;

    [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int ChangeDisplaySettings(ref Gdi.DEVMODE devMode, int flags);

    [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern int ChangeDisplaySettings(IntPtr devMode, int flags);

    [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool EnumDisplaySettings(string deviceName, int modeNumber, out Gdi.DEVMODE devMode);

    [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    [SuppressUnmanagedCodeSecurity]
    public static extern IntPtr GetDC(IntPtr windowHandle);

    [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool ReleaseDC(IntPtr windowHandle, IntPtr deviceContext);

    [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
    [SuppressUnmanagedCodeSecurity]
    public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
    [CLSCompliant(false)]
    public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
}