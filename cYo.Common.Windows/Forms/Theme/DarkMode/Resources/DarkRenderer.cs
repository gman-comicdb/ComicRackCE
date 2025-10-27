using cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Resources;

internal static class DarkRenderer
{
    internal static void DrawButtonBase(Graphics g, Rectangle bounds, PushButtonState state)
        => DrawDarkControl.DrawButtonBase(g, bounds, state);

    internal static void DrawTabItem(Graphics g, Rectangle bounds, TabItemState state, bool buttonMode)
        => DrawDarkControl.DrawTabItem(g, bounds, state, buttonMode);

    internal static void DrawCheckBox(Graphics g, Rectangle bounds, CheckState state)
        => DarkCheckBoxRenderer.DrawCheckBox(g, bounds, state, true);

    internal static void DrawCheckBox(Graphics g, Rectangle bounds, CheckState state, bool isEnabled)
        => DarkCheckBoxRenderer.DrawCheckBox(g, bounds, state, isEnabled);
}
