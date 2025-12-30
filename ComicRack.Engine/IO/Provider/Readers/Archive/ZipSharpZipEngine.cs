using System;
using System.Collections.Generic;
using System.IO;

using cYo.Projects.ComicRack.Engine.IO.Provider.XmlInfo;

using ICSharpCode.SharpZipLib.Zip;

namespace cYo.Projects.ComicRack.Engine.IO.Provider.Readers.Archive;

public class ZipSharpZipEngine : FileBasedAccessor
{
    public const int BufferSize = 131072;

    public ZipSharpZipEngine()
        : base(2)
    {
    }

    public override IEnumerable<ProviderImageInfo> GetEntryList(string source)
    {
        using (FileStream fs = new(source, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize))
        {
            using (ZipFile zf = new(fs))
            {
                foreach (ZipEntry item in zf)
                {
                    yield return new ProviderImageInfo((int)item.ZipFileIndex, item.Name, item.Size);
                }
            }
        }
    }

    public override byte[] ReadByteImage(string source, ProviderImageInfo info)
    {
        try
        {
            using (FileStream file = new(source, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize))
            {
                using (ZipFile zipFile = new(file))
                {
                    ZipEntry entry = zipFile.GetEntry(info.Name);
                    using (Stream stream = zipFile.GetInputStream(entry))
                    {
                        byte[] array = new byte[(int)entry.Size];
                        return stream.Read(array, 0, array.Length) != array.Length ? throw new IOException() : array;
                    }
                }
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    public override ComicInfo ReadInfo(string source)
    {
        try
        {
            using (FileStream file = new(source, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize))
            {
                using (ZipFile zipFile = new(file))
                {
                    return XmlInfoProviders.Readers.DeserializeAll(s =>
                    {
                        int num = zipFile.FindEntry(s, ignoreCase: true);
                        return num != -1 ? zipFile.GetInputStream(num) : null;
                    });
                }
            }
        }
        catch (Exception)
        {
        }
        return null;
    }
}
