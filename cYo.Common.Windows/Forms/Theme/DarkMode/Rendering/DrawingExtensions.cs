using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using cYo.Common.Windows.Forms.Theme.Resources;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;

internal static class DrawingExtensions
{
    #region ListView
    internal static void DrawDarkColumnHeader(this DrawListViewColumnHeaderEventArgs e)
    {
        e.DrawDarkBackground();
        e.DrawDarkSeparator();
        e.DrawDarkString();
    }

    internal static void DrawDarkSubItem(this DrawListViewSubItemEventArgs e)
    {
        e.DrawDarkBackground();
        e.DrawDarkFocusRectangle();
        e.DrawDarkContents();
    }

    #region ListViewColumnHeader
    private static void DrawDarkBackground(this DrawListViewColumnHeaderEventArgs e)
        => e.Graphics.DrawDarkBackground(e.Bounds, DarkColors.Header.Back);

    private static void DrawDarkSeparator(this DrawListViewColumnHeaderEventArgs e)
        => e.Graphics.DrawDarkColumnSeparator(e.Bounds, DarkColors.Header.Separator);

    private static void DrawDarkString(this DrawListViewColumnHeaderEventArgs e)
    {
        using (StringFormat stringFormat = new StringFormat
        {
            Alignment = StringAlignment.Near, // left align text
            LineAlignment = StringAlignment.Center, // vertically center text
            Trimming = StringTrimming.EllipsisCharacter
        })
            e.Graphics.DrawDarkString(e.Header.Text, e.Font, DarkColors.Header.Text, e.Bounds, stringFormat);
    }
    #endregion

