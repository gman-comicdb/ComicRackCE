using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
//using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using cYo.Common.ComponentModel;
using cYo.Common.Win32;

namespace cYo.Common.Windows.Forms
{
	public static class RichTextBoxExtensions
	{
		public static void RegisterColorize(this RichTextBox rtb, IEnumerable<ValuePair<Color, Regex>> colors)
		{
			Action action = delegate
			{
				using (rtb.SuspendPainting())
				{
					rtb.SelectAll();
					rtb.SelectionColor = SystemColors.WindowText;
					foreach (ValuePair<Color, Regex> color in colors)
					{
						foreach (Match item in color.Value.Matches(rtb.Text))
						{
							rtb.Select(item.Index, item.Length);
							rtb.SelectionColor = color.Key;
						}
					}
				}
			};
			rtb.TextChanged += delegate
			{
				action();
			};
			action();
		}

		public static void RegisterColorize(this RichTextBox rtb, IEnumerable<ValuePair<Color, string>> colors)
		{
			rtb.RegisterColorize(colors.Select((ValuePair<Color, string> vp) => new ValuePair<Color, Regex>(vp.Key, new Regex(vp.Value, RegexOptions.IgnoreCase))));
		}

		public static void RegisterColorize(this RichTextBox rtb, Color color, string expression)
		{
			rtb.RegisterColorize(new ValuePair<Color, string>[1]
			{
				new ValuePair<Color, string>(color, expression)
			});
		}

		public static void RegisterColorize(this RichTextBox rtb, Color color, Regex expression)
		{
			rtb.RegisterColorize(new ValuePair<Color, Regex>[1]
			{
				new ValuePair<Color, Regex>(color, expression)
			});
		}

		public static IDisposable SuspendPainting(this RichTextBox rtb)
		{
			return new PaintSuspend(rtb);
		}
	}
}
