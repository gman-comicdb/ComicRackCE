using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookGroupAlternateNumber : SingleComicGrouper
{
    public override IGroupInfo GetGroup(ComicBook item)
    {
        return item.CompareAlternateNumber.IsNumber
            ? ItemGroupCount.GetNumberGroup((int)item.CompareAlternateNumber.Number)
            : SingleComicGrouper.GetNameGroup(item.CompareAlternateNumber.Text);
    }
}
