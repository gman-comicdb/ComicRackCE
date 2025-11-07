using cYo.Common.Win32;
using cYo.Common.Windows.Forms.Theme;
using cYo.Common.Windows.Forms.Theme.Resources;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Win32Interop = cYo.Common.Win32.Win32;

namespace cYo.Common.Windows.Forms
{
    public class TreeViewEx : TreeView
    {
        private Timer scrollTimer;

        private int dragScrollRegion = 10;

        private int delta;

        [DefaultValue(10)]
        public int DragScrollRegion
        {
            get
            {
                return dragScrollRegion;
            }
            set
            {
                dragScrollRegion = value;
            }
        }

        public event ScrollEventHandler Scroll;

        protected override void Dispose(bool disposing)
        {
            if (disposing && scrollTimer != null)
            {
                scrollTimer.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Sets <see cref="TreeView"/> <typeparamref name="ForeColor"/> and <typeparamref name="BackColor"/> using P/invoke.
        /// </summary>
		/// <param name="treeView"><see cref="TreeView"/> control to set the colors of.</param>
		/// <param name="backColor"><typeparamref name="BackColor"/>  to set. If none is provided and Dark Mode is enabled, defaults to <see cref="ThemeColors.TreeView.Back"/> </param>
		/// <param name="foreColor"><typeparamref name="ForeColor"/> to set. If none is provided and Dark Mode is enabled, defaults to <see cref="ThemeColors.TreeView.Text"/> </param>
        /// <remarks>
        /// If <see cref="ThemeExtensions.IsDarkModeEnabled"/> is <paramref name="true"/> and no <see cref="Color"/> values are passed, will apply default <see cref="ThemeColors.TreeView"/> colors.
        /// </remarks>
        // REVIEW : Why is this required? TreeView base class ForeColor/BackColor is calling the native method anyway. Might be related to before/after handle creation
        public static void SetColor(TreeView treeView, Color? backColor = null, Color? foreColor = null)
        {
            Win32Interop.SetTreeViewBackColor(treeView, backColor ?? treeView.BackColor);
            Win32Interop.SetTreeViewForeColor(treeView, foreColor ?? treeView.ForeColor);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Win32Interop.EnableTreeViewDoubleBuffer(this);
            this.SetTreeViewColor();;
        }

        private void scrollTimer_Tick(object sender, EventArgs e)
        {
            Win32Interop.ScrollTreeViewLines(this, delta);
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            if (scrollTimer == null)
            {
                scrollTimer = new Timer
                {
                    Interval = 150
                };
                scrollTimer.Tick += scrollTimer_Tick;
            }
            Point point = PointToClient(new Point(e.X, e.Y));
            if (point.Y < dragScrollRegion)
            {
                delta = -1;
                scrollTimer.Enabled = true;
            }
            else if (point.Y > base.Height - dragScrollRegion)
            {
                delta = 1;
                scrollTimer.Enabled = true;
            }
            else
            {
                scrollTimer.Enabled = false;
            }
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
            if (scrollTimer != null)
            {
                scrollTimer.Enabled = false;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.IsVerticalScroll())
            {
                ScrollEventType type = (ScrollEventType)Enum.Parse(typeof(ScrollEventType), (m.WParam.ToInt32() & 0xFFFF).ToString());
                OnScroll(new ScrollEventArgs(type, (int)(m.WParam.ToInt64() >> 16) & 0xFF));
            }
        }

        protected virtual void OnScroll(ScrollEventArgs sea)
        {
            if (this.Scroll != null)
            {
                this.Scroll(this, sea);
            }
        }
    }
}
