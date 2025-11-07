using System;
using System.Runtime.InteropServices;
using static cYo.Common.Win32.PInvoke;
using static cYo.Common.Win32.PInvokeCore;

namespace cYo.Common.Win32;

/// <summary>
/// Wrappers around <see cref="PInvoke"/> functions.
/// </summary>
/// <remarks>
/// - Does not depend on <see cref="System.Drawing"/> or <see cref="System.Windows.Forms"/><br/>
/// - Contains unsafe code.
/// </remarks>
internal static class Win32Internal
{

    public static int GetDeviceCapsX()
        => GetDeviceCaps(GetDC(IntPtr.Zero), LOGPIXELSX);

    public static int GetDeviceCapsY()
        => GetDeviceCaps(GetDC(IntPtr.Zero), LOGPIXELSY);

    #region Control
    // ComboBox
    private static class ComboBox
    {
        private static COMBOBOXINFO GetChildHandle(IntPtr href)
        {
            COMBOBOXINFO pcbi = default;
            pcbi.cbSize = Marshal.SizeOf((object)pcbi);
            GetComboBoxInfo(href, ref pcbi);
            return pcbi;
        }

        internal static IntPtr GetTextHandle(IntPtr href)
        {
            COMBOBOXINFO pcbi = GetChildHandle(href);
            return pcbi.hwndEdit;
        }

        internal static IntPtr GetListHandle(IntPtr href)
        {
            COMBOBOXINFO pcbi = GetChildHandle(href);
            return pcbi.hwndList;
        }
    }
    
    internal static void SetPromptText(HandleRef href, string text)
    {
        IntPtr textHandle = ComboBox.GetTextHandle(href.Handle);
        if (textHandle != IntPtr.Zero)
            SendMessage(textHandle, EM_SETCUEBANNER, IntPtr.Zero, text);
    }

    public static void SetComboBoxWidth(HandleRef href, int width)
    {
        SendMessage(href.Handle, CB_SETDROPPEDWIDTH, width, 0);
    }

    internal static IntPtr GetDisabledBrush(int color)
    {
        return CreateSolidBrush(color);
    }
    internal static void SetDisabledComboBox(IntPtr hdc, int backColor, int textColor)
    {
        SetBkColor(hdc, backColor);
        SetTextColor(hdc, textColor);
    }

    internal static void SetTreeViewBackColor(HandleRef href, int backColor)
    {
        SendMessage(href.Handle, (int)TVM_SETBKCOLOR, 0, backColor);
    }

    internal static void SetTreeViewForeColor(HandleRef href, int textColor)
    {
        SendMessage(href.Handle, (int)TVM_SETTEXTCOLOR, 0, textColor);
    }
    public static void ScrollLine(IntPtr hWnd, bool downwards)
    {
        SendMessage(hWnd, WM_VSCROLL, downwards ? 1 : 0, 0);
    }

    public enum TVS_EX
    {
        TVS_EX_DOUBLEBUFFER = (int)PInvokeCore.TVS_EX_DOUBLEBUFFER
    }

    public static void SetTreeViewDoubleBuffer(HandleRef href)
    {
        TVS_EX tVS_EX = (TVS_EX)SendMessage(href.Handle, (int)TVM_GETEXTENDEDSTYLE, 0, 0);
        tVS_EX |= TVS_EX.TVS_EX_DOUBLEBUFFER;
        SendMessage(href.Handle, (int)TVM_SETEXTENDEDSTYLE, 0, (int)tVS_EX);
    }

    internal static bool ShouldSetDisabled(HandleRef href, IntPtr hdc, IntPtr hwnd, bool isSimpleDisabled)
    {
        IntPtr textHandle = ComboBox.GetTextHandle(href.Handle);
        IntPtr listHandle = ComboBox.GetListHandle(href.Handle);

        if (hwnd == textHandle || isSimpleDisabled && hwnd == listHandle)
            return true;
        return false;
    }

    public static void SetCueText(HandleRef href, string text)
    {
        SendMessage(href.Handle, EM_SETCUEBANNER, IntPtr.Zero, text);
    }
    #endregion

    internal static bool ShowShield(HandleRef href)
    {
        return SendMessage(href, BCM_SETSHIELD, new IntPtr(0), new IntPtr(1)) == 0;
    }

    internal static bool IsValidPathChar(char c)
    {
        uint charType = PathGetCharType(c);
        if (charType == GCT_INVALID || (charType & 0xCu) != 0)
            return false;
        return true;
    }

    public static void KeepThreadAlive(bool withDisplay = false)
    {
        EXECUTION_STATE execState = EXECUTION_STATE.ES_SYSTEM_REQUIRED;
        if (withDisplay)
            execState |= EXECUTION_STATE.ES_DISPLAY_REQUIRED;
        SetThreadExecutionState(execState);
    }

    public static IntPtr GetDesktopIconPointer()
    {
        IntPtr[] phiconLarge = new IntPtr[1];
        IntPtr[] array = new IntPtr[1];
        ExtractIconEx(Environment.SystemDirectory + "\\shell32.dll", 34, phiconLarge, array, 1u);
        return array[0];
    }

    internal static bool IsHeifSupportedNative(byte[] data)
    {
        GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
        IntPtr dataPtr = handle.AddrOfPinnedObject();

        try
        {
            heif_filetype_result result = heif_check_filetype(dataPtr, data.Length);

            if (result == heif_filetype_result.heif_filetype_yes_supported)
                return true;

            return false;
        }
        finally
        {
            handle.Free();
        }
    }

    public static void SelectBitmap(HandleRef hForm, IntPtr hBitmap, int alpha, int formLeft, int formTop, int bitmapWidth, int bitmapHeight)
    {
        IntPtr dC = GetDC(IntPtr.Zero);
        IntPtr intPtr = CreateCompatibleDC(dC);
        IntPtr hObject = IntPtr.Zero;

        try
        {
            hObject = SelectObject(intPtr, hBitmap);
            Size psize = new Size(bitmapWidth, bitmapHeight);
            Point pprSrc = new Point(0, 0);
            Point pptDst = new Point(formLeft, formTop);
            BLENDFUNCTION pblend = default(BLENDFUNCTION);
            pblend.BlendOp = 0;
            pblend.BlendFlags = 0;
            pblend.SourceConstantAlpha = (byte)alpha;
            pblend.AlphaFormat = 1;
            UpdateLayeredWindow(hForm.Handle, dC, ref pptDst, ref psize, intPtr, ref pprSrc, 0, ref pblend, 2);
        }
        finally
        {
            ReleaseDC(IntPtr.Zero, dC);
            if (hBitmap != IntPtr.Zero)
            {
                SelectObject(intPtr, hObject);
                DeleteObject(hBitmap);
            }
            DeleteDC(intPtr);
        }
    }
}
