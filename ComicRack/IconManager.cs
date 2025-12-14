using cYo.Common.Compression;
using cYo.Common.Drawing;
using cYo.Common.Runtime;
using cYo.Projects.ComicRack.Engine;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace cYo.Projects.ComicRack.Viewer;

internal static class IconManager
{
    private static readonly Regex dateRangeRegex = new(
        @"\((?<startYear>\d{4})(?:_(?<startMonth>\d{2}))?-(?<endYear>\d{4})(?:_(?<endMonth>\d{2}))?\)",
        RegexOptions.Compiled
    );

    public static void AddIcons(string iconPackagesPath)
    {
        IEnumerable<string> defaultLocations = IniFile.GetDefaultLocations(iconPackagesPath);
        ComicBook.PublisherIcons.AddRange(ZipFileFolder.CreateFromFiles(defaultLocations, "Publishers*.zip"), SplitIconKeysWithYearAndMonth);
        ComicBook.AgeRatingIcons.AddRange(ZipFileFolder.CreateFromFiles(defaultLocations, "AgeRatings*.zip"), SplitIconKeys);
        ComicBook.FormatIcons.AddRange(ZipFileFolder.CreateFromFiles(defaultLocations, "Formats*.zip"), SplitIconKeys);
        ComicBook.SpecialIcons.AddRange(ZipFileFolder.CreateFromFiles(defaultLocations, "Special*.zip"), SplitIconKeys);
        ComicBook.GenericIcons = CreateGenericsIcons(defaultLocations, "*.zip", "_", SplitIconKeys);
    }

    private static IEnumerable<string> SplitIconKeysWithYearAndMonth(string value)
    {
        foreach (string key in SplitIconKeys(value))
        {
            Match dateMatch = dateRangeRegex.Match(key);

            if (!dateMatch.Success)
            {
                yield return key;
                continue;
            }

            Group startYearGroup = dateMatch.Groups["startYear"];
            Group endYearGroup = dateMatch.Groups["endYear"];
            Group startMonthGroup = dateMatch.Groups["startMonth"];
            Group endMonthGroup = dateMatch.Groups["endMonth"];
            string baseKey = dateRangeRegex.Replace(key, string.Empty);

            int startYear = int.Parse(startYearGroup.Value);
            int endYear = int.Parse(endYearGroup.Value);
            int startMonth = startMonthGroup.Success ? int.Parse(startMonthGroup.Value) : 1;
            int endMonth = endMonthGroup.Success ? int.Parse(endMonthGroup.Value) : 12;

            for (int year = startYear; year <= endYear; year++)
            {
                // Output the Year only for the files that don't have a month
                yield return $"{baseKey}({year})";

                int startMonthValue = (year == startYear) ? startMonth : 1;
                int endMonthValue = (year == endYear) ? endMonth : 12;

                for (int month = startMonthValue; month <= endMonthValue; month++)
                {
                    yield return $"{baseKey}({year}_{month:00})";
                }
            }
        }
    }

    private static IEnumerable<string> SplitIconKeys(string value)
    {
        return value.Split(',', '#');
    }

    public static Dictionary<string, ImagePackage> CreateGenericsIcons(
        IEnumerable<string> folders,
        string searchPattern,
        string trigger,
        Func<string,
        IEnumerable<string>> mapKeys = null
    )
    {
        Dictionary<string, ImagePackage> dictionary = new Dictionary<string, ImagePackage>();
        foreach (var generic in ZipFileFolder.CreateDictionaryFromFiles(folders, searchPattern, trigger))
        {
            var icons = new ImagePackage { EnableWidthCropping = true };
            icons.Add(generic.Value, mapKeys);
            dictionary.Add(generic.Key, icons);
        }
        return dictionary;
    }
}
