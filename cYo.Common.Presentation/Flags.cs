using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;

using cYo.Common.Presentation.Properties;

using ICSharpCode.SharpZipLib.Zip;

namespace cYo.Common.Presentation;

public static class Flags
{
    private static readonly Dictionary<string, Image> flagDict;

    private static readonly bool available;

    public static bool Available => available;

    static Flags()
    {
        flagDict = new Dictionary<string, Image>();
        try
        {
            using (MemoryStream stream = new(Resources.Flags))
            {
                using (ZipFile zipFile = new(stream))
                {
                    foreach (ZipEntry item in zipFile)
                    {
                        if (item.IsFile)
                        {
                            using (Stream stream2 = zipFile.GetInputStream(item))
                            {
                                flagDict[Path.GetFileNameWithoutExtension(item.Name)] = Image.FromStream(stream2, useEmbeddedColorManagement: false, validateImageData: false);
                            }
                        }
                    }
                }
            }
            available = true;
        }
        catch (Exception)
        {
        }
    }

    public static Image GetFlagFromCountry(string countryCode)
    {
        return !available || countryCode == null || !flagDict.TryGetValue(countryCode.ToLower(), out var value)
            ? null
            : value.Clone() as Image;
    }

    public static Image GetFlagFromCulture(string cultureCode)
    {
        if (!available)
        {
            return null;
        }
        cultureCode ??= CultureInfo.CurrentUICulture.Name;
        string[] array = cultureCode.Split('-');
        Image flagFromCountry = GetFlagFromCountry(array[array.Length - 1]);
        return flagFromCountry != null || array.Length == 2
            ? flagFromCountry
            : (from ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
               where ci.Name.StartsWith(cultureCode)
               select GetFlagFromCulture(ci.Name)).FirstOrDefault(ci => ci != null);
    }

    public static Image GetFlag(CultureInfo ci)
    {
        return GetFlagFromCulture(ci.Name);
    }
}
