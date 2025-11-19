using cYo.Common.Collections;
using cYo.Common.Drawing3D;
using cYo.Common.Text.FunctionParser;
using cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;
using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Controls;

internal class DarkCheckBoxRenderer
{
    public static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state)
        => DrawCheckBox(g, new Rectangle(glyphLocation, GetGlyphSize(g, state)), state);

    public static void DrawCheckBox(Graphics g, Rectangle glyphBounds, ButtonState state)
    {
        CheckBoxState boxState = ConvertFromButtonState(state, false, false);

        // Ignore the glyphBounds we are passed as they need some adjustment that is not currently accounted for
        // This is likely related to ContentAlignment and/or flat CheckBoxes
        glyphBounds = new Rectangle(new Point(glyphBounds.X, glyphBounds.Y), GetGlyphSize(g, boxState));
        DrawCheckBox(g, glyphBounds, boxState);
    }

    public static void DrawCheckBox(Graphics g, Rectangle glyphBounds, CheckState state, bool enabled = true)
        => DrawCheckBox(g, glyphBounds, ConvertFromCheckState(state, enabled));

    public static class Box
    {
        private static Color[] GetColor(int index)
        {
            switch (index)
            {
                case 0:
                    return [
                        Color.Empty,
                            Unchecked.Back,
                            Unchecked.Back,
                            Unchecked.Back,
                            UncheckedDisabled.Back,
                            Checked.Back,
                            Checked.Back,
                            Checked.Back,
                            CheckedDisabled.Back,
                            Checked.Back,
                            Checked.Back,
                            Checked.Back,
                            CheckedDisabled.Back
                    ];
                case 1:
                    return [
                        Color.Empty,
                            Unchecked.Corner,
                            Unchecked.Corner,
                            Unchecked.Corner,
                            UncheckedDisabled.Corner,
                            Checked.Corner,
                            Checked.Corner,
                            Checked.Corner,
                            CheckedDisabled.Corner,
                            Checked.Corner,
                            Checked.Corner,
                            Checked.Corner,
                            CheckedDisabled.Corner
                    ];
                case 2:
                    return [
                        Color.Empty,
                            Unchecked.MiddleMask,
                            Unchecked.MiddleMask,
                            Unchecked.MiddleMask,
                            UncheckedDisabled.MiddleMask,
                            Checked.Middle,
                            Checked.Middle,
                            Checked.Middle,
                            CheckedDisabled.Middle,
                            Checked.Middle,
                            Checked.Middle,
                            Checked.Middle,
                            CheckedDisabled.Middle
                    ];
                case 3:
                    return [
                        Color.Empty,
                            Unchecked.InnerMask,
                            Unchecked.InnerMask,
                            Unchecked.InnerMask,
                            UncheckedDisabled.InnerMask,
                            Checked.Back,
                            Checked.Back,
                            Checked.Back,
                            CheckedDisabled.Back,
                            Checked.Back,
                            Checked.Back,
                            Checked.Back,
                            CheckedDisabled.Back
                    ];
                default:
                    return [UncheckedDisabled.Deco];
            }

        }

        public static Color BackColor(CheckBoxState state) => GetColor(0)[(int)state];
        public static Color CornerColor(CheckBoxState state) => GetColor(1)[(int)state];
        public static Color MiddleMask(CheckBoxState state) => GetColor(2)[(int)state];
        public static Color InnerMask(CheckBoxState state) => GetColor(3)[(int)state];
        public static Color DisabledDotColor() => GetColor(4)[0];

        private static class Unchecked
        {
            public static readonly Color Back = Color.FromArgb(36, 36, 36);
            public static readonly Color Corner = Color.FromArgb(106, 106, 106);
            public static readonly Color MiddleMask = Color.FromArgb(97, 255, 255, 255);
            public static readonly Color InnerMask = Color.FromArgb(19, 255, 255, 255);
            //public static readonly Color Corner = Color.FromArgb(109, 109, 109);
            //public static readonly Color Middle = Color.FromArgb(119, 119, 119);
            //public static readonly Color Border = Color.FromArgb(170, 170, 170);
        }

        private static class UncheckedDisabled
        {
            public static readonly Color Back = Color.FromArgb(40, 40, 40);
            public static readonly Color Corner = Color.FromArgb(55, 55, 55);
            public static readonly Color MiddleMask = Color.FromArgb(64, 128, 128, 128);
            public static readonly Color InnerMask = Color.FromArgb(8, 128, 128, 128);
            //public static readonly Color Corner = Color.FromArgb(56, 56, 56);
            //public static readonly Color Middle = Color.FromArgb(61, 61, 61);
            //public static readonly Color Border = Color.FromArgb(72, 72, 72);

            public static readonly Color Deco = Color.FromArgb(131, 131, 131);
        }

        private static class Checked
        {
            public static readonly Color Back = Color.FromArgb(96, 205, 255);
            public static readonly Color Corner = Color.FromArgb(64, 119, 150);
            public static readonly Color Middle = Color.FromArgb(92, 194, 241);
            //public static readonly Color Corner = Color.FromArgb(64, 119, 114);
            //public static readonly Color Middle = Color.FromArgb(92, 194, 241);
            //public static readonly Color Border = Color.FromArgb(96, 205, 255);
        }

        private static class CheckedDisabled
        {
            public static readonly Color Back = Color.FromArgb(74, 74, 74);
            public static readonly Color Corner = Color.FromArgb(56, 56, 56);
            public static readonly Color Middle = Color.FromArgb(72, 72, 72);
            //public static readonly Color Corner = Color.FromArgb(56, 56, 56);
            //public static readonly Color Middle = Color.FromArgb(72, 72, 72);
            //public static readonly Color Border = Color.FromArgb(74, 74, 74);

            public static readonly Color Deco = Color.FromArgb(131, 131, 131);
        }
    }

    private static readonly Dictionary<(CheckBoxState state, int width, float dpiScale), (Bitmap bg, Bitmap mark)> cache =
        new Dictionary<(CheckBoxState, int, float), (Bitmap, Bitmap)>();

    public static void DrawCheckBox(Graphics g, Rectangle glyphBounds, CheckBoxState state)
    {
        GetCachedBitmap(state, glyphBounds, out Bitmap bg, out Bitmap mark);
        g.DrawImageUnscaled(bg, glyphBounds.X, glyphBounds.Y);
        g.DrawImageUnscaled(mark, glyphBounds.X, glyphBounds.Y);
    }


    public static void GetCachedBitmap(CheckBoxState state, Rectangle glyphBounds, out Bitmap background, out Bitmap mark, float dpiScale = 1f)
    {
        var key = (state, glyphBounds.Width, dpiScale);
        if (cache.TryGetValue(key, out var cachedImages))
        {
            background = cachedImages.bg;
            mark = cachedImages.mark;
            return;
        }
        var images = DrawCheckBoxBitmap(state, glyphBounds);
        cache[key] = images;
        background = images.bg;
        mark = images.mark;
    }

    public static (Bitmap bg, Bitmap mark) DrawCheckBoxBitmap(CheckBoxState state, Rectangle glyphBounds, float dpiScale = 1f)
    {
        var bg = new Bitmap(glyphBounds.Width, glyphBounds.Height, PixelFormat.Format32bppArgb);
        var mark = new Bitmap(glyphBounds.Width, glyphBounds.Height, PixelFormat.Format32bppArgb);

        // so we can manipulate without breaking Bitmap
        // required for DrawRoundedRectangle
        Rectangle boxBounds = new Rectangle(
            0,
            0,
            glyphBounds.Width -= glyphBounds.Width % 13,
            glyphBounds.Height -= glyphBounds.Height % 13
        );

        using (Graphics g = Graphics.FromImage(bg))
        {
            //g.SmoothingMode = SmoothingMode.AntiAlias;
            //g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            if (!IsCheckedOrMixed(state))
                DrawCheckBackground(g, boxBounds, Box.BackColor(state));

            DrawRoundedRectangle(g, Box.CornerColor(state), boxBounds, 2);
            DrawRoundedRectangle(g, Box.MiddleMask(state), boxBounds, 3);
            DrawRoundedRectangle(g, Box.InnerMask(state), boxBounds, 5);

            if (IsCheckedOrMixed(state))
                DrawCheckBackground(g, boxBounds, Box.BackColor(state));
        }

        using (Graphics g = Graphics.FromImage(mark))
        {
            if (IsCheckedOrMixed(state))
            {
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                DrawCheckMark(g, boxBounds, state);
            }
        }
        return (bg, mark);
    }

    private static void DrawCheckmark(Graphics g, int size, Pen pen)
    {
        var scale = size / 13f;

        PointF[] pts =
        {
            new PointF(3 * scale, 7 * scale),
            new PointF(5 * scale, 9 * scale),
            new PointF(10 * scale, 4 * scale)
        };
        g.DrawLines(pen, pts);
    }

    private static void DrawMixedBar(Graphics g, int size, Color markColor)
    {
        var barWidth = size * 7f / 13f;
        var barHeight = size * 2f / 13f;

        float x = (size - barWidth) / 2f;
        float y = (size - barHeight) / 2f;

        using var b = new SolidBrush(markColor);
        g.FillRectangle(b, x, y, barWidth, barHeight);
    }

    public static void DrawRoundedRectangle(Graphics graphics, Color color, Rectangle bounds, int cornerRadius, bool fill = false)
        => DrawRoundedRectangle(graphics, DarkPens.FromDarkColor(color), bounds, cornerRadius, fill);

    public static void DrawRoundedRectangle(Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius, bool fill = false)
    {
        using (GraphicsPath path = RoundedRect(bounds, cornerRadius))
        {
            graphics.DrawPath(pen, path);
            if (fill)
                graphics.FillPath(DarkBrushes.FromDarkColor(pen.Color), path);
        }
    }

    public static GraphicsPath RoundedRect(Rectangle bounds, float radius)
    {
        // shrink bounds by 1 pixel to counteract GDI+'s stroke expansion
        bounds.Width--;
        bounds.Height--;
        float diameter = radius * 2f;
        SizeF size = new SizeF(diameter, diameter);
        RectangleF arc = new RectangleF(bounds.Location, size);
        GraphicsPath path = new GraphicsPath();

        if (radius == 0)
        {
            path.AddRectangle(bounds);
            return path;
        }

        // top left arc  
        path.AddArc(arc, 180, 90);

        // top right arc  
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);

        // bottom right arc  
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);

        // bottom left arc 
        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);

        path.CloseFigure();
        bounds.Width++;
        bounds.Height++;
        return path;
    }

    private static void DrawCheckBackground(Graphics g, Rectangle bounds, Color backColor, float inset = 1f)
    {
        Brush backBrush = DarkBrushes.FromDarkColor(backColor);

        // inset
        float i = bounds.Width * inset / 13f;
        g.FillRectangle(backBrush, new RectangleF(bounds.X + i, bounds.Y + i, bounds.Width - (2*i), bounds.Height - (2 * i)));
    }

    private static void DrawCheckMark(Graphics g, Rectangle bounds, CheckBoxState state)
    {
        if (!IsChecked(state))
            return;

        Point[] checkMark;

        if (IsChecked(state))
        {
            checkMark = [
                new (bounds.Left + 3, bounds.Top + (bounds.Height/2) + 1),
                new (bounds.Left + (bounds.Width / 2) - 1 , bounds.Bottom - 3),
                new (bounds.Right - 3, bounds.Top + (bounds.Height/2) - 1),
                new (bounds.Right - 3, bounds.Top + (bounds.Height/2) - 2),
                new (bounds.Left + (bounds.Width / 2) - 1, bounds.Bottom - 4),
                new (bounds.Left + 3, bounds.Top + (bounds.Height / 2))
            ];
        }
        else
        {
            // Mixed State
            checkMark = [
                new(bounds.Left + 3, bounds.Top + (bounds.Height / 2)),
                new(bounds.Right - 3, bounds.Top + (bounds.Height / 2))
            ];
        }
        //g.SmoothingMode = SmoothingMode.AntiAlias;
        //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.DrawPolygon(IsDisabled(state) ? SystemPens.GrayText : SystemPens.ControlLight, checkMark);
    }

    // functional but debatable whether quality is better
    private static void DrawSmoothCheckMark(Graphics g, Rectangle bounds, CheckBoxState state)
    {
        if (!IsChecked(state))
            return;

        PointF[] checkMark;

        if (IsChecked(state))
        {
            PointF start = new(
                bounds.Left + 3.5f,
                bounds.Top + (bounds.Height / 2f) + 0.5f
            );
            PointF bottom = new(
                bounds.Left + (bounds.Width / 2f) - 1f,
                bounds.Bottom - 3f
            );
            PointF end = new(
                bounds.Right - 3f,
                bounds.Top + (bounds.Height / 2f) - 2f
            );
            checkMark = [start, bottom, end, new(end.X, end.Y - 1), new(bottom.X, bottom.Y - 1), new(start.X, start.Y - 1)];
        }
        else
        {
            // Mixed State
            checkMark = [new(bounds.Left + 3.5f, bounds.Top + (bounds.Height / 2f))];
        }
        g.SmoothingMode = SmoothingMode.AntiAlias;
        //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        using Pen pen = new Pen(IsDisabled(state) ? SystemColors.GrayText : SystemColors.ControlLight, 1.5f);
        g.DrawPolygon(pen, checkMark);
    }

    internal static Rectangle GetCheckRectangle(Graphics g, Rectangle clientRectangle, Size glyphSize, ContentAlignment checkAlign = ContentAlignment.MiddleLeft)
    {
        Point glyphLocation = Helpers.GetImageAlignmentPoint(clientRectangle, glyphSize, checkAlign);

        Rectangle glyphBounds = new Rectangle(glyphLocation, glyphSize);

        if (checkAlign == ContentAlignment.MiddleRight)
            glyphBounds.X -= 1;
        return glyphBounds;
    }

    /// <summary>
    ///  Returns the size of the CheckBox glyph.
    /// </summary>
    public static Size GetGlyphSize(Graphics g, CheckBoxState state) => CheckBoxRenderer.GetGlyphSize(g, state);


    #region Conversion Methods
    internal static CheckState ConvertToCheckState(CheckBoxState state) => state switch
    {
        CheckBoxState.CheckedNormal or
        CheckBoxState.CheckedHot or
        CheckBoxState.CheckedPressed or
        CheckBoxState.CheckedDisabled => CheckState.Checked,

        CheckBoxState.MixedNormal or
        CheckBoxState.MixedHot or
        CheckBoxState.MixedPressed or
        CheckBoxState.MixedDisabled => CheckState.Indeterminate,

        CheckBoxState.UncheckedNormal or
        CheckBoxState.UncheckedHot or
        CheckBoxState.UncheckedPressed or
        CheckBoxState.UncheckedDisabled => CheckState.Unchecked,
    };

    internal static CheckBoxState ConvertFromCheckState(CheckState state, bool enabled)
    {
        if (enabled)
        {
            if (state == CheckState.Checked)
                return CheckBoxState.CheckedNormal;
            else if (state == CheckState.Unchecked)
                return CheckBoxState.UncheckedNormal;
            else
                return CheckBoxState.MixedNormal;
        }
        if (state == CheckState.Checked)
            return CheckBoxState.CheckedDisabled;
        else if (state == CheckState.Unchecked)
            return CheckBoxState.UncheckedDisabled;
        else
            return CheckBoxState.UncheckedDisabled;
    }

    internal static ButtonState ConvertToButtonState(CheckBoxState state) => state switch
    {
        CheckBoxState.CheckedNormal or CheckBoxState.CheckedHot => ButtonState.Checked,
        CheckBoxState.CheckedPressed => (ButtonState.Checked | ButtonState.Pushed),
        CheckBoxState.CheckedDisabled => (ButtonState.Checked | ButtonState.Inactive),
        CheckBoxState.UncheckedPressed => ButtonState.Pushed,
        CheckBoxState.UncheckedDisabled => ButtonState.Inactive,
        // Downlevel mixed drawing works only if ButtonState.Checked is set
        CheckBoxState.MixedNormal or CheckBoxState.MixedHot => ButtonState.Checked,
        CheckBoxState.MixedPressed => (ButtonState.Checked | ButtonState.Pushed),
        CheckBoxState.MixedDisabled => (ButtonState.Checked | ButtonState.Inactive),
        _ => ButtonState.Normal,
    };

    internal static CheckBoxState ConvertFromButtonState(ButtonState state, bool isMixed, bool isHot)
    {
        if (isMixed)
        {
            if ((state & ButtonState.Pushed) == ButtonState.Pushed)
            {
                return CheckBoxState.MixedPressed;
            }
            else if ((state & ButtonState.Inactive) == ButtonState.Inactive)
            {
                return CheckBoxState.MixedDisabled;
            }
            else if (isHot)
            {
                return CheckBoxState.MixedHot;
            }

            return CheckBoxState.MixedNormal;
        }
        else if ((state & ButtonState.Checked) == ButtonState.Checked)
        {
            if ((state & ButtonState.Pushed) == ButtonState.Pushed)
            {
                return CheckBoxState.CheckedPressed;
            }
            else if ((state & ButtonState.Inactive) == ButtonState.Inactive)
            {
                return CheckBoxState.CheckedDisabled;
            }
            else if (isHot)
            {
                return CheckBoxState.CheckedHot;
            }

            return CheckBoxState.CheckedNormal;
        }
        else
        {
            // Unchecked
            if ((state & ButtonState.Pushed) == ButtonState.Pushed)
            {
                return CheckBoxState.UncheckedPressed;
            }
            else if ((state & ButtonState.Inactive) == ButtonState.Inactive)
            {
                return CheckBoxState.UncheckedDisabled;
            }
            else if (isHot)
            {
                return CheckBoxState.UncheckedHot;
            }

            return CheckBoxState.UncheckedNormal;
        }
    }

    #endregion

    #region CheckBoxState booleans
    private static bool IsCheckedOrMixed(CheckBoxState state)
        => (IsChecked(state) || IsMixed(state));

    private static bool IsChecked(CheckBoxState state) => state switch
    {
        CheckBoxState.CheckedNormal
            or CheckBoxState.CheckedHot
            or CheckBoxState.CheckedPressed
            or CheckBoxState.CheckedDisabled => true,
        _ => false,
    };

    private static bool IsMixed(CheckBoxState state) => state switch
    {
        CheckBoxState.MixedNormal
            or CheckBoxState.MixedHot
            or CheckBoxState.MixedPressed
            or CheckBoxState.MixedDisabled => true,
        _ => false,
    };

    private static bool IsDisabled(CheckBoxState state) => state switch
    {
        CheckBoxState.CheckedDisabled or CheckBoxState.UncheckedDisabled or CheckBoxState.MixedDisabled => true,
        _ => false,
    };
    #endregion
}
