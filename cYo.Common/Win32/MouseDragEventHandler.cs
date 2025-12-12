using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace cYo.Common.Win32;

// TODO : add setting so can be enabled/disabled
/// <summary>
/// Allows moving a Window by clicking & dragging a Control.
/// </summary>
public static class MouseDragEventHandler
{
    private static class Native
    {
        public const int GWL_STYLE = -16;

        public const int WS_BORDER = 0x00800000;
        public const int WS_CAPTION = 0x00C00000;
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_THICKFRAME = 0x00040000; // Allows resizing

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public static void MoveWindow(IntPtr hWnd)
        {
            // releases any existing mouse capture
            ReleaseCapture();

            // fire fake "mouse is clicking caption" (title bar) message
            // NCL = Non-Client Left (Mouse Button Down)
            SendMessage(hWnd, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        }
    }

    /// <summary> Minimum distance (in pixels) mouse must be moved while being held down to trigger drag.</summary>
    private const int DragThreshold = 4; // pixels

    /// <summary> Initial mouse position.</summary>
    private static Point mouseDownPosition;

    /// <summary> Track dragging state.</summary>
    private static bool isDragging = false;

    /// <summary>
    /// Enables dragging the Window (<see cref="Control.FindForm"/>) by left-clicking and holding down the mouse.
    /// </summary>
    /// <param name="sender">The sender <see cref="Control"/>.</param>
    /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
    public static void OnMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left || sender is not Control control)
            return;

        mouseDownPosition = e.Location;
        isDragging = false;
        control.AttachMouseDragEventHandler();
    }

    /// <summary>
    /// Checks if the mouse has moved more than <see cref="DragThreshold"/> whilst being held down
    /// and triggers Window movement if it has.
    /// </summary>
    private static void OnMouseMoveCheckDistance(object sender, MouseEventArgs e)
    {
        Control control = sender as Control;
        if (!isDragging)
        {
            int dx = Math.Abs(e.X - mouseDownPosition.X);
            int dy = Math.Abs(e.Y - mouseDownPosition.Y);

            // has the mouse moved more than the drag threshold whilst being held down?
            if (dx >= DragThreshold || dy >= DragThreshold)
            {
                isDragging = true;

                // unhook to prevent further processing
                control.DetachMouseDragEventHandler();
                
                // begin moving window
                Form topForm = control.FindForm();
                if (topForm != null && topForm.Handle != IntPtr.Zero)
                    Native.MoveWindow(topForm.Handle);
            }
        }
    }

    /// <summary>
    /// Stops detecting mouse drag. For when MouseUp event fires without moving more than <see cref="DragThreshold"/>.
    /// </summary>
    private static void OnMouseUpUnhook(object sender, MouseEventArgs e)
    {
        (sender as Control).DetachMouseDragEventHandler();
        isDragging = false;
    }

    private static void AttachMouseDragEventHandler(this Control control)
    {
        control.Capture = true; // capture mouse to track movement
        control.MouseMove += OnMouseMoveCheckDistance; // subscribe to MouseMove to measure distance
        control.MouseUp += OnMouseUpUnhook; // subscribe to MouseUp so we can unhook
    }

    private static void DetachMouseDragEventHandler(this Control control)
    {
        control.Capture = false;
        control.MouseMove -= OnMouseMoveCheckDistance;
        control.MouseUp -= OnMouseUpUnhook;
    }
}
