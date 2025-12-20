using System;
using System.Windows.Forms;

using cYo.Common.Windows.Forms.Theme;

namespace cYo.Common.Windows.Forms;

public class UserControlEx : UserControl, ITheme
{
    public virtual UIComponent UIComponent => UIComponent.None;

    public virtual void ApplyTheme(Control control = null)
    {
        ThemeExtensions.Theme(control ?? this);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        ApplyTheme();
    }
}
