using System.Drawing;

using cYo.Common.Drawing;

namespace cYo.Projects.ComicRack.Engine.IO.Provider;

public class ExportImageContainer
{
    public required byte[] Data { get; init; }

    public Bitmap Bitmap => BitmapExtensions.BitmapFromBytes(Data);

    public required bool NeedsToConvert { get; init; }

}
