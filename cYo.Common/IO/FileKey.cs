using System;
using System.IO;

namespace cYo.Common.IO;

public class FileKey
{
    private string File { get; set; }

    private long Size { get; set; }

    private DateTime Modified { get; set; }

    public FileKey(string file)
    {
        FileInfo fileInfo = new(file);
        File = file;
        Modified = fileInfo.LastWriteTimeUtc;
        Size = fileInfo.Length;
    }

    public override bool Equals(object obj)
    {
        return obj is FileKey fileKey && File == fileKey.File && Size == fileKey.Size ? Modified == fileKey.Modified : false;
    }

    public override int GetHashCode()
    {
        return File.GetHashCode() ^ Size.GetHashCode() ^ Modified.GetHashCode();
    }
}
