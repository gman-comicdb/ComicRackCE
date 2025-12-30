namespace cYo.Common.Presentation.Ceco;

public struct FontSize
{
    public int Size;

    public bool Relative;

    public static readonly FontSize Empty = new(0, relative: true);

    public FontSize(int size, bool relative)
    {
        Size = size;
        Relative = relative;
    }

    public override bool Equals(object obj)
    {
        if (obj is not FontSize)
        {
            return false;
        }
        FontSize fontSize = (FontSize)obj;
        return fontSize.Relative == Relative ? fontSize.Size == Size : false;
    }

    public override int GetHashCode()
    {
        return Size.GetHashCode() ^ Relative.GetHashCode();
    }

    public static bool operator ==(FontSize a, FontSize b)
    {
        return object.Equals(a, b);
    }

    public static bool operator !=(FontSize a, FontSize b)
    {
        return !(a == b);
    }
}
