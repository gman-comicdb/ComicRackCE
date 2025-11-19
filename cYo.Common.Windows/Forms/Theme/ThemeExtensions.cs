using cYo.Common.Win32;
using cYo.Common.Windows.Forms.Theme.DarkMode;
using cYo.Common.Windows.Forms.Theme.DarkMode.Resources;
using cYo.Common.Windows.Forms.Theme.Internal;
using cYo.Common.Windows.Forms.Theme.Resources;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace cYo.Common.Windows.Forms.Theme;

/// <summary>
/// Provides extended Theme-related functionality for use outside of the <see cref="Forms.Theme"/> namespace, most notably <see cref="Theme"/>.
/// </summary>
/// <remarks>
/// Methods generally extend <see cref="Control"/>, <see cref="VisualStyleRenderer"/> or <see cref="DrawItemEventHandler"/>/<see cref="PaintEventHandler"/>.<br/>
/// <see cref="ControlPaint"/> does not allow extensions: <see cref="ControlPaintEx"/> should be used instead.
/// </remarks>
public static class ThemeExtensions
{
    /// <summary>
    /// Themes a <see cref="Control"/>, recursively. The specifics are determined by the current <see cref="ThemeHandler"/>.
    /// </summary>
    /// <param name="control"><see cref="Control"/> to be themed. Child controls will be themed.</param>
    public static void Theme(this Control control) => ThemeHandler.SetTheme(control, recursive: true);

    /// <summary>
    /// Themes a <see cref="Control"/>. The specifics are determined by the current <see cref="ThemeHandler"/>.
    /// </summary>
    /// <param name="control"><see cref="Control"/> to be themed. Child controls will not be themed.</param>
    public static void ThemeControl(this Control control) => ThemeHandler.SetTheme(control, recursive: false);

