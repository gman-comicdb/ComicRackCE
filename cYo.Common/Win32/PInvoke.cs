using System;
using System.Runtime.InteropServices;
using System.Text;
using static cYo.Common.Win32.PInvokeCore;

namespace cYo.Common.Win32;

/// <summary>
/// Platform Invoke calls.<br/>
/// If it uses DllImport and has an extern modifier, it should live here.
/// </summary>
/// <remarks>
/// Constants, enums, structs etc live in <see cref="PInvokeCore"/>
/// </remarks>
internal static class PInvoke
{
    [DllImport("libheif", CallingConvention = CallingConvention.Cdecl)]
    public static extern heif_filetype_result heif_check_filetype(IntPtr data, int len);

    #region GDI
    // FormUtility
    [DllImport("gdi32.dll")]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    // LayeredForm
    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

    // ComboBoxEx
    [DllImport("gdi32.dll")]
    public static extern IntPtr CreateSolidBrush(int crColor);

    // ComboBoxEx
    [DllImport("gdi32.dll")]
    public static extern int SetTextColor(IntPtr hdc, int crColor);

    // ComboBoxEx
    [DllImport("gdi32.dll")]
    public static extern int SetBkColor(IntPtr hdc, int crColor);

    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern Bool DeleteDC(IntPtr hdc);

    [DllImport("gdi32.dll", ExactSpelling = true)]
    public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern Bool DeleteObject(IntPtr hObject);

    // cYo.Common.Presentation.Ceco.TextRun
    //[DllImport("Gdi32")]
    //public static extern int SetGraphicsMode(HandleRef hdc, int mode);

    //[DllImport("Gdi32")]
    //public static extern bool SetWorldTransform(HandleRef hDC, XFORM xform);

    //[DllImport("Gdi32")]
    //public static extern int SelectClipRgn(HandleRef hDC, HandleRef hRgn);
    #endregion


    #region Kernel
    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool SetDllDirectory(string lpPathName);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);
    #endregion

    #region Shell
    [DllImport("Shell32.dll", CharSet = CharSet.Auto)]
    public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

    [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
    public static extern uint PathGetCharType(char c);

    [DllImport("shlwapi.dll")]
    public static extern bool PathIsNetworkPath(string pszPath);
    #endregion


    #region User32
    // Popup
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int AnimateWindow(IntPtr windowHandle, int time, AnimationFlags flags);

    // Program
    [DllImport("user32.dll")]
    public static extern bool SetProcessDPIAware();

    // FormUtility
    [DllImport("user32.dll")]
    public static extern bool IsProcessDPIAware();

    // ComboBoxEx
    [DllImport("user32.dll")]
    public static extern bool GetComboBoxInfo(IntPtr hwnd, ref COMBOBOXINFO pcbi);

    // FormUtility
    [DllImport("user32.dll")]
    public static extern IntPtr GetActiveWindow();

    // FormUtility
    [DllImport("user32.dll")]
    public static extern IntPtr GetMessageExtraInfo();

    // FormUtility
    // LayeredForm
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    // LayeredForm
    [DllImport("user32.dll", ExactSpelling = true)]
    public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

    // ShowShield
    [DllImport("user32.dll")]
    public static extern int SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    // MouseWheelDelegater
    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SendMessage(IntPtr handle, int msg, int wParam, int lParam);

    // RichTextBoxExtensions : PaintSuspend
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, ref Point lParam);

    // RichTextBoxExtensions : PaintSuspend
    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, IntPtr lParam);

    // ComboBoxEx
    // TextBoxExtensions
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern Bool UpdateLayeredWindow(
        IntPtr hwnd,
        IntPtr hdcDst,
        ref Point pptDst,
        ref Size psize,
        IntPtr hdcSrc,
        ref Point pprSrc,
        int crKey,
        ref BLENDFUNCTION pblend,
        int dwFlags);

    #endregion

    [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
    public static extern int GetCurrentThemeName(
        StringBuilder pszThemeFileName,
        int dwMaxNameChars,
        StringBuilder pszColorBuff,
        int dwMaxColorChars,
        StringBuilder pszSizeBuff,
        int cchMaxSizeChars);
}
