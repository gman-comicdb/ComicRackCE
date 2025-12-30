using System;
using System.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

[Serializable]
[Description("Is Linked")]
[ComicBookMatcherHint("FilePath")]
public class ComicBookIsLinkedMatcher : ComicBookYesNoMatcher
{
    protected override YesNo GetValue(ComicBook comicBook)
    {
        return !comicBook.IsLinked ? YesNo.No : YesNo.Yes;
    }
}
