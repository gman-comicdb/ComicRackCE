using System;
using System.ComponentModel;

namespace cYo.Projects.ComicRack.Engine.Database;

[Serializable]
[Description("Series: Published")]
[ComicBookMatcherHint("Series", "Volume", "FilePath", "EnableProposed", "Year", "Month", "Day", DisableOptimizedUpdate = true)]
public class SmartListSeriesLastPublishedTimeMatcher : ComicBookDateMatcher
{
    protected override DateTime GetValue(ComicBook comicBook)
    {
        return base.StatsProvider != null ? StatsProvider.GetSeriesStats(comicBook).LastPublishedTime : DateTime.MinValue;
    }
}
