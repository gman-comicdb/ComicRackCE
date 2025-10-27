using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;

internal static class DarkEventHandlers
{

    internal static void TextBox_Mouse(TextBoxBase textBox)
    {
        textBox.MouseLeave -= DarkTextBox.MouseLeave;
        textBox.MouseLeave += DarkTextBox.MouseLeave;
        textBox.MouseHover -= DarkTextBox.MouseHover;
        textBox.MouseHover += DarkTextBox.MouseHover;
        textBox.Enter -= DarkTextBox.Enter;
        textBox.Enter += DarkTextBox.Enter;
        textBox.Leave -= DarkTextBox.Leave;
        textBox.Leave += DarkTextBox.Leave;
    }

    internal static void ListView_Draw(ListView listView)
    {
        listView.DrawColumnHeader -= DarkListView.DrawColumnHeader;
        listView.DrawColumnHeader += DarkListView.DrawColumnHeader;
        listView.DrawItem -= DarkListView.DrawItem;
        listView.DrawItem += DarkListView.DrawItem;
        listView.DrawSubItem -= DarkListView.DrawSubItem;
        listView.DrawSubItem += DarkListView.DrawSubItem;
    }

    internal static void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        => DarkListView.DrawColumnHeader(sender, e);

    // TODO: handle disabled CheckBoxes with images/icons (currently they're wiped)
    public static void CheckBox_Paint(object sender, PaintEventArgs e)
    {
        CheckBox checkBox = sender as CheckBox;
        bool onlyDrawDisabledText = OsVersionEx.IsWindows11_OrGreater() || checkBox.Appearance == Appearance.Button;

        // default OS drawing for enabled Win11 CheckBox or Appearance.Button
        if (onlyDrawDisabledText && checkBox.Enabled) return;

        // Draw actual Check (Box + Mark)
        e.PaintDarkCheckBox(checkBox, onlyDrawDisabledText);
    }

    // Use custom SelectedText Highlight color.
    // related: cYo.Common.Windows.Forms.ComboBoxSkinner.comboBox_DrawItem (private, requires instantiation, comes with ComboBoxSkinner baggage)
    // currently unused as CustomDraw means DropDown button has incorrect style for DropDownList
    public static void ComboBox_DrawItemHighlight(object sender, DrawItemEventArgs e)
    {
        if (e.Index < 0)
            return;

        ComboBox comboBox = (ComboBox)sender;
        var item = comboBox.Items[e.Index];

        // override SelectedText highlighting
        if (comboBox.DroppedDown)
        {
            e.DrawDarkBackground();
            e.DrawDarkFocusRectangle();
        }
        e.DrawDarkString(comboBox.GetItemText(item));
    }

    /// <summary>
    /// Paint disabled Label text.
    /// </summary>
    internal static void Label_PaintDisabled(object sender, PaintEventArgs e)
    {
        Label label = sender as Label;

        if (!label.Enabled)
        {
            TextFormatFlags textFormatFlags = Helpers.GetTextFormatFlags(label);
            TextRenderer.DrawText(e.Graphics, label.Text, label.Font, label.ClientRectangle, SystemColors.GrayText, label.BackColor, textFormatFlags);
        }
    }

    // SOON : Use ControlPaintEx
    /// <summary>
    /// Paint ToolStripStatusLabel border
    /// </summary>
    internal static void ToolStripStatusLabel_PaintBorder(object sender, PaintEventArgs e)
    {
        using (var pen = new Pen(DarkColors.ToolStrip.BorderColor, 1))
        {
            e.Graphics.DrawRectangle(pen, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
        }
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
 
    #region ListView
    private static class DarkListView
    {
        /// <summary>
        /// Custom <see cref="DrawListViewColumnHeaderEventHandler"/>. Draws light <see cref="System.Windows.Forms.ColumnHeader"/> text on dark background.
        /// </summary>
        /// <remarks>
        /// <c>DrawDefault</c> is executed unless <see cref="ListView.View"/> is set to <see cref="View.Details"/>
        /// </remarks>
        internal static void DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if ((sender as ListView)?.View != View.Details)
            {
                e.DrawDefault = true;
                return;
            }
            e.DrawDefault = false;
            e.DrawDarkColumnHeader();
        }

        internal static void DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.Item.ListView.View != View.Details || e.Item.ListView.CheckBoxes)
                e.DrawDefault = true;
        }

        internal static void DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.Item.ListView.View != View.Details || e.Item == null)
            {
                e.DrawDefault = true;
                return;
            }
            e.DrawDefault = false;
            e.DrawDarkSubItem();
        }
    }
    #endregion

}