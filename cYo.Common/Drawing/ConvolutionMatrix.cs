namespace cYo.Common.Drawing;

public struct ConvolutionMatrix
{
    private int topLeft;

    private int topMid;

    private int topRight;

    private int midLeft;

    private int pixel;

    private int midRight;

    private int bottomLeft;

    private int bottomMid;

    private int bottomRight;

    private int divisor;

    private int offset;

    public int TopLeft
    {
        get => topLeft;
        set => topLeft = value;
    }

    public int TopMid
    {
        get => topMid;
        set => topMid = value;
    }

    public int TopRight
    {
        get => topRight;
        set => topRight = value;
    }

    public int MidLeft
    {
        get => midLeft;
        set => midLeft = value;
    }

    public int Pixel
    {
        get => pixel;
        set => pixel = value;
    }

    public int MidRight
    {
        get => midRight;
        set => midRight = value;
    }

    public int BottomLeft
    {
        get => bottomLeft;
        set => bottomLeft = value;
    }

    public int BottomMid
    {
        get => bottomMid;
        set => bottomMid = value;
    }

    public int BottomRight
    {
        get => bottomRight;
        set => bottomRight = value;
    }

    public int Divisor
    {
        get => divisor;
        set => divisor = value;
    }

    public int Offset
    {
        get => offset;
        set => offset = value;
    }

    public ConvolutionMatrix(int setToAll)
    {
        divisor = 1;
        offset = 0;
        pixel = setToAll;
        topLeft = topMid = topRight = midLeft = midRight = bottomLeft = bottomMid = bottomRight = setToAll;
    }

    public void SetAll(int value)
    {
        int num2 = BottomRight = value;
        int num4 = BottomMid = num2;
        int num6 = BottomLeft = num4;
        int num8 = MidRight = num6;
        int num10 = Pixel = num8;
        int num12 = MidLeft = num10;
        int num14 = TopRight = num12;
        int num17 = TopLeft = TopMid = num14;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        ConvolutionMatrix convolutionMatrix = (ConvolutionMatrix)obj;
        return convolutionMatrix.topLeft == topLeft && convolutionMatrix.topMid == topMid && convolutionMatrix.topRight == topRight && convolutionMatrix.midLeft == midLeft && convolutionMatrix.midRight == midRight && convolutionMatrix.bottomLeft == bottomLeft && convolutionMatrix.bottomMid == bottomMid
            ? convolutionMatrix.bottomRight == bottomRight
            : false;
    }

    public override int GetHashCode()
    {
        return TopLeft.GetHashCode() ^ topRight.GetHashCode() ^ bottomLeft.GetHashCode() ^ bottomRight.GetHashCode();
    }

    public static bool operator ==(ConvolutionMatrix a, ConvolutionMatrix b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(ConvolutionMatrix a, ConvolutionMatrix b)
    {
        return !(a == b);
    }
}
