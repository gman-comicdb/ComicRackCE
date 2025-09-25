using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using cYo.Common.Win32;
using cYo.Common.Windows.Forms.ColorScheme;
using System.Linq;

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //ColorSchemeExtensions.SetColorScheme(this);
            //Form buttons were not being themed correctly, presumably because there was no handle to them
            foreach (ButtonBase button in this.Controls.OfType<ButtonBase>())
            {
                UXTheme.ApplyDarkThemeToControl(button, ColorSchemeExtensions.IsDarkModeEnabled);
            }
        }
    }
}
