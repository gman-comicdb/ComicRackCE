using cYo.Common.Windows.Forms.Theme.DarkMode.Rendering;
using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using cYo.Common.Windows.Forms.Theme.Resources;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace cYo.Common.Windows.Forms.Theme.DarkMode;

/// <summary>
/// Provides Dark Mode extension methods to be used outside the <see cref="DarkMode"/> namespace.
/// </summary>
/// <summary>
/// Provides extended Dark Mode-related functionality for use outside of the <see cref="Theme.DarkMode"/> namespace.
/// </summary>
/// <remarks>
/// Methods generally extend <see cref="Control"/> or forward delegates to <see cref="DrawDark"/>/<see cref="GraphicsExtensions"/>
/// </remarks>
internal static class DarkThemeExtensions
{
    #region Control Extensions
    // Provide a way to color controls based on their UI "role" as oppose to their Type
    internal static void SetUIComponentColor(this Control control, UIComponent component)
    {
        Color backColor = DarkColors.GetUIComponentColor(component);
        if (backColor != Color.Empty)
        {
            control.BackColor = backColor;
            if (control.GetType().IsSubclassOf(typeof(TreeView)))
                TreeViewEx.SetColor((TreeView)control, backColor);
        }
    }

    /// <summary>
	/// Sets <see cref="TreeView.ForeColor"/> and <see cref="TreeView.BackColor"/>.
	/// </summary>
    /// <remarks>
    /// This is not done purely via <see cref="Theme.Resources.ThemeColors"/> as additional Win32 method call is required.<br/>
    /// <see cref="DarkControlDefinition"/> is based on <see cref="System.Type"/> and is not suitable for instance-specific settings.
    /// </remarks>
    // REVIEW : Review why this is required when TreeView base calls SetColor() equivalent
    internal static void SetTreeViewColor(this TreeView treeView)
    {
        treeView.BackColor = DarkColors.TreeView.Back;
        treeView.ForeColor = DarkColors.TreeView.Text;
        TreeViewEx.SetColor(treeView, DarkColors.TreeView.Back, DarkColors.TreeView.Text);
    }

    // HACK: Re-size button because its "theme parts" are larger in Dark Mode than in Light Mode
    // Removing this might require using <see cref="UXTheme"/> + OpenThemeData + GetTheme*, which is not worth the effort.
    // Alternatively could change the Designer Location/Size, but that will affect the default theme.
    /// <summary>
	/// Resizes <see cref="Button"/> pretending to be a <see cref="ComboBox"/> control.<br/>
    /// Because it's larger than the Light Mode counterpart but needs to fit in the same bounds (otherwise borders are chopped off)
	/// </summary>
    internal static void SetComboBoxButton(this Button button)
    {
        button.Location = new Point(button.Location.X, button.Location.Y + 1);
        button.Size = new Size(button.Size.Width, button.Size.Height - 2);
        button.BackColor = DarkColors.Button.Back;
        button.ForeColor = DarkColors.Button.Text;
    }
    #endregion

    // Provide a single point of entry for ThemeExtensions methods which need to make use of DarkMode internal methods
    #region Drawing Extensions [DarkMode.Rendering]
    internal static void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        => DrawDarkListView.ColumnHeader(sender, e);

    internal static void DrawSplitButtonBase(Graphics graphics, Rectangle rect, PushButtonState state)
        => DrawDark.ButtonBase(graphics, rect, state);

    internal static void DrawTabItem(Graphics g, Rectangle rect, TabItemState tabItemState, bool buttonMode)
        => DrawDark.TabItem(g, rect, tabItemState, buttonMode);
	#endregion

	#region Other
    // Modifies an HTML page to add a style attribute to the body that will replace colors for a dark mode
	internal static string ReplaceWebColors(this string webPage)
	{
		Regex rxWebBody = new Regex(@"<body(?=[^>]*)([^>]*?)\bstyle=""([^""]*)""([^>]*)>|<body([^>]*)>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
		string rxWebBodyReplace = "<body$1 style=\"$2background-color:#383838;color:#eeeeee;scrollbar-face-color:#4d4d4d;scrollbar-track-color:#171717;scrollbar-shadow-color:#171717;scrollbar-arrow-color:#676767;\"$3>";

		return rxWebBody.Replace(webPage, rxWebBodyReplace);
	}
	#endregion
}

/// <summary>
/// Draw <see cref="EventArgs"/> Extensions. Provide parity with native draw <see cref="EventArgs"/> methods, with <see cref="ThemeExtensions.InvokeAction"/> determining which method should be called.<br/>
/// Actual drawing is handled in <see cref="GraphicsExtensions"/>.
/// </summary>
/// <remarks>
/// Mostly <see cref="DrawItemEventArgs"/> methods.
/// </remarks>
internal static class DrawEventArgsExtensions
{
    public enum ItemHighlightStyle
    {
        None,
        Active,
        Inactive
    }

