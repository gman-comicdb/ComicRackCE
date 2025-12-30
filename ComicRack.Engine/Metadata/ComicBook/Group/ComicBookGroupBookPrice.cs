using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookGroupBookPrice : SingleComicGrouper
{
    private readonly string[] captions = GroupInfo.TRGroup.GetStrings("PriceGroups", "Unknown|Free|0-10|10-20|20-30|30-40|40-50|50-100|over 100", '|');

    public override IGroupInfo GetGroup(ComicBook item)
    {
        int num = (!(item.BookPrice < 0f)) ? ((item.BookPrice == 0f) ? 1 : ((item.BookPrice is >= 0f and < 10f) ? 2 : ((item.BookPrice is >= 10f and < 20f) ? 3 : ((item.BookPrice is >= 20f and < 30f) ? 4 : ((item.BookPrice is >= 30f and < 40f) ? 5 : ((item.BookPrice is >= 40f and < 50f) ? 6 : ((item.BookPrice is < 50f or >= 100f) ? 8 : 7))))))) : 0;
        return new GroupInfo(captions[num], num);
    }
}
