using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookGroupBlackAndWhite : SingleComicGrouper
{
    private readonly string[] captions = GroupInfo.TRGroup.GetStrings("BWGroups", "Black and White|Color|Unknown", '|');

    public override IGroupInfo GetGroup(ComicBook item)
    {
        return item.BlackAndWhite switch
        {
            YesNo.Yes => new GroupInfo(captions[0], 0),
            YesNo.No => new GroupInfo(captions[1], 1),
            _ => new GroupInfo(captions[2], 2),
        };
    }
}
