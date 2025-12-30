using System;
using System.ComponentModel;

namespace cYo.Projects.ComicRack.Engine.Database;

[Serializable]
[Description("Series: Start of Gap")]
[ComicBookMatcherHint("Series", "Volume", "FilePath", "EnableProposed", "Number", DisableOptimizedUpdate = true)]
public class SmartListSeriesStartOfGapMatcher : ComicBookYesNoMatcher
{
    protected override YesNo GetValue(ComicBook comicBook)
    {
        return base.StatsProvider != null
            ? !base.StatsProvider.GetSeriesStats(comicBook).IsGapStart(comicBook) ? YesNo.No : YesNo.Yes
            : YesNo.Unknown;
    }
}
