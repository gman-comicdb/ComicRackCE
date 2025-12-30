using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookGroupEnableProposed : SingleComicGrouper
{
    private readonly string[] captions = GroupInfo.TRGroup.GetStrings("EnableProposedGroups", "Proposed Values used|Proposed Values no used", '|');

    public override IGroupInfo GetGroup(ComicBook item)
    {
        return !item.EnableProposed ? new GroupInfo(captions[1], 1) : new GroupInfo(captions[0], 0);
    }
}
