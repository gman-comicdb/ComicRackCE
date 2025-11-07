using cYo.Common.ComponentModel;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static cYo.Common.Win32.FileOperations.IFileOperation;

namespace cYo.Common.Win32;

/// <summary>
/// Wrapper around <see cref="Win32Internal"/>
/// </summary>
/// <remarks>
/// - Depends on <see cref="System.Drawing"/> or <see cref="System.Windows.Forms"/>
/// - Does not contain unsafe code (except HandleRef and IntPtr types)
/// </remarks>
public static class Win32
{
    public static bool ShowShield(Button button)
    {
        if (!button.TryGetHandleRef(out HandleRef href))
            return false;

        button.FlatStyle = FlatStyle.System;
        return Win32Internal.ShowShield(href);
    }

    public static bool IsHeifSupportedNative(byte[] data)
    {
        if (data.Length < 12 || !Environment.Is64BitProcess)
            return false;

        return Win32Internal.IsHeifSupportedNative(data);
    }

    public class MouseWheelDelegater : IMessageFilter
    {
        private IntPtr lastHandle;

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case (int)PInvokeCore.WM_MOUSEMOVE:
                    lastHandle = m.HWnd;
                    break;
                case (int)PInvokeCore.WM_MOUSEWHEEL:
                case (int)PInvokeCore.WM_MOUSEHWHEEL:
                    if (!(m.HWnd == lastHandle))
                    {
                        PInvoke.SendMessage(lastHandle, m.Msg, m.WParam, m.LParam);
                        return true;
                    }
                    break;
            }
            return false;
        }
    }

    public static void SetComboBoxWidth(ComboBox comboBox, int width)
    {
        if (!comboBox.TryGetHandleRef(out HandleRef href))
            return;
        Win32Internal.SetComboBoxWidth(href, width);
    }

    public static void SetPromptText(ComboBox comboBox, string text)
    {
        if (!comboBox.TryGetHandleRef(out HandleRef href))
            return;
        Win32Internal.SetPromptText(href, text);
    }

    public static IntPtr DrawDisabledComboBox(ComboBox comboBox, IntPtr hdc, IntPtr hwnd)
    {
        bool isSimpleDisabled = comboBox.DropDownStyle == ComboBoxStyle.Simple && !comboBox.Enabled;
        
        if (!comboBox.TryGetHandleRef(out HandleRef href))
            return IntPtr.Zero;

        if (Win32Internal.ShouldSetDisabled(href, hdc, hwnd, isSimpleDisabled))
        {
            Win32Internal.SetDisabledComboBox(
                hdc,
                ColorTranslator.ToWin32(Color.FromArgb(64, 64, 64)),
                ColorTranslator.ToWin32(SystemColors.GrayText)
            );
            return darkEditBrush;
        }
        return IntPtr.Zero;
    }

    public static Icon GetDesktopIcon()
    {
        IntPtr pIcon = Win32Internal.GetDesktopIconPointer();
        return Icon.FromHandle(pIcon);
    }

    public static bool PathIsNetworkPath(string pszPath)
        => PInvoke.PathIsNetworkPath(pszPath);

    #region Message Extensions
    public static bool IsPaint(this Message m)
    {
        return (m.Msg == PInvokeCore.WM_PAINT);
    }

    public static bool IsColorStatic(this Message m)
    {
        return (m.Msg == PInvokeCore.WM_CTLCOLORSTATIC);
    }

    public static bool IsVerticalScroll(this Message m)
    {
        return (m.Msg == PInvokeCore.WM_VSCROLL);
    }
    #endregion

    #region Helpers
    //internal static HandleRef GetHandleRef(this Control control)
    //{
    //    return new HandleRef(control, control.Handle);
    //}

    private static readonly IntPtr darkEditBrush = Win32Internal.GetDisabledBrush(ColorTranslator.ToWin32(Color.FromArgb(64, 64, 64)));

    private static bool TryGetHandleRef(this Control control, out HandleRef href, bool doNotCreateHandle = false)
    {
        href = new HandleRef();

        if (control == null)
            return false; // throw new ArgumentNullException(nameof(control));

        if (!control.IsHandleCreated && doNotCreateHandle)
            return false; // throw new InvalidOperationException("The control's handle has not been created.");

        href = new HandleRef(control, control.Handle);
        return true;
    }

    //private static int AsInt(this uint uInt)
    //{
    //    return (int)uInt;
    //}
    #endregion

    public static void SelectBitmap(Form form, Bitmap bitmap, int alpha)
    {
        if (!form.TryGetHandleRef(out HandleRef href))
            return;

        IntPtr hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));

        Win32Internal.SelectBitmap(href, hBitmap, alpha, form.Left, form.Top, bitmap.Width, bitmap.Height);
    }

    public const int WS_EX_LAYERED = PInvokeCore.WS_EX_LAYERED;

    public const int WS_EX_NOACTIVATE = PInvokeCore.WS_EX_NOACTIVATE;

    public static void SetTreeViewBackColor(TreeView treeView, Color backColor)
    {
        if (!treeView.TryGetHandleRef(out HandleRef href))
            return;
        Win32Internal.SetTreeViewBackColor(href, ColorTranslator.ToWin32(backColor));
    }

    public static void SetTreeViewForeColor(TreeView treeView, Color textColor)
    {
        if (!treeView.TryGetHandleRef(out HandleRef href))
            return;
        Win32Internal.SetTreeViewForeColor(href, ColorTranslator.ToWin32(textColor));
    }

    public static void ScrollTreeViewLines(IWin32Window window, int lines)
    {
        int num = Math.Abs(lines);
        for (int i = 0; i < num; i++)
            Win32Internal.ScrollLine(window.Handle, lines >= 0);
    }

    public static void EnableTreeViewDoubleBuffer(TreeView treeView)
    {
        if (!treeView.TryGetHandleRef(out HandleRef href))
            return;
        Win32Internal.SetTreeViewDoubleBuffer(href);
    }

    public static void SetCueText(TextBox textBox, string text)
    {
        if (!textBox.TryGetHandleRef(out HandleRef href))
            return;
        Win32Internal.SetCueText(href, text);
    }
}

