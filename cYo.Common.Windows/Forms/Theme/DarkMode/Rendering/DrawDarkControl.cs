
using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using cYo.Common.Windows.Forms.Theme.Resources;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;

internal static class DrawDarkControl
{
    internal static void DrawButtonBase(Graphics g, Rectangle bounds, PushButtonState state)
    {
        switch (state)
        {
            case PushButtonState.Hot:
                g.FillRectangle(DarkBrushes.Button.MouseOverBack, bounds);
                break;
            case PushButtonState.Pressed:
                g.FillRectangle(DarkBrushes.Button.CheckedBack, bounds);
                break;
            default:
                g.FillRectangle(DarkBrushes.Button.Back, bounds);
                break;
        }
        ControlPaint.DrawBorder(g, bounds, DarkColors.Button.Border, ButtonBorderStyle.Solid);
    }

    internal static void DrawTabItem(Graphics g, Rectangle bounds, TabItemState state, bool buttonMode)
    {
        // currently DarkBrushes.TabBar.SelectedBack == DarkBrushes.TabBar.Back
        Brush backBrush = state == TabItemState.Selected ? DarkBrushes.TabBar.SelectedBack : DarkBrushes.TabBar.Back;
        g.FillRectangle(backBrush, bounds);
        DrawTabBorder(g, bounds, state);
    }

    private static void DrawTabBorder(Graphics g, Rectangle bounds, TabItemState state)
    {
        // currently select and not selected tab borders are the same color
        // right border of selected tab overlaps with next to be handled if different colors
        g.DrawLine(DarkPens.TabBar.Border, bounds.Left, bounds.Bottom - 1, bounds.Left, bounds.Top);           // Left
        g.DrawLine(DarkPens.TabBar.Border, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top);             // Top
        g.DrawLine(DarkPens.TabBar.Border, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1); // Right
    }
}
