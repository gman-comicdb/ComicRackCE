using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookGroupChecked : SingleComicGrouper
{
    private readonly string[] captions = GroupInfo.TRGroup.GetStrings("CheckedGroups", "Checked|Not Checked", '|');

    public override IGroupInfo GetGroup(ComicBook item)
    {
        return !item.Checked ? new GroupInfo(captions[1], 1) : new GroupInfo(captions[0], 0);
    }
}
