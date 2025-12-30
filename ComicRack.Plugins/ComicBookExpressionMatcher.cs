using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

using cYo.Projects.ComicRack.Engine;

namespace cYo.Projects.ComicRack.Plugins;

[Serializable]
[Description("Expression")]
[ComicBookMatcherHint(true)]
public class ComicBookExpressionMatcher : ComicBookValueMatcher<bool>
{
    private const string BookVariableName = "__book";

    private const string BookStatsVariableName = "__bookStats";

    private static readonly string[] opListNeutral = "is true|is false".Split('|');

    private static readonly string[] opList = ComicBookMatcher.TRMatcher.GetStrings("TrueFalseOperators", "is True|is False", '|');

    private readonly HashSet<string> properties = new();

    private bool usesStatistics;

    private string parsedMatchValue;

    [NonSerialized]
    private Func<ComicBook, IComicBookStatsProvider, bool> expression;

    [NonSerialized]
    private bool error;

    public override bool IsOptimizedCacheUpdateDisabled => !base.IsOptimizedCacheUpdateDisabled ? usesStatistics : true;

    public override string[] OperatorsListNeutral => opListNeutral;

    public override string[] OperatorsList => opList;

    public override int ArgumentCount => 1;

    protected override bool MatchBook(ComicBook book, bool value)
    {
        int matchOperator = MatchOperator;
        return matchOperator is 0 or not 1 ? value : !value;
    }

    protected override void OnMatchValueChanged()
    {
        base.OnMatchValueChanged();
        properties.Clear();
        parsedMatchValue = ComicBookValueMatcher<bool>.FieldExpression.Replace(MatchValue, delegate (Match e)
        {
            string newName = e.Groups[1].Value;
            if (ComicBookMatcher.IsComicProperty(newName))
            {
                if (ComicBook.MapPropertyName(newName, out newName, ComicValueType.Shadow))
                {
                    properties.Add(newName);
                    properties.Add("EnableProposed");
                    properties.Add("FilePath");
                }
                return $"{BookVariableName}.{newName}";
            }
            if (ComicBookMatcher.IsSeriesStatsProperty(newName))
            {
                usesStatistics = true;
                return $"{BookStatsVariableName}.GetSeriesStats({BookVariableName}).{ComicBookMatcher.ParseSeriesProperty(newName)}";
            }
            return string.Empty;
        });
        expression = null;
    }

    public override IEnumerable<string> GetDependentProperties()
    {
        return base.GetDependentProperties().Concat(properties);
    }

    public override bool UsesProperty(string propertyHint)
    {
        return !usesStatistics && !properties.Contains(propertyHint) ? base.UsesProperty(propertyHint) : true;
    }

    protected override void OnInitializeMatch()
    {
        base.OnInitializeMatch();
        error = false;
        try
        {
            expression ??= PythonCommand.CompileExpression<Func<ComicBook, IComicBookStatsProvider, bool>>(parsedMatchValue,
                [
                    BookVariableName,
                    BookStatsVariableName
                ]);
        }
        catch (Exception)
        {
            error = true;
        }
    }

    protected override bool GetValue(ComicBook comicBook)
    {
        if (error)
        {
            return false;
        }
        try
        {
            return expression(comicBook, base.StatsProvider);
        }
        catch
        {
            error = false;
            return false;
        }
    }
}
