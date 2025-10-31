using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm : Form, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    private BookmarkEditorWrapper GetBookmarkEditor()
    {
        return new BookmarkEditorWrapper(FormUtility.FindActiveService<IEditBookmark>());
    }

    public class BookmarkEditorWrapper : IEditBookmark
    {
        private IEditBookmark editor;

        public bool CanBookmark
        {
            get
            {
                if (editor != null)
                {
                    return editor.CanBookmark;
                }
                return false;
            }
        }

        public string BookmarkProposal
        {
            get
            {
                if (!CanBookmark)
                {
                    return string.Empty;
                }
                return editor.BookmarkProposal;
            }
        }

        public string Bookmark
        {
            get
            {
                if (!CanBookmark)
                {
                    return string.Empty;
                }
                return editor.Bookmark;
            }
            set
            {
                if (CanBookmark)
                {
                    editor.Bookmark = value;
                }
            }
        }

        public BookmarkEditorWrapper(IEditBookmark editor)
        {
            this.editor = editor;
        }
    }
}
