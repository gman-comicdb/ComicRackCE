using System.Collections.Generic;

namespace cYo.Projects.ComicRack.Engine.IO.Provider;

public static class KnownFileFormats
{
    public const int PDF = 1;

    public const int CBZ = 2;

    public const int CBR = 3;

    public const int XML = 4;

    public const int CBT = 5;

    public const int CB7 = 6;

    public const int CBW = 7;

    public const int DJVU = 8;

    public const int RAR5 = 9;

    public const int FOLDER = 100;

    public static IEnumerable<byte> GetSignature(int format)
    {
        return format switch
        {
            CBZ => new byte[2]
                            {
                80,
                75
                            },
            CB7 =>
                [
                55,
                122
                ],
            CBR =>
                [
                82,
                97,
                114,
                33,
                26,
                7,
                0
                ],
            RAR5 =>
                [
                82,
                97,
                114,
                33,
                26,
                7,
                1
                ],
            _ => null,
        };
    }
}
