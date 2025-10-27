using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using System.Drawing;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;

internal static class GraphicsExtensions
{
    internal static void DrawDarkStringDisabled(this Graphics g, string text, Font font, Color color, Rectangle bounds, TextFormatFlags textFormatFlags)
        => TextRenderer.DrawText(g, text, font, bounds, SystemColors.GrayText, textFormatFlags);

    internal static void DrawDarkString(this Graphics g, string text, Font font, Color color, Rectangle bounds, StringFormat stringFormat)
        => g.DrawString(text, font, DarkBrushes.FromDarkColor(color), bounds, stringFormat);

    internal static void DrawColumnSeparator(this Graphics g, Rectangle bounds)
    {
        int x = bounds.Right - 2;
        int y1 = bounds.Top;
        int y2 = bounds.Bottom;
        g.DrawLine(DarkPens.Header.Separator, x, y1, x, y2);
    }

    internal static void DrawDarkColumnSeparator(this Graphics g, Rectangle bounds, Color color)
    {
        int x = bounds.Right - 2;
        g.DrawLine(DarkPens.FromDarkColor(color), new Point(x, bounds.Top), new Point(x, bounds.Bottom));
    }

    #region DrawBorder
    internal static void DrawBorder(Graphics g, Rectangle bounds, Border3DStyle borderStyle)
        => DrawDarkBorder(g, bounds);

    internal static void DrawDarkBorder(this Graphics g, Rectangle bounds)
    {
        ControlPaint.DrawBorder(g, bounds, DarkColors.Border.Default, ButtonBorderStyle.Solid);
    }
    #endregion

    #region DrawFocusRectangle
    public static void DrawDarkFocusRectangle(this Graphics graphics, Rectangle bounds)
        => DrawDarkFocusRectangle(graphics, bounds, SystemColors.ControlText, SystemColors.Control);

    public static void DrawDarkFocusRectangle(this Graphics graphics, Rectangle bounds, Color foreColor, Color backColor)
        => DrawDarkFocusRectangle(graphics, bounds, backColor);

    private static void DrawDarkFocusRectangle(this Graphics g, Rectangle bounds, Color color)
        => g.DrawRectangle(DarkPens.SelectedText.Focus, Rectangle.Inflate(bounds, -1, -1));
    #endregion

    #region DrawBackground
    internal static void DrawDarkBackground(this Graphics g, Rectangle bounds, Color backColor)
        => g.FillRectangle(DarkBrushes.FromDarkColor(backColor), bounds); // this may not actually be a DarkColor, but that's not important; caching is
    #endregion
}
