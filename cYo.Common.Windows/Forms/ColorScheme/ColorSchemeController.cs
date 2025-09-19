using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace cYo.Common.Windows.Forms.ColorScheme
{
    public class ColorSchemeController
    {
        public static event Action SystemColorsChanging;
        public static event Action SystemColorsChanged;

        public ColorSchemeController()
        {
            // force init color table
            byte unused = SystemColors.Window.R;

            var systemDrawingAssembly = typeof(Color).Assembly;

            //string colorTableField = Runtime.IsMono ? "s_colorTable" : "colorTable";
            string colorTableField = "colorTable";
            _colorTableField = systemDrawingAssembly.GetType("System.Drawing.KnownColorTable")
                .GetField(colorTableField, BindingFlags.Static | BindingFlags.NonPublic);

            _colorTable = readColorTable();
            SystemEvents.UserPreferenceChanging += userPreferenceChanging;

            OriginalColors = _colorTable.ToArray();
            KnownOriginalColors = KnownColors.Cast<int>()
                .ToDictionary(i => i, i => OriginalColors[i]);

            _threadDataProperty = systemDrawingAssembly.GetType("System.Drawing.SafeNativeMethods")
                .GetNestedType("Gdip", BindingFlags.NonPublic)
                .GetProperty("ThreadData", BindingFlags.Static | BindingFlags.NonPublic);

            //string systemBrushesKeyField = Runtime.IsMono ? "s_systemBrushesKey" : "SystemBrushesKey";
            string systemBrushesKeyField = "SystemBrushesKey";

            SystemBrushesKey = typeof(SystemBrushes)
                .GetField(systemBrushesKeyField, BindingFlags.Static | BindingFlags.NonPublic)
                ?.GetValue(null);

            SystemPensKey = typeof(SystemPens)
                .GetField("SystemPensKey", BindingFlags.Static | BindingFlags.NonPublic)
                ?.GetValue(null);
        }

        private void userPreferenceChanging(object sender, UserPreferenceChangingEventArgs e)
        {
            if (e.Category != UserPreferenceCategory.Color)
                return;

            _colorTable = readColorTable();
            fireColorsChangedEvents();
        }

        private static void fireColorsChangedEvents()
        {
            SystemColorsChanging?.Invoke();
            SystemColorsChanged?.Invoke();
        }

        private int[] readColorTable() => (int[])_colorTableField.GetValue(null);

        public void SetColor(KnownColor knownColor, int argb)
        {
            setColor(knownColor, argb);

            if (SystemBrushesKey != null)
                ThreadData[SystemBrushesKey] = null;

            if (SystemPensKey != null)
                ThreadData[SystemPensKey] = null;

            fireColorsChangedEvents();
        }

        private void setColor(KnownColor knownColor, int argb) => _colorTable[(int)knownColor] = argb;

        public int GetOriginalColor(KnownColor knownColor) => OriginalColors[(int)knownColor];

        public int GetColor(KnownColor knownColor)
        {
            if (!KnownColors.Contains(knownColor))
                throw new ArgumentException();

            return _colorTable[(int)knownColor];
        }

        public IReadOnlyDictionary<int, int> Save() => KnownColors.Cast<int>().ToDictionary(i => i, i => _colorTable[i]);

        public void Load(IReadOnlyDictionary<int, int> saved)
        {
            foreach (var color in KnownColors)
            {
                var value = saved.TryGet((int)color, KnownOriginalColors[(int)color]);
                setColor(color, value);
            }

            if (SystemBrushesKey != null)
                ThreadData[SystemBrushesKey] = null;

            if (SystemPensKey != null)
                ThreadData[SystemPensKey] = null;

            fireColorsChangedEvents();
        }

        public void Reset(KnownColor color) => SetColor(color, OriginalColors[(int)color]);

        public void ResetAll() => Load(KnownOriginalColors);

        private IDictionary ThreadData => (IDictionary)_threadDataProperty.GetValue(null, null);

        private object SystemBrushesKey { get; }
        private object SystemPensKey { get; }

        public readonly HashSet<KnownColor> KnownColors = new HashSet<KnownColor>(
            new[]
            {
                SystemColors.Window,             // most backgrounds and gradient start (Toolstrip, menu)
                SystemColors.WindowText,         // most text boxes - list, combo etc
                SystemColors.GrayText,           // disabled menu text (expect should be other controls too)
                SystemColors.Highlight,          // all the highlights (except combobox): toolstrip, selected item, active menu item

                SystemColors.ButtonFace,         // main menu bar, toolstrip gradient
                SystemColors.ButtonShadow,       // menu border. Lines - menu dividers, toolstrip dividers
                SystemColors.ButtonHighlight,    // MainForm bottom line, toolstrip dividers, Re-size grip (bottom right)

                SystemColors.ControlLightLight,  // active tab label background ... but also button highlight
                SystemColors.ControlLight,       // grip highlight, remaining tasks text
                SystemColors.Control,            // Form background (not tabs or menu). gradient end. inactive tab label
                SystemColors.ControlText,        // most read-only text. active menu item. menu arrows
                SystemColors.ControlDark,        // inner active button shadow. selected pref menu buttom (checkbox?) outline
                SystemColors.ControlDarkDark,    // Outer button shadow

                SystemColors.MenuText,           // inactive (not-selected) menu text

                SystemColors.Info,               // hover-over tooltip background
                SystemColors.InfoText,           // hover-over tooltip text

                SystemColors.WindowFrame,        // hover-over tooltip border (probably others but not observed)

                SystemColors.AppWorkspace,       // 1px border around tab|panel|whatever-the-element-is. doubled for open comic as it's a seperate element.

                // these colors were not observed
                // might be due to OS settings, not interacting with a UI element that uses them, or they are genuinely not used
                SystemColors.Desktop,
                SystemColors.ScrollBar,         // I think this is intended as a joke

                SystemColors.HighlightText,
                SystemColors.HotTrack,

                SystemColors.ActiveBorder,
                SystemColors.ActiveCaption,
                SystemColors.ActiveCaptionText,
                SystemColors.GradientActiveCaption,

                SystemColors.InactiveBorder,
                SystemColors.InactiveCaption,
                SystemColors.InactiveCaptionText,
                SystemColors.GradientInactiveCaption,

                SystemColors.Menu,
                SystemColors.MenuBar,
                SystemColors.MenuHighlight

            }.Select(_ => _.ToKnownColor())
        );

        private int[] OriginalColors { get; }
        private IReadOnlyDictionary<int, int> KnownOriginalColors { get; }

        private int[] _colorTable;
        private readonly FieldInfo _colorTableField;
        private readonly PropertyInfo _threadDataProperty;

        public Bitmap RenderCheckboxToBitmap(CheckBoxState state, Size size)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent); // Optional
                CheckBoxRenderer.DrawCheckBox(g, new Point(0, 0), state);
            }
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
            return bmp;
        }

        public void InvertBitmapColors(Bitmap bmp)
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
    }

    public static class CollectionExtensions
    {
        public static TVal TryGet<TKey, TVal>(this IDictionary<TKey, TVal> dict, TKey key)
        {
            if (key == null)
                return default;

            dict.TryGetValue(key, out var val);

            return val;
        }

        public static TVal TryGet<TKey, TVal>(this IReadOnlyDictionary<TKey, TVal> dict, TKey key, TVal defaultValue)
        {
            if (key == null || !dict.TryGetValue(key, out var val))
                return defaultValue;

            return val;
        }
    }

}
