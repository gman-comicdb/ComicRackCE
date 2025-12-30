using System;
using System.ComponentModel;
using System.Linq;

namespace cYo.Projects.ComicRack.Engine;

[Serializable]
[Description("Has Custom Values")]
[ComicBookMatcherHint("CustomValuesStore")]
public class ComicBookHasCustomValuesMatcher : ComicBookYesNoMatcher
{
    protected override YesNo GetValue(ComicBook comicBook)
    {
        return !comicBook.GetCustomValues().Any() ? YesNo.No : YesNo.Yes;
    }
}
