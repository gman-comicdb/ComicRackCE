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

            public static readonly int DWMWA_USE_IMMERSIVE_DARK_MODE = GetDwmDarkModeAttribute();

            public const int WM_THEMECHANGED = 0x031A;

            public static int YES = 1;

            // Win32 API ListView constants
            public const int LVM_FIRST = 0x1000;
            public const int LVM_GETHEADER = LVM_FIRST + 31;

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

            [DllImport("uxtheme.dll", EntryPoint = "#133", SetLastError = true)]
            internal static extern bool AllowDarkModeForWindow(IntPtr hWnd, bool allow);

            [DllImport("uxtheme.dll", EntryPoint = "#135", SetLastError = true)]
            internal static extern bool AllowDarkModeForApp(bool allow);

            [DllImport("uxtheme.dll", EntryPoint = "#135", SetLastError = true)]
            internal static extern PreferredAppMode SetPreferredAppMode(PreferredAppMode mode);

            [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

            [DllImport("dwmapi.dll", PreserveSig = true)]
            public static extern int DwmSetWindowAttribute(IntPtr hwnd, int dwAttribute, ref int pvAttribute, int cbAttribute);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

            public enum PreferredAppMode
            {
                Default,
                AllowDark,
                ForceDark,
                ForceLight,
                Max
            }

        }

        private static bool _initialized = false;
        private static bool _isDarkModeSupported = false;

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
            typeof(CheckBox),
            //typeof(ListView) // ListView can be set to DarkMode_Explorer (dark scrollbar; unthemed list) or DarkMode_ItemView (dark list; unthemed scrollbar)
        };

        private static Type[] CFDControl = {
            typeof(ComboBox) // ComboBox may need to be set to DarkMode_CFD

        };

        public static void Initialize(bool darkMode = false)
        {
            if (_initialized || !darkMode) return;
            _initialized = true;

            //IntPtr hUxTheme = Native.LoadLibraryEx("uxtheme.dll", IntPtr.Zero, Native.LOAD_LIBRARY_SEARCH_SYSTEM32);
            //if (hUxTheme == IntPtr.Zero) return;
            _isDarkModeSupported = Native.DWMWA_USE_IMMERSIVE_DARK_MODE != 0;

            Native.AllowDarkModeForApp(true);
            //Native.AllowDarkModeForWindow(hUxTheme, true);
            Native.SetPreferredAppMode(Native.PreferredAppMode.ForceDark);
        }

        public static void ApplyDarkThemeToWindow(Form form, bool darkMode = false, bool recurse = true)
        {
            if (!darkMode || !_isDarkModeSupported) return;

            IntPtr hwnd = form.Handle;
            Native.AllowDarkModeForWindow(hwnd, true);
            Native.SetWindowTheme(hwnd, "DarkMode_Explorer", null);

            Native.DwmSetWindowAttribute(hwnd, Native.DWMWA_USE_IMMERSIVE_DARK_MODE, ref Native.YES, sizeof(int));

            if (recurse)
                ApplyDarkThemeRecursive(form, darkMode);
        }

        public static void ApplyDarkThemeToControl(Control control, bool darkMode = false)
        {
            if (!darkMode || !_isDarkModeSupported || ExcludedControl.Any(t => t.IsInstanceOfType(control))) return;
            if (!control.IsHandleCreated) return;

            IntPtr hwnd = control.Handle;
            if (control is TabControl)
            {
                Native.SetWindowTheme(hwnd, null, "DarkMode::FileExplorerBannerContainer");
            }
            else if (control is ListView listView)
            {
                // HeaderStyle is irrelevant; this is a temporary workaround ascannot reference ListViewEx class
                if (listView.ShowGroups && listView.HeaderStyle != ColumnHeaderStyle.None)
                {
                    // sacrifice dark scrollbars to show readable group headers
                    Native.SetWindowTheme(hwnd, "DarkMode_ItemsView", null);
                    //Native.SetWindowTheme(hwnd,null, "DarkMode_ItemsView::ListView"); //messed up scrollbar
                }
                else
                {
                    // we don't have groups - let's get dark scrollbars
                    Native.SetWindowTheme(hwnd, "DarkMode_Explorer", null);
                }

                // header has to be themed separately
                IntPtr columnHeaderHandle = Native.SendMessage(hwnd, Native.LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
                if (columnHeaderHandle != IntPtr.Zero)
                {
                    Native.SetWindowTheme(columnHeaderHandle, "DarkMode_ItemsView", null); //working but dark header text
                    //NativeMethods.SetWindowTheme(columnHeaderHandle, null, "DarkMode_ItemsView::Header");
                }
            }
            else
            {
                string themeClass = GetThemeClassForControl(control);
                Native.SetWindowTheme(hwnd, themeClass, null);
            }
            Native.AllowDarkModeForWindow(hwnd, true);
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
            Native.AllowDarkModeForWindow(hwnd, true);
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

        private static int GetDwmDarkModeAttribute()
        {
            // DWMWA_USE_IMMERSIVE_DARK_MODE is 20 in recent builds
            // If a build is too old to support Dark Mode, return 0
            var build = Environment.OSVersion.Version.Build;
            return (build < 17763) ? 0 : (build >= 18985) ? 20 : 19;
        }
    }
}