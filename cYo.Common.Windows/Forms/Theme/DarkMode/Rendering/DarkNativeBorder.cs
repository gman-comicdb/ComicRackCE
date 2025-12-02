using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;

internal static class DarkNativeBorder
{
    #region Native interop & constants
    private static class Native
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public Rectangle ToRectangle() =>
                Rectangle.FromLTRB(Left, Top, Right, Bottom);
        }
    }
    #endregion

    /// <summary>
    /// Draw a border around the specified <paramref name="control"/> with the <see cref="DarkColors.Border.Default"/> border color.
    /// </summary>
    /// <remarks>
    /// For Native Win32 controls where drawing a border in <see cref="Control.Paint"/> or <see cref="DrawItemEventHandler"/> is not feasible.<br/>
    /// </remarks>
    /// <param name="control">The <see cref="Control"/> to draw a border around.</param>
    public static void DrawDarkNativeBorder(this Control control)
        => control.DrawDarkNativeBorder(DarkColors.Border.Default);

    /// <summary>
    /// Draw a border around the specified <paramref name="control"/> with the specified <paramref name="borderColor"/>.
    /// </summary>
    /// <remarks>
    /// For Native Win32 controls where drawing a border in <see cref="Control.Paint"/> or <see cref="DrawItemEventHandler"/> is not feasible.<br/>
    /// </remarks>
    /// <param name="control">The <see cref="Control"/> to draw a border around.</param>
    /// <param name="borderColor">The <see cref="Color"/> to use when drawing the border.</param>
    public static void DrawDarkNativeBorder(this Control control, Color borderColor)
        => new NativeBorder(control, borderColor);

    /// <summary>
    /// <see cref="NativeWindow"/> subclass that draws a border in WM_NCPAINT WndProc. (Non-Client Paint Window Procedure message loop).
    /// </summary>
    private class NativeBorder : NativeWindow, IDisposable
    {
        private readonly Control control;

        private readonly Color borderColor;

        private bool isAttached = false;

        // SOMEDAY : Either set BorderStyle to FixedSingle or modify ClientRectangle accordingly
        //           i.e reduce when BorderStyle is set to None (to leave room for border)
        //           and increase when BorderStyle is set to 3D (so we don't have a gap between border + ClientRectangle)
        public NativeBorder(Control control, Color borderColor)
        {
            this.control = control ?? throw new ArgumentNullException(nameof(control));
            this.borderColor = borderColor;

            if (control.IsHandleCreated)
                Attach();

            control.HandleCreated += (s, e) => Attach();
            control.HandleDestroyed += (s, e) => Detach();
        }

        #region Attach/Detach/Dispose
        private void Attach()
        {
            if (isAttached) return;
            AssignHandle(control.Handle);
            isAttached = true;

            control.Invalidate();
        }

        private void Detach()
        {
            if (!isAttached) return;
            ReleaseHandle();
            isAttached = false;
        }

        public void Dispose() => Detach();
        #endregion

        protected override void WndProc(ref Message m)
        {
            const int WM_NCPAINT = 0x0085;

            base.WndProc(ref m);

            if (m.Msg == WM_NCPAINT)
                DrawBorder(control.Handle, borderColor, control.Bounds);
        }
    }

    private static void DrawBorder(IntPtr hWnd, Color borderColor)
        => DrawBorder(hWnd, borderColor, GetWindowRectangle(hWnd));

    private static void DrawBorder(IntPtr hWnd, Color borderColor, Rectangle bounds)
    {
        if (hWnd == IntPtr.Zero || bounds == Rectangle.Empty)
            return;

        IntPtr hdc = Native.GetWindowDC(hWnd);
        if (hdc == IntPtr.Zero)
            return;

        // Convert screen to local coordinates [(X,Y) -> (0,0)]
        // idempotent - no change if coordinates are already local
        bounds.Location = Point.Empty;

        try
        {
            // Create GDI+ drawing surface + use to draw border
            using Graphics g = Graphics.FromHdc(hdc);
            DarkControlPaint.DrawBorder(g, bounds, borderColor);
        }
        finally
        {
            Native.ReleaseDC(hWnd, hdc);
        }
    }

    #region Helpers
    private static Rectangle GetWindowRectangle(IntPtr hWnd)
    {
        if (hWnd == IntPtr.Zero) return Rectangle.Empty;

        Native.GetWindowRect(hWnd, out Native.RECT rect);
        Rectangle bounds = rect.ToRectangle();
        
        return bounds;
    }
    #endregion
}
