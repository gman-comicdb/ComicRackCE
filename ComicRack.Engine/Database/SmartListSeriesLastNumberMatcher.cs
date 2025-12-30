using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace cYo.Projects.ComicRack.Engine.Database;

[Serializable]
[Description("Series: Last Number")]
[ComicBookMatcherHint("Series", "Volume", "FilePath", "EnableProposed", "Number", DisableOptimizedUpdate = true)]
[XmlType("SmartListSeriesMaxNumbertMatcher")]
public class SmartListSeriesLastNumberMatcher : ComicBookNumericMatcher
{
    protected override float GetValue(ComicBook comicBook)
    {
        return base.StatsProvider != null ? StatsProvider.GetSeriesStats(comicBook).LastNumber : 0f;
    }
}
