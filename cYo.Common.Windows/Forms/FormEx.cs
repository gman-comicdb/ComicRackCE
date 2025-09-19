using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using cYo.Common.Win32;
using cYo.Common.Windows.Forms.ColorScheme;

namespace cYo.Common.Windows.Forms
{
    public class FormEx : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ColorSchemeExtensions.SetColorScheme(this);
            UXTheme.ApplyDarkThemeToWindow(this, ColorSchemeExtensions.IsDarkModeEnabled, recurse: true);

        }
    }
}