    public static ItemHighlightStyle GetItemHighlight(DrawItemState itemState)
        => GetItemHighlight(itemState, false);

    public static ItemHighlightStyle GetItemHighlight(DrawItemState itemState, bool controlFocused)
    {
        bool selected = (itemState & DrawItemState.Selected) == DrawItemState.Selected;
        bool focused = (itemState & DrawItemState.Focus) == DrawItemState.Focus;
        bool inactive = (itemState & DrawItemState.Inactive) == DrawItemState.Inactive;
        bool hot = (itemState & DrawItemState.HotLight) == DrawItemState.HotLight;
        bool disabled = (itemState & DrawItemState.Disabled) == DrawItemState.Disabled;
        bool comboEdit = (itemState & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit;

        if (disabled || comboEdit)
            return ItemHighlightStyle.None;
        else if (!inactive && selected && (focused | controlFocused) | hot)
            return ItemHighlightStyle.Active;
        else if (selected)
            return ItemHighlightStyle.Inactive;

        return ItemHighlightStyle.None;
    }


    #region DrawItem
    internal static void DrawDarkFocusRectangle(this DrawItemEventArgs e, bool controlFocused = false)
        => e.DrawDarkFocusRectangle(GetItemHighlight(e.State, controlFocused));

    internal static void DrawDarkFocusRectangle(this DrawItemEventArgs e, ItemHighlightStyle style)
    {
        Color focusColor = style == ItemHighlightStyle.Active ? DarkColors.SelectedText.Focus
            : style == ItemHighlightStyle.Inactive ? DarkColors.SelectedText.InactiveFocus : Color.Empty;

        if (focusColor != Color.Empty)
            e.Graphics.DrawDarkFocusRectangle(e.Bounds, focusColor);
    }

    internal static void DrawDarkBackground(this DrawItemEventArgs e)
        => e.DrawDarkBackground(e.BackColor);

    internal static void DrawDarkBackground(this DrawItemEventArgs e, bool drawFocus = false, bool controlFocused = false)
        => e.DrawDarkBackground(e.BackColor, drawFocus, controlFocused);

    internal static void DrawDarkBackground(this DrawItemEventArgs e, Color backColor, bool drawFocus = false, bool controlFocused = false)
    {

        ItemHighlightStyle style = GetItemHighlight(e.State, controlFocused);
        e.DrawDarkBackground(backColor, style);
        if (drawFocus)
            e.DrawDarkFocusRectangle(style);
    }

    internal static void DrawDarkBackground(this DrawItemEventArgs e, Color backColor, ItemHighlightStyle style)
    {
        if (style == ItemHighlightStyle.Active)
            backColor = DarkColors.SelectedText.Highlight;
        else if (style == ItemHighlightStyle.Inactive)
            backColor = DarkColors.SelectedText.InactiveHighlight;
        else if ((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit)
            backColor = (e.State & DrawItemState.Disabled) == DrawItemState.Disabled
                ? DarkColors.ComboBox.Disabled
                : DarkColors.ComboBox.Back;

        e.Graphics.DrawDarkBackground(e.Bounds, backColor);
    }

    internal static void DrawDarkString(this DrawItemEventArgs e, string text)
    {
        using StringFormat format = new StringFormat(StringFormatFlags.NoWrap)
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Near
        };
        e.Graphics.DrawDarkString(text, e.Font, e.ForeColor, e.Bounds, format);
    }
    #endregion

    #region DrawToolTip
    internal static void DrawDarkBackground(this DrawToolTipEventArgs e, Color? backColor = null)
        => e.Graphics.DrawDarkBackground(e.Bounds, backColor ?? DarkColors.ToolTip.Back);

    internal static void DrawDarkBorder(this DrawToolTipEventArgs e)
        => e.Graphics.DrawDarkBorder(e.Bounds);

    internal static void DrawDarkText(this DrawToolTipEventArgs e, Color? textColor = null)
        => TextRenderer.DrawText(e.Graphics, e.ToolTipText, e.Font, e.Bounds, textColor ?? ThemeColors.ToolTip.InfoText);

    internal static void DrawDarkText(this DrawToolTipEventArgs e, Rectangle bounds, Color? textColor = null)
        => TextRenderer.DrawText(e.Graphics, e.ToolTipText, e.Font, bounds, textColor ?? ThemeColors.ToolTip.InfoText);
    #endregion

    #region ListViewColumnHeader
    internal static void DrawDarkBackground(this DrawListViewColumnHeaderEventArgs e)
        => e.Graphics.DrawDarkBackground(e.Bounds, DarkColors.Header.Back);

    internal static void DrawDarkSeparator(this DrawListViewColumnHeaderEventArgs e)
        => e.Graphics.DrawDarkColumnSeparator(e.Bounds, DarkColors.Header.Separator);

    internal static void DrawDarkString(this DrawListViewColumnHeaderEventArgs e)
    {
        using StringFormat stringFormat = new StringFormat
        {
            Alignment = StringAlignment.Near, // left align text
            LineAlignment = StringAlignment.Center, // vertically center text
            Trimming = StringTrimming.EllipsisCharacter
        };
        e.Graphics.DrawDarkString(e.Header.Text, e.Font, DarkColors.Header.Text, e.Bounds, stringFormat);
    }
    #endregion

    #region ListViewSubItem
    //internal static void DrawDarkBackground(this DrawListViewSubItemEventArgs e, Color backColor)
    //    => e.Graphics.DrawDarkBackground(e.Bounds, backColor);

    internal static void DrawDarkBackground(this DrawListViewSubItemEventArgs e)
    {
        // internal implementation
        //Color color = ((itemIndex == -1) ? item.BackColor : subItem.BackColor);
        //using Brush brush = new SolidBrush(color);
        //Graphics.FillRectangle(brush, bounds);

        Color backColor = e.Item.Selected ? DarkColors.SelectedText.Highlight : e.Item.BackColor;
        e.Graphics.DrawDarkBackground(e.Bounds, backColor);

    }

    internal static void DrawDarkFocusRectangle(this DrawListViewSubItemEventArgs e)
    {
        // internal implementation
        //if ((itemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
        //{
        //    ControlPaint.DrawFocusRectangle(graphics, Rectangle.Inflate(bounds, -1, -1), item.ForeColor, item.BackColor);
        //}
        if ((e.ItemState & ListViewItemStates.Focused) == ListViewItemStates.Focused)
            //ControlPaint.DrawFocusRectangle(e.Graphics, Rectangle.Inflate(e.Bounds, -1, -1), e.Item.ForeColor, e.Item.BackColor);
            e.Graphics.DrawDarkFocusRectangle(Rectangle.Inflate(e.Bounds, -1, -1));
    }
    #endregion
}

/// <summary>
/// <see cref="Graphics"/> Extensions. Allow custom colors used for simple drawing.<br/>
/// The logic around calculations/geometry is the same as internal .NET methods; changes may be required due to Dark Mode having different part sizes.
/// </summary>
internal static class GraphicsExtensions
{
    internal static void DrawDarkStringDisabled(this Graphics g, string text, Font font, Color color, Rectangle bounds, TextFormatFlags textFormatFlags)
        => TextRenderer.DrawText(g, text, font, bounds, SystemColors.GrayText, textFormatFlags);

