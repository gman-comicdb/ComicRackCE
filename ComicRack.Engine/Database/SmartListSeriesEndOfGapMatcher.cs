using System;
using System.ComponentModel;

namespace cYo.Projects.ComicRack.Engine.Database;

[Serializable]
[Description("Series: End of Gap")]
[ComicBookMatcherHint("Series", "Volume", "FilePath", "EnableProposed", "Number", DisableOptimizedUpdate = true)]
public class SmartListSeriesEndOfGapMatcher : ComicBookYesNoMatcher
{
    protected override YesNo GetValue(ComicBook comicBook)
    {
        return base.StatsProvider != null
            ? !base.StatsProvider.GetSeriesStats(comicBook).IsGapEnd(comicBook) ? YesNo.No : YesNo.Yes
            : YesNo.Unknown;
    }
}
