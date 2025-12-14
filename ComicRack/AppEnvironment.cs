using cYo.Common.Localize;
using cYo.Common.Windows.Forms.Theme;
using cYo.Common.Windows.Forms.Theme.Resources;
using cYo.Projects.ComicRack.Plugins.Theme;
using cYo.Projects.ComicRack.Viewer.Properties;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer;

/// <summary>Very rough cut</summary>
public static class AppEnvironment
{
    public static void Initialize(string culture, Themes theme)
    {
        SetUICulture(culture);
        SetVisualStyleSettings(theme);
    }

    public static void SetToolStripRenderer(bool useDarkMode, bool systemToolBars, bool forceTanColorSchema)
    {
        if (useDarkMode)
        {
            ToolStripManager.Renderer = new ThemeToolStripProRenderer();
            return;
        }
            
        ToolStripRenderer renderer;
        if (systemToolBars)
        {
            renderer = new ToolStripSystemRenderer();
        }
        else
        {
            // OSVersion 5 is Windows XP, Windows 2000 or Windows 2003
            bool isWinXp = Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major == 5;

            // Should consider moving OptimizedProfessionalColorTable and OptimizedTanColorTable
            ProfessionalColorTable professionalColorTable = !(forceTanColorSchema || isWinXp)
                ? new OptimizedProfessionalColorTable()
                : new OptimizedTanColorTable();

            renderer = new ThemeToolStripProRenderer(professionalColorTable)
            {
                RoundedEdges = false
            };
        }
        ToolStripManager.Renderer = renderer;
    }

    private static void SetUICulture(string culture)
    {
        if (!string.IsNullOrEmpty(culture))
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
                TR.DefaultCulture = new CultureInfo(culture);
            }
            catch (Exception)
            {
            }
        }
    }

    private static void SetVisualStyleSettings(Themes theme)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(defaultValue: false);
        ThemeManager.Initialize(theme); // if using dark mode, replace SystemColors and initialize native Windows theming
        ResourceManagerEx.InitResourceManager(theme);
        ThemePlugin.Register(theme); // Register the current theme for the IThemePlugin interface for plugins
    }
}
