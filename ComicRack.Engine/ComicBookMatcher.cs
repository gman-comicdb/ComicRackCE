using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

using cYo.Common.ComponentModel;
using cYo.Common.Localize;
using cYo.Common.Reflection;
using cYo.Common.Text;

namespace cYo.Projects.ComicRack.Engine;

[Serializable]
public abstract class ComicBookMatcher : IComicBookMatcher, IMatcher<ComicBook>, ICloneable
{
    public const string ClipboardFormat = "ComicBookMatcher";

    private static TR trMatcher;

    public const string SeriesStatsPropertyPrefix = "Stats";

    public static readonly string[] ComicProperties = ComicBook.GetProperties(onlyWritable: true).ToArray();

    public static readonly string[] SeriesStatsProperties = (from name in ComicBookSeriesStatistics.GetProperties()
                                                             select SeriesStatsPropertyPrefix + name).ToArray();

    [NonSerialized]
    private IComicBookStatsProvider statsProvider;

    private bool propertyCheckInitialized;

    private ISet<string> usedProperties;

    private bool isOptimizedCacheUpdateDisabled;

    private readonly string[] wildCardProperty =
    [
        "*"
    ];

    public static TR TRMatcher
    {
        get
        {
            trMatcher ??= TR.Load("Matchers");
            return trMatcher;
        }
    }

    [XmlAttribute]
    [DefaultValue(false)]
    public bool Not { get; set; }

    [XmlIgnore]
    public IComicBookStatsProvider StatsProvider
    {
        get => statsProvider;
        set => statsProvider = value;
    }

    public virtual bool IsOptimizedCacheUpdateDisabled
    {
        get
        {
            InitializeFromProperty();
            return isOptimizedCacheUpdateDisabled;
        }
    }

    public virtual bool TimeDependant => false;

    public abstract IEnumerable<ComicBook> Match(IEnumerable<ComicBook> items);

    public abstract object Clone();

    public virtual bool IsSame(ComicBookMatcher cbm)
    {
        return cbm != null && cbm.GetType() == GetType() ? cbm.Not == Not : false;
    }

    private void InitializeFromProperty()
    {
        if (!propertyCheckInitialized)
        {
            propertyCheckInitialized = true;
            if (Attribute.GetCustomAttribute(GetType(), typeof(ComicBookMatcherHintAttribute)) is ComicBookMatcherHintAttribute comicBookMatcherHintAttribute)
            {
                usedProperties = comicBookMatcherHintAttribute.Properties;
                isOptimizedCacheUpdateDisabled = comicBookMatcherHintAttribute.DisableOptimizedUpdate;
            }
        }
    }

    public virtual IEnumerable<string> GetDependentProperties()
    {
        InitializeFromProperty();
        return usedProperties == null ? wildCardProperty : usedProperties;
    }

    public virtual bool UsesProperty(string propertyHint)
    {
        InitializeFromProperty();
        return usedProperties != null ? usedProperties.Contains(propertyHint) : true;
    }

    public override string ToString()
    {
        return ConvertToString(this);
    }

    public static string ConvertToString(IComicBookMatcher matcher)
    {
        return (matcher.Not ? "Not " : string.Empty) + "[" + (matcher.GetType().Description().Escape("[]", '\\') ?? matcher.GetType().Name) + "]";
    }

    public static bool IsComicProperty(string prop)
    {
        return ComicProperties.Contains(prop);
    }

    public static bool IsSeriesStatsProperty(string prop)
    {
        return SeriesStatsProperties.Contains(prop);
    }

    public static string ParseSeriesProperty(string prop)
    {
        return prop.Substring(SeriesStatsPropertyPrefix.Length);
    }
}
