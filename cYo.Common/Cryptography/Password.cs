using System;
using System.Security.Cryptography;
using System.Text;

namespace cYo.Common.Cryptography;

public static class Password
{
    private static readonly HashAlgorithm algorithm = new SHA1Managed();

    public static string CreateHash(string text)
    {
        return Convert.ToBase64String(CreateByteHash(text));
    }

    public static byte[] CreateByteHash(string text)
    {
        return CreateByteHash(Encoding.UTF8.GetBytes(text));
    }

    public static byte[] CreateByteHash(byte[] text)
    {
        return text.Length == 0 ? [] : algorithm.ComputeHash(text);
    }

    public static bool Verify(string text, string hashValue)
    {
        return !string.IsNullOrEmpty(hashValue) ? hashValue.Equals(CreateHash(text)) : false;
    }

    public static string Create(int len)
    {
        StringBuilder stringBuilder = new();
        Random random = new();
        for (int i = 0; i < len; i++)
        {
            int num = random.Next(60);
            if (num < 10)
            {
                stringBuilder.Append(48 + num);
                continue;
            }
            num -= 10;
            if (num < 25)
            {
                stringBuilder.Append(97 + num);
                continue;
            }
            num -= 25;
            stringBuilder.Append(65 + num);
        }
        return stringBuilder.ToString();
    }
}
