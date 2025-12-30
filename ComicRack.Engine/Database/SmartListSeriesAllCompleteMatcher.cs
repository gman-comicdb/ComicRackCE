using System;
using System.ComponentModel;

namespace cYo.Projects.ComicRack.Engine.Database;

[Serializable]
[Description("Series: All complete")]
[ComicBookMatcherHint("Series", "Volume", "FilePath", "EnableProposed", "SeriesComplete", DisableOptimizedUpdate = true)]
public class SmartListSeriesAllCompleteMatcher : ComicBookYesNoMatcher
{
    protected override YesNo GetValue(ComicBook comicBook)
    {
        return base.StatsProvider != null ? StatsProvider.GetSeriesStats(comicBook).AllComplete : YesNo.Unknown;
    }
}
