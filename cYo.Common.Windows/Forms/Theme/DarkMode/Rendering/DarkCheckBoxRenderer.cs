
using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using System.Drawing;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;

internal static class DarkCheckBoxRenderer
{
    internal static void DrawCheckBox(Graphics g, Rectangle bounds, CheckState checkState, bool isEnabled)
    {
        DrawCheck.Background(g, bounds, checkState);

        if (checkState == CheckState.Checked)
            DrawCheck.Mark(g, bounds, isEnabled);
        DrawCheck.Box(g, bounds, checkState, isEnabled);
    }

    internal static void DrawCheckBoxText(Graphics g, Rectangle bounds, Color backColor, Color textColor, string text, Font font, TextFormatFlags textFormatFlags)
    {
        // Clear text area
        using (var backBrush = new SolidBrush(backColor))
            g.FillRectangle(backBrush, bounds);

        // Draw text
        TextRenderer.DrawText(g, text, font, bounds, textColor, textFormatFlags);
    }

    private static class DrawCheck
    {
        internal static void Background(Graphics g, Rectangle bounds, CheckState checkState)
        {
            Brush backBrush = checkState == CheckState.Checked ? DarkBrushes.CheckBox.Back : DarkBrushes.CheckBox.UncheckedBack;
            g.FillRectangle(backBrush, new Rectangle(bounds.X + 2, bounds.Y, bounds.Width - 3, bounds.Height));
            g.FillRectangle(backBrush, new Rectangle(bounds.X, bounds.Y + 2, bounds.Width, bounds.Height - 3));
        }

        internal static void Mark(Graphics g, Rectangle bounds, bool isEnabled)
        {
            Point[] checkMark = new Point[6]
                {
                new Point(bounds.Left + 2, bounds.Top + (bounds.Height/2)+2),
                new Point(bounds.Left + bounds.Width / 2 -1, bounds.Bottom - 1),
                new Point(bounds.Right - 2, bounds.Top + 5),
                new Point(bounds.Right - 2, bounds.Top + 3),
                new Point(bounds.Left + bounds.Width / 2 - 1, bounds.Bottom - 3),
                new Point(bounds.Left + 2, bounds.Top + (bounds.Height / 2)),
                 };

            g.DrawPolygon(isEnabled ? SystemPens.ControlText : SystemPens.GrayText, checkMark);
            g.FillPolygon(isEnabled ? SystemBrushes.ControlText : SystemBrushes.GrayText, checkMark);
        }

        internal static void Box(Graphics g, Rectangle bounds, CheckState checkState, bool isEnabled)
        {
            Pen borderPen = DarkPens.CheckBox.Border;

            Brush borderEdgeBrush = DarkBrushes.CheckBox.BorderEdge;
            Brush backCornerBrush = DarkBrushes.CheckBox.BackCorner;
            Brush backVertexBrush = DarkBrushes.CheckBox.BackVertex;

            if (checkState != CheckState.Checked)
                SetUncheckedBrushes(isEnabled, out borderPen, out borderEdgeBrush, out backCornerBrush, out backVertexBrush);

            DrawBorder(g, bounds, borderPen); // Main Border
            DrawBorderEdge(g, bounds, borderEdgeBrush);
            DrawInnerCorners(g, bounds, backCornerBrush); // Inner Corners
            DrawInnerVertices(g, bounds, backVertexBrush);

            if (checkState == CheckState.Checked)
                DrawBorderCorners(g, bounds);
        }

        private static void SetSides(Rectangle bounds, out int left, out int right, out int top, out int bottom)
        {
            left = bounds.X;
            right = bounds.X + bounds.Width;
            top = bounds.Y;
            bottom = bounds.Y + bounds.Height;
        }

        private static void DrawBorder(Graphics g, Rectangle bounds, Pen borderPen)
        {
            SetSides(bounds, out var left, out var right, out var top, out var bottom);

            g.DrawLine(borderPen, new Point(left + 3, top), new Point(right - 3, top));
            g.DrawLine(borderPen, new Point(left + 3, bottom), new Point(right - 3, bottom));
            g.DrawLine(borderPen, new Point(left, top + 3), new Point(left, bottom - 3));
            g.DrawLine(borderPen, new Point(right, top + 3), new Point(right, bottom - 3));
        }

