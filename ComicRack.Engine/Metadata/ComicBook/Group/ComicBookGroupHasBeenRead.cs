using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookGroupHasBeenRead : SingleComicGrouper
{
    private readonly string[] captions = GroupInfo.TRGroup.GetStrings("HasBeenReadGroups", "Read|Not Read", '|');

    public override IGroupInfo GetGroup(ComicBook item)
    {
        return !item.HasBeenRead ? new GroupInfo(captions[1], 1) : new GroupInfo(captions[0], 0);
    }
}
