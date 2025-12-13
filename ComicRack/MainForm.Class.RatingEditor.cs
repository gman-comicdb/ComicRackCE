using cYo.Common.Collections;
using cYo.Common.Drawing;
using cYo.Common.Localize;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using cYo.Projects.ComicRack.Viewer.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm
{
    public IEditRating GetRatingEditor()
    {
        IGetBookList getBookList = FormUtility.FindActiveService<IGetBookList>();
        return new RatingEditor(Form.ActiveForm ?? this, getBookList?.GetBookList(ComicBookFilterType.IsEditable | ComicBookFilterType.Selected));
    }

    public class RatingEditor : IEditRating
    {
        private IEnumerable<ComicBook> books;

        private IWin32Window parent;

        public RatingEditor(IWin32Window parent, IEnumerable<ComicBook> books)
        {
            this.parent = parent;
            this.books = books;
        }

        public bool IsValid()
        {
            if (books != null)
            {
                return !books.IsEmpty();
            }
            return false;
        }

        public void SetRating(float rating)
        {
            if (IsValid())
            {
                Program.Database.Undo.SetMarker(TR.Messages["UndoRating", "Change Rating"]);
                books.ForEach((ComicBook cb) =>
                {
                    cb.Rating = rating;
                });
            }
        }

        public float GetRating()
        {
            float num = -1f;
            if (IsValid())
            {
                foreach (ComicBook book in books)
                {
                    if (num == -1f)
                    {
                        num = book.Rating;
                    }
                    else if (num != book.Rating)
                    {
                        return -1f;
                    }
                }
                return num;
            }
            return num;
        }

        public bool QuickRatingAndReview()
        {
            Program.Database.Undo.SetMarker(TR.Messages["QuickRating", "Quick Rating"]);
            return QuickRatingDialog.Show(parent, books.FirstOrDefault());
        }
    }
}
