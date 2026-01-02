using System;
using System.Collections.Generic;
using System.Linq;

using cYo.Common.Collections;

namespace cYo.Common.Windows.Forms;

[Serializable]
public class ItemViewGroupsStatus
{
    public enum GroupStatus
    {
        AllExpanded,
        AllCollapsed,
        KeysExpanded,
        KeysCollapsed
    }

    private HashSet<int> keys = new();

    public GroupStatus Status { get; set; }

    public HashSet<int> Keys => keys;

    public ItemViewGroupsStatus()
        : this(null)
    {
    }

    public ItemViewGroupsStatus(IEnumerable<GroupHeaderInformation> headers)
    {
        Status = GroupStatus.AllExpanded;
        if (headers == null)
        {
            return;
        }
        headers = headers.ToArray();
        int num = headers.Count();
        int num2 = headers.Count(h => h.Collapsed);
        int num3 = num - num2;
        if (num3 == num)
        {
            Status = GroupStatus.AllExpanded;
        }
        else if (num2 == num)
        {
            Status = GroupStatus.AllCollapsed;
        }
        else if (num2 < num3)
        {
            Status = GroupStatus.KeysCollapsed;
            keys.AddRange(from h in headers
                          where h.Collapsed
                          select h.Caption.GetHashCode());
        }
        else
        {
            Status = GroupStatus.KeysExpanded;
            keys.AddRange(from h in headers
                          where !h.Collapsed
                          select h.Caption.GetHashCode());
        }
    }

    public bool IsCollapsed(string caption)
    {
        return Status switch
        {
            GroupStatus.AllExpanded => false,
            GroupStatus.KeysCollapsed => keys.Contains(caption.GetHashCode()),
            GroupStatus.KeysExpanded => !keys.Contains(caption.GetHashCode()),
            _ => true,
        };
    }

    public bool IsCollapsed(GroupHeaderInformation header)
    {
        return IsCollapsed(header.Caption);
    }

    public bool IsExpanded(string caption)
    {
        return !IsCollapsed(caption);
    }

    public bool IsExpanded(GroupHeaderInformation header)
    {
        return IsExpanded(header.Caption);
    }
}
