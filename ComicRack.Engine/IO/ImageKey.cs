using System;
using System.IO;
using System.Xml.Serialization;

using cYo.Common.Drawing;

namespace cYo.Projects.ComicRack.Engine.IO;

[Serializable]
[XmlInclude(typeof(PageKey))]
[XmlInclude(typeof(ThumbnailKey))]
public abstract class ImageKey
{
    private string location;

    private DateTime modified;

    private long size;

    private int index;

    [NonSerialized]
    private WeakReference source;

    private ImageRotation rotation;

    private int hashCode;

    public string Location { get => location; set => location = value; }

    public DateTime Modified { get => modified; set => modified = value; }

    public long Size { get => size; set => size = value; }

    public int Index { get => index; set => index = value; }

    [XmlIgnore]
    public object Source
    {
        get => source?.Target;
        set => source = new WeakReference(value);
    }

    public ImageRotation Rotation { get => rotation; set => rotation = value; }

    protected ImageKey(object source, string location, long size, DateTime modified, int index, ImageRotation rotation)
    {
        Source = source;
        this.location = location;
        this.modified = modified;
        this.size = size;
        this.index = index;
        this.rotation = rotation;
    }

    protected ImageKey(ImageKey key)
        : this(key.Source, key.location, key.size, key.modified, key.index, key.rotation)
    {
    }

    protected ImageKey()
    {
    }

    public void UpdateFileInfo()
    {
        modified = GetSafeModifiedTime(location);
        size = GetSafeSize(location);
        hashCode = 0;
    }

    public bool IsSameFile(string location, long size, DateTime modified)
    {
        return this.location == location && this.size == size ? this.modified == modified : false;
    }

    protected virtual int CreateHashCode()
    {
        int num = location.GetHashCode() ^ index.GetHashCode() ^ modified.GetHashCode();
        ImageRotation imageRotation = rotation;
        return num ^ imageRotation.GetHashCode();
    }

    public override int GetHashCode()
    {
        if (hashCode == 0)
        {
            hashCode = CreateHashCode();
        }
        return hashCode;
    }

    public override bool Equals(object obj)
    {
        return obj is not ImageKey imageKey
            ? false
            : IsSameFile(imageKey.location, imageKey.size, imageKey.modified) && index == imageKey.index
            ? rotation == imageKey.rotation
            : false;
    }

    public override string ToString()
    {
        return $"[{location}:{index}]";
    }

    public static DateTime GetSafeModifiedTime(string file)
    {
        try
        {
            return File.GetLastWriteTimeUtc(file);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    public static long GetSafeSize(string file)
    {
        try
        {
            return new FileInfo(file).Length;
        }
        catch
        {
            return 0L;
        }
    }
}
