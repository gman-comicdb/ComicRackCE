using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace cYo.Common.Win32
{
    public static class UXTheme
    {
        internal static class Native
        {
            public const uint LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800;

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern IntPtr GetProcAddress(IntPtr hModule, IntPtr ordinal);

            [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

            [DllImport("dwmapi.dll", PreserveSig = true)]
            public static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

            [DllImport("user32.dll")]
            public static extern int SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        }

        internal static class NativeDelegates
        {
            public enum PreferredAppMode
            {
                Default = 0,
                AllowDark = 1,
                ForceDark = 2,
                ForceLight = 3,
                Max = 4
            }

            public delegate bool ShouldAppsUseDarkModeDelegate();
            public delegate bool AllowDarkModeForWindowDelegate(IntPtr hWnd, bool allow);
            public delegate PreferredAppMode SetPreferredAppModeDelegate(PreferredAppMode appMode);
        }

        private static bool _initialized = false;
        private static bool _isDarkModeSupported = false;
        //private static bool _isDarkModeEnabled = false; // this would the app to follow system settings

        //private static NativeDelegates.ShouldAppsUseDarkModeDelegate _shouldAppsUseDarkMode;
        private static NativeDelegates.AllowDarkModeForWindowDelegate _allowDarkModeForWindow;
        private static NativeDelegates.SetPreferredAppModeDelegate _setPreferredAppMode;

        private static readonly int DWMWA_USE_IMMERSIVE_DARK_MODE = GetDwmDarkModeAttribute();

        private static Type[] ExcludedControl = {
            //typeof(Panel),
            //typeof(ListView) // ListView can be set to DarkMode_Explorer (dark scrollbar; unthemed list) or DarkMode_ItemView (dark list; unthemed scrollbar)
        };

        private static Type[] ExplorerControl = {
            typeof(TextBox),
            typeof(Form),
            typeof(TreeView),
            typeof(ScrollBar),
            typeof(ScrollableControl),
            typeof(Button),
            //typeof(ListView) // ListView can be set to DarkMode_Explorer (dark scrollbar; unthemed list) or DarkMode_ItemView (dark list; unthemed scrollbar)
        };

        private static Type[] CFDControl = {
            typeof(ComboBox) // ComboBox may need to be set to DarkMode_CFD

        };

        public static void Initialize(bool darkMode = false)
        {
            if (_initialized || !darkMode) return;
            _initialized = true;

            IntPtr hUxTheme = Native.LoadLibraryEx("uxtheme.dll", IntPtr.Zero, Native.LOAD_LIBRARY_SEARCH_SYSTEM32);
            if (hUxTheme == IntPtr.Zero) return;

            //_shouldAppsUseDarkMode = GetDelegate<NativeDelegates.ShouldAppsUseDarkModeDelegate>(hUxTheme, 132);
            _allowDarkModeForWindow = GetDelegate<NativeDelegates.AllowDarkModeForWindowDelegate>(hUxTheme, 133);
            _setPreferredAppMode = GetDelegate<NativeDelegates.SetPreferredAppModeDelegate>(hUxTheme, 135);

            //if (_shouldAppsUseDarkMode != null && _allowDarkModeForWindow != null && _setPreferredAppMode != null)
            if (_allowDarkModeForWindow != null && _setPreferredAppMode != null)
            {
                _isDarkModeSupported = true;
                //_isDarkModeEnabled = _shouldAppsUseDarkMode();
                _setPreferredAppMode(NativeDelegates.PreferredAppMode.ForceDark);
            }
        }

        public static void ApplyDarkThemeToWindow(Form form, bool darkMode = false, bool recurse = true)
        {
            if (!darkMode || !_isDarkModeSupported) return;

            IntPtr hwnd = form.Handle;
            _allowDarkModeForWindow?.Invoke(hwnd, true);
            Native.SetWindowTheme(hwnd, "DarkMode_Explorer", null);

            int useDark = 1;
            Native.DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDark, sizeof(int));

            if (recurse)
                ApplyDarkThemeRecursive(form, darkMode);
        }

        public static void ApplyDarkThemeToControl(Control control, bool darkMode = false)
        {
            if (!darkMode || !_isDarkModeSupported || ExcludedControl.Any(t => t.IsInstanceOfType(control))) return;
            if (!control.IsHandleCreated) return;

            IntPtr hwnd = control.Handle;
            string themeClass = GetThemeClassForControl(control);
            Native.SetWindowTheme(hwnd, themeClass, null);
            _allowDarkModeForWindow?.Invoke(hwnd, true);
        }

        public static void ApplyDarkThemeRecursive(Control parent, bool darkMode = false)
        {
            if (!darkMode) return;
            ApplyDarkThemeToControl(parent, true);
            foreach (Control child in parent.Controls)
            {
                ApplyDarkThemeRecursive(child, true);
                if (child.Controls.Count != 0)
                    UXTheme.ApplyDarkThemeRecursive(child, true);
            }
            IntPtr hwnd = parent.Handle;
            _allowDarkModeForWindow?.Invoke(hwnd, true);
        }

        private static string GetThemeClassForControl(Control control)
        {
            // helpful dark theme classes list: https://www.zabkat.com/blog/darkmode-listview.htm
            // no success with any sublasses (probably requires later OS version). currently unused DarkMode classes of interest:
            //     DarkMode_ItemsView, DarkMode_InfoPaneToolbar
            if (ExplorerControl.Any(t => t.IsInstanceOfType(control)))
            {
                return "DarkMode_Explorer";
            }
            if (CFDControl.Any(t => t.IsInstanceOfType(control)))
            {
                return "DarkMode_CFD";
            }
            return "DarkMode_Explorer";
        }


        private static T? GetDelegate<T>(IntPtr moduleHandle, int ordinal) where T : class
        {
            IntPtr proc = Native.GetProcAddress(moduleHandle, (IntPtr)ordinal);
            return proc == IntPtr.Zero ? null : Marshal.GetDelegateForFunctionPointer(proc, typeof(T)) as T;
        }

        private static int GetDwmDarkModeAttribute()
        {
            // DWMWA_USE_IMMERSIVE_DARK_MODE is 20 in recent builds
            Version v = Environment.OSVersion.Version;
            return (v.Build >= 18985) ? 20 : 19;
        }
    }
}