    #region ListViewSubItem
    private static void DrawDarkBackground(this DrawListViewSubItemEventArgs e)
    {
        Color backColor = e.ItemIndex == -1 ? e.Item.BackColor : e.SubItem.BackColor;
        if (e.Item.Selected || (e.ItemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
            backColor = DarkColors.SelectedText.Highlight;
        e.Graphics.DrawDarkBackground(e.Bounds, backColor);
    }

    private static void DrawDarkFocusRectangle(this DrawListViewSubItemEventArgs e)
    {
        if (e.Item.Selected || (e.ItemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
            e.Graphics.DrawDarkFocusRectangle(e.Bounds);
    }

    private static void DrawDarkContents(this DrawListViewSubItemEventArgs e)
    {
        Color textColor = e.ItemIndex == -1 ? e.Item.ForeColor : e.SubItem.ForeColor;
        Color backColor = e.ItemIndex == -1 ? e.Item.BackColor : e.SubItem.BackColor;

        if (e.Item.Selected || (e.ItemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
        {
            textColor = SystemColors.HighlightText;
            backColor = DarkColors.SelectedText.Highlight;
        }
            
        Rectangle textBounds = e.Bounds;
        int leftPad = e.ColumnIndex == 0 ? 4 : 2;
        leftPad += DrawSubItem.CheckBox(e, leftPad);
        leftPad += DrawSubItem.Image(e, leftPad);

        textBounds.X += leftPad;
        textBounds.Width = e.Bounds.Right - textBounds.X;

        // Draw text
        if (e.ItemIndex == -1)
            DrawSubItem.ItemText(e, textBounds, textColor, backColor);
        else
            DrawSubItem.Text(e, textBounds, textColor, backColor);

        // Draw column divider
        if (e.ColumnIndex < e.Item.ListView.Columns.Count)
            DrawSubItem.ColumnDivider(e.Graphics, e.Bounds);
    }

    private static class DrawSubItem
    {
        public static void ColumnDivider(Graphics g, Rectangle bounds)
        {
            using (var pen = new Pen(Color.FromArgb(77, 77, 77)))
                g.DrawLine(pen, bounds.Right - 2, bounds.Top, bounds.Right - 2, bounds.Bottom);
        }

        public static int CheckBox(DrawListViewSubItemEventArgs e, int padding)
        {
            if (e.ColumnIndex != 0 || e.Item is null || !e.Item.ListView.CheckBoxes)
                return 0;

            ImageList checkImageList = e.Item.ListView.StateImageList;

            if (checkImageList is not null)
                return checkImageList.ImageSize.Width;
            else
                return 16;
        }

        public static int Image(DrawListViewSubItemEventArgs e, int padding)
        {
            if (e.ColumnIndex != 0 || e.Item is null || e.Item.ImageList is null)
                return 0;

            ImageList imageList = e.Item.ImageList;

            int imageWidth = imageList.ImageSize.Width;
            int imageHeight = imageList.ImageSize.Height;
            int imageY = e.Bounds.Top + (e.Bounds.Height - imageHeight) / 2;
            int imageX = e.Bounds.Left + padding; // small padding from left

            // Draw the image if there is one to draw
            int imageIndex = e.Item.ImageIndex;
            if (imageIndex >= 0 && imageIndex < imageList.Images.Count)
                imageList.Draw(e.Graphics, imageX, imageY, imageWidth, imageHeight, imageIndex);

            // Shift text based on imageList != null, whether this item has an image or not
            //return imageX + imageWidth;
            return imageWidth;
        }

        public static void Text(DrawListViewSubItemEventArgs e, Rectangle textBounds, Color textColor, Color backColor)
        {
            TextRenderer.DrawText(
                e.Graphics,
                e.SubItem.Text,
                e.SubItem.Font,
                textBounds,
                textColor,
                backColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        public static void ItemText(DrawListViewSubItemEventArgs e, Rectangle textBounds, Color textColor, Color backColor)
        {
            TextRenderer.DrawText(
                e.Graphics,
                e.Item.Text,
                e.Item.Font,
                textBounds,
                textColor,
                backColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }
    }
    #endregion

    #endregion

    #region Paint
    internal static void PaintDarkCheckBox(this PaintEventArgs e, CheckBox checkBox)
        => PaintDarkCheckBox(e, checkBox, false);

    internal static void PaintDarkCheckBox(this PaintEventArgs e, CheckBox checkBox, bool textOnly)
    {
        TextFormatFlags textFormatFlags = Helpers.GetTextFormatFlags(checkBox);
        Rectangle boxBounds = Helpers.GetCheckRectangle(checkBox, e.Graphics);
        Rectangle textBounds = checkBox.Appearance == Appearance.Button ? e.ClipRectangle : Helpers.GetTextRectangle(checkBox, e.Graphics, boxBounds);
        Color textColor = checkBox.Enabled ? checkBox.ForeColor : SystemColors.GrayText;
        //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

        e.Graphics.Clear(checkBox.BackColor);

        if (!textOnly)
            DarkCheckBoxRenderer.DrawCheckBox(e.Graphics, boxBounds, checkBox.CheckState, checkBox.Enabled);
        DarkCheckBoxRenderer.DrawCheckBoxText(e.Graphics, textBounds, checkBox.BackColor, textColor, checkBox.Text, checkBox.Font, textFormatFlags);
    }
    
    internal static void DrawDarkBackground(this PaintEventArgs e, Rectangle bounds, Color backColor)
    {
        e.Graphics.Clear(backColor); // emulating non-VisualStyleRenderer path for now
    }
    #endregion

    #region DrawItem
    internal static void DrawDarkFocusRectangle(this DrawItemEventArgs e)
    {
        if ((e.State & DrawItemState.Focus) == DrawItemState.Focus && (e.State & DrawItemState.NoFocusRect) != DrawItemState.NoFocusRect)
            e.Graphics.DrawDarkFocusRectangle(e.Bounds);
            //ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds, Color.Red, Color.Cyan);
    }

    internal static void DrawDarkBackground(this DrawItemEventArgs e)
    {
        if (e.State.HasFlag(DrawItemState.Selected))
            e.Graphics.DrawDarkBackground(e.Bounds, DarkColors.SelectedText.Highlight);
        else
            e.Graphics.DrawDarkBackground(e.Bounds, e.BackColor);
    }

    internal static void DrawDarkBackground(this DrawItemEventArgs e, Color backColor)
    {
        if (e.State.HasFlag(DrawItemState.Selected))
            e.Graphics.DrawDarkBackground(e.Bounds, DarkColors.SelectedText.Highlight);
        else
            e.Graphics.DrawDarkBackground(e.Bounds, backColor);
    }

    internal static void DrawDarkString(this DrawItemEventArgs e, string text)
    {
        using (StringFormat format = new StringFormat(StringFormatFlags.NoWrap)
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Near
        })
            e.Graphics.DrawDarkString(text, e.Font, e.ForeColor, e.Bounds, format);
    }
    #endregion

    #region DrawToolTip
    internal static void DrawDarkBackground(this DrawToolTipEventArgs e, Color? backColor = null)
        => e.Graphics.DrawDarkBackground(e.Bounds, backColor ?? DarkColors.ToolTip.Back);

    internal static void DrawDarkBorder(this DrawToolTipEventArgs e) //Color? backColor = null
        => e.Graphics.DrawDarkBorder(e.Bounds);

    internal static void DrawDarkText(this DrawToolTipEventArgs e, Color? textColor = null)
        => TextRenderer.DrawText(e.Graphics, e.ToolTipText, e.Font, e.Bounds, textColor ?? ThemeColors.ToolTip.InfoText);

    internal static void DrawDarkText(this DrawToolTipEventArgs e, Rectangle bounds, Color? textColor = null)
        => TextRenderer.DrawText(e.Graphics, e.ToolTipText, e.Font, bounds, textColor ?? ThemeColors.ToolTip.InfoText);
    #endregion
}
