using System;
using System.Drawing;

using cYo.Common.Collections;
using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.Presentation.Ceco.Builders;

namespace cYo.Common.Presentation.Ceco;

public static class XHtmlRenderer
{
    private class BodyKey
    {
        public Font Font { get; set; }

        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            return obj is BodyKey bodyKey && bodyKey.Font == Font ? bodyKey.Text == Text : false;
        }

        public override int GetHashCode()
        {
            return Font.GetHashCode() ^ Text.GetHashCode();
        }
    }

    private static Cache<BodyKey, BodyBlock> bodyCache = new(100);

    public static void DrawString(Graphics graphics, string s, Font font, Color foreColor, int x, int y)
    {
        DrawString(graphics, s, font, foreColor, new Point(x, y));
    }

    public static void DrawString(Graphics graphics, string s, Font font, Color foreColor, Point location)
    {
        DrawString(graphics, s, font, foreColor, new Rectangle(location, Size.Empty));
    }

    public static void DrawString(Graphics graphics, string s, Font font, Color foreColor, Rectangle layoutRectangle)
    {
        DrawString(graphics, s, font, foreColor, layoutRectangle, ContentAlignment.TopLeft);
    }

    public static void DrawString(Graphics graphics, string s, Font font, Color foreColor, Rectangle layoutRectangle, StringFormat sf)
    {
        DrawString(graphics, s, font, foreColor, layoutRectangle, EnumExtensions.FromAlignments(sf.Alignment, sf.LineAlignment));
    }

    public static void DrawString(Graphics graphics, string s, Font font, Color foreColor, Rectangle layoutRectangle, ContentAlignment align)
    {
        using (IItemLock<BodyBlock> itemLock = GetBody(s, font))
        {
            BodyBlock item = itemLock.Item;
            item.ForeColor = foreColor;
            if (layoutRectangle.Width <= 0)
            {
                layoutRectangle.Width = int.MaxValue;
            }
            if (layoutRectangle.Height <= 0)
            {
                layoutRectangle.Height = int.MaxValue;
            }
            item.Align = align.ToAlignment().ToHorizontalAlignment();
            VerticalAlignment verticalAlignment = align.ToLineAlignment().ToVerticalAlignment();
            if (verticalAlignment is not 0 or not VerticalAlignment.Top)
            {
                item.Measure(graphics, layoutRectangle.Width);
                layoutRectangle.Y += verticalAlignment switch
                {
                    VerticalAlignment.Middle => (layoutRectangle.Height - item.ActualSize.Height) / 2,
                    VerticalAlignment.Bottom => layoutRectangle.Bottom - item.ActualSize.Height,
                    _ => throw new ArgumentOutOfRangeException(),
                };
            }
            item.Bounds = new Rectangle(Point.Empty, layoutRectangle.Size);
            item.Draw(graphics, layoutRectangle.Location);
        }
    }

    public static Size MeasureString(Graphics graphics, string s, Font font)
    {
        return MeasureString(graphics, s, font, 0);
    }

    public static Size MeasureString(Graphics graphics, string s, Font font, int width)
    {
        using (IItemLock<BodyBlock> itemLock = GetBody(s, font))
        {
            BodyBlock item = itemLock.Item;
            if (width <= 0)
            {
                width = int.MaxValue;
            }
            item.Measure(graphics, width);
            return item.ActualSize;
        }
    }

    private static IItemLock<BodyBlock> GetBody(string text, Font font)
    {
        return bodyCache.LockItem(new BodyKey
        {
            Text = text,
            Font = font
        }, delegate (BodyKey bk)
        {
            BodyBlock bodyBlock = new();
            bodyBlock.Inlines.AddRange(XHtmlParser.Parse(bk.Text).Inlines);
            bodyBlock.Font = font;
            return bodyBlock;
        });
    }

    public static HorizontalAlignment ToHorizontalAlignment(this StringAlignment align)
    {
        return align switch
        {
            StringAlignment.Near => HorizontalAlignment.Left,
            StringAlignment.Center => HorizontalAlignment.Center,
            StringAlignment.Far => HorizontalAlignment.Right,
            _ => HorizontalAlignment.None,
        };
    }

    public static VerticalAlignment ToVerticalAlignment(this StringAlignment align)
    {
        return align switch
        {
            StringAlignment.Near => VerticalAlignment.Top,
            StringAlignment.Center => VerticalAlignment.Middle,
            StringAlignment.Far => VerticalAlignment.Bottom,
            _ => VerticalAlignment.None,
        };
    }
}