        private static void DrawBorderEdge(Graphics g, Rectangle bounds, Brush borderEdgeBrush)
        {
            SetSides(bounds, out var left, out var right, out var top, out var bottom);

            // Border Edges
            // Top - Left + Right
            g.FillRectangle(borderEdgeBrush, left + 2, top, 1, 1);
            g.FillRectangle(borderEdgeBrush, right - 2, top, 1, 1);
            // Bottom - Left + Right
            g.FillRectangle(borderEdgeBrush, left + 2, bottom, 1, 1);
            g.FillRectangle(borderEdgeBrush, right - 2, bottom, 1, 1);
            // Left - Top + Bottom
            g.FillRectangle(borderEdgeBrush, left, top + 2, 1, 1);
            g.FillRectangle(borderEdgeBrush, left, bottom - 2, 1, 1);
            // Right - Top + Bottom
            g.FillRectangle(borderEdgeBrush, right, top + 2, 1, 1);
            g.FillRectangle(borderEdgeBrush, right, bottom - 2, 1, 1);
        }

        private static void DrawInnerCorners(Graphics g, Rectangle bounds, Brush backCornerBrush)
        {
            SetSides(bounds, out var left, out var right, out var top, out var bottom);

            // Inner Corners
            g.FillRectangle(backCornerBrush, left + 1, top + 1, 1, 1);
            g.FillRectangle(backCornerBrush, right - 1, top + 1, 1, 1);
            g.FillRectangle(backCornerBrush, right - 1, bottom - 1, 1, 1);
            g.FillRectangle(backCornerBrush, left + 1, bottom - 1, 1, 1);
        }

        private static void DrawInnerVertices(Graphics g, Rectangle bounds, Brush backVertexBrush)
        {
            SetSides(bounds, out var left, out var right, out var top, out var bottom);

            // Inner Vertices
            // Top - Left + Right
            g.FillRectangle(backVertexBrush, left + 2, top + 1, 1, 1);
            g.FillRectangle(backVertexBrush, right - 2, top + 1, 1, 1);
            // Bottom - Left + Right
            g.FillRectangle(backVertexBrush, left + 2, bottom - 1, 1, 1);
            g.FillRectangle(backVertexBrush, right - 2, bottom - 1, 1, 1);
            // Left - Top + Bottom
            g.FillRectangle(backVertexBrush, left + 1, top + 2, 1, 1);
            g.FillRectangle(backVertexBrush, left + 1, bottom - 2, 1, 1);
            // Right - Top + Bottom
            g.FillRectangle(backVertexBrush, right - 1, top + 2, 1, 1);
            g.FillRectangle(backVertexBrush, right - 1, bottom - 2, 1, 1);
        }
        private static void DrawBorderCorners(Graphics g, Rectangle bounds)
        {
            SetSides(bounds, out var left, out var right, out var top, out var bottom);

            Brush cornerBrush = DarkBrushes.CheckBox.BorderCorner;
            g.FillRectangle(cornerBrush, left + 1, top, 1, 1);
            g.FillRectangle(cornerBrush, left + 1, bottom, 1, 1);
            g.FillRectangle(cornerBrush, left, top + 1, 1, 1);
            g.FillRectangle(cornerBrush, left, bottom - 1, 1, 1);
            g.FillRectangle(cornerBrush, right - 1, top, 1, 1);
            g.FillRectangle(cornerBrush, right - 1, bottom, 1, 1);
            g.FillRectangle(cornerBrush, right, top + 1, 1, 1);
            g.FillRectangle(cornerBrush, right, bottom - 1, 1, 1);
        }

        private static void SetUncheckedBrushes(bool isEnabled, out Pen borderPen, out Brush borderEdgeBrush, out Brush backCornerBrush, out Brush backVertexBrush)
        {
            borderPen = DarkPens.CheckBox.UncheckedBorder;

            borderEdgeBrush = DarkBrushes.CheckBox.UncheckedBorderEdge;
            borderEdgeBrush = DarkBrushes.CheckBox.UncheckedBorderEdge;
            backCornerBrush = DarkBrushes.CheckBox.UncheckedBackCorner;
            backVertexBrush = DarkBrushes.CheckBox.UncheckedBackVertex;

            if (!isEnabled)
            {
                borderPen = DarkPens.CheckBox.UncheckedDisabledBorder;

                borderEdgeBrush = DarkBrushes.CheckBox.UncheckedDisabledBorderEdge;
                backCornerBrush = DarkBrushes.CheckBox.UncheckedDisabledBackCorner;
                backVertexBrush = DarkBrushes.CheckBox.UncheckedDisabledBackVertex;
            }
        }
    }
}
