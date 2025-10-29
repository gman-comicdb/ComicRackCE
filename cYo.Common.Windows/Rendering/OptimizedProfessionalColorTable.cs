using System.Drawing;
using System.Windows.Forms;

namespace cYo.Common.Windows.Rendering;

public class OptimizedProfessionalColorTable : ProfessionalColorTable
{
    public override Color MenuStripGradientEnd => base.MenuStripGradientBegin;

    public override Color MenuItemSelectedGradientEnd => Color.FromArgb(128, base.MenuItemSelectedGradientBegin);

    public override Color ButtonSelectedGradientEnd => Color.FromArgb(128, base.ButtonSelectedGradientBegin);
}
