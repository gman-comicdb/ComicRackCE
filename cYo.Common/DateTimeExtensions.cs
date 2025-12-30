using System;

using cYo.Common.Localize;

namespace cYo.Common;

public static class DateTimeExtensions
{
    private static Lazy<string[]> relativeFormat = new(() => TR.Load("Common").GetStrings("RelativeDateTimeFormat", "minute|minutes|hour|hours|day|days|week|weeks|month|months|year|years|{0} ago|in {0}", '|'));

    public static bool IsDateOnly(this DateTime dt)
    {
        return dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0 ? dt.Millisecond == 0 : false;
    }

    public static DateTime DateOnly(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day);
    }

    public static int CompareTo(this DateTime dt, DateTime value, bool ignoreTime)
    {
        if (!ignoreTime)
        {
            return dt.CompareTo(value);
        }
        int num = Math.Sign(dt.Year - value.Year);
        if (num != 0)
        {
            return num;
        }
        num = Math.Sign(dt.Month - value.Month);
        return num != 0 ? num : Math.Sign(dt.Day - value.Day);
    }

    public static DateTime SafeToLocalTime(this DateTime dt)
    {
        return !(dt == DateTime.MinValue) ? dt.ToLocalTime() : dt;
    }

    public static string ToRelativeDateString(this DateTime dt, DateTime toDate)
    {
        string[] value = relativeFormat.Value;
        string format;
        if (dt > toDate)
        {
            CloneUtility.Swap(ref dt, ref toDate);
            format = value[value.Length - 1];
        }
        else
        {
            format = value[value.Length - 2];
        }
        TimeSpan timeSpan = toDate - dt;
        int num = (int)timeSpan.TotalMinutes;
        int num2 = (int)timeSpan.TotalHours;
        int num3 = (int)timeSpan.TotalDays;
        int num4 = num3 / 7;
        int num5 = num4 / 4;
        int num6 = num4 / 52;
        string arg = num <= 1
            ? "1 " + value[0]
            : num2 switch
            {
                0 => num + " " + value[1],
                1 => "1 " + value[2],
                _ => num3 switch
                {
                    0 => num2 + " " + value[3],
                    1 => "1 " + value[4],
                    _ => num4 switch
                    {
                        0 => num3 + " " + value[5],
                        1 => "1 " + value[6],
                        _ => num5 switch
                        {
                            0 => num4 + " " + value[7],
                            1 => "1 " + value[8],
                            _ => num6 switch
                            {
                                0 => num5 + " " + value[9],
                                1 => "1 " + value[10],
                                _ => num6 + " " + value[11],
                            },
                        },
                    },
                },
            };
        return string.Format(format, arg);
    }
}
