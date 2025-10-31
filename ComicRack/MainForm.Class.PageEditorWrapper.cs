using cYo.Common.Drawing;
using cYo.Common.Windows.Forms;
using cYo.Projects.ComicRack.Engine;
using cYo.Projects.ComicRack.Plugins;
using cYo.Projects.ComicRack.Plugins.Automation;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

public partial class MainForm : Form, IMain, IContainerControl, IPluginConfig, IApplication, IBrowser
{
    public IEditPage GetPageEditor()
    {
        return new PageEditorWrapper(FormUtility.FindActiveService<IEditPage>());
    }

    public class PageEditorWrapper : IEditPage
    {
        private IEditPage editor;

        public bool IsValid
        {
            get
            {
                if (editor != null)
                {
                    return editor.IsValid;
                }
                return false;
            }
        }

        public ComicPageType PageType
        {
            get
            {
                if (!IsValid)
                {
                    return ComicPageType.Story;
                }
                return editor.PageType;
            }
            set
            {
                if (IsValid)
                {
                    editor.PageType = value;
                }
            }
        }

        public ImageRotation Rotation
        {
            get
            {
                if (!IsValid)
                {
                    return ImageRotation.None;
                }
                return editor.Rotation;
            }
            set
            {
                if (IsValid)
                {
                    editor.Rotation = value;
                }
            }
        }

        public PageEditorWrapper(IEditPage editor)
        {
            this.editor = editor;
        }
    }
}
