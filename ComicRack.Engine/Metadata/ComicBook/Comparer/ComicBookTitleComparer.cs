using System.Collections.Generic;

using cYo.Common.ComponentModel;
using cYo.Common.Text;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookTitleComparer : Comparer<ComicBook>
{
    private static readonly IComparer<ComicBook>[] list =
    [
        new ComicBookVolumeComparer(),
        new ComicBookNumberComparer()
    ];

    public override int Compare(ComicBook x, ComicBook y)
    {
        int num = ExtendedStringComparer.Compare(x.ShadowTitle, y.ShadowTitle, ExtendedStringComparison.IgnoreArticles | ExtendedStringComparison.IgnoreCase);
        return num != 0 ? num : list.Compare(x, y);
    }
}
