using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using System;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;

internal static class DarkEventHandlers
{

    internal static void TextBox_Mouse(TextBoxBase textBox)
    {
        textBox.Attach(DarkTextBox.MouseLeave);
        textBox.Attach(DarkTextBox.MouseHover);
        textBox.Attach(DarkTextBox.Enter);
        textBox.Attach(DarkTextBox.Leave);
    }

    #region TextBox
    private static class DarkTextBox
    {
        internal static void MouseLeave(object sender, EventArgs e)
        {
            if (!(sender as TextBoxBase).Focused)
                (sender as TextBoxBase).BackColor = DarkColors.TextBox.Back;
        }
        internal static void MouseHover(object sender, EventArgs e)
        {
            TextBoxBase textBox = sender as TextBoxBase;
            if (textBox.Enabled && !textBox.Focused)
                textBox.BackColor = DarkColors.TextBox.MouseOverBack;
        }
        internal static void Enter(object sender, EventArgs e)
        {
            TextBoxBase textBox = sender as TextBoxBase;
            textBox.BackColor = DarkColors.TextBox.EnterBack;
        }
        internal static void Leave(object sender, EventArgs e)
        {
            TextBoxBase textBox = sender as TextBoxBase;
            textBox.BackColor = DarkColors.TextBox.Back;
        }
    }
    #endregion
}
