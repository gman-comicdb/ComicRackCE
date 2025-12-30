using System.Collections.Generic;

using cYo.Common.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controls;

public abstract class CoverViewItemComparer : Comparer<CoverViewItem>, IComparer<IViewableItem>
{
    protected abstract int OnCompare(CoverViewItem x, CoverViewItem y);

    public override int Compare(CoverViewItem x, CoverViewItem y)
    {
        return OnCompareInternal(x, y);
    }

    public int Compare(IViewableItem x, IViewableItem y)
    {
        return OnCompareInternal(x as CoverViewItem, y as CoverViewItem);
    }

    private int OnCompareInternal(CoverViewItem x, CoverViewItem y)
    {
        return x == null && y == null ? 0 : x == null ? -1 : y == null ? 1 : OnCompare(x, y);
    }
}
