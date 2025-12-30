using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Engine;

public class ComicBookGroupNumber : SingleComicGrouper
{
    public override IGroupInfo GetGroup(ComicBook item)
    {
        return item.CompareNumber.IsNumber
            ? ItemGroupCount.GetNumberGroup((int)item.CompareNumber.Number)
            : SingleComicGrouper.GetNameGroup(item.CompareNumber.Text);
    }
}
