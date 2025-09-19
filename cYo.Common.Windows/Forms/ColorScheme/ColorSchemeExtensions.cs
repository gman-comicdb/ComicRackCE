using cYo.Common.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static cYo.Common.Windows.Forms.FolderTreeView;
using static System.Windows.Forms.AxHost;

namespace cYo.Common.Windows.Forms.ColorScheme
{
    public static class ColorSchemeExtensions
    {

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
        }

        // make the following ajustments to strict inversion:
        // - darkest is (15,15,15) instead of (0,0,0)
        // - lightest is (240,240,240) instead of 255
        // - [Reverted] Control Light and Dark are flipped so that Dark is still dark and Light is still light.
        //     [Reverted] This makes tab labels look bad (bright), but is important to button lighting
        // - Highlight colors are inverted and then Blue/Red channels swapped, to maintain a blue color instead of orange
        // - Info is overriden with a closer-to-CR dark orange
        private static Dictionary<string, Color> DarkColors = new Dictionary<string, Color>()
        {
            {"ActiveBorder", Color.FromArgb(99,99,99)},
            {"ActiveCaption", Color.FromArgb(46,73,102)},
            {"ActiveCaptionText", Color.FromArgb(240,240,240)},
            {"AppWorkspace", Color.FromArgb(107,107,107)},
            {"ButtonFace", Color.FromArgb(38,38,38)},
            {"ButtonHighlight", Color.FromArgb(15,15,15)},
            {"ButtonShadow", Color.FromArgb(117,117,117)},
            {"Control", Color.FromArgb(38,38,38)},
            {"ControlDark", Color.FromArgb(117,117,117)},
            {"ControlDarkDark", Color.FromArgb(162,162,162)},
            {"ControlLight", Color.FromArgb(53,53,53)},
            {"ControlLightLight", Color.FromArgb(15,15,15)},
            {"ControlText", Color.FromArgb(240,240,240)},
            {"Desktop", Color.FromArgb(240,240,240)},
            {"GradientActiveCaption", Color.FromArgb(21,45,70)},
            {"GradientInactiveCaption", Color.FromArgb(13,26,40)},
            {"GrayText", Color.FromArgb(159,159,159)},
            //{"Highlight", Color.FromArgb(40,160,255)},
            {"Highlight", Color.FromArgb(200,200,200)},           // replace blue with grey, ala Windows Dark Mode
            //{"HighlightText", Color.FromArgb(15,15,15)},
            {"HighlightText", Color.FromArgb(150,15,15)},         // testing
            {"HotTrack", Color.FromArgb(51,153,255)},
            {"InactiveBorder", Color.FromArgb(3,6,11)},
            {"InactiveCaption", Color.FromArgb(36,50,64)},
            {"InactiveCaptionText", Color.FromArgb(240,240,240)},
            {"Info", Color.FromArgb(240,120,0)},                  // ~straight inversion: (30,30,0)
            {"InfoText", Color.FromArgb(240,240,240)},
            {"Menu", Color.FromArgb(38,38,38)},
            {"MenuBar", Color.FromArgb(38,38,38)},
            {"MenuHighlight", Color.FromArgb(0,102,204)},
            {"MenuText", Color.FromArgb(240,240,240)},
            {"ScrollBar", Color.FromArgb(80,80,80)},
            {"Window", Color.FromArgb(15,15,15)},
            //{"Window", Color.FromArgb(95,95,95)},                 // testing
            {"WindowFrame", Color.FromArgb(166,166,166)},
            {"WindowText", Color.FromArgb(240,240,240)}
        };

        private static Color GetDarkSystemColor(string colorName)
        {

            try
            {
                return DarkColors[colorName];
            }
            catch
            {
                // if it's an unhandled color, return red
                return Color.Red;
            }
        }

