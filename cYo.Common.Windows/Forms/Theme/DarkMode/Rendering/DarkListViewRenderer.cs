using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using System.Drawing;
using System.Windows.Forms;
namespace cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;

internal static partial class DarkListViewRenderer
{

    internal static void DrawColumnHeader(Graphics g, Rectangle bounds, string text, Font font)
    {
        g.DrawDarkBackground(bounds, DarkColors.Header.Back);

        g.DrawDarkColumnSeparator(bounds, DarkColors.Header.Separator);

        // Draw the header text with custom color and font
        StringFormat stringFormat = new StringFormat
        {
            Alignment = StringAlignment.Near, // left align text
            LineAlignment = StringAlignment.Center, // vertically center text
            Trimming = StringTrimming.EllipsisCharacter
        };
        g.DrawString(text, font, DarkBrushes.Header.Text, bounds, stringFormat);
    }

    internal static void DrawSubItem(DrawListViewSubItemEventArgs e)
    {
        Color textColor = e.SubItem.ForeColor;

        if (e.ItemIndex == -1)
        {
            textColor = e.Item.ForeColor;
        }

        if (e.Item.Selected || (e.ItemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
        {
            textColor = SystemColors.HighlightText; // currently same as ForeColor
        }
    }

}
