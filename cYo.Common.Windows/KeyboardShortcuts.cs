using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace cYo.Common.Windows;

[Serializable]
public class KeyboardShortcuts : ICloneable
{
    private readonly List<KeyboardCommand> commands = new();

    public List<KeyboardCommand> Commands => commands;

    public KeyboardShortcuts()
    {
    }

    public KeyboardShortcuts(KeyboardShortcuts copy)
    {
        Commands.AddRange(copy.Commands.Select(kc => new KeyboardCommand(kc)));
    }

    public bool HandleKey(CommandKey key)
    {
        foreach (KeyboardCommand command in Commands)
        {
            if (command.Handles(key))
            {
                command.Invoke(key);
                return true;
            }
        }
        return false;
    }

    public bool HandleKey(CommandKey key, CommandKey modifiers)
    {
        return HandleKey(key | modifiers);
    }

    public bool HandleKey(CommandKey key, Keys modifiers)
    {
        return HandleKey(key, (CommandKey)modifiers);
    }

    public bool HandleKey(MouseButtons button, bool doubleClick, bool isTouch)
    {
        if (isTouch && HandleKey(doubleClick ? CommandKey.TouchDoubleTap : CommandKey.TouchTap, Control.ModifierKeys))
        {
            return true;
        }
        if ((button & MouseButtons.Left) != 0)
        {
            return HandleKey(doubleClick ? CommandKey.MouseDoubleLeft : CommandKey.MouseLeft, Control.ModifierKeys);
        }
        return (button & MouseButtons.Right) != 0
            ? HandleKey(doubleClick ? CommandKey.MouseDoubleRight : CommandKey.MouseRight, Control.ModifierKeys)
            : (button & MouseButtons.Middle) != 0
            ? HandleKey(doubleClick ? CommandKey.MouseDoubleMiddle : CommandKey.MouseMiddle, Control.ModifierKeys)
            : (button & MouseButtons.XButton1) != 0
            ? HandleKey(doubleClick ? CommandKey.MouseDoubleButton4 : CommandKey.MouseButton4, Control.ModifierKeys)
            : (button & MouseButtons.XButton2) != 0
            ? HandleKey(doubleClick ? CommandKey.MouseDoubleButton5 : CommandKey.MouseButton5, Control.ModifierKeys)
            : false;
    }

    public bool HandleKey(Keys k)
    {
        Keys keys = k & Keys.KeyCode;
        return Enum.IsDefined(typeof(CommandKey), (int)keys) ? HandleKey((CommandKey)k) : false;
    }

    public KeyboardCommand FindCommandByKey(string key)
    {
        return commands.FirstOrDefault(kc => kc.Id == key);
    }

    public void SetKeyMapping(IEnumerable<StringPair> list)
    {
        foreach (StringPair item in list)
        {
            KeyboardCommand keyboardCommand = FindCommandByKey(item.Key);
            if (keyboardCommand != null)
            {
                keyboardCommand.KeyList = item.Value;
            }
        }
    }

    public IEnumerable<StringPair> GetKeyMapping()
    {
        return Commands.Select(kc => new StringPair(kc.Id, kc.KeyList));
    }

    public object Clone()
    {
        return new KeyboardShortcuts(this);
    }
}
