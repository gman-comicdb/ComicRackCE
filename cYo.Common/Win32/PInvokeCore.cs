using System;
using System.Runtime.InteropServices;

namespace cYo.Common.Win32;

/// <summary>
/// Platform Invoke constants, enums, structs etc.<br/>
/// Anything required to support <see cref="PInvoke"/>.
/// </summary>
internal static class PInvokeCore
{

    /// <summary>Sent to the dialog box procedure immediately before a dialog box is displayed.
    /// Dialog box procedures typically use this message to initialize controls and carry out any other initialization tasks that affect the appearance of the dialog box.</summary>
    /// <returns>
    /// <para>The dialog box procedure should return **TRUE** to direct the system to set the keyboard focus to the control specified by *wParam*.
    /// Otherwise, it should return **FALSE** to prevent the system from setting the default keyboard focus. The dialog box procedure should return the value directly.
    /// The **DWL\_MSGRESULT** value set by the [**SetWindowLong**](/windows/desktop/api/winuser/nf-winuser-setwindowlonga) function is ignored.</para>
    /// </returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/dlgbox/wm-initdialog#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_INITDIALOG = 272U;

    /// <summary>Sent when the user selects a command item from a menu, when a control sends a notification message to its parent window, or when an accelerator keystroke is translated.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/menurc/wm-command#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_COMMAND = 273U;

    /// <summary>A window receives this message when the user chooses a command from the Window menu (formerly known as the system or control menu)
    /// or when the user chooses the maximize button, minimize button, restore button, or close button.</summary>
    /// <returns>An application should return zero if it processes this message.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/menurc/wm-syscommand#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_SYSCOMMAND = 274U;

    /// <summary>Posted to the installing thread's message queue when a timer expires. The message is posted by the GetMessage or PeekMessage function.</summary>
    /// <returns>
    /// <para>Type: **LRESULT** An application should return zero if it processes this message.</para>
    /// </returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/winmsg/wm-timer#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_TIMER = 275U;

    /// <summary>The WM\_HSCROLL message is sent to a window when a scroll event occurs in the window's standard horizontal scroll bar.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/wm-hscroll#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_HSCROLL = 276U;

    /// <summary>The WM\_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/wm-vscroll#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_VSCROLL = 277U;

    public const int WM_GETMINMAXINFO = 36;

    public const int WM_PRINT = 791;

    public const int WM_USER = 1024;

    public const int WM_REFLECT = 8192;


    [Flags]
    public enum AnimationFlags
    {
        Roll = 0x0,
        HorizontalPositive = 0x1,
        HorizontalNegative = 0x2,
        VerticalPositive = 0x4,
        VerticalNegative = 0x8,
        Center = 0x10,
        Hide = 0x10000,
        Activate = 0x20000,
        Slide = 0x40000,
        Blend = 0x80000,
        Mask = 0xFFFFF
    }

    public struct MINMAXINFO
    {
        public Point reserved;

        public Size maxSize;

        public Point maxPosition;

        public Size minTrackSize;

        public Size maxTrackSize;
    }

    public const int WM_NCHITTEST = 132;

    public const int WM_NCACTIVATE = 134;

    public const int HTTRANSPARENT = -1;

    public const int HTCAPTION = 2;

    public const int HTLEFT = 10;

    public const int HTRIGHT = 11;

    public const int HTTOP = 12;

    public const int HTTOPLEFT = 13;

    public const int HTTOPRIGHT = 14;

    public const int HTBOTTOM = 15;

    public const int HTBOTTOMLEFT = 16;

    public const int HTBOTTOMRIGHT = 17;

    

    public const int CBN_DROPDOWN = 7;

    

    // Window style information
    public const int GWL_HINSTANCE = -6;
    public const int GWL_ID = -12;
    public const int GWL_STYLE = -16;
    public const int GWL_EXSTYLE = -20;