        private static void ThemeControl (Button button)
        {
            if (button.Image == null)
            {
                button.FlatStyle = FlatStyle.System;
                uint msg = 5644u;
                NativeMethods.SendMessage(new HandleRef(button, button.Handle), msg, new IntPtr(0), new IntPtr(0));
            }
            else
            {
                // manually set Dark Mode button settings. This is not-OS version aware
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = button.Name == "btnVTagsHelp" ? Color.Transparent : Color.FromArgb(51, 51, 51);
                button.ForeColor = button.Name == "btnVTagsHelp" ? Color.Transparent : Color.FromArgb(255, 255, 255);
                button.FlatAppearance.BorderSize = button.Name == "btnVTagsHelp" ? 0 : 1;
                button.FlatAppearance.BorderColor = Color.FromArgb(155, 155, 155);
                button.FlatAppearance.CheckedBackColor = Color.FromArgb(102, 102, 102);
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 71, 71);
                // This _should_ handle buttons with images but it apparently missing something
                //IntPtr imageHandle = new Bitmap(button.Image).GetHicon();
                //const uint BM_SETIMAGE = 0xF7;
                //NativeMethods.SendMessage(new HandleRef(button, button.Handle), BM_SETIMAGE, new IntPtr(1), imageHandle);
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

        private static void ThemeControl(TextBox textBox)
        {
            // TextBoxEx did not like BorderStyle being set 
            textBox.BackColor = Color.FromArgb(56, 56, 56);
            textBox.ForeColor = GetDarkSystemColor("ControlText");
            textBox.BorderStyle = BorderStyle.FixedSingle;
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

        public static void SetColorScheme(this Control control, bool darkMode = false)
        {
            if (!darkMode) return;

            //Need to set the color even if the SystemColors as been changed so all the Control is drawn correctly.
            if (control is Form form)
            {
                control.BackColor = GetDarkSystemColor("ControlLightLight");
                control.ForeColor = GetDarkSystemColor("WindowText");
            }
            else if (control is TreeView treeView)
            {
                treeView.BackColor = GetDarkSystemColor("Control");
                treeView.ForeColor = GetDarkSystemColor("ControlText");
                treeView.BorderStyle = BorderStyle.None;
            }
            else if (control is Panel panel)
            {
                panel.BackColor = GetDarkSystemColor("Control");
                //panel.ForeColor = GetDarkSystemColor("ControlText");
                //panel.BorderStyle = BorderStyle.None;
            }
            else if (control is UserControl userControl)
            {
                userControl.BackColor = GetDarkSystemColor("Control");
                userControl.ForeColor = GetDarkSystemColor("ControlText");
                userControl.BorderStyle = BorderStyle.None;
            }
            else if (control is Button button && control.Name != "btAssociateExtensions")
            {
                ThemeControl(button);

            }
            else if (control is CheckBox checkBox)
            {
                ThemeControl(checkBox);
            }
            else if (control is ComboBox comboBox)
            {
                comboBox.FlatStyle = FlatStyle.System;
                //comboBox.EnabledChanged += (object? sender1, EventArgs e1) =>
                //{
                //    if (sender1 != null)
                //    {
                //        UXTheme.ApplyDarkThemeToControl((Control)sender1);
                //    }
                //};
            }
            else if (control is TextBox textBox && !(control is TextBoxEx))
            {
                ThemeControl(textBox);
            }
            else if (control is StatusStrip statusStrip)
            {
                foreach (ToolStripStatusLabel tsLabel in statusStrip.Items)
                {
                    if (tsLabel.BorderStyle.Equals(Border3DStyle.SunkenOuter))
                        tsLabel.BorderStyle = Border3DStyle.Flat;
                }
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
            foreach (Control child in control.Controls)
            {
                SetColorScheme(child, true);
            }
        }

        public static void SetDarkMode(bool darkMode = false)
        {
            if (darkMode)
            {
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
                        colorSchemeController.SetColor(knownColor, GetDarkSystemColor(prop.Name).ToArgb());
                    }
                }
            }

        }

    }
}
