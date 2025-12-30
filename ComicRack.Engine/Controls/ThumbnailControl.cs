using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.Windows.Forms;
using cYo.Common.Windows.Forms.Theme;
using cYo.Projects.ComicRack.Engine.Drawing;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Engine.IO.Cache;
using cYo.Projects.ComicRack.Engine.IO.Provider;

namespace cYo.Projects.ComicRack.Engine.Controls;

public class ThumbnailControl : Control, IBitmapDisplayControl, IDisposable
{
    private ComicBook comicBook;

    private volatile Bitmap image;

    private IThumbnailPool thumbnailPool;

    private int page = -1;

    private ThumbnailDrawingOptions drawingFlags = ThumbnailDrawingOptions.Default;

    private ComicTextElements textElements = ComicTextElements.DefaultComic;

    private bool tile;

    private BitmapAdjustment colorAdjustment = BitmapAdjustment.Empty;

    private bool highQuality = true;

    private bool threeD;

    private string publishedYear;

    private string publisherIcon;

    private string imprintIcon;

    private string ageRatingIcon;

    private string formatIcon;

    private string tagsIcon;

    private YesNo? seriesCompleteIcon;

    private YesNo? blackAndWhiteIcon;

    private MangaYesNo? mangaIcon;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ComicBook ComicBook
    {
        get => comicBook;
        set
        {
            if (ComicBook != value)
            {
                if (comicBook != null)
                {
                    comicBook.BookChanged -= comicBook_PropertyChanged;
                }
                comicBook = value;
                if (comicBook != null)
                {
                    comicBook.BookChanged += comicBook_PropertyChanged;
                }
                Invalidate();
            }
        }
    }

