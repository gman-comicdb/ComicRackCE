using System;
using System.ComponentModel;

using cYo.Common.Text;

namespace cYo.Projects.ComicRack.Engine;

[Serializable]
[Description("Number")]
[ComicBookMatcherHint("Number", "FilePath", "EnableProposed")]
public class ComicBookNumberMatcher : ComicBookNumericMatcher
{
    protected override float GetValue(ComicBook comicBook)
    {
        return !comicBook.ShadowNumber.TryParse(out float f, invariant: true) ? -1f : f;
    }
}
