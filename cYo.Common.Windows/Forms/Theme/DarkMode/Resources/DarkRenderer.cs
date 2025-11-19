using cYo.Common.Windows.Forms.Theme.DarkMode.Controls;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Resources;

internal static class DarkRenderer
{
    internal static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state)
        => DarkCheckBoxRenderer.DrawCheckBox(g, glyphLocation, state);

    internal static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state, Size glyphSize)
        => DarkCheckBoxRenderer.DrawCheckBox(g, new Rectangle(glyphLocation, glyphSize), state);

    internal static void DrawCheckBox(Graphics g, Rectangle rectangle, ButtonState state)
        => DarkCheckBoxRenderer.DrawCheckBox(g, rectangle, state);
}
