using System.Collections.Generic;

using cYo.Common.Collections;
using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookMetadataCollection : SmartList<ComicBookMetadata>
{
    public ComicBookMetadata FindById(int id)
    {
        return Find(h => h.Id == id);
    }

    public ComicBookMetadata FindBySorter(IComparer<ComicBook> comp)
    {
        return comp != null ? Find(h => h.GetComparer<ComicBook>() == comp) : default;
    }

    public ComicBookMetadata FindByGrouper(IGrouper<ComicBook> comp)
    {
        return comp != null ? Find(h => h.GetGrouper<ComicBook>() == comp) : default;
    }
}
