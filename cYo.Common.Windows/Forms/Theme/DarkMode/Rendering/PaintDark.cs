using cYo.Common.Windows.Forms.Theme.DarkMode.Controls;
using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;

internal static class PaintDark
{
    // TODO: handle disabled CheckBoxes with images/icons (currently they're wiped)
    public static void CheckBox(object sender, PaintEventArgs e)
    {
        CheckBox checkBox = sender as CheckBox;
        DrawCheckBoxArgs drawArgs = new(checkBox, e.Graphics);

        // default OS drawing for enabled Win11 CheckBox or Appearance.Button
        if (drawArgs.DisabledTextOnly && checkBox.Enabled) return;

        DarkCheckBoxRenderer.DrawCheckBox(e.Graphics, drawArgs);
    }

    /// <summary>
    /// Paint disabled Label text.
    /// </summary>
    internal static void Label(object sender, PaintEventArgs e)
    {
        Label label = sender as Label;

        if (!label.Enabled)
            DarkControlPaint.DrawDisabledText(e.Graphics, label);
    }

    /// <summary>
    /// Paint ToolStripStatusLabel border
    /// </summary>
    internal static void ToolStripStatusLabel(object sender, PaintEventArgs e)
        => e.Graphics.DrawDarkBorder(e.ClipRectangle, DarkColors.ToolStrip.BorderColor);

}
