using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms;

public class AutoHideManager : Component
{
    private readonly Timer timer = new();

    private bool enabled = true;

    private Control control;

    private Control autoHideControl;

    private Rectangle hotBounds;

    private AutoHideBounds autoBounds;

    private int autoWidth = 20;

    private TimeSpan showTime = TimeSpan.FromSeconds(2.0);

    private TimeSpan hideTime = TimeSpan.FromSeconds(2.0);

    private bool mouseInRegion;

    private bool inAutoHideControl;

    private DateTime startInRegion = DateTime.MinValue;

    private DateTime startOutRegion = DateTime.MinValue;

    [DefaultValue(true)]
    public bool Enabled
    {
        get => enabled;
        set
        {
            enabled = value;
            HitTest();
            autoHideControl?.Hide();
        }
    }

    [DefaultValue(null)]
    public Control Control
    {
        get => control;
        set
        {
            if (control != null)
            {
                control.MouseMove -= control_MouseMove;
                control.MouseLeave -= control_MouseLeave;
            }
            control = value;
            if (control != null)
            {
                control.MouseMove += control_MouseMove;
                control.MouseLeave += control_MouseLeave;
            }
        }
    }

    [DefaultValue(null)]
    public Control AutoHideControl
    {
        get => autoHideControl;
        set
        {
            if (autoHideControl != null)
            {
                autoHideControl.Leave -= autoHideControl_Leave;
                autoHideControl.VisibleChanged -= autoHideControl_VisibleChanged;
                autoHideControl.MouseEnter -= autoHideControl_MouseEnter;
                autoHideControl.MouseLeave -= autoHideControl_MouseLeave;
            }
            autoHideControl = value;
            if (autoHideControl != null)
            {
                autoHideControl.Leave += autoHideControl_Leave;
                autoHideControl.VisibleChanged += autoHideControl_VisibleChanged;
                autoHideControl.MouseEnter += autoHideControl_MouseEnter;
                autoHideControl.MouseLeave += autoHideControl_MouseLeave;
            }
        }
    }

    [DefaultValue(typeof(Rectangle), "0, 0, 0, 0")]
    public Rectangle HotBounds
    {
        get => hotBounds;
        set => hotBounds = value;
    }

    [DefaultValue(AutoHideBounds.None)]
    public AutoHideBounds AutoBounds
    {
        get => autoBounds;
        set => autoBounds = value;
    }

    [DefaultValue(20)]
    public int AutoWidth
    {
        get => autoWidth;
        set => autoWidth = value;
    }

    [DefaultValue(typeof(TimeSpan), "00:00:02")]
    public TimeSpan ShowTime
    {
        get => showTime;
        set => showTime = value;
    }

    [DefaultValue(typeof(TimeSpan), "00:00:02")]
    public TimeSpan HideTime
    {
        get => hideTime;
        set => hideTime = value;
    }

    public AutoHideManager()
    {
        timer.Interval = 250;
        timer.Enabled = true;
        timer.Tick += timer_Tick;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            timer.Dispose();
        }
        base.Dispose(disposing);
    }

    private void autoHideControl_VisibleChanged(object sender, EventArgs e)
    {
        Control control = (Control)sender;
        if (!control.Visible && control.Focused && control.Parent != null)
        {
            control.Parent.Focus();
        }
        if (!control.Visible)
        {
            inAutoHideControl = false;
        }
    }

    private void timer_Tick(object sender, EventArgs e)
    {
        if (autoHideControl == null || Control == null || base.DesignMode)
        {
            return;
        }
        if (mouseInRegion)
        {
            if (DateTime.Now - startInRegion > showTime)
            {
                autoHideControl.Show();
                timer.Enabled = false;
            }
        }
        else if (DateTime.Now - startOutRegion > hideTime)
        {
            autoHideControl.Hide();
            timer.Enabled = false;
        }
    }

    private void control_MouseMove(object sender, MouseEventArgs e)
    {
        HitTest();
    }

    private void autoHideControl_Leave(object sender, EventArgs e)
    {
        ((Control)sender).Hide();
        mouseInRegion = false;
        startOutRegion = DateTime.Now;
    }

    private void control_MouseLeave(object sender, EventArgs e)
    {
        HitTest();
    }

    private void autoHideControl_MouseLeave(object sender, EventArgs e)
    {
        inAutoHideControl = false;
        HitTest();
    }

    private void autoHideControl_MouseEnter(object sender, EventArgs e)
    {
        inAutoHideControl = true;
        HitTest();
    }

    private void HitTest()
    {
        bool flag = inAutoHideControl || (Control != null && TestRegion(Control.PointToClient(Cursor.Position)));
        if (flag != mouseInRegion)
        {
            if (flag)
            {
                startInRegion = DateTime.Now;
            }
            else
            {
                startOutRegion = DateTime.Now;
            }
            mouseInRegion = flag;
            timer.Enabled = true;
        }
    }

    private Rectangle GetHotBounds()
    {
        return !enabled || control == null || !control.Focused
            ? Rectangle.Empty
            : autoBounds switch
            {
                AutoHideBounds.Top => new Rectangle(0, 0, control.Width, autoWidth),
                AutoHideBounds.Bottom => new Rectangle(0, control.Height - autoWidth, control.Width, autoWidth),
                AutoHideBounds.Left => new Rectangle(0, 0, autoWidth, control.Height),
                AutoHideBounds.Right => new Rectangle(control.Width - autoWidth, 0, autoWidth, control.Height),
                _ => hotBounds,
            };
    }

    private bool TestRegion(Point pt)
    {
        return GetHotBounds().Contains(pt);
    }
}
