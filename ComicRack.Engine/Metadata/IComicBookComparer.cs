using System.Collections.Generic;

namespace cYo.Projects.ComicRack.Engine;

/// <summary>
/// Used to expose a comparer for ComicBook objects.
/// </summary>
public interface IComicBookComparer
{
    public IComparer<ComicBook> Comparer { get; }
}