    public const int WS_MINIMIZE = 0x20000000;
    public const int WS_MAXIMIZE = 0x01000000;
    public const int WS_THICKFRAME = 0x00040000;
    public const int WS_SYSMENU = 0x00080000;
    public const int WS_BORDER = 0x00800000;
    public const int WS_DLGFRAME = 0x00400000;
    public const int WS_CAPTION = 0x00C00000;
    public const int WS_MINIMIZEBOX = 0x00020000;
    public const int WS_MAXIMIZEBOX = 0x00010000;
    public const int WS_DISABLED = 0x08000000;
    public const int WS_CHILD = 0x40000000;
    public const int WS_POPUP = unchecked((int)0x80000000);

    public const int WS_EX_DLGMODALFRAME = 0x00000001;
    public const int WS_EX_TOPMOST = 0x00000008;
    public const int WS_EX_TRANSPARENT = 0x00000020;
    public const int WS_EX_MDICHILD = 0x00000040;
    public const int WS_EX_TOOLWINDOW = 0x00000080;
    public const int WS_EX_APPWINDOW = 0x00040000;
    public const int WS_EX_LAYERED = 0x00080000;
    public const int WS_EX_NOACTIVATE = 0x08000000;


    public enum Bool
    {
        False,
        True
    }

    public struct Point
    {
        public readonly int x;

