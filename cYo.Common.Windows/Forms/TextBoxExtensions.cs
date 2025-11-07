using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Win32Interop = cYo.Common.Win32.Win32;

namespace cYo.Common.Windows.Forms
{
	public static class TextBoxExtensions
	{
		public const string OnlyNumberKeys = "0123456789.,";

		public static void SetCueText(this TextBox tb, string text)
		{
            Win32Interop.SetCueText(tb, text);
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
	}
}
