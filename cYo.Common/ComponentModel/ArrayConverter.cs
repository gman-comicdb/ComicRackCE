using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

using cYo.Common.Text;

namespace cYo.Common.ComponentModel;

public class ArrayConverter<T> : TypeConverter
{
    private readonly TypeConverter tc = TypeDescriptor.GetConverter(typeof(T));

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        return sourceType == typeof(string) && tc.CanConvertFrom(context, sourceType) ? true : base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        return destinationType == typeof(string) && tc.CanConvertTo(context, destinationType)
            ? true
            : base.CanConvertTo(context, destinationType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        return value is string text
            ? (from x in text.Split(culture.TextInfo.ListSeparator, StringSplitOptions.RemoveEmptyEntries)
               select (T)tc.ConvertFrom(context, culture, x)).ToArray()
            : base.ConvertFrom(context, culture, value);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is IEnumerable<T> enumerable)
        {
            StringBuilder stringBuilder = new();
            foreach (T item in enumerable)
            {
                if (stringBuilder.Length != 0)
                {
                    stringBuilder.Append(culture.TextInfo.ListSeparator);
                    stringBuilder.Append(" ");
                }
                stringBuilder.Append(tc.ConvertTo(context, culture, item, destinationType));
            }
            return stringBuilder.ToString();
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}