        public readonly int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Size
    {
        public readonly int cx;

        public readonly int cy;

        public Size(int cx, int cy)
        {
            this.cx = cx;
            this.cy = cy;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ARGB
    {
        public readonly byte Blue;

        public readonly byte Green;

        public readonly byte Red;

        public readonly byte Alpha;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BLENDFUNCTION
    {
        public byte BlendOp;

        public byte BlendFlags;

        public byte SourceConstantAlpha;

        public byte AlphaFormat;
    }

    public const int AC_SRC_OVER = 0x00000000;
    public const int ULW_COLORKEY = 0x00000001;
    public const int ULW_ALPHA = 0x00000002;
    public const int ULW_OPAQUE = 0x00000004;

    

    public const byte AC_SRC_ALPHA = 1;

    // Button Command
    internal const uint BCM_SETSHIELD = 0x160Cu;

    // GetDeviceCaps()
    internal const int LOGPIXELSX = 88;
    internal const int LOGPIXELSY = 90;

    // HIEF
    public enum heif_filetype_result
    {
        heif_filetype_no,
        heif_filetype_yes_supported,   // it is heif and can be read by libheif
        heif_filetype_yes_unsupported, // it is heif, but cannot be read by libheif
        heif_filetype_maybe // not sure whether it is an heif, try detection with more input data
    };

    // ComboBox
    // CB
    public const uint CB_SETDROPPEDWIDTH = 352U;

    public struct RECT
    {
        public int left;

        public int top;

        public int right;

        public int bottom;
    }

    public enum ComboBoxButtonState
    {
        STATE_SYSTEM_NONE = 0,
        STATE_SYSTEM_INVISIBLE = 0x8000,
        STATE_SYSTEM_PRESSED = 8
    }

    public struct COMBOBOXINFO
    {
        public int cbSize;
        public RECT rcItem;
        public RECT rcButton;
        public ComboBoxButtonState buttonState;
        public IntPtr hwndCombo;
        public IntPtr hwndEdit;
        public IntPtr hwndList;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PAINTSTRUCT
    {
        public IntPtr hdc;
        public bool fErase;
        public RECT rcPaint;
        public bool fRestore;
        public bool fIncUpdate;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] rgbReserved;
    }

    // ECM
    public const int ECM_FIRST = 5376;

    // EM
    public const int EM_SETCUEBANNER = 5377;

    public const int EM_GETCUEBANNER = 5378;

    #region WM - Window Message? Windows Message?
    public const int WM_CTLCOLORSTATIC = 0x0138; //312U;

    public const int WM_PAINT = 15;
    #endregion

    #region Thread
    [Flags]
    internal enum EXECUTION_STATE : uint
    {
        ES_AWAYMODE_REQUIRED = 0x40u,
        ES_CONTINUOUS = 0x80000000u,
        ES_DISPLAY_REQUIRED = 0x2u,
        ES_SYSTEM_REQUIRED = 0x1u
    }
    #endregion

    #region GCD
    internal const uint GCT_INVALID = 0U;

    internal const uint GCT_LFNCHAR = 1U;

    internal const uint GCT_SHORTCHAR = 2U;

    internal const uint GCT_WILD = 4U;

    internal const uint GCT_SEPARATOR = 8U;
    #endregion

    // cYo.Common.Presentation.Ceco.TextRun
    //#region GDI
    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    //public class XFORM
    //{
    //    public float eM11;

    //    public float eM12;

    //    public float eM21;

    //    public float eM22;

    //    public float eDx;

    //    public float eDy;

    //    private XFORM()
    //    {
    //    }

    //    public XFORM(float[] elements)
    //    {
    //        eM11 = elements[0];
    //        eM12 = elements[1];
    //        eM21 = elements[2];
    //        eM22 = elements[3];
    //        eDx = elements[4];
    //        eDy = elements[5];
    //    }
    //}

    //public const int GM_ADVANCED = 2;
    //#endregion

    #region Mouse
    internal const uint WM_MOUSEFIRST = 512U;

    /// <summary>Posted to a window when the cursor moves.
    /// If the mouse is not captured, the message is posted to the window that contains the cursor. Otherwise, the message is posted to the window that has captured the mouse.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mousemove#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_MOUSEMOVE = 512U;

    /// <summary>Posted when the user presses the left mouse button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-lbuttondown#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_LBUTTONDOWN = 513U;

    /// <summary>Posted when the user releases the left mouse button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-lbuttonup#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_LBUTTONUP = 514U;

    /// <summary>Posted when the user double-clicks the left mouse button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-lbuttondblclk#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_LBUTTONDBLCLK = 515U;

    /// <summary>Posted when the user presses the right mouse button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-rbuttondown#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_RBUTTONDOWN = 516U;

    /// <summary>Posted when the user releases the right mouse button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-rbuttonup#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_RBUTTONUP = 517U;

    /// <summary>Posted when the user double-clicks the right mouse button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-rbuttondblclk#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_RBUTTONDBLCLK = 518U;

    /// <summary>Posted when the user presses the middle mouse button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mbuttondown#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_MBUTTONDOWN = 519U;

    /// <summary>Posted when the user releases the middle mouse button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mbuttonup#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_MBUTTONUP = 520U;

    /// <summary>Posted when the user double-clicks the middle mouse button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mbuttondblclk#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_MBUTTONDBLCLK = 521U;

    /// <summary>Sent to the focus window when the mouse wheel is rotated.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mousewheel#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_MOUSEWHEEL = 522U;

    /// <summary>Posted when the user presses the first or second X button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return **TRUE**. For more information about processing the return value, see the Remarks section.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-xbuttondown#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_XBUTTONDOWN = 523U;

    /// <summary>Posted when the user releases the first or second X button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return **TRUE**. For more information about processing the return value, see the Remarks section.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-xbuttonup#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_XBUTTONUP = 524U;

    /// <summary>Posted when the user double-clicks the first or second X button while the cursor is in the client area of a window.</summary>
    /// <returns>If an application processes this message, it should return **TRUE**. For more information about processing the return value, see the Remarks section.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-xbuttondblclk#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_XBUTTONDBLCLK = 525U;

    /// <summary>Sent to the active window when the mouse's horizontal scroll wheel is tilted or rotated.</summary>
    /// <returns>If an application processes this message, it should return zero.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/inputdev/wm-mousehwheel#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint WM_MOUSEHWHEEL = 526U;

    internal const uint WM_MOUSELAST = 526U;
    #endregion

    #region TreeView
    /// <summary>Inserts a new item in a tree-view control. You can send this message explicitly or by using the TreeView\_InsertItem macro.</summary>
    /// <returns>Returns the **HTREEITEM** handle to the new item if successful, or **NULL** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-insertitem">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_INSERTITEMA = 4352U;

    /// <summary>Inserts a new item in a tree-view control. You can send this message explicitly or by using the TreeView\_InsertItem macro.</summary>
    /// <returns>Returns the **HTREEITEM** handle to the new item if successful, or **NULL** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-insertitem">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_INSERTITEMW = 4402U;

    /// <summary>Inserts a new item in a tree-view control. You can send this message explicitly or by using the TreeView\_InsertItem macro.</summary>
    /// <returns>Returns the **HTREEITEM** handle to the new item if successful, or **NULL** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-insertitem">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_INSERTITEM = 4402U;

    /// <summary>Removes an item and all its children from a tree-view control. You can send this message explicitly or by using the TreeView\_DeleteItem macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-deleteitem#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_DELETEITEM = 4353U;

    /// <summary>The TVM\_EXPAND message expands or collapses the list of child items associated with the specified parent item, if any.
    /// You can send this message explicitly or by using the TreeView\_Expand macro.</summary>
    /// <returns>Returns nonzero if the operation was successful, or zero otherwise.</returns>
    /// <remarks>
    //// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-expand#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_EXPAND = 4354U;

    /// <summary>Retrieves the bounding rectangle for a tree-view item and indicates whether the item is visible.
    /// You can send this message explicitly or by using the TreeView\_GetItemRect macro.</summary>
    /// <returns>If the item is visible and the bounding rectangle was successfully retrieved, the return value is **TRUE**.
    /// Otherwise, the message returns **FALSE** and does not retrieve the bounding rectangle.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getitemrect#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETITEMRECT = 4356U;

    /// <summary>Retrieves a count of the items in a tree-view control. You can send this message explicitly or by using the TreeView\_GetCount macro.</summary>
    /// <returns>Returns the count of items.</returns>
    internal const uint TVM_GETCOUNT = 4357U;

    /// <summary>Retrieves the amount, in pixels, that child items are indented relative to their parent items. You can send this message explicitly or by using the TreeView\_GetIndent macro.</summary>
    /// <returns>Returns the amount of indentation.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getindent">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETINDENT = 4358U;

    /// <summary>Sets the width of indentation for a tree-view control and redraws the control to reflect the new width. You can send this message explicitly or by using the TreeView\_SetIndent macro.</summary>
    /// <returns>No return value.</returns>
    internal const uint TVM_SETINDENT = 4359U;

    /// <summary>Retrieves the handle to the normal or state image list associated with a tree-view control. You can send this message explicitly or by using the TreeView\_GetImageList macro.</summary>
    /// <returns>Returns an HIMAGELIST handle to the specified image list.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getimagelist">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETIMAGELIST = 4360U;

    /// <summary>Sets the normal or state image list for a tree-view control and redraws the control using the new images. You can send this message explicitly or by using the TreeView\_SetImageList macro.</summary>
    /// <returns>Returns the handle to the previous image list, if any, or **NULL** otherwise.</returns>
    internal const uint TVM_SETIMAGELIST = 4361U;

    /// <summary>Retrieves the tree-view item that bears the specified relationship to a specified item. You can send this message explicitly, by using the TreeView\_GetNextItem macro.</summary>
    /// <returns>Returns the handle to the item if successful. For most cases, the message returns a **NULL** value to indicate an error. See the Remarks section for details.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getnextitem#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETNEXTITEM = 4362U;

    /// <summary>Selects the specified tree-view item, scrolls the item into view, or redraws the item in the style used to indicate the target of a drag-and-drop operation.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-selectitem#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_SELECTITEM = 4363U;

    /// <summary>Retrieves some or all of a tree-view item's attributes. You can send this message explicitly or by using the TreeView\_GetItem macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getitem#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETITEMA = 4364U;

    /// <summary>Retrieves some or all of a tree-view item's attributes. You can send this message explicitly or by using the TreeView\_GetItem macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getitem#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETITEMW = 4414U;

    /// <summary>Retrieves some or all of a tree-view item's attributes. You can send this message explicitly or by using the TreeView\_GetItem macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getitem#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETITEM = 4414U;

    /// <summary>The TVM\_SETITEM message sets some or all of a tree-view item's attributes. You can send this message explicitly or by using the TreeView\_SetItem macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    internal const uint TVM_SETITEMA = 4365U;

    /// <summary>The TVM\_SETITEM message sets some or all of a tree-view item's attributes. You can send this message explicitly or by using the TreeView\_SetItem macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    internal const uint TVM_SETITEMW = 4415U;

    /// <summary>The TVM\_SETITEM message sets some or all of a tree-view item's attributes. You can send this message explicitly or by using the TreeView\_SetItem macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    internal const uint TVM_SETITEM = 4415U;

    /// <summary>Begins in-place editing of the specified item's text, replacing the text of the item with a single-line edit control containing the text.</summary>
    /// <returns>Returns the handle to the edit control used to edit the item text if successful, or **NULL** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-editlabel#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_EDITLABELA = 4366U;

    /// <summary>Begins in-place editing of the specified item's text, replacing the text of the item with a single-line edit control containing the text.</summary>
    /// <returns>Returns the handle to the edit control used to edit the item text if successful, or **NULL** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-editlabel#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_EDITLABELW = 4417U;

    /// <summary>Begins in-place editing of the specified item's text, replacing the text of the item with a single-line edit control containing the text.</summary>
    /// <returns>Returns the handle to the edit control used to edit the item text if successful, or **NULL** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-editlabel#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_EDITLABEL = 4417U;

    /// <summary>Retrieves the handle to the edit control being used to edit a tree-view item's text. You can send this message explicitly or by using the TreeView\_GetEditControl macro.</summary>
    /// <returns>Returns the handle to the edit control if successful, or **NULL** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-geteditcontrol#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETEDITCONTROL = 4367U;

    /// <summary>Obtains the number of items that can be fully visible in the client window of a tree-view control. You can send this message explicitly or by using the TreeView\_GetVisibleCount macro.</summary>
    /// <returns>Returns the number of items that can be fully visible in the client window of the tree-view control.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getvisiblecount#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETVISIBLECOUNT = 4368U;

    /// <summary>Determines the location of the specified point relative to the client area of a tree-view control. You can send this message explicitly or by using the TreeView\_HitTest macro.</summary>
    /// <returns>Returns the handle to the tree-view item that occupies the specified point, or **NULL** if no item occupies the point.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-hittest">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_HITTEST = 4369U;

    /// <summary>Creates a dragging bitmap for the specified item in a tree-view control.</summary>
    /// <returns>Returns the handle to the image list to which the dragging bitmap was added if successful, or **NULL** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-createdragimage#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_CREATEDRAGIMAGE = 4370U;

    /// <summary>Sorts the child items of the specified parent item in a tree-view control. You can send this message explicitly or by using the TreeView\_SortChildren macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    internal const uint TVM_SORTCHILDREN = 4371U;

    /// <summary>Ensures that a tree-view item is visible, expanding the parent item or scrolling the tree-view control, if necessary.
    /// You can send this message explicitly or by using the TreeView\_EnsureVisible macro.</summary>
    /// <returns>Returns nonzero if the system scrolled the items in the tree-view control and no items were expanded. Otherwise, the message returns zero.</returns>
    internal const uint TVM_ENSUREVISIBLE = 4372U;

    /// <summary>Sorts tree-view items using an application-defined callback function that compares the items. You can send this message explicitly or by using the TreeView\_SortChildrenCB macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-sortchildrencb">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_SORTCHILDRENCB = 4373U;

    /// <summary>Ends the editing of a tree-view item's label. You can send this message explicitly or by using the TreeView\_EndEditLabelNow macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    internal const uint TVM_ENDEDITLABELNOW = 4374U;

    /// <summary>Retrieves the incremental search string for a tree-view control.</summary>
    /// <returns>Returns the number of characters in the incremental search string.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getisearchstring#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETISEARCHSTRINGA = 4375U;

    /// <summary>Retrieves the incremental search string for a tree-view control.</summary>
    /// <returns>Returns the number of characters in the incremental search string.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getisearchstring#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETISEARCHSTRINGW = 4416U;

    /// <summary>Retrieves the incremental search string for a tree-view control.</summary>
    /// <returns>Returns the number of characters in the incremental search string.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getisearchstring#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETISEARCHSTRING = 4416U;

    /// <summary>Sets a tree-view control's child tooltip control. You can send this message explicitly or by using the TreeView\_SetToolTips macro.</summary>
    /// <returns>Returns the handle to tooltip control previously set for the tree-view control, or **NULL** if tooltips were not previously used.</returns>
    internal const uint TVM_SETTOOLTIPS = 4376U;

    /// <summary>Retrieves the handle to the child tooltip control used by a tree-view control. You can send this message explicitly or by using the TreeView\_GetToolTips macro.</summary>
    /// <returns>Returns the handle to the child tooltip control, or **NULL** if the control is not using tooltips.</returns>
    internal const uint TVM_GETTOOLTIPS = 4377U;

    /// <summary>Sets the insertion mark in a tree-view control. You can send this message explicitly or by using the TreeView\_SetInsertMark macro.</summary>
    /// <returns>Returns nonzero if successful, or zero otherwise.</returns>
    internal const uint TVM_SETINSERTMARK = 4378U;

    /// <summary>TVM_SETUNICODEFORMAT message - Sets the Unicode character format flag for the control.</summary>
    /// <returns>Returns the previous Unicode format flag for the control.</returns>
    internal const uint TVM_SETUNICODEFORMAT = 8197U;

    /// <summary>Retrieves the Unicode character format flag for the control. You can send this message explicitly or use the TreeView\_GetUnicodeFormat macro.</summary>
    /// <returns>Returns the Unicode format flag for the control. If this value is nonzero, the control is using Unicode characters. If this value is zero, the control is using ANSI characters.</returns>
    internal const uint TVM_GETUNICODEFORMAT = 8198U;

    /// <summary>Sets the height of the tree-view items. You can send this message explicitly or by using the TreeView\_SetItemHeight macro.</summary>
    /// <returns>Returns the previous height of the items, in pixels.</returns>
    internal const uint TVM_SETITEMHEIGHT = 4379U;

    /// <summary>Retrieves the current height of the each tree-view item. You can send this message explicitly or by using the TreeView\_GetItemHeight macro.</summary>
    /// <returns>Returns the height of each item, in pixels.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getitemheight">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETITEMHEIGHT = 4380U;

    /// <summary>Sets the background color of the control. You can send this message explicitly or by using the TreeView\_SetBkColor macro.</summary>
    /// <returns>Returns a [**COLORREF**](/windows/desktop/gdi/colorref) value that represents the previous background color.
    /// If this value is -1, the control was using the system color for the background color.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-setbkcolor">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_SETBKCOLOR = 4381U;

    /// <summary>Sets the text color of the control. You can send this message explicitly or by using the TreeView\_SetTextColor macro.</summary>
    /// <returns>Returns a **COLORREF** value that represents the previous text color. If this value is -1, the control was using the system color for the text color.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-settextcolor">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_SETTEXTCOLOR = 4382U;

    /// <summary>Retrieves the current background color of the control. You can send this message explicitly or by using the TreeView\_GetBkColor macro.</summary>
    /// <returns>Returns a [**COLORREF**](/windows/desktop/gdi/colorref) value that represents the current background color.
    /// If this value is -1, the control is using the system color for the background color.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getbkcolor">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETBKCOLOR = 4383U;

    /// <summary>Retrieves the current text color of the control. You can send this message explicitly or by using the TreeView\_GetTextColor macro.</summary>
    /// <returns>Returns a [**COLORREF**](/windows/desktop/gdi/colorref) value that represents the current text color. If this value is -1, the control is using the system color for the text color.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-gettextcolor">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETTEXTCOLOR = 4384U;

    /// <summary>Sets the maximum scroll time for the tree-view control. You can send this message explicitly or by using the TreeView\_SetScrollTime macro.</summary>
    /// <returns>Returns the previous maximum scroll time, in milliseconds.</returns>
    internal const uint TVM_SETSCROLLTIME = 4385U;

    /// <summary>Retrieves the maximum scroll time for the tree-view control. You can send this message explicitly or by using the TreeView\_GetScrollTime macro.</summary>
    /// <returns>Returns the maximum scroll time, in milliseconds.</returns>
    internal const uint TVM_GETSCROLLTIME = 4386U;

    /// <summary>Sets the color used to draw the insertion mark for the tree view. You can send this message explicitly or by using the TreeView\_SetInsertMarkColor macro.</summary>
    /// <returns>Returns a **COLORREF** value that contains the previous insertion mark color.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-setinsertmarkcolor">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_SETINSERTMARKCOLOR = 4389U;

    /// <summary>Retrieves the color used to draw the insertion mark for the tree view. You can send this message explicitly or by using the TreeView\_GetInsertMarkColor macro.</summary>
    /// <returns>Returns a [**COLORREF**](/windows/desktop/gdi/colorref) value that contains the current insertion mark color.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getinsertmarkcolor">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETINSERTMARKCOLOR = 4390U;

    /// <summary>Sets the size of the border for the items in a tree-view control. You can send the message explicitly or by using the TreeView\_SetBorder macro.</summary>
    /// <returns>Returns a **LONG** value that contains the previous border size, in pixels.
    /// The [**LOWORD**](/previous-versions/windows/desktop/legacy/ms632659(v=vs.85)) contains the previous size of the horizontal border,
    /// and the [**HIWORD**](/previous-versions/windows/desktop/legacy/ms632657(v=vs.85)) contains the previous size of the vertical border.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-setborder#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_SETBORDER = 4387U;

    /// <summary>Retrieves some or all of a tree-view item's state attributes. You can send this message explicitly or by using the TreeView\_GetItemState macro.</summary>
    /// <returns>Returns a **UINT** value with the appropriate state bits set to **TRUE**. Only those bits that are specified by *lParam* and that are **TRUE** will be set.
    /// This value is equivalent to the **state** member of [**TVITEMEX**](/windows/win32/api/commctrl/ns-commctrl-tvitemexa).</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getitemstate">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETITEMSTATE = 4391U;

    /// <summary>The TVM\_SETLINECOLOR message sets the current line color.</summary>
    /// <returns>Returns the previous line color.</returns>
    internal const uint TVM_SETLINECOLOR = 4392U;

    /// <summary>The TVM\_GETLINECOLOR message gets the current line color.</summary>
    /// <returns>Returns the current line color, or the CLR\_DEFAULT value if none has been specified.</returns>
    internal const uint TVM_GETLINECOLOR = 4393U;

    /// <summary>Maps an accessibility ID to an HTREEITEM.</summary>
    /// <returns>Returns the **HTREEITEM** that the specified accessibility ID is mapped to.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-mapaccidtohtreeitem#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_MAPACCIDTOHTREEITEM = 4394U;

    /// <summary>Maps an HTREEITEM to an accessibility ID.</summary>
    /// <returns>Returns an accessibility ID.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-maphtreeitemtoaccid#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_MAPHTREEITEMTOACCID = 4395U;

    /// <summary>Informs the tree-view control to set extended styles. Send this message or use the macro TreeView\_SetExtendedStyle.</summary>
    /// <returns>If this message succeeds, it returns **S\_OK**. Otherwise, it returns an **HRESULT** error code.</returns>
    internal const uint TVM_SETEXTENDEDSTYLE = 4396U;

    /// <summary>Retrieves the extended style for a tree-view control. Send this message explicitly or by using the TreeView\_GetExtendedStyle macro.</summary>
    /// <returns>Returns the value of extended style.For more information on styles, see [Tree-View Control Extended Styles](tree-view-control-window-extended-styles.md).</returns>
    internal const uint TVM_GETEXTENDEDSTYLE = 4397U;

    /// <summary>Sets information used to determine auto-scroll characteristics. You can send this message explicitly or by using the TreeView\_SetAutoScrollInfo macro.</summary>
    /// <returns>Returns **TRUE**.</returns>
    internal const uint TVM_SETAUTOSCROLLINFO = 4411U;

    /// <summary>Sets the hot item for a tree-view control. You can send this message explicitly or by using the TreeView\_SetHot macro.</summary>
    /// <returns>Returns **TRUE** if successful, or **FALSE** otherwise.</returns>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-sethot#">Read more on docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_SETHOT = 4410U;

    /// <summary>TVM_GETSELECTEDCOUNT message - Not implemented.</summary>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/Controls/tvm-getselectedcount">Learn more about this API from docs.microsoft.com</see>.</para>
    /// </remarks>
    internal const uint TVM_GETSELECTEDCOUNT = 4422U;

    /// <summary>Shows the infotip for a specified item in a tree-view control. You can send this message explicitly or by using the TreeView\_ShowInfoTip macro..</summary>
    /// <returns>Returns zero.</returns>
    internal const uint TVM_SHOWINFOTIP = 4423U;

    internal const uint TVS_HASBUTTONS = 1U;

    internal const uint TVS_HASLINES = 2U;

    internal const uint TVS_LINESATROOT = 4U;

    internal const uint TVS_EDITLABELS = 8U;

    internal const uint TVS_DISABLEDRAGDROP = 16U;

    internal const uint TVS_SHOWSELALWAYS = 32U;

    internal const uint TVS_RTLREADING = 64U;

    internal const uint TVS_NOTOOLTIPS = 128U;

    internal const uint TVS_CHECKBOXES = 256U;

    internal const uint TVS_TRACKSELECT = 512U;

    internal const uint TVS_SINGLEEXPAND = 1024U;

    internal const uint TVS_INFOTIP = 2048U;

    internal const uint TVS_FULLROWSELECT = 4096U;

    internal const uint TVS_NOSCROLL = 8192U;

    internal const uint TVS_NONEVENHEIGHT = 16384U;

    internal const uint TVS_NOHSCROLL = 32768U;

    internal const uint TVS_EX_NOSINGLECOLLAPSE = 1U;

    internal const uint TVS_EX_MULTISELECT = 2U;

    internal const uint TVS_EX_DOUBLEBUFFER = 4U;

    internal const uint TVS_EX_NOINDENTSTATE = 8U;

    internal const uint TVS_EX_RICHTOOLTIP = 16U;

    internal const uint TVS_EX_AUTOHSCROLL = 32U;

    internal const uint TVS_EX_FADEINOUTEXPANDOS = 64U;

    internal const uint TVS_EX_PARTIALCHECKBOXES = 128U;

    internal const uint TVS_EX_EXCLUSIONCHECKBOXES = 256U;

    internal const uint TVS_EX_DIMMEDCHECKBOXES = 512U;

    internal const uint TVS_EX_DRAWIMAGEASYNC = 1024U;
    #endregion
}
