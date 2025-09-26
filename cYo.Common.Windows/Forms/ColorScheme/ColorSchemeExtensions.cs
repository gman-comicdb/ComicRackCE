using cYo.Common.Drawing;
using cYo.Common.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static cYo.Common.Windows.Forms.ComboBoxSkinner;


namespace cYo.Common.Windows.Forms.ColorScheme
{
    public static class ColorSchemeExtensions
    {

        public static bool IsDarkModeEnabled = false;

        internal static class NativeMethods
        {
            //private const uint LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800;

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

            // P/Invoke declaration
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int SendMessage(IntPtr handle, uint messg, int wparam, int lparam);

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

            public static int YES = 1;

            [DllImport("comctl32.dll")]
            public static extern bool SetWindowSubclass(IntPtr hWnd, SubclassProc pfnSubclass, uint uIdSubclass, IntPtr dwRefData);

            [DllImport("comctl32.dll")]
            public static extern IntPtr DefSubclassProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            public delegate IntPtr SubclassProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam, UIntPtr uIdSubclass, IntPtr dwRefData);

        }

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

            // .NET 10 - System.Windows.Forms.DarkProfessionalColors
            {"MenuItemPressedGradientBegin" , Color.FromArgb(unchecked((int)0xFF606060))},  // 
            {"MenuItemPressedGradientMiddle", Color.FromArgb(unchecked((int)0xFF606060))},  // 
            {"MenuItemPressedGradientEnd"   , Color.FromArgb(unchecked((int)0xFF606060))},  // 
            {"MenuItemSelected"             , Color.FromArgb(unchecked((int)0xFFFFFFFF))},  // SystemColors.ControlText
            {"MenuItemSelectedGradientBegin", Color.FromArgb(unchecked((int)0xFF404040))},  // 
            {"MenuItemSelectedGradientEnd"  , Color.FromArgb(unchecked((int)0xFF404040))},  // 
            {"MenuStripGradientBegin"       , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
            {"MenuStripGradientEnd"         , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
            {"StatusStripGradientBegin"     , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
            {"StatusStripGradientEnd"       , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
            {"ToolStripDropDownBackground"  , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
            {"ImageMarginGradientBegin"     , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
            {"ImageMarginGradientMiddle"    , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
            {"ImageMarginGradientEnd"       , Color.FromArgb(unchecked((int)0xFF373737))},  // SystemColors.Control
        };

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
                    tsLabel.Paint -= ToolStripStatusLabel_Paint;
                    tsLabel.Paint += ToolStripStatusLabel_Paint;
                }

            }
        }

        private static void ToolStripStatusLabel_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(100, 100, 100), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            }
        }

        private static void ThemeControl(CheckBox checkBox)
        {

            checkBox.FlatStyle = FlatStyle.Flat;
            if (checkBox.Appearance == Appearance.Button)
            {
                // although it has the appearance of a button, the theme engine doesn't style it as such, so we have to do it manually
                // this is likely handled correctly in Win11 builds
                checkBox.BackColor = Color.FromArgb(51, 51, 51);
                checkBox.ForeColor = Color.FromArgb(255, 255, 255);
                checkBox.FlatAppearance.BorderSize = 1;
                checkBox.FlatAppearance.BorderColor = Color.FromArgb(155, 155, 155);
                checkBox.FlatAppearance.CheckedBackColor = Color.FromArgb(102, 102, 102);
                checkBox.FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 71, 71);
            }
            else
            {
                checkBox.FlatStyle = FlatStyle.Flat;
                checkBox.Paint -= CheckBox_Paint;
                checkBox.Paint += CheckBox_Paint;
            }
        }

        private static void CheckBox_Paint(object sender, PaintEventArgs e)
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

                // how taxing is generating and then pixel-by-pixel inverting a checkbox?
                ColorSchemeController colorSchemeController = new ColorSchemeController();

