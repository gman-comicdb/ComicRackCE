using System;
using System.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

[Serializable]
[Description("Modified Info")]
[ComicBookMatcherHint("ComicInfoIsDirty")]
public class ComicBookModifiedInfoMatcher : ComicBookYesNoMatcher
{
    protected override YesNo GetValue(ComicBook comicBook)
    {
        return !comicBook.ComicInfoIsDirty ? YesNo.No : YesNo.Yes;
    }
}
