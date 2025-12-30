using System.ComponentModel;
using System.Drawing;

namespace cYo.Common.Windows.Forms;

public class AutoScrollEventArgs : CancelEventArgs
{
    private Point delta;

    public Point Delta
    {
        get => delta;
        set => delta = value;
    }

    public int X
    {
        get => delta.X;
        set => delta.X = value;
    }

    public int Y
    {
        get => delta.Y; set => delta.Y = value;
    }
}
