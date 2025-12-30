using System;
using System.ComponentModel;

namespace cYo.Projects.ComicRack.Engine.Database;

[Serializable]
[Description("Series: Book added")]
[ComicBookMatcherHint("Series", "Volume", "FilePath", "EnableProposed", "AddedTime", DisableOptimizedUpdate = true)]
public class SmartListSeriesLastAddedTimeMatcher : ComicBookDateMatcher
{
    protected override DateTime GetValue(ComicBook comicBook)
    {
        return base.StatsProvider != null ? StatsProvider.GetSeriesStats(comicBook).LastAddedTime : DateTime.MinValue;
    }
}
