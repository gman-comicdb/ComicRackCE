using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace cYo.Projects.ComicRack.Engine.Database;

[Serializable]
[Description("Series: First Number")]
[ComicBookMatcherHint("Series", "Volume", "FilePath", "EnableProposed", "Number", DisableOptimizedUpdate = true)]
[XmlType("SmartListSeriesMinNumbertMatcher")]
public class SmartListSeriesFirstNumberMatcher : ComicBookNumericMatcher
{
    protected override float GetValue(ComicBook comicBook)
    {
        return base.StatsProvider != null ? StatsProvider.GetSeriesStats(comicBook).FirstNumber : 0f;
    }
}
