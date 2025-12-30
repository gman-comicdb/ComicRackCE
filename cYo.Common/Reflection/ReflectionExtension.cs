using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace cYo.Common.Reflection;

public static class ReflectionExtension
{
    public static IEnumerable<T> GetAttributes<T>(this MemberInfo member, bool inherit = true)
    {
        return member.GetCustomAttributes(typeof(T), inherit).OfType<T>();
    }

    public static T GetAttribute<T>(this MemberInfo member, bool inherit = true)
    {
        return member.GetAttributes<T>(inherit).FirstOrDefault();
    }

    public static bool HasAttribute<T>(this MemberInfo member, bool inherit = true)
    {
        return member.GetAttribute<T>(inherit) != null;
    }

    public static IEnumerable<T> GetAttributes<T>(this Type member, bool inherit = true)
    {
        return member.GetCustomAttributes(typeof(T), inherit).OfType<T>();
    }

    public static T GetAttribute<T>(this Type member, bool inherit = true)
    {
        return member.GetAttributes<T>(inherit).FirstOrDefault();
    }

    public static bool Browsable(this MemberInfo member, bool forced = false)
    {
        return member.GetAttribute<BrowsableAttribute>()?.Browsable ?? (!forced);
    }

    public static string Category(this MemberInfo member)
    {
        CategoryAttribute attribute = member.GetAttribute<CategoryAttribute>();
        return attribute != null && !string.IsNullOrEmpty(attribute.Category) ? attribute.Category : null;
    }

    public static string Description(this MemberInfo member)
    {
        DescriptionAttribute attribute = member.GetAttribute<DescriptionAttribute>();
        return attribute != null && !string.IsNullOrEmpty(attribute.Description) ? attribute.Description : null;
    }

    public static T DefaultValue<T>(this MemberInfo member, T v = default)
    {
        DefaultValueAttribute attribute = member.GetAttribute<DefaultValueAttribute>();
        return attribute != null ? (T)Convert.ChangeType(attribute.Value, typeof(T)) : v;
    }

    public static object DefaultValue(this MemberInfo member)
    {
        return member.GetAttribute<DefaultValueAttribute>()?.Value;
    }

    public static bool HasDefaultValue(this MemberInfo member)
    {
        return member.GetAttribute<DefaultValueAttribute>() != null;
    }

    public static string Name(this Enum e)
    {
        return Enum.GetName(e.GetType(), e);
    }

    public static string Description(this Enum e)
    {
        Type type = e.GetType();
        string name = Enum.GetName(type, e);
        return type.GetField(name).Description() ?? name;
    }
}
