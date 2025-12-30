using System.Drawing;

namespace cYo.Common.Windows.Forms;

public class ItemSizeInformation
{
    private Rectangle bounds;

    public Graphics Graphics { get; set; }

    public Rectangle Bounds
    {
        get => bounds;
        set => bounds = value;
    }

    public int Width
    {
        get => bounds.Width;
        set => bounds.Width = value;
    }

    public int Height
    {
        get => bounds.Height;
        set => bounds.Height = value;
    }

    public Size Size
    {
        get => bounds.Size;
        set => bounds.Size = value;
    }

    public ItemViewMode DisplayType { get; set; }

    public int Item { get; set; }

    public int GroupItem { get; set; }

    public int SubItem { get; set; }

    public IColumn Header { get; set; }
}
