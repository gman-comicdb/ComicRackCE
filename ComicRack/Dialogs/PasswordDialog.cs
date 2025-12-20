using cYo.Common.Windows;
using cYo.Common.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Dialogs;

public partial class PasswordDialog : FormEx
{
    public string Description
    {
        get => lblDescription.Text;
        set => lblDescription.Text = value;
    }

    public string Password => txPassword.Text;

    public bool RememberPassword => chkRemember.Checked;

    public PasswordDialog()
    {
        LocalizeUtility.UpdateRightToLeft(this);
        InitializeComponent();
        LocalizeUtility.Localize(this, null);
    }
}
