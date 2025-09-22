using cYo.Common.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace cYo.Common.Windows.Forms.ColorScheme
{
    public static class ColorSchemeExtensions
    {

        public static bool IsDarkModeEnabled = false;

        internal static class NativeMethods
        {
            private const uint LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800;

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, IntPtr ordinal);

            [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

            [DllImport("dwmapi.dll", PreserveSig = true)]
            public static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

            [DllImport("user32.dll")]
            public static extern int SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            public static extern int SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, HandleRef lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

            public struct RECT
            {
                public int left;

                public int top;

                public int right;

                public int bottom;
            }

            public enum ComboBoxButtonState
            {
                STATE_SYSTEM_NONE = 0,
                STATE_SYSTEM_INVISIBLE = 0x8000,
                STATE_SYSTEM_PRESSED = 8
            }

            public struct COMBOBOXINFO
            {
                public int cbSize;

                public RECT rcItem;

                public RECT rcButton;

                public ComboBoxButtonState buttonState;

                public IntPtr hwndCombo;

                public IntPtr hwndEdit;

                public IntPtr hwndList;
            }

            public const int ECM_FIRST = 5376;

            public const int EM_SETCUEBANNER = 5377;

            [DllImport("user32.dll")]
            public static extern bool GetComboBoxInfo(IntPtr hwnd, ref COMBOBOXINFO pcbi);

            public static readonly int DWMWA_USE_IMMERSIVE_DARK_MODE = GetDwmDarkModeAttribute();
        }

        // make the following ajustments to strict inversion:
        // - darkest is (15,15,15) instead of (0,0,0)
        // - lightest is (240,240,240) instead of 255
        // - [Reverted] Control Light and Dark are flipped so that Dark is still dark and Light is still light.
        //     [Reverted] This makes tab labels look bad (bright), but is important to button lighting
        // - Highlight colors are inverted and then Blue/Red channels swapped, to maintain a blue color instead of orange
        // - Info is overriden with a closer-to-CR dark orange
        // make the following ajustments to strict inversion:
        // - darkest is (15,15,15) instead of (0,0,0)
        // - lightest is (240,240,240) instead of 255
        // - [Reverted] Control Light and Dark are flipped so that Dark is still dark and Light is still light.
        //     [Reverted] This makes tab labels look bad (bright), but is important to button lighting
        // - Highlight colors are inverted and then Blue/Red channels swapped, to maintain a blue color instead of orange
        // - Info is overriden with a closer-to-CR dark orange
        //private static Dictionary<string, Color> DarkColors = new Dictionary<string, Color>()
        //{
        //    {"ActiveBorder", Color.FromArgb(99,99,99)},
        //    {"ActiveCaption", Color.FromArgb(46,73,102)},
        //    {"ActiveCaptionText", Color.FromArgb(240,240,240)},
        //    //{"AppWorkspace", Color.FromArgb(107,107,107)},
        //    {"ButtonFace", Color.FromArgb(38,38,38)},
        //    {"ButtonHighlight", Color.FromArgb(15,15,15)},
        //    {"ButtonShadow", Color.FromArgb(117,117,117)},
        //    {"Control", Color.FromArgb(38,38,38)},
        //    {"ControlDark", Color.FromArgb(117,117,117)},
        //    {"ControlDarkDark", Color.FromArgb(162,162,162)},
        //    //{"ControlLight", Color.FromArgb(53,53,53)},         // why isn't this between Control and ControlLightLight?
        //    {"ControlLight", Color.FromArgb(27,27,27)},
        //    {"ControlLightLight", Color.FromArgb(15,15,15)},
        //    {"ControlText", Color.FromArgb(240,240,240)},
        //    {"Desktop", Color.FromArgb(240,240,240)},
        //    {"GradientActiveCaption", Color.FromArgb(21,45,70)},
        //    {"GradientInactiveCaption", Color.FromArgb(13,26,40)},
        //    {"GrayText", Color.FromArgb(159,159,159)},
        //    //{"Highlight", Color.FromArgb(40,160,255)},
        //    {"Highlight", Color.FromArgb(200,200,200)},           // replace blue with grey, ala Windows Dark Mode
        //    {"HighlightText", Color.FromArgb(15,15,15)},
        //    {"HotTrack", Color.FromArgb(51,153,255)},
        //    {"InactiveBorder", Color.FromArgb(3,6,11)},
        //    {"InactiveCaption", Color.FromArgb(36,50,64)},
        //    {"InactiveCaptionText", Color.FromArgb(240,240,240)},
        //    {"Info", Color.FromArgb(240,120,0)},                  // ~straight inversion = (30,30,0)
        //    {"InfoText", Color.FromArgb(240,240,240)},
        //    {"Menu", Color.FromArgb(38,38,38)},
        //    {"MenuBar", Color.FromArgb(38,38,38)},
        //    {"MenuHighlight", Color.FromArgb(0,102,204)},
        //    {"MenuText", Color.FromArgb(240,240,240)},
        //    {"ScrollBar", Color.FromArgb(80,80,80)},
        //    //{"Window", Color.FromArgb(15,15,15)},
        //    {"Window", Color.FromArgb(27,27,27)},                 // testing
        //    {"WindowFrame", Color.FromArgb(166,166,166)},
        //    {"WindowText", Color.FromArgb(240,240,240)}
        //};

        private static Dictionary<string, Color> DarkSystemColors = new Dictionary<string, Color>(34)
        {
            // .NET 10 - System.Drawing.KnownColorTable.AlternateSystemColors
            {"ActiveBorder"           , Color.FromArgb(unchecked((int)0xFF464646))},  // Dark gray
            {"ActiveCaption"          , Color.FromArgb(unchecked((int)0xFF3C5F78))},  // Highlighted Text Background
            {"ActiveCaptionText"      , Color.FromArgb(unchecked((int)0xFFFFFFFF))},  // White
            {"AppWorkspace"           , Color.FromArgb(unchecked((int)0xFF3C3C3C))},  // Panel Background
            {"Control"                , Color.FromArgb(unchecked((int)0xFF202020))},  // Normal Panel/Windows Background
            {"ControlDark"            , Color.FromArgb(unchecked((int)0xFF4A4A4A))},  // A lighter gray for dark mode
            {"ControlDarkDark"        , Color.FromArgb(unchecked((int)0xFF5A5A5A))},  // An even lighter gray for dark mode
            {"ControlLight"           , Color.FromArgb(unchecked((int)0xFF2E2E2E))},  // Unfocused Textbox Background
            {"ControlLightLight"      , Color.FromArgb(unchecked((int)0xFF1F1F1F))},  // Focused Textbox Background
            {"ControlText"            , Color.FromArgb(unchecked((int)0xFFFFFFFF))},  // Control Forecolor and Text Color
            {"Desktop"                , Color.FromArgb(unchecked((int)0xFF101010))},  // Black
            {"GrayText"               , Color.FromArgb(unchecked((int)0xFF969696))},  // Prompt Text Focused TextBox
            {"Highlight"              , Color.FromArgb(unchecked((int)0xFF2864B4))},  // Highlighted Panel in DarkMode
            {"HighlightText"          , Color.FromArgb(unchecked((int)0xFF000000))},  // White
            {"HotTrack"               , Color.FromArgb(unchecked((int)0xFF2D5FAF))},  // Background of the ToggleSwitch
            {"InactiveBorder"         , Color.FromArgb(unchecked((int)0xFF3C3F41))},  // Dark gray
            {"InactiveCaption"        , Color.FromArgb(unchecked((int)0xFF374B5A))},  // Highlighted Panel in DarkMode
            {"InactiveCaptionText"    , Color.FromArgb(unchecked((int)0xFFBEBEBE))},  // Middle Dark Panel
            {"Info"                   , Color.FromArgb(unchecked((int)0xFF50503C))},  // Link Label
            {"InfoText"               , Color.FromArgb(unchecked((int)0xFFBEBEBE))},  // Prompt Text Color
            {"Menu"                   , Color.FromArgb(unchecked((int)0xFF373737))},  // Normal Menu Background
            {"MenuText"               , Color.FromArgb(unchecked((int)0xFFF0F0F0))},  // White
            {"ScrollBar"              , Color.FromArgb(unchecked((int)0xFF505050))},  // Scrollbars and Scrollbar Arrows
            {"Window"                 , Color.FromArgb(unchecked((int)0xFF323232))},  // Window Background
            {"WindowFrame"            , Color.FromArgb(unchecked((int)0xFF282828))},  // White
            {"WindowText"             , Color.FromArgb(unchecked((int)0xFFF0F0F0))},  // White
            {"ButtonFace"             , Color.FromArgb(unchecked((int)0xFF202020))},  // Same as Window Background
            {"ButtonHighlight"        , Color.FromArgb(unchecked((int)0xFF101010))},  // White
            {"ButtonShadow"           , Color.FromArgb(unchecked((int)0xFF464646))},  // Same as Scrollbar Elements
            {"GradientActiveCaption"  , Color.FromArgb(unchecked((int)0XFF416482))},  // Same as Highlighted Text Background
            {"GradientInactiveCaption", Color.FromArgb(unchecked((int)0xFF557396))},  // Same as Highlighted Panel in DarkMode
            {"MenuBar"                , Color.FromArgb(unchecked((int)0xFF373737))},  // Same as Normal Menu Background
            {"MenuHighlight"          , Color.FromArgb(unchecked((int)0xFF2A80D2))},  // Same as Highlighted Menu Background
        };

        //private static Dictionary<string, Color> DarkProfessionalColors = new Dictionary<string, Color>(34)
        //{
        //    // .NET 10 - System.Windows.Forms.DarkProfessionalColors
        //    {"MenuItemPressedGradientBegin" , Color.FromArgb(unchecked((int)0xFF606060))},  // 
        //    {"MenuItemPressedGradientMiddle", Color.FromArgb(unchecked((int)0xFF606060))},  // 
        //    {"MenuItemPressedGradientEnd"   , Color.FromArgb(unchecked((int)0xFF606060))},  // 
        //    {"MenuItemSelected"             , Color.FromArgb(unchecked((int)0xFFFFFFFF))},  // SystemColors.ControlText
        //    {"MenuItemSelectedGradientBegin", Color.FromArgb(unchecked((int)0xFF404040))},  // 
        //    {"MenuItemSelectedGradientEnd"  , Color.FromArgb(unchecked((int)0xFF404040))},  // 
        //    {"MenuStripGradientBegin"       , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
        //    {"MenuStripGradientEnd"         , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
        //    {"StatusStripGradientBegin"     , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
        //    {"StatusStripGradientEnd"       , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
        //    {"ToolStripDropDownBackground"  , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
        //    {"ImageMarginGradientBegin"     , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
        //    {"ImageMarginGradientMiddle"    , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
        //    {"ImageMarginGradientEnd"       , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
        //};

        private static Color GetDarkSystemColor(string colorName)
        {

            try
            {
                return DarkSystemColors[colorName];
            }
            catch
            {
                // if it's an unhandled color, return red
                return Color.Red;
            }
        }

        private static void ThemeControl(StatusStrip statusStrip)
        {
            statusStrip.BackColor = GetDarkSystemColor("ControlLightLight");
            statusStrip.ForeColor = GetDarkSystemColor("ControlText");
            foreach (ToolStripStatusLabel tsLabel in statusStrip.Items)
            {
                if (tsLabel.BorderStyle.Equals(Border3DStyle.SunkenOuter))
                {
                    tsLabel.BorderSides = ToolStripStatusLabelBorderSides.None;
                    tsLabel.Paint += (sender, e) =>
                    {
                        using (var pen = new Pen(Color.FromArgb(100, 100, 100), 1))
                        {
                            e.Graphics.DrawRectangle(pen, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
                        }
                    };
                }

            }
        }

        private static void ThemeControl(CheckBox checkBox)
        {

            if (checkBox.Appearance == Appearance.Button)
            {
                if (checkBox.Image == null)
                {
                    checkBox.FlatStyle = FlatStyle.System;
                    uint msg = 5644u;
                    NativeMethods.SendMessage(new HandleRef(checkBox, checkBox.Handle), msg, new IntPtr(0), new IntPtr(0));
                }
                else
                {
                    // manually set Dark Mode button settings. This is not-OS version aware
                    checkBox.FlatStyle = FlatStyle.Flat;
                    checkBox.BackColor = Color.FromArgb(51, 51, 51);
                    checkBox.ForeColor = Color.FromArgb(255, 255, 255);
                    checkBox.FlatAppearance.BorderSize = 1;
                    checkBox.FlatAppearance.BorderColor = Color.FromArgb(155, 155, 155);
                    checkBox.FlatAppearance.CheckedBackColor = Color.FromArgb(102, 102, 102);
                    checkBox.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 71, 71);
                }
            }
            else
            {
                checkBox.FlatStyle = FlatStyle.Popup;
                checkBox.Paint += (sender, e) =>
                {
                    // based on CheckedListEx.OnDrawItem()
                    // this is not a great solution and is currently not Dpi aware.
                    var checkBox = sender as CheckBox;
                    CheckState itemCheckState = checkBox.CheckState;
                    CheckBoxState state;
                    Size size;
                    Brush backGroundBrush = new SolidBrush(checkBox.Parent.BackColor);

                    if (!checkBox.Enabled)
                    {
                        e.Graphics.Clear(checkBox.Parent.BackColor);
                    }
                    if (Application.RenderWithVisualStyles)
                    {
                        if (checkBox.Enabled)
                        {
                            state = itemCheckState == CheckState.Unchecked ? CheckBoxState.UncheckedNormal : itemCheckState == CheckState.Checked ? CheckBoxState.CheckedNormal : CheckBoxState.MixedNormal;
                        }
                        else
                        {
                            state = itemCheckState == CheckState.Unchecked ? CheckBoxState.UncheckedDisabled : itemCheckState == CheckState.Checked ? CheckBoxState.CheckedDisabled : CheckBoxState.MixedDisabled;
                        }
                        size = CheckBoxRenderer.GetGlyphSize(e.Graphics, state);
                        using (backGroundBrush)
                        {
                            e.Graphics.FillRectangle(backGroundBrush, 0, 0, size.Width, size.Height + 1);
                        }
                        // how taxing it generating and then pixel-by-pixel inverting a checkbox?
                        ColorSchemeController colorSchemeController = new ColorSchemeController();
                        e.Graphics.DrawImage(colorSchemeController.RenderCheckboxToBitmap(state, size), 0, 0);
                    }
                    else
                    {
                        size = new Size(14, 14);
                    }
                    if (!checkBox.Enabled)
                    {
                        TextRenderer.DrawText(e.Graphics, checkBox.Text, checkBox.Font, new Point(size.Width + 2, 2), SystemColors.GrayText);
                    }
                };
            }
        }

        private static int GetDwmDarkModeAttribute()
        {
            Version v = Environment.OSVersion.Version;
            return (v.Build >= 18985) ? 20 : 19;
        }

        private static void ThemeControl(TextBox textBox)
        {
            // TextBoxEx did not like BorderStyle being set 
            if (!(textBox is TextBoxEx))
                textBox.BorderStyle = BorderStyle.FixedSingle;

            textBox.BackColor = Color.FromArgb(56, 56, 56);
            textBox.ForeColor = GetDarkSystemColor("ControlText");
            textBox.MouseLeave += (sender, e) =>
            {
                if (!(sender as TextBox).Focused)
                    (sender as TextBox).BackColor = Color.FromArgb(56, 56, 56);
            };
            textBox.MouseHover += (sender, e) =>
            {
                if ((sender as TextBox).Enabled)
                    (sender as TextBox).BackColor = Color.FromArgb(86, 86, 86);
            };
            textBox.Enter += (sender, e) =>
            {
                (sender as TextBox).BackColor = Color.FromArgb(71, 71, 71);
                (sender as TextBox).BorderStyle = BorderStyle.Fixed3D;
            };
            textBox.Leave += (sender, e) =>
            {
                (sender as TextBox).BackColor = Color.FromArgb(56, 56, 56);
                (sender as TextBox).BorderStyle = BorderStyle.FixedSingle;
            };
        }

        private static void ThemeControl(ComboBox comboBox)
        {
            comboBox.BackColor = Color.FromArgb(56, 56, 56);
            comboBox.ForeColor = GetDarkSystemColor("ControlText");

            if (comboBox.Handle == null) return;

            NativeMethods.COMBOBOXINFO pcbi = default(NativeMethods.COMBOBOXINFO);
            pcbi.cbSize = Marshal.SizeOf((object)pcbi);
            NativeMethods.GetComboBoxInfo(comboBox.Handle, ref pcbi);
            IntPtr textHandle = pcbi.hwndEdit;
            IntPtr listHandle = pcbi.hwndList;
            if (textHandle != IntPtr.Zero)
            {
                NativeMethods.SendMessage(textHandle, NativeMethods.EM_SETCUEBANNER, IntPtr.Zero, "");
            }
            if (listHandle != IntPtr.Zero)
            {
                NativeMethods.SetWindowTheme(listHandle, "DarkMode_Explorer", null);

                int useDark = 1;
                NativeMethods.DwmSetWindowAttribute(listHandle, NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDark, sizeof(int));

                //const int WM_THEMECHANGED = 0x031A;
                //NativeMethods.SendMessage(listHandle, WM_THEMECHANGED, IntPtr.Zero, IntPtr.Zero);
            }
        }

        public static void SetColorScheme(this Control control)
        {
            if (!IsDarkModeEnabled) return;

            //Need to set the color even if the SystemColors as been changed so all the Control is drawn correctly.
            if (control is Form form)
            {
                control.BackColor = GetDarkSystemColor("ControlLightLight");
                control.ForeColor = GetDarkSystemColor("WindowText");
            }
            else if (control is TreeView treeView)
            {
                treeView.BackColor = GetDarkSystemColor("ControlLight");
                treeView.ForeColor = GetDarkSystemColor("ControlText");
                treeView.BorderStyle = BorderStyle.None;
            }
            else if (control is Panel panel)
            {
                panel.BackColor = GetDarkSystemColor("Control");
                //panel.ForeColor = GetDarkSystemColor("ControlText");
                //panel.BorderStyle = BorderStyle.None;
            }
            else if (control is GroupBox grpBox)
            {
                //grpBox.BackColor = GetDarkSystemColor("Control");
                grpBox.ForeColor = GetDarkSystemColor("ControlText");
            }
            else if (control is UserControl userControl)
            {
                userControl.BackColor = GetDarkSystemColor("Control");
                userControl.ForeColor = GetDarkSystemColor("ControlText");
                userControl.BorderStyle = BorderStyle.None;
            }
            else if (control is StatusStrip statusStrip)
            {
                ThemeControl(statusStrip);
            }
            else if (control is Button button)
            {
                button.FlatStyle = FlatStyle.System;

            }
            else if (control is CheckBox checkBox)
            {
                ThemeControl(checkBox);
            }
            else if (control is ComboBox comboBox)
            {
                ThemeControl(comboBox);
                //comboBox.FlatStyle = FlatStyle.System;
                //comboBox.EnabledChanged += (object? sender1, EventArgs e1) =>
                //{
                //    if (sender1 != null)
                //    {
                //        UXTheme.ApplyDarkThemeToControl((Control)sender1);
                //    }
                //};
            }
            else if (control is TextBox textBox)
            {
                ThemeControl(textBox);
            }
            else if (!(control is TextBoxEx))
            {
                try
                {
                    control.GetType().GetProperty("BorderStyle")?.SetValue(control, BorderStyle.None);
                }
                catch
                {
                    control.GetType().GetProperty("BorderStyle")?.SetValue(control, Border3DStyle.Flat);
                }

            }
            //else if (control is MdiClient mdiClient)
            //{
            //    mdiClient.BackColor = Color.Red; // GetDarkSystemColor("Control");
            //    mdiClient.ForeColor = GetDarkSystemColor("ControlText");
            //}
            //else if (control is ScrollableControl scrollableControl)
            //{
            //    scrollableControl.BackColor = Color.Cyan; // GetDarkSystemColor("Control");
            //    scrollableControl.ForeColor = GetDarkSystemColor("ControlText");
            //}
            else
            {
                control.BackColor = Color.Red;
                control.ForeColor = Color.Orange;
            }

            foreach (Control child in control.Controls)
            {
                SetColorScheme(child);
            }
        }

        public static void SetDarkMode(bool useDarkMode = false)
        {
            if (!useDarkMode) return;

            IsDarkModeEnabled = useDarkMode;
            // WhiteSmoke is (245,245,245), but (11,11,11) would be too dark
            int blackSmoke = Color.FromArgb(31, 31, 31).ToArgb();

            ColorSchemeController colorSchemeController = new ColorSchemeController();

            KnownColor knownColor = KnownColor.WhiteSmoke;
            colorSchemeController.SetColor(knownColor, blackSmoke);

            foreach (PropertyInfo prop in typeof(SystemColors).GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                if (prop.PropertyType == typeof(Color))
                {
                    knownColor = (KnownColor)Enum.Parse(typeof(KnownColor), prop.Name);
                    colorSchemeController.SetColor(knownColor, GetDarkSystemColor(knownColor.ToString()).ToArgb());
                }
            }
        }

    }
}
