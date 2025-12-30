using System;

using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine.Metadata.VirtualTags;

public class ComicBookVirtualTagGrouper : SingleComicGrouper
{
    private readonly IVirtualTag vtag;

    public ComicBookVirtualTagGrouper(IVirtualTag vtag)
    {
        this.vtag = vtag;
    }

    public override IGroupInfo GetGroup(ComicBook item)
    {
        return SingleComicGrouper.GetNameGroup(item.GetVirtualTagValue(vtag.ID));
    }

    public override ComicBookMatcher CreateMatcher(IGroupInfo info)
    {
        ComicBookVirtualTagMatcher val = Activator.CreateInstance(ComicBookVirtualTagMatcher.GetMatcher(vtag)) as ComicBookVirtualTagMatcher;
        val.MatchOperator = info.Caption.Contains(",") ? ComicBookStringMatcher.OperatorListContains : ComicBookStringMatcher.OperatorEquals;
        val.MatchValue = info.Caption;
        return val;
    }
}
