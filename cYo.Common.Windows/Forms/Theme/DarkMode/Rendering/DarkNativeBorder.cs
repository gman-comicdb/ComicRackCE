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
    /// Draw a Border around the specified <paramref name="control"/>.
    /// </summary>
    /// <remarks>
    /// For Native Win32 controls where drawing a border in <see cref="Control.Paint"/> or <see cref="DrawItemEventHandler"/> is not feasible.<br/>
    /// </remarks>
    /// <param name="control">Control to draw a Border around.</param>
    /// <param name="borderColor">Border <see cref="Color"/>. <see cref="DarkColors.Border.Default"/> if not specified.</param>
    public static void DrawDarkNativeBorder(this Control control, Color? borderColor = null)
        => new NativeBorder(control, borderColor ?? DarkColors.Border.Default);

    private class NativeBorder : NativeWindow, IDisposable
    {
        private readonly Control control;

        private readonly Color borderColor;

        private bool attached = false;

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
            if (attached) return;
            AssignHandle(control.Handle);
            attached = true;

            control.Invalidate();
        }

        private void Detach()
        {
            if (!attached) return;
            ReleaseHandle();
            attached = false;
        }

        public void Dispose()
        {
            Detach();
        }
        #endregion

        protected override void WndProc(ref Message m)
        {
            const int WM_NCPAINT = 0x0085;

            base.WndProc(ref m);

            if (m.Msg == WM_NCPAINT)
                DrawBorder(control.Handle, borderColor, control.Bounds);
        }
    }

    private static void DrawBorder(IntPtr hWnd, Color borderColor, Rectangle bounds = default)
    {
        if (hWnd == IntPtr.Zero)
            return;

        IntPtr hdc = Native.GetWindowDC(hWnd);
        if (hdc == IntPtr.Zero)
            return;

        try
        {
            // Create GDI+ drawing surface
            using Graphics g = Graphics.FromHdc(hdc);

            if (bounds == default)
            {
                Native.GetWindowRect(hWnd, out Native.RECT rect);
                bounds = rect.ToRectangle();
                bounds.Location = Point.Empty; // Screen (x,y) to local (0,0) coordinates
            }
            bounds.Width--; bounds.Height--;
            g.DrawRectangle(DarkPens.FromDarkColor(borderColor), 0, 0, bounds.Width, bounds.Height);
        }
        finally
        {
            Native.ReleaseDC(hWnd, hdc);
        }
    }
}
