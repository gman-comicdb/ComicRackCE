using System;

namespace cYo.Common.Text;

public class FileLengthFormat : IFormatProvider, ICustomFormatter
{
    public object GetFormat(Type formatType)
    {
        return this;
    }

    public string Format(string format, object arg, IFormatProvider formatProvider)
    {
        long num;
        try
        {
            num = (long)arg;
        }
        catch (Exception innerException)
        {
            throw new ArgumentException($"The argument \"{arg}\" cannot be converted to an integer value.", innerException);
        }
        return num < 1024
            ? $"{num} Bytes"
            : num < 1048576
            ? $"{(float)num / 1024f:.00} kB"
            : num < 1073741824 ? $"{(float)num / 1024f / 1024f:.00} MB" : $"{(float)num / 1024f / 1024f / 1024f:.00} GB";
    }
}
