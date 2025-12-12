using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace cYo.Common.Win32;

// TODO : add setting so can be enabled/disabled. Should depend on MouseDrag being enabled.
/// <summary>
/// Methods to remove the Title aka Caption Bar from a window. For Borderless FullScreen or just a minimal window.
/// </summary>
public static class WindowStyle
{
    private static class Native
    {
        #region Constants
        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;

        // Standard window styles
        public const int WS_BORDER = 0x00800000;
        public const int WS_CAPTION = 0x00C00000;
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_THICKFRAME = 0x00040000; // for resizing

        // Extended window styles
        public const int WS_EX_DLGMODALFRAME = 0x00000001;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_WINDOWEDGE = 0x00000100;
        public const int WS_EX_CLIENTEDGE = 0x00000200;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        // Set Window Position
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_FRAMECHANGED = 0x0020;

        // Redraw
        public const int RDW_INVALIDATE = 0x0001;
        public const int RDW_UPDATENOW = 0x0100;
        public const int RDW_FRAME = 0x0400;
        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        #region Functions
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy,
            int flags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RedrawWindow(
            IntPtr hWnd,
            IntPtr lprcUpdate,
            IntPtr hrgnUpdate,
            int flags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool AdjustWindowRectEx(
            ref RECT lpRect,
            int dwStyle,
            bool bMenu,
            int dwExStyle);
        #endregion

        public static int RemoveTitleBar(IntPtr hWnd)
        {
            int style = GetWindowLong(hWnd, GWL_STYLE);

            // Remove caption (title bar), keep re-sizable frame
            style &= ~WS_CAPTION;   // icon, text, background, dragging, maximize (double-click)
            style &= ~WS_SYSMENU;   // control buttons (minimize, maximize, close), context menu (right-click)
            style |= WS_THICKFRAME; // ensure resizing is still allowed

            SetWindowLong(hWnd, GWL_STYLE, style);
            return style;
        }

        public static int ShowTitleBar(IntPtr hWnd)
        {
            int style = GetWindowLong(hWnd, GWL_STYLE);

            style |= WS_CAPTION;
            style |= WS_SYSMENU;

            SetWindowLong(hWnd, GWL_STYLE, style);
            return style;
        }

        public static void RefreshWindow(IntPtr hWnd)
        {
            // force Windows to re-evaluate the frame
            SetWindowPos(
                hWnd,
                IntPtr.Zero,
                0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);

            // refresh non-client area
            RedrawWindow(
                hWnd,
                IntPtr.Zero,
                IntPtr.Zero,
                RDW_INVALIDATE | RDW_FRAME | RDW_UPDATENOW);
        }

        public static Size CalculateWindowSize(Size clientSize, int style, int exStyle, bool hasMenuStrip)
        {
            RECT rect = new RECT
            {
                Left = 0,
                Top = 0,
                Right = clientSize.Width,
                Bottom = clientSize.Height
            };
            AdjustWindowRectEx(ref rect, style, hasMenuStrip, exStyle);

            int width = rect.Right - rect.Left;
            int height = rect.Bottom - rect.Top;

            return new Size(width, height);
        }
    }

    /// <summary>Removes the <paramref name="form"/> title bar and adds a top border to allow resizing.</summary>
    /// <remarks><see cref="Form.Size"/> is updated so that <see cref="Form.ClientSize"/> is unaffected.</remarks>
    public static void RemoveTitleBar(this Form form)
    {
        Size newSize = RemoveTitleBar(form, form.ClientSize, form.MainMenuStrip != null);
        form.Size = newSize;
    }

    /// <summary>Re-adds the <paramref name="form"/> title bar.</summary>
    /// <remarks><see cref="Form.Size"/> is updated so that <see cref="Form.ClientSize"/> is unaffected.</remarks>
    public static void ShowTitleBar(this Form form)
    {
        Size newSize = ShowTitleBar(form, form.ClientSize, form.MainMenuStrip != null);
        form.Size = newSize;
    }

    /// <summary>Removes the <paramref name="window"/> title bar and adds a top border to allow resizing.</summary>
    /// <param name="window"><see cref="Form"/> or other <see cref="IWin32Window"/> window.</param>
    /// <param name="clientSize"><see cref="Form.ClientSize"/>. Required for calculating new <see cref="Form.Size"/></param>
    /// <param name="hasMenuStrip">Whether <see cref="Form.MainMenuStrip"/> is not null. Required for calculating new <see cref="Form.Size"/></param>
    /// <returns><see cref="Size"/> of <paramref name="window"/> required to maintain same <paramref name="clientSize"/></returns>
    public static Size RemoveTitleBar(this IWin32Window window, Size clientSize, bool hasMenuStrip)
    {
        IntPtr hWnd = window.Handle;

        int style = Native.RemoveTitleBar(hWnd);
        int exStyle = Native.GetWindowLong(hWnd, Native.GWL_EXSTYLE);

        Native.RefreshWindow(hWnd);
        
        return clientSize.CalculateWindowSize(style, exStyle, hasMenuStrip);
    }

    /// <summary>Re-adds the <paramref name="window"/> title bar.</summary>
    /// <param name="window"><see cref="Form"/> or other <see cref="IWin32Window"/> window.</param>
    /// <param name="clientSize"><see cref="Form.ClientSize"/>. Required for calculating new <see cref="Form.Size"/></param>
    /// <param name="hasMenuStrip">Whether <see cref="Form.MainMenuStrip"/> is not null. Required for calculating new <see cref="Form.Size"/></param>
    /// <returns><see cref="Size"/> of <paramref name="window"/> required to maintain same <paramref name="clientSize"/></returns>
    public static Size ShowTitleBar(this IWin32Window window, Size clientSize, bool hasMenuStrip)
    {
        IntPtr hWnd = window.Handle;

        int style = Native.ShowTitleBar(hWnd);
        int exStyle = Native.GetWindowLong(hWnd, Native.GWL_EXSTYLE);

        Native.RefreshWindow(hWnd);

        return clientSize.CalculateWindowSize(style, exStyle, hasMenuStrip);
    }

    /// <summary>
    /// IWin32Window methods which do not rely on <see cref="System.Windows.Forms"/>,
    /// for when maintaining the same <see cref="Control.ClientSize"/> is not required.
    /// </summary>
    #region IWin32Window only methods
    /// <summary>Removes the <paramref name="window"/> title bar and adds a top border to allow resizing.</summary>
    public static void RemoveTitleBar(this IWin32Window window)
    {
        _ = Native.RemoveTitleBar(window.Handle);
        Native.RefreshWindow(window.Handle);
    }

    /// <summary>
    /// Given a <paramref name="style"/> and <paramref name="exStyle"/>, calculates the <see cref="Size"/> required
    /// to achieve a given <see cref="Control.ClientSize"/>.
    /// </summary>
    private static Size CalculateWindowSize(this Size clientSize, int style, int exStyle, bool hasMenuStrip)
        => Native.CalculateWindowSize(clientSize, style, exStyle, hasMenuStrip);

    /// <summary>Re-adds the <paramref name="window"/> title bar.</summary>
    public static void ShowTitleBar(this IWin32Window window)
    {
        _ = Native.ShowTitleBar(window.Handle);
        Native.RefreshWindow(window.Handle);
    }
    #endregion
}
