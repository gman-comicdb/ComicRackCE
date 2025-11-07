using cYo.Common.Win32;
using cYo.Common.Windows.Forms.Theme;
using cYo.Common.Windows.Forms.Theme.Resources;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Win32Interop = cYo.Common.Win32.Win32;

namespace cYo.Common.Windows.Forms
{
	public class ComboBoxEx : ComboBox, IPromptText
	{
		private bool quickSelectAll;

		private string promptText;

		private bool focusSelect = true;

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("Appearance")]
		[Description("The prompt text to display when there is nothing in the Text property.")]
		public string PromptText
		{
			get
			{
				return promptText;
			}
			set
			{
				promptText = value;
				if (base.IsHandleCreated)
				{
					SetPromptText();
				}
			}
		}

		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Category("Behavior")]
		[Description("Automatically select the text when control receives the focus.")]
		[DefaultValue(true)]
		public bool FocusSelect
		{
			get
			{
				return focusSelect;
			}
			set
			{
				focusSelect = value;
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			SetPromptText();
		}

        private void SetPromptText()
		{
            Win32Interop.SetPromptText(this, promptText);
		}

		private void DrawDisabledDropDownListText(Graphics g)
		{
            // The text area for DropDownList (excluding the dropdown button)
            Rectangle textBounds = ClientRectangle;
            textBounds.Width -= SystemInformation.VerticalScrollBarWidth;

            // Fill the background
            using var bgBrush = new SolidBrush(ThemeColors.DarkMode.ComboBox.Disabled);
            g.FillRectangle(bgBrush, textBounds);

            // Draw the text
            TextRenderer.DrawText(
                g,
                Text,
                Font,
                textBounds,
                SystemColors.GrayText,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis
			);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

			if (!ThemeManager.IsDarkModeEnabled)
				return;

            if (m.IsColorStatic())
            {
                IntPtr darkBrush = Win32Interop.DrawDisabledComboBox(this, m.WParam, m.LParam);

                if (darkBrush != IntPtr.Zero)
                    m.Result = darkBrush;
                    return;
            }
			else if (m.IsPaint())
			{
				if (!GetStyle(ControlStyles.UserPaint) && (FlatStyle == FlatStyle.Flat || FlatStyle == FlatStyle.Popup) && !(SystemInformation.HighContrast && BackColor == SystemColors.Window))
				{
					if (!Enabled && DropDownStyle == ComboBoxStyle.DropDownList)
					{
						using Graphics g = Graphics.FromHdc(m.WParam);
                        DrawDisabledDropDownListText(g);
                    }
				}
            }
        }

        protected override void OnEnter(EventArgs e)
		{
			if (string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(PromptText))
			{
				Text = PromptText;
			}
			if (Text.Length > 0 && focusSelect)
			{
				SelectAll();
				quickSelectAll = true;
			}
			base.OnEnter(e);
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			if (!string.IsNullOrEmpty(PromptText) && base.SelectedText == PromptText)
			{
				Text = string.Empty;
			}
			quickSelectAll = false;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (quickSelectAll)
			{
				SelectAll();
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			quickSelectAll = false;
		}

		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			quickSelectAll = false;
		}
	}
}