    internal static void DrawDarkString(this Graphics g, string text, Font font, Color color, Rectangle bounds, StringFormat stringFormat)
        => g.DrawString(text, font, DarkBrushes.FromDarkColor(color), bounds, stringFormat);

    internal static void DrawDarkColumnSeparator(this Graphics g, Rectangle bounds, Color color)
    {
        int x = bounds.Right - 2;
        g.DrawLine(DarkPens.FromDarkColor(color), new Point(x, bounds.Top), new Point(x, bounds.Bottom));
    }

    // DrawBackground
    internal static void DrawDarkBackground(this Graphics g, Rectangle bounds, Color backColor)
    {
        g.FillRectangle(DarkBrushes.FromDarkColor(backColor), bounds);
    }

    // DrawBorder
    internal static void DrawDarkBorder(this Graphics g, Rectangle bounds)
        => g.DrawDarkBorder(bounds, DarkColors.Border.Default);

    internal static void DrawDarkBorder(this Graphics g, Rectangle bounds, Color borderColor)
    {
        g.DrawRectangle(DarkPens.FromDarkColor(borderColor), bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
    }

    // DrawFocusRectangle
    public static void DrawDarkFocusRectangle(this Graphics g, Rectangle bounds)
        => g.DrawDarkFocusRectangle(bounds, DarkColors.SelectedText.Focus);

    public static void DrawDarkFocusRectangle(this Graphics g, Rectangle bounds, Color focusColor)
    {
        bounds.Width--;
        bounds.Height--;
        g.DrawRectangle(DarkPens.FromDarkColor(focusColor), bounds);
    }
}

internal static class PaintEventArgsExtensions
{
    // currently only for RatingControl and CollapsibleGroupBox
    internal static void DrawDarkBackground(this PaintEventArgs e, Rectangle bounds, Color backColor, Rectangle content)
    {
        e.Graphics.Clear(backColor);

        // Draw border if VisualStyleRenderer indicates content area is smaller than bounds
        if (content.Width < bounds.Width || content.Height < bounds.Height)
            e.Graphics.DrawDarkBorder(bounds);
    }
}
