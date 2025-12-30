using System;
using System.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

[Serializable]
[Description("Is Missing")]
[ComicBookMatcherHint("FileIsMissing", DisableOptimizedUpdate = true)]
public class ComicBookIsMissingMatcher : ComicBookYesNoMatcher
{
    protected override YesNo GetValue(ComicBook comicBook)
    {
        return comicBook.IsLinked && comicBook.FileIsMissing ? YesNo.Yes : YesNo.No;
    }
}
