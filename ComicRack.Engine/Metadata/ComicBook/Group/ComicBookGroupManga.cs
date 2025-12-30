using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookGroupManga : SingleComicGrouper
{
    private readonly string[] captions = GroupInfo.TRGroup.GetStrings("MangaGroups", "Manga (Right to Left)|Manga|No Manga|Unknown", '|');

    public override IGroupInfo GetGroup(ComicBook item)
    {
        return item.Manga switch
        {
            MangaYesNo.YesAndRightToLeft => new GroupInfo(captions[0], 0),
            MangaYesNo.Yes => new GroupInfo(captions[1], 1),
            MangaYesNo.No => new GroupInfo(captions[2], 2),
            _ => new GroupInfo(captions[3], 3),
        };
    }
}
