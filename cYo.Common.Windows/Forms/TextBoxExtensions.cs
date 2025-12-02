using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms
{
	public static class TextBoxExtensions
	{
		private static class Native
		{
			public const int ECM_FIRST = 5376;

			public const int EM_SETCUEBANNER = 5377;

			public const int EM_GETCUEBANNER = 5378;

			[DllImport("user32.dll", CharSet = CharSet.Auto)]
			public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
		}

		public const string OnlyNumberKeys = "0123456789.,";

        private static void SetCueTextLegacy(this TextBox tb, string text)
		{
            Native.SendMessage(tb.Handle, Native.EM_SETCUEBANNER, IntPtr.Zero, text);
        }

        public static void SetCueText(this TextBox tb, string text, bool legacy = false)
		{
			if (legacy)
				tb.SetCueTextLegacy(text);
			else
                tb.SetCueText(text, SystemColors.GrayText);

        }

        public static void SetCueText(this TextBox tb, string text, Color textColor)
		{
            using var graphics = tb.CreateGraphics();
            using var italic = new Font(tb.Font.FontFamily, tb.Font.Size, FontStyle.Italic);
            TextRenderer.DrawText(
                graphics,
                text,
                italic,
                tb.ClientRectangle,
                textColor, // use ThemeColors.PromptText or similar
                tb.BackColor,
                tb.GetTextFormatFlags() // TextAlign HorizontalAlignment -> TextFormatFlags
            );
        }

		public static void EnableKeys(this TextBoxBase tb, IEnumerable<char> enabledKeys)
		{
			tb.KeyPress += delegate(object s, KeyPressEventArgs e)
			{
				e.Handled = !enabledKeys.Contains(e.KeyChar) && !"\b".Contains(e.KeyChar);
			};
		}

		public static void EnableOnlyNumberKeys(this TextBoxBase tb)
		{
			tb.EnableKeys("0123456789.,");
		}

        /// <summary>
        /// Converts <see cref="TextBox.TextAlign"/> from <see cref="HorizontalAlignment"/> to <see cref="TextFormatFlags"/>.
        /// </summary>
        public static TextFormatFlags GetTextFormatFlags(this TextBox tb)
        {
            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
			if (tb.RightToLeft == RightToLeft.Yes)
				flags |= TextFormatFlags.RightToLeft;
			else
				flags &= ~TextFormatFlags.RightToLeft;

            return tb.TextAlign switch
			{
				HorizontalAlignment.Left => flags |= TextFormatFlags.Left,

				HorizontalAlignment.Center => flags |= TextFormatFlags.HorizontalCenter,

				HorizontalAlignment.Right => flags |= TextFormatFlags.Right,

				_ => flags |= TextFormatFlags.Left
			};
        }
    }
}