public static class Kernel32
{
    public static bool SetDllDirectory(string lpPathName)
        => PInvoke.SetDllDirectory(lpPathName);
}

public static class User32
{
    public static bool SetProcessDPIAware()
        => PInvoke.SetProcessDPIAware();

    public static bool IsProcessDPIAware()
        => PInvoke.IsProcessDPIAware();

    public static Control GetActiveWindowControl()
    {
        return Control.FromHandle(PInvoke.GetActiveWindow());
    }

    public static bool MessageHasExtraInfo()
    {
        return PInvoke.GetMessageExtraInfo() != IntPtr.Zero;
    }

    public static bool IsTouchMessage()
    {
        return (PInvoke.GetMessageExtraInfo().ToInt64() & 0xFFFFFF00u) == 4283520768u;
    }

    public static Size GetDpiSize()
    {
        return new Size(Win32Internal.GetDeviceCapsX(), Win32Internal.GetDeviceCapsY());
    }
}

public class PaintSuspend : DisposableObject
{
    private const int WM_USER = 1024;

    private const int WM_SETREDRAW = 11;

    private const int EM_GETEVENTMASK = 1083;

    private const int EM_SETEVENTMASK = 1093;

    private const int EM_GETSCROLLPOS = 1245;

    private const int EM_SETSCROLLPOS = 1246;

    private Point scrollPoint;

    private readonly IntPtr eventMask;

    private readonly int suspendIndex;

    private readonly int suspendLength;

    private RichTextBox rtb;

