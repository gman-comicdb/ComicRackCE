using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookGroupLinked : SingleComicGrouper
{
    private readonly string[] captions = GroupInfo.TRGroup.GetStrings("LinkedGroups", "Linked to File|Not linked to File", '|');

    public override IGroupInfo GetGroup(ComicBook item)
    {
        return !item.IsLinked ? new GroupInfo(captions[1], 1) : new GroupInfo(captions[0], 0);
    }
}
