using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using cYo.Common.ComponentModel;

namespace cYo.Common.Windows.Forms;

public class ItemViewColumn : BaseViewItem, IColumn, IBaseViewItem, INotifyPropertyChanged
{
    private int id;

    private int formatId;

    private bool visible = true;

    private int width;

    private StringAlignment alignment;

    private DateTime lastTimeVisible = DateTime.MinValue;

    private readonly string[] formatTexts = [];

    public int Id
    {
        get => id;
        set
        {
            if (id != value)
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
    }

    public int FormatId
    {
        get => formatId;
        set
        {
            if (formatId != value)
            {
                formatId = value;
                OnPropertyChanged("FormatId");
            }
        }
    }

    public IComparer<IViewableItem> ColumnSorter { get; set; }

    public IGrouper<IViewableItem> ColumnGrouper { get; set; }

    public bool Visible
    {
        get => visible;
        set
        {
            if (visible != value)
            {
                visible = value;
                if (!visible)
                {
                    lastTimeVisible = DateTime.Now;
                }
                OnPropertyChanged("Visible");
            }
        }
    }

    public int Width
    {
        get => width;
        set
        {
            if (value >= 0 && width != value)
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }
    }

    public StringAlignment Alignment
    {
        get => alignment;
        set
        {
            if (alignment != value)
            {
                alignment = value;
                OnPropertyChanged("Alignment");
            }
        }
    }

    public DateTime LastTimeVisible
    {
        get => lastTimeVisible;
        set => lastTimeVisible = value;
    }

    public string[] FormatTexts => formatTexts;

    public ItemViewColumn()
    {
    }

    public ItemViewColumn(int id, string text, int width, object tag, IComparer<IViewableItem> sorter = null, IGrouper<IViewableItem> grouper = null, bool visible = true, StringAlignment align = StringAlignment.Near, string[] formatTexts = null)
        : base(text, tag)
    {
        this.id = id;
        this.width = width;
        ColumnSorter = sorter;
        ColumnGrouper = grouper;
        this.visible = visible;
        alignment = align;
        if (formatTexts != null)
        {
            this.formatTexts = formatTexts;
        }
    }

    public void DrawHeader(Graphics gr, Rectangle rc, HeaderState state)
    {
        HeaderAdornments headerAdornments = (FormatTexts.Length != 0 && (state == HeaderState.Hot || state == HeaderState.Pressed)) ? HeaderAdornments.DropDown : HeaderAdornments.None;
        if (base.View.ItemSorter != null && base.View.ItemSorter == ColumnSorter && base.View.ItemSortOrder != 0)
        {
            headerAdornments |= (base.View.ItemSortOrder != SortOrder.Ascending) ? HeaderAdornments.SortUp : HeaderAdornments.SortDown;
        }
        HeaderControl.Draw(gr, rc, base.View.Font, Alignment, Text, state, headerAdornments);
    }
}
