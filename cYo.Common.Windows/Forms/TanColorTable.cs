using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

using Microsoft.Win32;

namespace cYo.Common.Windows.Forms;

public class TanColorTable : ProfessionalColorTable
{
    private static class DisplayInformation
    {
        [ThreadStatic]
        private static string colorScheme;

        [ThreadStatic]
        private static bool isLunaTheme;

        private const string lunaFileName = "luna.msstyles";

        public static string ColorScheme => colorScheme;

        public static bool IsLunaTheme => isLunaTheme;

        static DisplayInformation()
        {
            SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
            SetScheme();
        }

        private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            SetScheme();
        }

        private static void SetScheme()
        {
            isLunaTheme = false;
            if (VisualStyleRenderer.IsSupported)
            {
                colorScheme = VisualStyleInformation.ColorScheme;
                if (VisualStyleInformation.IsEnabledByUser)
                {
                    StringBuilder stringBuilder = new(512);
                    GetCurrentThemeName(stringBuilder, stringBuilder.Capacity, null, 0, null, 0);
                    string path = stringBuilder.ToString();
                    isLunaTheme = string.Equals(lunaFileName, Path.GetFileName(path), StringComparison.InvariantCultureIgnoreCase);
                }
            }
            else
            {
                colorScheme = null;
            }
        }

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, StringBuilder pszColorBuff, int dwMaxColorChars, StringBuilder pszSizeBuff, int cchMaxSizeChars);
    }

    private enum KnownColors
    {
        ButtonPressedBorder = 0,
        MenuItemBorder = 1,
        MenuItemBorderMouseOver = 2,
        MenuItemSelected = 3,
        CheckBackground = 4,
        CheckBackgroundMouseOver = 5,
        GripDark = 6,
        GripLight = 7,
        MenuStripGradientBegin = 8,
        MenuStripGradientEnd = 9,
        ImageMarginRevealedGradientBegin = 10,
        ImageMarginRevealedGradientEnd = 11,
        ImageMarginRevealedGradientMiddle = 12,
        MenuItemPressedGradientBegin = 13,
        MenuItemPressedGradientEnd = 14,
        ButtonPressedGradientBegin = 0xF,
        ButtonPressedGradientEnd = 0x10,
        ButtonPressedGradientMiddle = 17,
        ButtonSelectedGradientBegin = 18,
        ButtonSelectedGradientEnd = 19,
        ButtonSelectedGradientMiddle = 20,
        OverflowButtonGradientBegin = 21,
        OverflowButtonGradientEnd = 22,
        OverflowButtonGradientMiddle = 23,
        ButtonCheckedGradientBegin = 24,
        ButtonCheckedGradientEnd = 25,
        ButtonCheckedGradientMiddle = 26,
        ImageMarginGradientBegin = 27,
        ImageMarginGradientEnd = 28,
        ImageMarginGradientMiddle = 29,
        MenuBorder = 30,
        ToolStripDropDownBackground = 0x1F,
        ToolStripBorder = 0x20,
        SeparatorDark = 33,
        SeparatorLight = 34,
        LastKnownColor = 34
    }

    private const string blueColorScheme = "NormalColor";

    private const string oliveColorScheme = "HomeStead";

    private const string silverColorScheme = "Metallic";

    private Dictionary<KnownColors, Color> tanRGB;

    public override Color ButtonCheckedGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonCheckedGradientBegin) : base.ButtonCheckedGradientBegin;

    public override Color ButtonCheckedGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonCheckedGradientEnd) : base.ButtonCheckedGradientEnd;

    public override Color ButtonCheckedGradientMiddle => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonCheckedGradientMiddle) : base.ButtonCheckedGradientMiddle;

    public override Color ButtonPressedBorder => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonPressedBorder) : base.ButtonPressedBorder;

    public override Color ButtonPressedGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonPressedGradientBegin) : base.ButtonPressedGradientBegin;

    public override Color ButtonPressedGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonPressedGradientEnd) : base.ButtonPressedGradientEnd;

    public override Color ButtonPressedGradientMiddle => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonPressedGradientMiddle) : base.ButtonPressedGradientMiddle;

    public override Color ButtonSelectedBorder => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonPressedBorder) : base.ButtonSelectedBorder;

    public override Color ButtonSelectedGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonSelectedGradientBegin) : base.ButtonSelectedGradientBegin;

    public override Color ButtonSelectedGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonSelectedGradientEnd) : base.ButtonSelectedGradientEnd;

    public override Color ButtonSelectedGradientMiddle => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonSelectedGradientMiddle) : base.ButtonSelectedGradientMiddle;

    public override Color CheckBackground => !UseBaseColorTable ? FromKnownColor(KnownColors.CheckBackground) : base.CheckBackground;

    public override Color CheckPressedBackground => !UseBaseColorTable ? FromKnownColor(KnownColors.CheckBackgroundMouseOver) : base.CheckPressedBackground;

    public override Color CheckSelectedBackground => !UseBaseColorTable ? FromKnownColor(KnownColors.CheckBackgroundMouseOver) : base.CheckSelectedBackground;

    internal static string ColorScheme => DisplayInformation.ColorScheme;

    private Dictionary<KnownColors, Color> ColorTable
    {
        get
        {
            if (tanRGB == null)
            {
                tanRGB = new Dictionary<KnownColors, Color>(34);
                InitTanLunaColors(ref tanRGB);
            }
            return tanRGB;
        }
    }

    public override Color GripDark => !UseBaseColorTable ? FromKnownColor(KnownColors.GripDark) : base.GripDark;

    public override Color GripLight => !UseBaseColorTable ? FromKnownColor(KnownColors.GripLight) : base.GripLight;

    public override Color ImageMarginGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.ImageMarginGradientBegin) : base.ImageMarginGradientBegin;

    public override Color ImageMarginGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.ImageMarginGradientEnd) : base.ImageMarginGradientEnd;

    public override Color ImageMarginGradientMiddle => !UseBaseColorTable ? FromKnownColor(KnownColors.ImageMarginGradientMiddle) : base.ImageMarginGradientMiddle;

    public override Color ImageMarginRevealedGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.ImageMarginRevealedGradientBegin) : base.ImageMarginRevealedGradientBegin;

    public override Color ImageMarginRevealedGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.ImageMarginRevealedGradientEnd) : base.ImageMarginRevealedGradientEnd;

    public override Color ImageMarginRevealedGradientMiddle => !UseBaseColorTable ? FromKnownColor(KnownColors.ImageMarginRevealedGradientMiddle) : base.ImageMarginRevealedGradientMiddle;

    public override Color MenuBorder => !UseBaseColorTable ? FromKnownColor(KnownColors.MenuBorder) : base.MenuItemBorder;

    public override Color MenuItemBorder => !UseBaseColorTable ? FromKnownColor(KnownColors.MenuItemBorder) : base.MenuItemBorder;

    public override Color MenuItemPressedGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.MenuItemPressedGradientBegin) : base.MenuItemPressedGradientBegin;

    public override Color MenuItemPressedGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.MenuItemPressedGradientEnd) : base.MenuItemPressedGradientEnd;

    public override Color MenuItemPressedGradientMiddle => !UseBaseColorTable ? FromKnownColor(KnownColors.ImageMarginRevealedGradientMiddle) : base.MenuItemPressedGradientMiddle;

    public override Color MenuItemSelected => !UseBaseColorTable ? FromKnownColor(KnownColors.MenuItemSelected) : base.MenuItemSelected;

    public override Color MenuItemSelectedGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonSelectedGradientBegin) : base.MenuItemSelectedGradientBegin;

    public override Color MenuItemSelectedGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.ButtonSelectedGradientEnd) : base.MenuItemSelectedGradientEnd;

    public override Color MenuStripGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.MenuStripGradientBegin) : base.MenuStripGradientBegin;

    public override Color MenuStripGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.MenuStripGradientEnd) : base.MenuStripGradientEnd;

    public override Color OverflowButtonGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.OverflowButtonGradientBegin) : base.OverflowButtonGradientBegin;

    public override Color OverflowButtonGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.OverflowButtonGradientEnd) : base.OverflowButtonGradientEnd;

    public override Color OverflowButtonGradientMiddle => !UseBaseColorTable ? FromKnownColor(KnownColors.OverflowButtonGradientMiddle) : base.OverflowButtonGradientMiddle;

    public override Color RaftingContainerGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.MenuStripGradientBegin) : base.RaftingContainerGradientBegin;

    public override Color RaftingContainerGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.MenuStripGradientEnd) : base.RaftingContainerGradientEnd;

    public override Color SeparatorDark => !UseBaseColorTable ? FromKnownColor(KnownColors.SeparatorDark) : base.SeparatorDark;

    public override Color SeparatorLight => !UseBaseColorTable ? FromKnownColor(KnownColors.SeparatorLight) : base.SeparatorLight;

    public override Color ToolStripBorder => !UseBaseColorTable ? FromKnownColor(KnownColors.ToolStripBorder) : base.ToolStripBorder;

    public override Color ToolStripDropDownBackground => !UseBaseColorTable ? FromKnownColor(KnownColors.ToolStripDropDownBackground) : base.ToolStripDropDownBackground;

    public override Color ToolStripGradientBegin => !UseBaseColorTable ? FromKnownColor(KnownColors.ImageMarginGradientBegin) : base.ToolStripGradientBegin;

    public override Color ToolStripGradientEnd => !UseBaseColorTable ? FromKnownColor(KnownColors.ImageMarginGradientEnd) : base.ToolStripGradientEnd;

    public override Color ToolStripGradientMiddle => !UseBaseColorTable ? FromKnownColor(KnownColors.ImageMarginGradientMiddle) : base.ToolStripGradientMiddle;

    private bool UseBaseColorTable
    {
        get
        {
            bool flag = !DisplayInformation.IsLunaTheme || (ColorScheme != "HomeStead" && ColorScheme != "NormalColor");
            if (flag && tanRGB != null)
            {
                tanRGB.Clear();
                tanRGB = null;
            }
            return flag;
        }
    }

    private Color FromKnownColor(KnownColors color)
    {
        return ColorTable[color];
    }

    private static void InitTanLunaColors(ref Dictionary<KnownColors, Color> rgbTable)
    {
        rgbTable[KnownColors.GripDark] = Color.FromArgb(193, 190, 179);
        rgbTable[KnownColors.SeparatorDark] = Color.FromArgb(197, 194, 184);
        rgbTable[KnownColors.MenuItemSelected] = Color.FromArgb(193, 210, 238);
        rgbTable[KnownColors.ButtonPressedBorder] = Color.FromArgb(49, 106, 197);
        rgbTable[KnownColors.CheckBackground] = Color.FromArgb(225, 230, 232);
        rgbTable[KnownColors.MenuItemBorder] = Color.FromArgb(49, 106, 197);
        rgbTable[KnownColors.CheckBackgroundMouseOver] = Color.FromArgb(49, 106, 197);
        rgbTable[KnownColors.MenuItemBorderMouseOver] = Color.FromArgb(75, 75, 111);
        rgbTable[KnownColors.ToolStripDropDownBackground] = Color.FromArgb(252, 252, 249);
        rgbTable[KnownColors.MenuBorder] = Color.FromArgb(138, 134, 122);
        rgbTable[KnownColors.SeparatorLight] = Color.FromArgb(255, 255, 255);
        rgbTable[KnownColors.ToolStripBorder] = Color.FromArgb(163, 163, 124);
        rgbTable[KnownColors.MenuStripGradientBegin] = Color.FromArgb(229, 229, 215);
        rgbTable[KnownColors.MenuStripGradientEnd] = Color.FromArgb(244, 242, 232);
        rgbTable[KnownColors.ImageMarginGradientBegin] = Color.FromArgb(254, 254, 251);
        rgbTable[KnownColors.ImageMarginGradientMiddle] = Color.FromArgb(236, 231, 224);
        rgbTable[KnownColors.ImageMarginGradientEnd] = Color.FromArgb(189, 189, 163);
        rgbTable[KnownColors.OverflowButtonGradientBegin] = Color.FromArgb(243, 242, 240);
        rgbTable[KnownColors.OverflowButtonGradientMiddle] = Color.FromArgb(226, 225, 219);
        rgbTable[KnownColors.OverflowButtonGradientEnd] = Color.FromArgb(146, 146, 118);
        rgbTable[KnownColors.MenuItemPressedGradientBegin] = Color.FromArgb(252, 252, 249);
        rgbTable[KnownColors.MenuItemPressedGradientEnd] = Color.FromArgb(246, 244, 236);
        rgbTable[KnownColors.ImageMarginRevealedGradientBegin] = Color.FromArgb(247, 246, 239);
        rgbTable[KnownColors.ImageMarginRevealedGradientMiddle] = Color.FromArgb(242, 240, 228);
        rgbTable[KnownColors.ImageMarginRevealedGradientEnd] = Color.FromArgb(230, 227, 210);
        rgbTable[KnownColors.ButtonCheckedGradientBegin] = Color.FromArgb(225, 230, 232);
        rgbTable[KnownColors.ButtonCheckedGradientMiddle] = Color.FromArgb(225, 230, 232);
        rgbTable[KnownColors.ButtonCheckedGradientEnd] = Color.FromArgb(225, 230, 232);
        rgbTable[KnownColors.ButtonSelectedGradientBegin] = Color.FromArgb(193, 210, 238);
        rgbTable[KnownColors.ButtonSelectedGradientMiddle] = Color.FromArgb(193, 210, 238);
        rgbTable[KnownColors.ButtonSelectedGradientEnd] = Color.FromArgb(193, 210, 238);
        rgbTable[KnownColors.ButtonPressedGradientBegin] = Color.FromArgb(152, 181, 226);
        rgbTable[KnownColors.ButtonPressedGradientMiddle] = Color.FromArgb(152, 181, 226);
        rgbTable[KnownColors.ButtonPressedGradientEnd] = Color.FromArgb(152, 181, 226);
        rgbTable[KnownColors.GripLight] = Color.FromArgb(255, 255, 255);
    }
}
