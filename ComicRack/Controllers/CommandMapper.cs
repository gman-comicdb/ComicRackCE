using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Projects.ComicRack.Viewer.Controllers;

public sealed class CommandMapper
{
    //public Dictionary<Command, CommandBinding> Bindings { get; } = [];

    public CommandMapper(Type actions, Type available, Type visible, Type update)
    {
        //Bindings = new Dictionary<Command, CommandBinding>();
        
        var commands = Command.All;

        foreach (var cmd in commands)
        {
            // find Execute method
            var executeMethod = actions.GetMethod(cmd.Name);
            var canExecuteMethod = available.GetMethod(cmd.Name);
            var showMethod = visible.GetMethod(cmd.Name);
            var updateMethod = update.GetMethod(cmd.Name);

            Action executeAction = executeMethod != null
                ? (Action)Delegate.CreateDelegate(typeof(Action), executeMethod)
                : cmd.Action; // no-op fallback

            Func<bool> canExecuteFunc = canExecuteMethod != null
                ? (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), canExecuteMethod)
                : cmd.CanExecute;

            Func<bool> showFunc = showMethod != null
                ? (Func<bool>)Delegate.CreateDelegate(typeof(Func<bool>), showMethod)
                : cmd.Show;

            Action<ToolStripItem> updateAction = updateMethod != null
                ? (Action<ToolStripItem>)Delegate.CreateDelegate(typeof(Action<ToolStripItem>), updateMethod)
                : cmd.UpdateHandler;

            cmd.Action = executeAction;
            cmd.CanExecute = canExecuteFunc;
            cmd.Show = showFunc;
            //cmd.EventHandler;
            cmd.UpdateHandler = updateAction;
            //Bindings[cmd] = new CommandBinding(executeAction, canExecuteFunc, showFunc, updateAction);
        }
    }

}

//public sealed class CommandBinding(Action execute, Func<bool> canExecute = null, Func<bool> show = null, Action<ToolStripMenuItem> update = null)
//{
//    public Action Execute { get; set; } = execute;

//    public Func<bool> CanExecute { get; set; } = canExecute ?? (() => true);

//    public Func<bool> Show { get; set; } = show ?? (() => true);

//    public Action<ToolStripMenuItem> Update { get; set; } = update ?? null;
//}

//public interface ICommandManager
//{
//    void Execute(Command command);

//    bool CanExecute(Command command);
//}

//public sealed class CommandManager : ICommandManager
//{
//    private readonly Dictionary<string, CommandBinding> bindings;

//    public CommandManager(CommandMapper map)
//    {
//        bindings = map.Bindings.ToDictionary(k => k.Key.Name, v => v.Value);
//    }

//    public void Execute(Command cmd)
//    {
//        if (bindings.TryGetValue(cmd.Name, out var binding) && binding.CanExecute())
//        {
//            binding.Execute();
//        }
//    }

//    public bool CanExecute(Command cmd) =>
//        bindings.TryGetValue(cmd.Name, out var binding) && binding.CanExecute();
//}