                var x = 0;
                var y = 0;
                if (checkBox.CheckAlign == System.Drawing.ContentAlignment.TopLeft)
                {
                    // default, expected, here so we can color unhandled cases in red
                }
                else if (checkBox.CheckAlign == System.Drawing.ContentAlignment.MiddleLeft)
                {
                    y = (e.ClipRectangle.Height / 2) - (size.Height / 2);
                }
                else if (checkBox.CheckAlign == System.Drawing.ContentAlignment.MiddleRight)
                {
                    x = e.ClipRectangle.Width - size.Width;
                    y = (e.ClipRectangle.Height / 2) - (size.Height / 2);
                }
                else
                {
                    e.Graphics.Clear(Color.Red);
                }
                using (backGroundBrush)
                {
                    e.Graphics.FillRectangle(backGroundBrush, x, y, size.Width, size.Height + 1);
                }
                e.Graphics.DrawImage(RenderCheckboxToBitmap(state, size), x, y);
            }
            else
            {
                size = new Size(14, 14);
            }
            if (!checkBox.Enabled)
            {
                // may need to check TextAlign similar to CheckAlign above
                TextRenderer.DrawText(e.Graphics, checkBox.Text, checkBox.Font, new Point(size.Width + 1, 2), SystemColors.GrayText);
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
            textBox.MouseLeave -= TextBox_MouseLeave;
            textBox.MouseLeave += TextBox_MouseLeave;
            textBox.MouseHover -= TextBox_MouseHover;
            textBox.MouseHover += TextBox_MouseHover;
            textBox.Enter -= TextBox_Enter;
            textBox.Enter += TextBox_Enter;
            textBox.Leave -= TextBox_Leave;
            textBox.Leave += TextBox_Leave;
        }

        private static void TextBox_MouseLeave(object sender, EventArgs e)
        {
            if (!(sender as TextBox).Focused)
                (sender as TextBox).BackColor = Color.FromArgb(56, 56, 56);
        }
        private static void TextBox_MouseHover(object sender, EventArgs e)
        {
            if ((sender as TextBox).Enabled)
                (sender as TextBox).BackColor = Color.FromArgb(86, 86, 86);
        }
        private static void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.BackColor = Color.FromArgb(71, 71, 71);
            if (!textBox.Multiline)
            {
                textBox.BorderStyle = BorderStyle.Fixed3D;
            }
        }
        private static void TextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.BackColor = Color.FromArgb(56, 56, 56);
            if (!textBox.Multiline)
            {
                textBox.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private static void ThemeControl(ComboBox comboBox)
        {
            comboBox.BackColor = Color.FromArgb(56, 56, 56);
            comboBox.ForeColor = GetDarkSystemColor("ControlText");

            if (comboBox.Handle == null) return;

            // Blue -> Gray highlight
            // results in DropDown instead of DropDownList theme formatting (highlighted text when not dropped down)
            // we can get (mostly) work around it by not drawing the background in ComboBox_DrawItem but 2 issues:
            // - we lose visual feedback that this is the the selected box (could draw out focus rectangle)
            // - dropdown button still has editable theming (seperator line)
            if (comboBox.DrawMode == DrawMode.Normal)
            {
                // OwnerDrawFixed is an unverified assumption
                comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                comboBox.DrawItem -= ComboBox_DrawItem;
                comboBox.DrawItem += ComboBox_DrawItem;
            }
            //NativeMethods.SetWindowTheme(comboBox.Handle, "DarkMode_CFD", null);
            NativeMethods.COMBOBOXINFO pcbi = default(NativeMethods.COMBOBOXINFO);
            pcbi.cbSize = Marshal.SizeOf((object)pcbi);
            NativeMethods.GetComboBoxInfo(comboBox.Handle, ref pcbi);
            IntPtr listHandle = pcbi.hwndList;
            if (listHandle != IntPtr.Zero)
            {
                NativeMethods.SetWindowTheme(listHandle, "DarkMode_Explorer", null);
                NativeMethods.DwmSetWindowAttribute(listHandle, NativeMethods.DWMWA_USE_IMMERSIVE_DARK_MODE, ref NativeMethods.YES, sizeof(int));

                //const int WM_THEMECHANGED = 0x031A;
                //NativeMethods.SendMessage(listHandle, WM_THEMECHANGED, IntPtr.Zero, IntPtr.Zero);
            }
        }

        // internal const uint LVM_SETINSERTMARKCOLOR = 4266U;


        private static void ThemeControl(ListView listView)
        {
            listView.BackColor = Color.FromArgb(56, 56, 56);
            listView.ForeColor = GetDarkSystemColor("ControlText");


            if (!(listView is ListViewEx) && listView.View == View.Details && listView.HeaderStyle != ColumnHeaderStyle.None)
            {
                listView.OwnerDraw = true;
                listView.DrawItem -= ListView_DrawItem;
                listView.DrawItem += ListView_DrawItem;
                listView.DrawColumnHeader -= ListView_DrawColumnHeader;
                listView.DrawColumnHeader += ListView_DrawColumnHeader;
                listView.DrawSubItem -= ListView_DrawSubItem;
                listView.DrawSubItem += ListView_DrawSubItem;
            }
        }

        private static void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if ((sender as ListView).View != View.Details)
            {
                e.DrawDefault = true;
                return;
            }

            e.DrawDefault = false;
            using (Brush bgBrush = new SolidBrush(Color.FromArgb(32, 32, 32)))
            {
                e.Graphics.FillRectangle(bgBrush, e.Bounds);
            }

            using (Pen sepPen = new Pen(Color.FromArgb(99, 99, 99)))
            {
                int x = e.Bounds.Right - 1;
                int y1 = e.Bounds.Top;
                int y2 = e.Bounds.Bottom;
                e.Graphics.DrawLine(sepPen, x, y1, x, y2);
            }

            using (Brush textBrush = new SolidBrush(Color.White))
            {
                // Draw the header text with custom color and font
                e.Graphics.DrawString(
                    e.Header.Text,
                    e.Font,
                    textBrush,
                    e.Bounds,
                    new StringFormat
                    {
                        Alignment = StringAlignment.Near, // left align text
                        LineAlignment = StringAlignment.Center, // vertically center text
                        Trimming = StringTrimming.EllipsisCharacter
                    });
            }
        }

        private static void ListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private static void ListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        // trimmed version of cYo.Common.Windows.Forms.ComboBoxSkinner.comboBox_DrawItem
        // TODO: de-duplicate
        private static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            ComboBox comboBox = (ComboBox)sender;
            object obj = comboBox.Items[e.Index];
            IComboBoxItem comboBoxItem = obj as IComboBoxItem;
            bool flag = (e.State & DrawItemState.ComboBoxEdit) != 0;
            bool flag2 = comboBoxItem != null && comboBoxItem.IsSeparator && !flag && e.Index > 0;
            Pen separatorPen = ColorSchemeExtensions.IsDarkModeEnabled ? SystemPens.ControlText : SystemPens.ControlLight;

            // override ForeColor so we can draw non-highlighted text when not DroppedDown
            Color foreColor = SystemColors.ControlText;

            // only draw background and use actual ForeColor when DroppedDown
            if (comboBox.DroppedDown)
            {
                e.DrawBackground();
                // Blue -> Grey selected item highlight
                if (ColorSchemeExtensions.IsDarkModeEnabled && e.State.HasFlag(DrawItemState.Selected))
                    e.Graphics.FillRectangle(SystemBrushes.GrayText, e.Bounds);
                foreColor = e.ForeColor;
            }
            e.DrawFocusRectangle();
            // this doesn't look great as it's an inner border and omits the button
            // not sure how to draw out of combobox items bounds
            // ControlPaint.DrawBorder(e.Graphics, e.Bounds, Color.Red, ButtonBorderStyle.Solid);

            using (Brush brush = new SolidBrush(foreColor))
            {
                Rectangle rectangle = e.Bounds;
                if (comboBoxItem != null && comboBoxItem.IsOwnerDrawn)
                {
                    comboBoxItem.Draw(e.Graphics, rectangle, e.ForeColor, comboBox.Font);
                }
                else
                {
                    using (StringFormat format = new StringFormat(StringFormatFlags.NoWrap)
                    {
                        LineAlignment = StringAlignment.Center,
                        Alignment = StringAlignment.Near
                    })
                    {
                        e.Graphics.DrawString(comboBox.GetItemText(obj), comboBox.Font, brush, rectangle, format);
                    }
                }
            }
        }

        public static void SetColorScheme(this Control control)
        {
            if (!IsDarkModeEnabled) return;

            //Need to set the color even if the SystemColors as been changed so all the Control is drawn correctly.
            if (control is Form form)
            {
                //control.BackColor = GetDarkSystemColor("ControlLightLight");
                //control.ForeColor = GetDarkSystemColor("WindowText");
            }
            else if (control is TreeView treeView)
            {
                //treeView.BackColor = GetDarkSystemColor("ControlLight");
                //treeView.ForeColor = GetDarkSystemColor("ControlText");
                //treeView.BorderStyle = BorderStyle.None;
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
                grpBox.ForeColor = GetDarkSystemColor("WindowText");
            }
            else if (control is UserControl userControl)
            {
                //userControl.BackColor = GetDarkSystemColor("Control");
                //userControl.ForeColor = GetDarkSystemColor("ControlText");
                userControl.BorderStyle = BorderStyle.None;
            }
            else if (control is StatusStrip statusStrip)
            {
                ThemeControl(statusStrip);
            }
            else if (control is Button button)
            {
                if (button.Image == null && button.BackgroundImage == null)
                {
                    button.FlatStyle = FlatStyle.System;
                }
                else
                {
                    button.FlatStyle = FlatStyle.Flat;
                    if (button.FlatAppearance.BorderSize != 0)
                    {
                        button.FlatAppearance.BorderSize = 1;
                        button.FlatAppearance.BorderColor = Color.FromArgb(155, 155, 155);
                    }
                    //if (button.Image != null)
                    //{
                    //    InvertImage(button.Image);
                    //}
                    //else if (button.BackgroundImage != null)
                    //{
                    //    InvertImage(button.BackgroundImage);
                    //}
                }
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
            //else if (control is MenuStrip menuStrip)
            //{
            //    ThemeControl(menuStrip);
            //}
            else if (control is ListView listView && !(control is ListViewEx))
            {
                ThemeControl(listView);
            }
            else if (control is ListBox listBox)
            {
                listBox.BackColor = Color.FromArgb(56, 56, 56); //GetDarkSystemColor("ControlDarkDark");
                listBox.ForeColor = GetDarkSystemColor("WindowText");
                listBox.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (!(control is TextBoxEx))
            {
                try
                {
                    control.GetType().GetProperty("BorderStyle")?.SetValue(control, BorderStyle.None);
                }
                catch
                {
                    try
                    {
                        control.GetType().GetProperty("BorderStyle")?.SetValue(control, Border3DStyle.Flat);
                    }
                    catch
                    {
                        control.BackColor = Color.Cyan;
                        control.ForeColor = Color.Purple;
                    }
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

        public static Bitmap RenderCheckboxToBitmap(CheckBoxState state, Size size)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                CheckBoxRenderer.DrawCheckBox(g, new Point(0, 0), state);
            }
            InvertBitmap(bmp);
            return bmp;
        }

        public static void InvertImage(Image image)
        {
            if (image is Bitmap bmp)
            {
                InvertBitmap(bmp);
            }
            else
            {
                // not working
                //CreateErrorImage(ref image);
            }
        }
        public static void InvertImage(Bitmap image)
        {

            if (image is Bitmap bmp)
            {
                InvertBitmap(image);
            }
            else
            {
                // not working
                //CreateErrorImage(ref image);
            }
        }

        //private static void CreateErrorImage(ref Image image)
        //{
        //    Bitmap redBitmap = new Bitmap(12, 12);

        //    using (Graphics g = Graphics.FromImage(redBitmap))
        //    {
        //        g.Clear(Color.Red);
        //    }

        //    image.Dispose();
        //    image = redBitmap;
        //}

        public static void InvertBitmap(Bitmap bmp)
        {
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color original = bmp.GetPixel(x, y);
                    Color inverted = Color.FromArgb(
                        original.A,
                        255 - original.R,
                        255 - original.G,
                        255 - original.B);
                    bmp.SetPixel(x, y, inverted);
                }
            }
        }

        public static Color InvertLuminance(Color color)
        {
            // Convert RGB to HSL
            double hue, saturation, lightness;
            RgbToHsl(color, out hue, out saturation, out lightness);

            // Invert lightness
            lightness = 1.0 - lightness;

            // Convert back to RGB
            return HslToRgb(hue, saturation, lightness, color.A);
        }

        private static void RgbToHsl(Color color, out double hue, out double saturation, out double lightness)
        {
            double red = color.R / 255.0;
            double green = color.G / 255.0;
            double blue = color.B / 255.0;

            double max = Math.Max(red, Math.Max(green, blue));
            double min = Math.Min(red, Math.Min(green, blue));
            lightness = (max + min) / 2.0;

            if (max == min)
            {
                // achromatic case (no saturation)
                hue = 0;
                saturation = 0;
            }
            else
            {
                double delta = max - min;

                saturation = lightness > 0.5
                    ? delta / (2.0 - max - min)
                    : delta / (max + min);

                if (max == red)
                    hue = (green - blue) / delta + (green < blue ? 6 : 0);
                else if (max == green)
                    hue = (blue - red) / delta + 2;
                else
                    hue = (red - green) / delta + 4;

                hue /= 6;
            }
        }

        private static Color HslToRgb(double hue, double saturation, double lightness, byte alpha)
        {
            double red, green, blue;

            if (saturation == 0)
            {
                // achromatic (gray)
                red = green = blue = lightness;
            }
            else
            {
                double q = lightness < 0.5
                    ? lightness * (1 + saturation)
                    : lightness + saturation - lightness * saturation;
                double p = 2 * lightness - q;

                Func<double, double, double, double> hueToRgb = (tempP, tempQ, tempT) =>
                {
                    if (tempT < 0) tempT += 1;
                    if (tempT > 1) tempT -= 1;
                    if (tempT < 1.0 / 6) return tempP + (tempQ - tempP) * 6 * tempT;
                    if (tempT < 1.0 / 2) return tempQ;
                    if (tempT < 2.0 / 3) return tempP + (tempQ - tempP) * (2.0 / 3 - tempT) * 6;
                    return tempP;
                };

                red = hueToRgb(p, q, hue + 1.0 / 3);
                green = hueToRgb(p, q, hue);
                blue = hueToRgb(p, q, hue - 1.0 / 3);
            }

            return Color.FromArgb(
                alpha,
                (int)Math.Round(red * 255),
                (int)Math.Round(green * 255),
                (int)Math.Round(blue * 255)
            );
        }

        public static int ToWin32(Color c)
        {
            return unchecked(c.R << 0 | c.G << 8 | c.B << 16);
        }

    }
}
