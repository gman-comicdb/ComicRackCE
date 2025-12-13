using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controllers;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
internal class CommandAttribute : Attribute
{
    public Action Action { get; }
    
    public Func<bool> CanExecute { get; set; } = () => true;

    public Keys Shortcut { get; set; }

    public CommandAttribute() { }

    public CommandAttribute(Action action, Func<bool> canExecute, Keys shortcut)
    {
        Action = action;
        CanExecute = canExecute;
        Shortcut = shortcut;
    }

    public CommandAttribute(Action action, Keys shortcut)
    {
        Action = action;
        Shortcut = shortcut;
    }
}
