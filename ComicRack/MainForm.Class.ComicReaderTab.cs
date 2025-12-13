using cYo.Common.ComponentModel;
using cYo.Common.Drawing;
using cYo.Common.Windows.Forms;
using cYo.Common.Windows.Forms.Theme.Resources;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Engine.Drawing;
using cYo.Projects.ComicRack.Engine.IO;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using System.Drawing;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm
{
    public partial class ComicReaderTab : TabBar.TabBarItem
    {
        private readonly ComicBookNavigator nav;

        private readonly Font font;

        private readonly string shortcut;

        public ComicReaderTab(string text, ComicBookNavigator nav, Font font, string shortcut)
            : base(text)
        {
            this.nav = nav;
            this.font = font;
            this.shortcut = shortcut;
            ToolTipSize = new Size(400, 200).ScaleDpi();
        }

        public override bool ShowToolTip()
        {
            return nav != null;
        }

        public override void DrawTooltip(Graphics gr, Rectangle rc)
        {
            base.DrawTooltip(gr, rc);
            if (nav == null)
            {
                return;
            }
            try
            {
                ComicBook comic = nav.Comic;
                using (IItemLock<ThumbnailImage> itemLock = Program.ImagePool.GetThumbnail(comic.GetFrontCoverThumbnailKey(), nav, onErrorThrowException: false))
                {
                    rc.Inflate(-10, -5);
                    if (!string.IsNullOrEmpty(shortcut))
                    {
                        using (Brush brush = new SolidBrush(Color.FromArgb(128, SystemColors.InfoText)))
                        {
                            using (StringFormat format = new StringFormat
                            {
                                Alignment = StringAlignment.Far,
                                LineAlignment = StringAlignment.Far
                            })
                            {
                                gr.DrawString(shortcut, FC.Get(font, 6f), brush, rc, format);
                            }
                        }
                        rc.Height -= 10;
                    }
                    ThumbTileRenderer.DrawTile(gr, rc, itemLock?.Item.GetThumbnail(rc.Height), comic, font, ThemeColors.ToolTip.InfoText, Color.Transparent, ThumbnailDrawingOptions.DefaultWithoutBackground, ComicTextElements.DefaultComic, threeD: false, comic.GetIcons());
                }
            }
            catch
            {
            }
        }
    }
}
