using System.Drawing;

namespace cYo.Common.Presentation.Ceco;

public abstract class Block : Span, IRender
{
    private int lastLayoutWidth;

    private int border = -1;

    private VerticalAlignment vAlign;

    private SizeValue blockWidth;

    private int blockHeight;

    private Size margin;

    public bool IsWhiteSpace => false;

    public int Border
    {
        get
        {
            if (border == -1)
            {
                if (base.ParentInline is Block block)
                {
                    return block.Border;
                }
            }
            return border;
        }
        set
        {
            if (border != value)
            {
                border = value;
                OnBorderChanged();
            }
        }
    }

    public virtual VerticalAlignment VAlign
    {
        get
        {
            if (vAlign == VerticalAlignment.None)
            {
                if (base.ParentInline is Block block)
                {
                    return block.VAlign;
                }
            }
            return vAlign;
        }
        set
        {
            if (vAlign != value)
            {
                vAlign = value;
                OnVAlignChanged();
            }
        }
    }

    public SizeValue BlockWidth
    {
        get => blockWidth;
        set
        {
            if (!(blockWidth == value))
            {
                blockWidth = value;
                OnBlockWidthChanged();
            }
        }
    }

    public int BlockHeight
    {
        get => blockHeight;
        set
        {
            if (blockHeight != value)
            {
                blockHeight = value;
                OnBlockHeightChanged();
            }
        }
    }

    public virtual Size Margin
    {
        get => margin;
        set
        {
            if (!(margin == value))
            {
                margin = value;
                OnMarginChanged();
            }
        }
    }

    public override bool IsBlock => true;

    public int MinimumWidth { get; set; }

    public override bool IsNode => false;

    public virtual void Measure(Graphics gr, int maxWidth)
    {
        LayoutType layoutType = base.PendingLayout;
        maxWidth -= margin.Width * 2;
        if (layoutType == LayoutType.None && maxWidth != lastLayoutWidth)
        {
            layoutType = LayoutType.Position;
        }
        if (layoutType != 0)
        {
            CoreMeasure(gr, maxWidth, layoutType);
            lastLayoutWidth = maxWidth;
        }
        base.X += margin.Width;
        base.Y += margin.Height;
        base.PendingLayout = LayoutType.None;
    }

    public virtual void Draw(Graphics gr, Point location)
    {
        if (base.ParentInline == null)
        {
            Measure(gr, base.Size.Width);
        }
    }

    public void SetAlign(ContentAlignment contentAlignment)
    {
        Align = contentAlignment switch
        {
            ContentAlignment.TopCenter or ContentAlignment.MiddleCenter or ContentAlignment.BottomCenter => HorizontalAlignment.Center,
            ContentAlignment.TopRight or ContentAlignment.MiddleRight or ContentAlignment.BottomRight => HorizontalAlignment.Right,
            _ => HorizontalAlignment.Left,
        };
        VAlign = contentAlignment switch
        {
            ContentAlignment.BottomLeft or ContentAlignment.BottomCenter or ContentAlignment.BottomRight => VerticalAlignment.Bottom,
            ContentAlignment.MiddleLeft or ContentAlignment.MiddleCenter or ContentAlignment.MiddleRight => VerticalAlignment.Middle,
            _ => VerticalAlignment.Top,
        };
    }

    protected abstract void CoreMeasure(Graphics gr, int maxWidth, LayoutType tbl);

    protected virtual void OnVAlignChanged()
    {
        InvokeLayout(LayoutType.Position);
    }

    protected virtual void OnBorderChanged()
    {
        InvokeLayout(LayoutType.Full);
    }

    protected virtual void OnBlockWidthChanged()
    {
        InvokeLayout(LayoutType.Full);
    }

    protected virtual void OnBlockHeightChanged()
    {
        InvokeLayout(LayoutType.Full);
    }

    protected virtual void OnMarginChanged()
    {
        InvokeLayout(LayoutType.Full);
    }
}