    [DefaultValue(null)]
    public Bitmap Bitmap
    {
        get => image;
        set
        {
            if (image != value)
            {
                image = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(null)]
    public IThumbnailPool ThumbnailPool
    {
        get => thumbnailPool;
        set
        {
            if (thumbnailPool != value)
            {
                thumbnailPool = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(-1)]
    public int Page
    {
        get => page;
        set
        {
            if (page != value)
            {
                page = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(ThumbnailDrawingOptions.Default)]
    public ThumbnailDrawingOptions DrawingFlags
    {
        get => drawingFlags;
        set
        {
            if (drawingFlags != value)
            {
                drawingFlags = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(ComicTextElements.DefaultComic)]
    public ComicTextElements TextElements
    {
        get => textElements;
        set
        {
            if (textElements != value)
            {
                textElements = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(false)]
    public bool Tile
    {
        get => tile;
        set
        {
            if (tile != value)
            {
                tile = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(typeof(BitmapAdjustment), "0, 0, 0")]
    public BitmapAdjustment ColorAdjustment
    {
        get => colorAdjustment;
        set
        {
            if (!(colorAdjustment == value))
            {
                colorAdjustment = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(true)]
    public bool HighQuality
    {
        get => highQuality;
        set
        {
            if (highQuality != value)
            {
                highQuality = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(false)]
    public bool ThreeD
    {
        get => threeD;
        set
        {
            if (value != threeD)
            {
                threeD = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(null)]
    public string PublishedYear
    {
        get
        {
            string yearAsText = publishedYear;
            if (yearAsText == null)
            {
                if (comicBook == null)
                {
                    return publishedYear;
                }
                yearAsText = comicBook.YearAsText;
            }
            return yearAsText;
        }
        set
        {
            if (!(publishedYear == value))
            {
                publishedYear = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(null)]
    public string PublisherIcon
    {
        get
        {
            string publisher = publisherIcon;
            if (publisher == null)
            {
                if (comicBook == null)
                {
                    return publisherIcon;
                }
                publisher = comicBook.Publisher;
            }
            return publisher;
        }
        set
        {
            if (!(publisherIcon == value))
            {
                publisherIcon = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(null)]
    public string ImprintIcon
    {
        get
        {
            string imprint = imprintIcon;
            if (imprint == null)
            {
                if (comicBook == null)
                {
                    return imprintIcon;
                }
                imprint = comicBook.Imprint;
            }
            return imprint;
        }
        set
        {
            if (!(imprintIcon == value))
            {
                imprintIcon = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(null)]
    public string AgeRatingIcon
    {
        get
        {
            string ageRating = ageRatingIcon;
            if (ageRating == null)
            {
                if (comicBook == null)
                {
                    return ageRatingIcon;
                }
                ageRating = comicBook.AgeRating;
            }
            return ageRating;
        }
        set
        {
            if (!(ageRatingIcon == value))
            {
                ageRatingIcon = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(null)]
    public string FormatIcon
    {
        get
        {
            string format = formatIcon;
            if (format == null)
            {
                if (comicBook == null)
                {
                    return formatIcon;
                }
                format = comicBook.Format;
            }
            return format;
        }
        set
        {
            if (!(formatIcon == value))
            {
                formatIcon = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(null)]
    public string TagsIcon
    {
        get
        {
            string tags = tagsIcon;
            if (tags == null)
            {
                if (comicBook == null)
                {
                    return tagsIcon;
                }
                tags = comicBook.Tags;
            }
            return tags;
        }
        set
        {
            if (!(tagsIcon == value))
            {
                tagsIcon = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(YesNo.Unknown)]
    public YesNo SeriesCompleteIcon
    {
        get => !seriesCompleteIcon.HasValue ? comicBook == null ? YesNo.Unknown : comicBook.SeriesComplete : seriesCompleteIcon.Value;
        set
        {
            if (!seriesCompleteIcon.HasValue || seriesCompleteIcon.Value != value)
            {
                seriesCompleteIcon = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(YesNo.Unknown)]
    public YesNo BlackAndWhiteIcon
    {
        get => !blackAndWhiteIcon.HasValue ? comicBook == null ? YesNo.Unknown : comicBook.BlackAndWhite : blackAndWhiteIcon.Value;
        set
        {
            if (!blackAndWhiteIcon.HasValue || blackAndWhiteIcon.Value != value)
            {
                blackAndWhiteIcon = value;
                Invalidate();
            }
        }
    }

    [DefaultValue(MangaYesNo.Unknown)]
    public MangaYesNo MangaIcon
    {
        get => !mangaIcon.HasValue ? comicBook == null ? MangaYesNo.Unknown : comicBook.Manga : mangaIcon.Value;
        set
        {
            if (!mangaIcon.HasValue || mangaIcon.Value != value)
            {
                mangaIcon = value;
                Invalidate();
            }
        }
    }

    public ThumbnailControl()
    {
        SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
        SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
        SetStyle(ControlStyles.UserPaint, value: true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
        SetStyle(ControlStyles.ResizeRedraw, value: true);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && comicBook != null)
        {
            comicBook.BookChanged -= comicBook_PropertyChanged;
        }
        base.Dispose(disposing);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using (e.Graphics.SaveState())
        {
            if (HighQuality)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            }
            Bitmap bitmap = null;
            Bitmap bitmap2 = Bitmap;
            if (bitmap2 == null && thumbnailPool != null)
            {
                using (IImageProvider provider = ComicBook.OpenProvider(Page))
                {
                    using (IItemLock<ThumbnailImage> itemLock = thumbnailPool.GetThumbnail((Page == -1) ? ComicBook.GetFrontCoverThumbnailKey() : ComicBook.GetThumbnailKey(Page), provider, onErrorThrowException: false))
                    {
                        if (itemLock != null && itemLock.Item != null)
                        {
                            bitmap = bitmap2 = itemLock.Item.Bitmap.Clone() as Bitmap;
                        }
                    }
                }
            }
            try
            {
                using (Image image = bitmap2.CreateAdjustedBitmap(colorAdjustment, PixelFormat.Format32bppArgb, alwaysClone: true))
                {
                    Image image2 = image ?? bitmap2;
                    if (tile)
                    {
                        ThumbTileRenderer.DrawTile(e.Graphics, base.ClientRectangle, image2, comicBook, page, Font, ForeColor, Color.Transparent, drawingFlags, textElements, ThreeD, GetIcons());
                    }
                    else
                    {
                        ThumbRenderer.DrawThumbnail(e.Graphics, image2, base.ClientRectangle, drawingFlags, comicBook);
                    }
                    if (base.DesignMode)
                    {
                        //ControlPaint.DrawFocusRectangle(e.Graphics, base.ClientRectangle);
                        ControlPaintEx.DrawFocusRectangle(e.Graphics, base.ClientRectangle);
                    }
                }
            }
            finally
            {
                bitmap?.Dispose();
            }
        }
    }

    private IEnumerable<Image> GetIcons()
    {
        if (ComicBook == null)
        {
            return [];
        }
        ComicBook comicBook = (ComicBook)ComicBook.Clone();
        if (!int.TryParse(PublishedYear, out var result))
        {
            result = -1;
        }
        comicBook.Publisher = PublisherIcon;
        comicBook.Year = result;
        comicBook.Imprint = ImprintIcon;
        comicBook.AgeRating = AgeRatingIcon;
        comicBook.Format = FormatIcon;
        comicBook.Tags = TagsIcon;
        comicBook.Manga = MangaIcon;
        comicBook.SeriesComplete = SeriesCompleteIcon;
        comicBook.BlackAndWhite = BlackAndWhiteIcon;
        return comicBook.GetIcons();
    }

    public void SetBitmap(Bitmap image)
    {
        Bitmap bitmap = Bitmap;
        Bitmap = image;
        bitmap?.Dispose();
    }

    private void comicBook_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Invalidate();
    }

    //Decompile Error
    //object IBitmapDisplayControl.get_Tag()
    //{
    //	return base.Tag;
    //}

    //void IBitmapDisplayControl.set_Tag(object value)
    //{
    //	base.Tag = value;
    //}
}
