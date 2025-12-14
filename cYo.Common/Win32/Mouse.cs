using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cYo.Common.Win32;

public static class Mouse
{
    private static class Native
    {
        #region Mouse Constants
        internal const uint WM_MOUSEFIRST = 512U;

        /// <summary>Posted to a window when the cursor moves.<remarks>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mousemove#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_MOUSEMOVE = 512U;

        /// <summary>Posted when the user presses the left mouse button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-lbuttondown#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_LBUTTONDOWN = 513U;

        /// <summary>Posted when the user releases the left mouse button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-lbuttonup#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_LBUTTONUP = 514U;

        /// <summary>Posted when the user double-clicks the left mouse button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-lbuttondblclk#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_LBUTTONDBLCLK = 515U;

        /// <summary>Posted when the user presses the right mouse button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-rbuttondown#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_RBUTTONDOWN = 516U;

        /// <summary>Posted when the user releases the right mouse button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-rbuttonup#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_RBUTTONUP = 517U;

        /// <summary>Posted when the user double-clicks the right mouse button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-rbuttondblclk#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_RBUTTONDBLCLK = 518U;

        /// <summary>Posted when the user presses the middle mouse button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mbuttondown#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_MBUTTONDOWN = 519U;

        /// <summary>Posted when the user releases the middle mouse button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mbuttonup#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_MBUTTONUP = 520U;

        /// <summary>Posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mbuttondblclk#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_MBUTTONDBLCLK = 521U;

        /// <summary>Sent to the focus window when the mouse wheel is rotated.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mousewheel#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_MOUSEWHEEL = 522U;

        /// <summary>Posted when the user presses the first or second X button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-xbuttondown#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_XBUTTONDOWN = 523U;

        /// <summary>Posted when the user releases the first or second X button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-xbuttonup#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_XBUTTONUP = 524U;

        /// <summary>Posted when the user double-clicks the first or second X button while the cursor is in the client area of a window.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-xbuttondblclk#">Microsoft Documentation</see>.</remarks>
        internal const uint WM_XBUTTONDBLCLK = 525U;

        /// <summary>Sent to the active window when the mouse's horizontal scroll wheel is tilted or rotated.</summary>
        /// <remarks><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mousehwheel#">Microsoft Documentation</see></remarks>
        internal const uint WM_MOUSEHWHEEL = 526U;

        internal const uint WM_MOUSELAST = 526U;
        #endregion

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    }

    public class MouseWheelDelegator : IMessageFilter
    {
        private IntPtr lastHandle;

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)Native.WM_MOUSEMOVE:
                    lastHandle = m.HWnd;
                    break;
                case (int)Native.WM_MOUSEWHEEL:
                case (int)Native.WM_MOUSEHWHEEL:
                    if (!(m.HWnd == lastHandle))
                    {
                        Native.SendMessage(lastHandle, m.Msg, m.WParam, m.LParam);
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}
