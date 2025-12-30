using System;
using System.ComponentModel;

namespace cYo.Projects.ComicRack.Engine.Database;

[Serializable]
[Description("Series: Book released")]
[ComicBookMatcherHint("Series", "Volume", "FilePath", "EnableProposed", "ReleasedTime", DisableOptimizedUpdate = true)]
public class SmartListSeriesLastReleasedTimeMatcher : ComicBookDateMatcher
{
    protected override DateTime GetValue(ComicBook comicBook)
    {
        return base.StatsProvider != null ? StatsProvider.GetSeriesStats(comicBook).LastReleasedTime : DateTime.MinValue;
    }
}
