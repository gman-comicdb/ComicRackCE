using System.Collections.Generic;

using cYo.Common.Text;

namespace cYo.Projects.ComicRack.Engine.Metadata.VirtualTags;

public class ComicBookVirtualTagComparer : Comparer<ComicBook>
{
    private readonly string _property;

    public ComicBookVirtualTagComparer(string property)
    {
        _property = property;
    }

    public override int Compare(ComicBook x, ComicBook y)
    {
        return ExtendedStringComparer.Compare(x.GetStringPropertyValue(_property), y.GetStringPropertyValue(_property), ExtendedStringComparison.IgnoreCase | ExtendedStringComparison.IgnoreArticles);
    }
}