    /// <summary>
    /// Runs the provided <see cref="Action"/> only if the Theme isn't <see cref="Themes.Default"/> or if <paramref name="isDefaultAction"/> is false and we are in the <see cref="Themes.Default"/> Theme, then it will always draw.
    /// </summary>
    /// <param name="action">The <see cref="Action"/> to run</param>
    /// <param name="isDefaultAction"></param>
    /// <returns>a <see cref="bool"/> if it was successful in drawing</returns>
    public static bool InvokeAction(Action action, bool isDefaultAction = false)
    {
        if (!ThemeColors.IsDefault && !isDefaultAction || ThemeColors.IsDefault && isDefaultAction) // We are themed or we have requested to only draw when in the default 
        {
            action();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Runs one of two provided <see cref="Action"/>s depending on whether <see cref="ThemeManager.IsDarkModeEnabled"/> is <paramref name="true"/> or <paramref name="false"/>.
    /// </summary>
    /// <param name="defaultAction"><see cref="Action"/> to run when <see cref="ThemeManager.IsDarkModeEnabled"/> is <paramref name="false"/></param>
    /// <param name="darkModeAction"><see cref="Action"/> to run when <see cref="ThemeManager.IsDarkModeEnabled"/> is <paramref name="true"/> </param>
    public static void InvokeAction(Action defaultAction, Action darkModeAction)
    {
        if (ThemeManager.IsDarkModeEnabled)
            darkModeAction();
        else
            defaultAction();
    }

    /// <summary>
    /// Runs one of two provided <see cref="Func{TResult}"/> depending on whether <see cref="ThemeManager.IsDarkModeEnabled"/> is <paramref name="true"/> or <paramref name="false"/>.
    /// </summary>
    /// <param name="defaultFunc"><see cref="Func{TResult}"/> to run when <see cref="ThemeManager.IsDarkModeEnabled"/> is <paramref name="false"/></param>
    /// <param name="darkModeFunc"><see cref="Func{TResult}"/> to run when <see cref="ThemeManager.IsDarkModeEnabled"/> is <paramref name="true"/> </param>
    public static TResult InvokeFunc<TResult>(Func<TResult> defaultFunc, Func<TResult> darkModeFunc)
    {
        if (ThemeManager.IsDarkModeEnabled)
            return darkModeFunc();
        else
            return defaultFunc();
    }

    /// <summary>
	/// Invokes <see cref="Action{Control}"/> if <see cref="Control.IsHandleCreated"/>, and subscribes to
	/// <see cref="Control.OnHandleCreated(EventArgs)"/> otherwise.
    /// </summary>
    internal static void WhenHandleCreated(this Control control, Action<Control> handleCreatedAction)
    {
        if (handleCreatedAction == null)
            return;

        if (control.IsHandleCreated)
        {
            handleCreatedAction(control);
            return;
        }

        EventHandler handler = null!;
        handler = (s, e) =>
        {
            control.HandleCreated -= handler;
            handleCreatedAction(s as Control);
        };
        control.HandleCreated += handler;
    }

    // Generally, Dark Mode instance theming
    #region Control Extensions
    /// <summary>
    /// Apply <see cref="UIComponent.SidePanel"/> <typeparamref name="BackColor"/> to a <see cref="Control"/>.
    /// </summary>
    /// <remarks>
    /// This cannot be applied on the <see cref="System.Type"/> as it only applies to specific instances.
    /// </remarks>
    public static void SetSidePanelColor(this Control control)
        => InvokeAction(() => DarkThemeExtensions.SetUIComponentColor(control, UIComponent.SidePanel));

    /// <summary>
    /// Apply <see cref="UIComponent.Content"/> <typeparamref name="BackColor"/> to a <see cref="Control"/>.
    /// </summary>
    public static void SetContentColor(this Control control)
        => InvokeAction(() => DarkThemeExtensions.SetUIComponentColor(control, UIComponent.Content));

    /// <summary>
    /// Apply <see cref="UIComponent.Window"/> <typeparamref name="BackColor"/> to a <see cref="Control"/>.
    /// </summary>
    public static void SetWindowColor(this Control control)
        => InvokeAction(() => DarkThemeExtensions.SetUIComponentColor(control, UIComponent.Window));

    /// <summary>
    /// Resizes <see cref="Button"/> pretending to be a <see cref="ComboBox"/> control.
	/// </summary>
    public static void SetComboBoxButton(this Button button)
        => InvokeAction(() => DarkThemeExtensions.SetComboBoxButton(button));

    /// <summary>
	/// <term>DarkMode <see cref="Action"/></term>
	/// <description>Set <see cref="TreeView.ForeColor"/> and <see cref="TreeView.BackColor"/></description>
	/// </summary>
    public static void SetTreeViewColor(this TreeView treeView)
        => InvokeAction(() => DarkThemeExtensions.SetTreeViewColor(treeView));
    #endregion

    // Generally, either draw default or draw Dark Mode version
    #region Drawing Extensions
    /// <summary>
    /// <list type="bullet">
    /// <item>
	/// <term>Default <see cref="Action"/></term>
	/// <description>System draws default <see cref="ColumnHeader"/></description>
	/// </item>
    /// <item>
	/// <term>DarkMode <see cref="Action"/></term>
	/// <description><see cref="DarkThemeExtensions"/> draws <see cref="ColumnHeader"/></description>
	/// </item>
    /// </list>
    /// </summary>
    public static void ListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        => InvokeAction(
            () => e.DrawDefault = true,
            () => DarkThemeExtensions.ListView_DrawColumnHeader(sender, e)
        );

    public static void DrawThemeBackground(this DrawListViewSubItemEventArgs e)
    => InvokeAction(
        () => e.DrawBackground(),
        () => e.DrawDarkBackground()
    );

    public static void DrawThemeFocusRectangle(this DrawItemEventArgs e)
        => InvokeAction(
            () => e.DrawFocusRectangle(),
            () => e.DrawDarkFocusRectangle()
        );

    public static void DrawThemeFocusRectangle(this DrawItemEventArgs e, bool focused)
        => InvokeAction(
            () => e.DrawFocusRectangle(),
            () => e.DrawDarkFocusRectangle(controlFocused: focused)
        );

    public static void DrawThemeBackground(this VisualStyleRenderer vsr, PaintEventArgs e, Rectangle bounds, Color backColor)
        => InvokeAction(
            () => vsr.DrawBackground(e.Graphics, bounds),
            () => e.DrawDarkBackground(bounds, backColor, vsr.GetBackgroundContentRectangle(e.Graphics, bounds))
        );

    public static void DrawThemeBackground(this VisualStyleRenderer vsr, DrawToolTipEventArgs e)
        => InvokeAction(
            () => vsr.DrawBackground(e.Graphics, e.Bounds),
            () => e.DrawDarkBackground()
        );

    public static void DrawThemeText(this VisualStyleRenderer vsr, Graphics g, Color foreColor, Font font, Rectangle rect, string text, bool drawDisabled, TextFormatFlags textFormatFlags)
        => InvokeAction(
            () => { using (FontDC dc = new FontDC(g, font)) { vsr.DrawText(dc, rect, text, drawDisabled, textFormatFlags); } },
            () => TextRenderer.DrawText(g, text, font, rect, foreColor, textFormatFlags)
        );

    public static void DrawThemeText(this VisualStyleRenderer vsr, DrawToolTipEventArgs e, FontDC dc, Rectangle bounds)
        => InvokeAction(
            () => vsr.DrawText(dc, bounds, e.ToolTipText),
            () => e.DrawDarkText(bounds)
        );

    public static void DrawThemeBackground(this DrawToolTipEventArgs e) => InvokeAction(
        () => e.DrawBackground(),
        () => e.DrawDarkBackground()
    );

    public static void DrawThemeBackground(this DrawItemEventArgs e, bool drawFocus = false, bool focused = false)
        => InvokeAction(
            () => e.DrawBackground(),
            () => e.DrawDarkBackground(drawFocus, controlFocused: focused)
        );

    public static void DrawThemeBorder(this DrawToolTipEventArgs e)
        => InvokeAction(
            () => e.DrawBorder(),
            () => e.DrawDarkBorder()
        );

    // draw Dark Mode version if Dark Mode is enabled (default version is implemented in the Control class itself)
    public static void DrawSplitButtonBase(Graphics g, Rectangle rect, PushButtonState state)
        => InvokeAction(() => DarkThemeExtensions.DrawSplitButtonBase(g, rect, state));

    public static void DrawTabItem(Graphics g, Rectangle rect, TabItemState tabItemState, bool buttonMode)
        => InvokeAction(() => DarkThemeExtensions.DrawTabItem(g, rect, tabItemState, buttonMode));
    #endregion

    // Replace ReplaceWebColors & GetTabBarBorderColor
    #region Other Extensions
    public static string ReplaceWebColors(this string webPage)
        => InvokeFunc(
            () => webPage,
            () => DarkThemeExtensions.ReplaceWebColors(webPage)
    );

    public static Color GetTabBarBorderColor(this VisualStyleRenderer visualStyleRenderer, ColorProperty colorProperty)
        => InvokeFunc(
            () => visualStyleRenderer.GetColor(colorProperty),
            () => ThemeColors.TabBar.DefaultBorder
    );
    #endregion
}

/// <summary>
/// Dark Mode-aware versions of <see cref="ControlPaint"/> Draw methods.
/// </summary>
public static class ControlPaintEx
{
    public static void DrawBorder3D(Graphics g, Rectangle rectangle, Border3DStyle borderStyle)
        => ThemeExtensions.InvokeAction(
            () => ControlPaint.DrawBorder3D(g, rectangle, borderStyle),
            () => g.DrawDarkBorder(rectangle)
        );

    public static void DrawFocusRectangle(Graphics g, Rectangle rectangle, bool focused)
        => ThemeExtensions.InvokeAction(
            () => ControlPaint.DrawFocusRectangle(g, rectangle),
            () => g.DrawDarkFocusRectangle(rectangle)
        );

    public static void DrawFocusRectangle(Graphics g, Rectangle rectangle)
        => ThemeExtensions.InvokeAction(
            () => ControlPaint.DrawFocusRectangle(g, rectangle),
            () => g.DrawDarkFocusRectangle(rectangle)
        );

    public static void DrawStringDisabled(Graphics g, string text, Font font, Color color, Rectangle rectangle, TextFormatFlags textFormatFlags)
        => ThemeExtensions.InvokeAction(
            () => ControlPaint.DrawStringDisabled(g, text, font, color, rectangle, textFormatFlags),
            () => g.DrawDarkStringDisabled(text, font, color, rectangle, textFormatFlags) // color is currently not used
        );

    public static void DrawCheckBox(Graphics g, Rectangle rectangle, ButtonState state)
         => ThemeExtensions.InvokeAction(
            () => ControlPaint.DrawCheckBox(g, rectangle, state),
            () => DarkRenderer.DrawCheckBox(g, rectangle, state)
        );
}

// Win11 could draw themed CheckBoxes.
// - Not when using ControlPaint (above)
// - When using CheckBoxRenderer, UXTheme.OpenThemeData will have the Light color mode theme
public static class CheckBoxRendererEx
{
    public static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state)
         => ThemeExtensions.InvokeAction(
            () => CheckBoxRenderer.DrawCheckBox(g, glyphLocation, state),
            () => DarkRenderer.DrawCheckBox(g, glyphLocation, state)
        );
    public static void DrawCheckBox(Graphics g, Point glyphLocation, CheckBoxState state, Size glyphSize)
         => ThemeExtensions.InvokeAction(
            () => CheckBoxRenderer.DrawCheckBox(g, glyphLocation, state),
            () => DarkRenderer.DrawCheckBox(g, glyphLocation, state, glyphSize)
        );
}

/// <summary>
/// Extension methods used to attach <see cref="ITheme"/> to <see cref="Control"/> objects.<br/>
/// Used for outside objects like plugins that need it for selecting the <see cref="UIComponent"/>
/// </summary>
public static class ThemeBinding
{
    private static readonly ConditionalWeakTable<Control, ITheme> _attachedThemes = new();

    public static void AttachTheme(this Control control, ITheme theme)
    {
        if (control == null || theme == null)
            return;

        _attachedThemes.Remove(control);
        _attachedThemes.Add(control, theme);
    }

    public static ITheme GetAttachedTheme(this Control control)
    {
        return control != null && _attachedThemes.TryGetValue(control, out var theme) ? theme : null;
    }
}