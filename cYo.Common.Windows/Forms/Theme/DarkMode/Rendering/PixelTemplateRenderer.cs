using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public static class PixelTemplateRenderer
{
    // ------------------------
    // Public templates (Option C)
    // Fill these once (see Import helper below) or paste them as generated constants.
    // Offsets are in the 13x13 logical coordinate space (0..12).
    // ------------------------
    public static Point[] BorderOffsets = Array.Empty<Point>();
    public static Point[] BorderEdgeOffsets = Array.Empty<Point>();
    public static Point[] BackCornerOffsets = Array.Empty<Point>();
    public static Point[] BackVertexOffsets = Array.Empty<Point>();
    public static Point[] CornerExtraOffsets = Array.Empty<Point>(); // used for checked
    // Mark shapes
    public static Point[] CheckMarkOffsets = Array.Empty<Point>();
    public static Point[] MixedMarkOffsets = Array.Empty<Point>();

    // Colors (from your extracted values)
    public static Color EnabledFill = Color.FromArgb(96, 205, 255);
    public static Color EnabledBorder = Color.FromArgb(68, 130, 158);
    public static Color CheckColor = Color.FromArgb(24, 51, 64);
    public static Color UncheckedBorder = Color.FromArgb(120, 120, 120);

    // Cache for final bitmaps: (state, dpi) -> (background, mark)
    private static readonly Dictionary<(CheckState state, float dpi), (Bitmap bg, Bitmap mark)> _cache
        = new Dictionary<(CheckState, float), (Bitmap, Bitmap)>();

    // Public getter: returns two bitmaps you should DrawImageUnscaled in sequence.
    public static void GetCachedBitmaps(CheckState state, float dpiScale, out Bitmap background, out Bitmap mark)
    {
        var key = (state, dpiScale);
        if (_cache.TryGetValue(key, out var pair))
        {
            background = pair.bg;
            mark = pair.mark;
            return;
        }

        var built = BuildBitmaps(state, dpiScale);
        _cache[key] = built;
        background = built.bg;
        mark = built.mark;
    }

    // Clear cache (call on theme change/DPI change)
    public static void InvalidateCache()
    {
        foreach (var kv in _cache.Values)
        {
            kv.bg?.Dispose();
            kv.mark?.Dispose();
        }
        _cache.Clear();
    }

    // Build both bitmaps (background-only, mark-only)
    private static (Bitmap bg, Bitmap mark) BuildBitmaps(CheckState state, float dpiScale)
    {
        // logical size is 13 x 13 pixels
        int logicalSize = 13;
        int size = (int)Math.Max(1, Math.Round(logicalSize * dpiScale));

        // We'll build at device pixel size and set resolution if desired.
        var bg = new Bitmap(size, size, PixelFormat.Format32bppArgb);
        var mark = new Bitmap(size, size, PixelFormat.Format32bppArgb);

        // Precompute scaled offsets for each template (scale from 13 -> size)
        float s = size / (float)logicalSize;
        Point[] Scale(Point[] pts)
        {
            if (pts == null || pts.Length == 0) return Array.Empty<Point>();
            var a = new Point[pts.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                a[i] = new Point((int)Math.Round(pts[i].X * s), (int)Math.Round(pts[i].Y * s));
            }
            return a;
        }

        var bOffsets = Scale(BorderOffsets);
        var beOffsets = Scale(BorderEdgeOffsets);
        var bcOffsets = Scale(BackCornerOffsets);
        var bvOffsets = Scale(BackVertexOffsets);
        var ceOffsets = Scale(CornerExtraOffsets);

        var chkOffsets = Scale(CheckMarkOffsets);
        var mixOffsets = Scale(MixedMarkOffsets);

        // Build background by locking bits and writing pixels (fast and exact)
        FillBitmapWithOffsets(bg, GetBackgroundColorForState(state), dpiScale, bOffsets, beOffsets, bcOffsets, bvOffsets, ceOffsets);

        // Build mark: depending on state, write only mark pixels (transparent elsewhere)
        if (state == CheckState.Checked)
            FillBitmapWithOffsets(mark, CheckColor, chkOffsets);
        else if (state == CheckState.Indeterminate)
            FillBitmapWithOffsets(mark, CheckColor, mixOffsets);
        // else leave mark transparent

        // Set resolution (optional)
        bg.SetResolution(96f * dpiScale, 96f * dpiScale);
        mark.SetResolution(96f * dpiScale, 96f * dpiScale);

        return (bg, mark);
    }

    private static Color GetBackgroundColorForState(CheckState state)
    {
        if (state == CheckState.Unchecked)
            //return Color.Transparent; // user PNG had transparent interior for unchecked
            return Color.FromArgb(36, 36, 36);
        return EnabledFill;
    }

    // Fill the bitmap with many offset lists colored by the same color (fast lockbits writer)
    private static void FillBitmapWithOffsets(Bitmap bmp, Color fillColor, float dpiScale = 1f, params Point[][] offsetLists)
    {
        // If fillColor is Transparent and offsetLists only includes border-type lists, we still write those pixels.
        int w = bmp.Width, h = bmp.Height;
        var data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, bmp.PixelFormat);

        try
        {
            // Work with 32bppArgb memory layout: 4 bytes per pixel
            int stride = data.Stride;
            IntPtr scan0 = data.Scan0;
            unsafe
            {
                byte* basePtr = (byte*)scan0.ToPointer();
                // First, optionally clear to transparent
                for (int y = 0; y < h; y++)
                {
                    uint* row = (uint*)(basePtr + y * stride);
                    for (int x = 0; x < w; x++) row[x] = 0u;
                }

                // Helper to write a single ARGB uint
                uint colorArgb(uint c) => c;
                uint cval = (uint)ColorToArgb(fillColor); // may be 0 for transparent

                // If offsetLists contains multiple lists, they are all drawn using the same color passed.
                foreach (var list in offsetLists)
                {
                    if (list == null) continue;
                    foreach (var p in list)
                    {
                        int x = p.X;
                        int y = p.Y;
                        if ((uint)x >= (uint)w || (uint)y >= (uint)h) continue;
                        uint* dst = (uint*)(basePtr + y * stride) + x;
                        *dst = cval;
                    }
                }
            }
        }
        finally
        {
            bmp.UnlockBits(data);
        }
    }

    // Overload: write offsets with a single color (for marks)
    private static void FillBitmapWithOffsets(Bitmap bmp, Color color, Point[] offsets)
    {
        FillBitmapWithOffsets(bmp, color, 1f, new[] { offsets });
    }

    // Convert Color to ARGB uint (0xAARRGGBB)
    private static uint ColorToArgb(Color c)
    {
        uint a = (uint)c.A;
        uint r = (uint)c.R;
        uint g = (uint)c.G;
        uint b = (uint)c.B;
        return (a << 24) | (r << 16) | (g << 8) | b;
    }

    // -----------------------------
    // Helper: import offsets from an example Bitmap (useful to generate templates)
    // This scans the example PNG and emits two lists:
    //   - background offsets (non-transparent pixels that belong to background)
    //   - mark offsets (non-transparent pixels that belong to the mark)
    //
    // Heuristics: you may pass one or more markColors (exact colors) that identify the mark pixels.
    // If markColors is empty, the function will attempt to identify the darkest inner pixels as the mark.
    // -----------------------------
    public static void ImportOffsetsFromExample(Bitmap example, out Point[] backgroundOffsets, out Point[] markOffsets, params Color[] markColors)
    {
        if (example == null) throw new ArgumentNullException(nameof(example));
        int w = example.Width, h = example.Height;
        var bgList = new List<Point>();
        var markList = new List<Point>();

        // read pixel array
        var bmpData = example.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        try
        {
            int stride = bmpData.Stride;
            IntPtr scan0 = bmpData.Scan0;
            unsafe
            {
                byte* basePtr = (byte*)scan0.ToPointer();
                for (int y = 0; y < h; y++)
                {
                    uint* row = (uint*)(basePtr + y * stride);
                    for (int x = 0; x < w; x++)
                    {
                        uint px = row[x];
                        byte a = (byte)(px >> 24);
                        if (a == 0) continue;

                        byte r = (byte)(px >> 16);
                        byte g = (byte)(px >> 8);
                        byte b = (byte)(px);

                        bool isMark = false;
                        if (markColors != null && markColors.Length > 0)
                        {
                            foreach (var mc in markColors)
                            {
                                if (mc.R == r && mc.G == g && mc.B == b)
                                {
                                    isMark = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // heuristic: mark pixels are inside the box (not near edges)
                            if (x > 1 && x < w - 2 && y > 1 && y < h - 2)
                            {
                                // darker => mark
                                var lum = 0.2126 * r + 0.7152 * g + 0.0722 * b;
                                if (lum < 80) isMark = true;
                            }
                        }

                        if (isMark) markList.Add(new Point(x, y));
                        else bgList.Add(new Point(x, y));
                    }
                }
            }
        }
        finally
        {
            example.UnlockBits(bmpData);
        }

        backgroundOffsets = bgList.ToArray();
        markOffsets = markList.ToArray();
    }
}
