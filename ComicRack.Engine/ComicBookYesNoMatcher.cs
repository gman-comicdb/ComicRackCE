using System;

namespace cYo.Projects.ComicRack.Engine;

[Serializable]
public abstract class ComicBookYesNoMatcher : ComicBookValueMatcher<YesNo>
{
    private static readonly string[] opListNeutral = "equals yes|equals no|equals unknown".Split('|');

    private static readonly string[] opList = ComicBookMatcher.TRMatcher.GetStrings("YesNoOperators", "is Yes|is No|is Unknown", '|');

    public override string[] OperatorsListNeutral => opListNeutral;

    public override string[] OperatorsList => opList;

    public override int ArgumentCount => 0;

    protected override bool MatchBook(ComicBook comicBook, YesNo yesNo)
    {
        return MatchOperator switch
        {
            1 => yesNo == YesNo.No,
            2 => yesNo == YesNo.Unknown,
            _ => yesNo == YesNo.Yes,
        };
    }
}
