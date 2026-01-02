using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace cYo.Projects.ComicRack.Engine;

[Serializable]
public abstract class ComicBookStringMatcher : ComicBookValueMatcher<string>
{
    public const int OperatorEquals = 0;

    public const int OperatorContains = 1;

    public const int OperatorContainsAny = 2;

    public const int OperatorContainsAll = 3;

    public const int OperatorStartsWith = 4;

    public const int OperatorEndsWith = 5;

    public const int OperatorListContains = 6;

    public const int OperatorRegex = 7;

    private static readonly string[] opListNeutral = "equals|contains|contains any of|contains all of|starts with|ends with|list contains|regex".Split('|');

    private static readonly string[] opList = ComicBookMatcher.TRMatcher.GetStrings("StringOperators", "is|contains|contains any of|contains all of|starts with|ends with|list contains|regular expression", '|');

    private string[] parsedMatchValues = [];

    private Regex rxList;

    private Regex rxMatch;

    private bool ignoreCase = true;

    public override int ArgumentCount => 1;

    public virtual int MatchColumn => 0;

    public override string[] OperatorsListNeutral => opListNeutral;

    public override string[] OperatorsList => opList;

    [XmlAttribute]
    [DefaultValue(true)]
    public bool IgnoreCase
    {
        get => ignoreCase;
        set => ignoreCase = value;
    }

    protected override bool MatchBook(ComicBook comicBook, string value)
    {
        if (!PreCheck(comicBook))
        {
            return false;
        }
        StringComparison sc = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
        value ??= string.Empty;
        switch (MatchOperator)
        {
            case OperatorEquals://is
                return string.Equals(value, GetMatchValue(comicBook), sc);
            case OperatorContains://contains
                {
                    string matchValue = GetMatchValue(comicBook);
                    return !string.IsNullOrEmpty(matchValue) ? value.IndexOf(matchValue, sc) != -1 : true;
                }
            case OperatorContainsAny://contains any of
                if (parsedMatchValues.Length != 0)
                {
                    return parsedMatchValues.Any(s => value.IndexOf(s, sc) != -1);
                }
                return true;
            case OperatorContainsAll://contains all of
                if (!string.IsNullOrEmpty(value))
                {
                    return parsedMatchValues.All(s => value.IndexOf(s, sc) != -1);
                }
                return false;
            case OperatorStartsWith://starts with
                {
                    string matchValue2 = GetMatchValue(comicBook);
                    return !string.IsNullOrEmpty(matchValue2) ? value.StartsWith(matchValue2, sc) : true;
                }
            case OperatorEndsWith://ends with
                {
                    string matchValue3 = GetMatchValue(comicBook);
                    return !string.IsNullOrEmpty(matchValue3) ? value.EndsWith(matchValue3, sc) : true;
                }
            case OperatorListContains://list contains
                if (rxList != null)
                {
                    return rxList.Match(value).Success;
                }
                return false;
            case OperatorRegex://regular expression
                if (rxMatch != null)
                {
                    return rxMatch.Match(value).Success;
                }
                return false;
            default:
                return false;
        }
    }

    protected override string GetMatchValue(ComicBook comicBook)
    {
        return ((MatchColumn == 0) ? base.GetMatchValue(comicBook) : base.GetMatchValue2(comicBook)) ?? string.Empty;
    }

    protected override string GetMatchValue2(ComicBook comicBook)
    {
        return ((MatchColumn != 0) ? base.GetMatchValue(comicBook) : base.GetMatchValue2(comicBook)) ?? string.Empty;
    }

    protected virtual bool PreCheck(ComicBook comicBook)
    {
        return true;
    }

    protected override void OnMatchValueChanged()
    {
        base.OnMatchValueChanged();
        parsedMatchValues = Regex.Split((MatchColumn == 0) ? MatchValue : MatchValue2, @"[,;]|(?<![,;].*)\s+(?!.*[,;])").Where(x => !string.IsNullOrEmpty(x.Trim())).Select(x => x.Trim()).ToArray();
        try
        {
            rxList = new Regex($"(?<=^|[,;])\\s*{Regex.Escape(MatchValue.Trim())}\\s*(?=$|[,;])", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }
        catch
        {
            rxList = null;
        }
        try
        {
            rxMatch = new Regex((MatchColumn == 0) ? MatchValue : MatchValue2);
        }
        catch
        {
            rxMatch = null;
        }
    }
}
