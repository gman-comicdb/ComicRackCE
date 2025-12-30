using System.Drawing;

namespace cYo.Common.Drawing;

public static class EnumExtensions
{
    private static ImageRotation FromDegrees(int degrees)
    {
        degrees %= 360;
        if (degrees < 0)
        {
            degrees += 360;
        }
        return (ImageRotation)(degrees / 90);
    }

    public static ImageRotation RotateRight(this ImageRotation rotate)
    {
        return (ImageRotation)((int)(rotate + 1) % 4);
    }

    public static ImageRotation RotateLeft(this ImageRotation rotate)
    {
        return (ImageRotation)((int)(rotate - 1 + 4) % 4);
    }

    public static ImageRotation Add(this ImageRotation rotate, int degrees)
    {
        return FromDegrees(rotate.ToDegrees() + degrees);
    }

    public static int ToDegrees(this ImageRotation rotation)
    {
        return rotation switch
        {
            ImageRotation.Rotate90 => 90,
            ImageRotation.Rotate180 => 180,
            ImageRotation.Rotate270 => 270,
            _ => 0,
        };
    }

    public static StringAlignment ToAlignment(this ContentAlignment ca)
    {
        return ca switch
        {
            ContentAlignment.TopCenter or ContentAlignment.MiddleCenter or ContentAlignment.BottomCenter => StringAlignment.Center,
            ContentAlignment.TopRight or ContentAlignment.MiddleRight or ContentAlignment.BottomRight => StringAlignment.Far,
            _ => StringAlignment.Near,
        };
    }

    public static StringAlignment ToLineAlignment(this ContentAlignment ca)
    {
        return ca switch
        {
            ContentAlignment.BottomLeft or ContentAlignment.BottomCenter or ContentAlignment.BottomRight => StringAlignment.Far,
            ContentAlignment.MiddleLeft or ContentAlignment.MiddleCenter or ContentAlignment.MiddleRight => StringAlignment.Center,
            _ => StringAlignment.Near,
        };
    }

    public static ContentAlignment FromAlignments(StringAlignment alignment, StringAlignment lineAlignment)
    {
        switch (alignment)
        {
            case StringAlignment.Near:
                switch (lineAlignment)
                {
                    case StringAlignment.Near:
                        return ContentAlignment.TopLeft;
                    case StringAlignment.Center:
                        return ContentAlignment.MiddleLeft;
                    case StringAlignment.Far:
                        return ContentAlignment.BottomLeft;
                }
                break;
            case StringAlignment.Center:
                switch (lineAlignment)
                {
                    case StringAlignment.Near:
                        return ContentAlignment.TopCenter;
                    case StringAlignment.Center:
                        return ContentAlignment.MiddleCenter;
                    case StringAlignment.Far:
                        return ContentAlignment.BottomCenter;
                }
                break;
            case StringAlignment.Far:
                switch (lineAlignment)
                {
                    case StringAlignment.Near:
                        return ContentAlignment.TopRight;
                    case StringAlignment.Center:
                        return ContentAlignment.MiddleRight;
                    case StringAlignment.Far:
                        return ContentAlignment.BottomRight;
                }
                break;
        }
        return ContentAlignment.TopLeft;
    }
}
