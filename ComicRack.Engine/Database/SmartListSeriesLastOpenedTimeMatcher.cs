using System;
using System.ComponentModel;

namespace cYo.Projects.ComicRack.Engine.Database;

[Serializable]
[Description("Series: Opened")]
[ComicBookMatcherHint("Series", "Volume", "FilePath", "EnableProposed", "OpenedTime", DisableOptimizedUpdate = true)]
public class SmartListSeriesLastOpenedTimeMatcher : ComicBookDateMatcher
{
    protected override DateTime GetValue(ComicBook comicBook)
    {
        return base.StatsProvider != null ? StatsProvider.GetSeriesStats(comicBook).LastOpenedTime : DateTime.MinValue;
    }
}
