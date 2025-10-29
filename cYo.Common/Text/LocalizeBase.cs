using cYo.Common.Localize;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace cYo.Common.Text;

public static class LocalizeBase
{
    public static string LocalizeEnum(Type enumType, int value)
    {
        string name = Enum.GetName(enumType, value);
        return TR.Load(enumType.Name)[name, GetEnumDescription(enumType, name).PascalToSpaced()];
    }

    private static string GetEnumDescription(Type enumType, string name)
    {
        FieldInfo field = enumType.GetField(name);
        DescriptionAttribute descriptionAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false).Cast<DescriptionAttribute>().FirstOrDefault();
        if (descriptionAttribute != null)
        {
            return descriptionAttribute.Description;
        }
        return name;
    }
}
