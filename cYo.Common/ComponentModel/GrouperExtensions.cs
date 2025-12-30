using System.Collections.Generic;
using System.Linq;

using cYo.Common.Collections;

namespace cYo.Common.ComponentModel;

public static class GrouperExtensions
{
    public static IGrouper<T> Append<T>(this IGrouper<T> itemGrouper, IGrouper<T> grouper, int max, bool removeIfContained = false)
    {
        if (itemGrouper == null)
        {
            return grouper;
        }
        List<IGrouper<T>> list = new();
        if (itemGrouper is not CompoundSingleGrouper<T> compoundSingleGrouper)
        {
            list.Add(itemGrouper);
        }
        else
        {
            list.AddRange(compoundSingleGrouper.Groupers);
        }
        if (list.Contains(grouper))
        {
            list.Remove(grouper);
        }
        else
        {
            list.Add(grouper);
        }
        return list.Count != 0 ? new CompoundSingleGrouper<T>(list.Take(max).ToArray()) : (IGrouper<T>)null;
    }

    public static bool Contains<T>(this IGrouper<T> itemGrouper, IGrouper<T> grouper)
    {
        return (itemGrouper as CompoundSingleGrouper<T>)?.Groupers.Contains(grouper) ?? (itemGrouper == grouper);
    }

    public static IEnumerable<IGrouper<T>> GetGroupers<T>(this IGrouper<T> itemGrouper)
    {
        return itemGrouper == null
            ? []
            : itemGrouper is not CompoundSingleGrouper<T> compoundSingleGrouper ? ListExtensions.AsEnumerable<IGrouper<T>>(itemGrouper) : compoundSingleGrouper.Groupers;
    }

    public static IGrouper<T> First<T>(this IGrouper<T> itemGrouper)
    {
        return itemGrouper.GetGroupers().FirstOrDefault();
    }
}