    [DllImport("user32.dll")]
    public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, ref Point lParam);

    public PaintSuspend(RichTextBox rtb)
    {
        this.rtb = rtb;
        suspendIndex = rtb.SelectionStart;
        suspendLength = rtb.SelectionLength;
        SendMessage(rtb.Handle, EM_GETSCROLLPOS, 0, ref scrollPoint);
        PInvoke.SendMessage(rtb.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
        eventMask = PInvoke.SendMessage(rtb.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            rtb.Select(suspendIndex, suspendLength);
            SendMessage(rtb.Handle, EM_SETSCROLLPOS, 0, ref scrollPoint);
            PInvoke.SendMessage(rtb.Handle, EM_SETEVENTMASK, 0, eventMask);
            PInvoke.SendMessage(rtb.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            rtb.Invalidate();
        }
        base.Dispose(disposing);
    }
}

public static class WebpImage
{
    [DllImport("Resources\\libwebp32.dll", EntryPoint = "WebPFree")]
    private static extern void WebPFree32(IntPtr toDeallocate);

    [DllImport("Resources\\libwebp64.dll", EntryPoint = "WebPFree")]
    private static extern void WebPFree64(IntPtr toDeallocate);

    [DllImport("Resources\\libwebp32.dll", EntryPoint = "WebPGetInfo")]
    private static extern int WebPGetInfo32([In] IntPtr data, UIntPtr dataSize, ref int width, ref int height);

    [DllImport("Resources\\libwebp64.dll", EntryPoint = "WebPGetInfo")]
    private static extern int WebPGetInfo64([In] IntPtr data, UIntPtr dataSize, ref int width, ref int height);

    [DllImport("Resources\\libwebp32.dll", EntryPoint = "WebPDecodeBGRAInto")]
    private static extern IntPtr WebPDecodeBGRAInto32([In] IntPtr data, UIntPtr dataSize, IntPtr outputBuffer, UIntPtr outputBufferSize, int outputStride);

    [DllImport("Resources\\libwebp64.dll", EntryPoint = "WebPDecodeBGRAInto")]
    private static extern IntPtr WebPDecodeBGRAInto64([In] IntPtr data, UIntPtr dataSize, IntPtr outputBuffer, UIntPtr outputBufferSize, int outputStride);

    [DllImport("Resources\\libwebp32.dll", EntryPoint = "WebPEncodeLosslessBGR")]
    private static extern UIntPtr WebPEncodeLosslessBGR32([In] IntPtr bgr, int width, int height, int stride, ref IntPtr output);

    [DllImport("Resources\\libwebp64.dll", EntryPoint = "WebPEncodeLosslessBGR")]
    private static extern UIntPtr WebPEncodeLosslessBGR64([In] IntPtr bgr, int width, int height, int stride, ref IntPtr output);

    [DllImport("Resources\\libwebp32.dll", EntryPoint = "WebPEncodeLosslessBGRA")]
    private static extern UIntPtr WebPEncodeLosslessBGRA32([In] IntPtr bgra, int width, int height, int stride, ref IntPtr output);

    [DllImport("Resources\\libwebp64.dll", EntryPoint = "WebPEncodeLosslessBGRA")]
    private static extern UIntPtr WebPEncodeLosslessBGRA64([In] IntPtr bgra, int width, int height, int stride, ref IntPtr output);

    [DllImport("Resources\\libwebp32.dll", EntryPoint = "WebPEncodeBGR")]
    private static extern UIntPtr WebPEncodeBGR32([In] IntPtr bgr, int width, int height, int stride, float qualityFactor, ref IntPtr output);

    [DllImport("Resources\\libwebp64.dll", EntryPoint = "WebPEncodeBGR")]
    private static extern UIntPtr WebPEncodeBGR64([In] IntPtr bgr, int width, int height, int stride, float qualityFactor, ref IntPtr output);

    [DllImport("Resources\\libwebp32.dll", EntryPoint = "WebPEncodeBGRA")]
    private static extern IntPtr WebPEncodeBGRA32([In] IntPtr bgra, int width, int height, int stride, float qualityFactor, ref IntPtr output);

    [DllImport("Resources\\libwebp64.dll", EntryPoint = "WebPEncodeBGRA")]
    private static extern IntPtr WebPEncodeBGRA64([In] IntPtr bgra, int width, int height, int stride, float qualityFactor, ref IntPtr output);

    public static int WebPGetInfo(IntPtr data, UIntPtr dataSize, ref int width, ref int height)
    {
        if (!Environment.Is64BitProcess)
        {
            return WebPGetInfo32(data, dataSize, ref width, ref height);
        }
        return WebPGetInfo64(data, dataSize, ref width, ref height);
    }

    public static void WebPFree(IntPtr toDeallocate)
    {
        if (Environment.Is64BitProcess)
        {
            WebPFree64(toDeallocate);
        }
        else
        {
            WebPFree32(toDeallocate);
        }
    }

    public static IntPtr WebPDecodeBGRAInto(IntPtr data, UIntPtr dataSize, IntPtr outputBuffer, UIntPtr outputBufferSize, int outputStride)
    {
        if (!Environment.Is64BitProcess)
        {
            return WebPDecodeBGRAInto32(data, dataSize, outputBuffer, outputBufferSize, outputStride);
        }
        return WebPDecodeBGRAInto64(data, dataSize, outputBuffer, outputBufferSize, outputStride);
    }

    public static UIntPtr WebPEncodeLosslessBGR(IntPtr bgr, int width, int height, int stride, ref IntPtr output)
    {
        if (!Environment.Is64BitProcess)
        {
            return WebPEncodeLosslessBGR32(bgr, width, height, stride, ref output);
        }
        return WebPEncodeLosslessBGR64(bgr, width, height, stride, ref output);
    }

    public static UIntPtr WebPEncodeLosslessBGRA(IntPtr bgra, int width, int height, int stride, ref IntPtr output)
    {
        if (!Environment.Is64BitProcess)
        {
            return WebPEncodeLosslessBGRA32(bgra, width, height, stride, ref output);
        }
        return WebPEncodeLosslessBGRA64(bgra, width, height, stride, ref output);
    }

    public static UIntPtr WebPEncodeBGR(IntPtr bgr, int width, int height, int stride, float qualityFactor, ref IntPtr output)
    {
        if (!Environment.Is64BitProcess)
        {
            return WebPEncodeBGR32(bgr, width, height, stride, qualityFactor, ref output);
        }
        return WebPEncodeBGR64(bgr, width, height, stride, qualityFactor, ref output);
    }

    public static IntPtr WebPEncodeBGRA(IntPtr bgra, int width, int height, int stride, float qualityFactor, ref IntPtr output)
    {
        if (!Environment.Is64BitProcess)
        {
            return WebPEncodeBGRA32(bgra, width, height, stride, qualityFactor, ref output);
        }
        return WebPEncodeBGRA64(bgra, width, height, stride, qualityFactor, ref output);
    }
}