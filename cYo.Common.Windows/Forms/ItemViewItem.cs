using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms;

public class ItemViewItem : BaseViewItem, IViewableItem, IBaseViewItem, INotifyPropertyChanged
{
    private ItemViewMode cachedType;

    private Size cachedSize;

    private Size cachedInSize;

    protected ItemViewStates State
    {
        get => base.View != null ? base.View.GetItemState(this) : ItemViewStates.None;
        set => base.View?.SetItemState(this, value);
    }

    public bool Focused
    {
        get => (State & ItemViewStates.Focused) != 0;
        set => State |= ItemViewStates.Focused;
    }

    public bool Selected
    {
        get => (State & ItemViewStates.Selected) != 0;
        set => State = value ? (State | ItemViewStates.Selected) : (State & ~ItemViewStates.Selected);
    }

    public bool Hot
    {
        get => (State & ItemViewStates.Hot) != 0;
        set => State = value ? (State | ItemViewStates.Hot) : (State & ~ItemViewStates.Hot);
    }

    public int Index => base.View != null ? base.View.Items.IndexOf(this) : -1;

    [field: NonSerialized]
    public event PropertyChangedEventHandler BookChanged;

    public ItemViewItem()
    {
    }

    public ItemViewItem(string text)
        : base(text)
    {
    }

    public void EnsureVisible()
    {
        base.View?.EnsureItemVisible(this);
    }

    public void Update()
    {
        Update(sizeChanged: false);
    }

    public void Update(bool sizeChanged)
    {
        if (sizeChanged)
        {
            ForceRecalcSize();
        }
        base.View?.UpdateItem(this, sizeChanged);
    }

    protected override void OnPropertyChanged(string name)
    {
        base.OnPropertyChanged(name);
        Update(sizeChanged: true);
    }

    protected virtual void OnBookChanged(PropertyChangedEventArgs e)
    {
        BookChanged?.Invoke(this, e);
    }

    private void ForceRecalcSize()
    {
        cachedSize = Size.Empty;
    }

    public virtual bool OnClick(Point pt)
    {
        return false;
    }

    public void OnMeasure(ItemSizeInformation sizeInfo)
    {
        try
        {
            sizeInfo.Size = sizeInfo.SubItem == -1
                ? Measure(sizeInfo.Graphics, sizeInfo.Bounds.Size, sizeInfo.DisplayType)
                : MeasureColumn(sizeInfo.Graphics, sizeInfo.Header, sizeInfo.Bounds.Size);
        }
        catch (Exception)
        {
        }
    }

    public Size Measure(Graphics graphics, Size defaultSize, ItemViewMode displayType)
    {
        if (cachedSize.IsEmpty || cachedInSize != defaultSize || cachedType != displayType)
        {
            cachedInSize = defaultSize;
            cachedType = displayType;
            cachedSize = MeasureItem(graphics, defaultSize, displayType);
        }
        return cachedSize;
    }

    public virtual Control GetEditControl(int subItem)
    {
        return null;
    }

    public virtual ItemViewStates GetOwnerDrawnStates(ItemViewMode displayType)
    {
        return ItemViewStates.None;
    }

    protected virtual Size MeasureItem(Graphics graphics, Size defaultSize, ItemViewMode displayType)
    {
        return defaultSize;
    }

    protected virtual Size MeasureColumn(Graphics graphics, IColumn header, Size defaultSize)
    {
        return defaultSize;
    }

    public virtual void OnDraw(ItemDrawInformation drawInfo)
    {
    }
}
