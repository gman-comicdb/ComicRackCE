using System.Collections.Generic;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookIdComparer : Comparer<ComicBook>
{
    public override int Compare(ComicBook x, ComicBook y)
    {
        return x.Id.CompareTo(y.Id);
    }
}
