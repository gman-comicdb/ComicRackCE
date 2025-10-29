using System.Drawing;

namespace cYo.Common.Windows.Rendering;

public class OptimizedTanColorTable : TanColorTable
{
    public override Color MenuStripGradientEnd => base.MenuStripGradientBegin;

    public override Color ToolStripBorder => Color.Empty;
}
