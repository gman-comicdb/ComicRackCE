using System.Collections.Generic;

using cYo.Common.Collections;
using cYo.Common.ComponentModel;

namespace cYo.Common.Windows.Forms;

public class ItemViewColumnCollection<T> : SmartList<T> where T : IColumn
{
    public T FindById(int id)
    {
        return Find(h => h.Id == id);
    }

    public T FindBySorter(IComparer<IViewableItem> comp)
    {
        return comp != null ? Find(h => h.ColumnSorter == comp) : default;
    }

    public T FindByGrouper(IGrouper<IViewableItem> comp)
    {
        return comp != null ? Find(h => h.ColumnGrouper == comp) : default;
    }
}
