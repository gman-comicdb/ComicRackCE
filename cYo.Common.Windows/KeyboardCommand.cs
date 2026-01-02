using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;

using cYo.Common.Localize;
using cYo.Common.Text;

namespace cYo.Common.Windows;

[Serializable]
public class KeyboardCommand
{
    public const int NumberOfKeys = 4;

    private readonly CommandKey[] keyboard = new CommandKey[4];

    [NonSerialized]
    private Action method;

    [NonSerialized]
    private Action<CommandKey> methodWithKey;

    public string Group { get; private set; }

    public Image Image { get; private set; }

    public string Id { get; private set; }

    public string Text { get; private set; }

    public CommandKey[] Keyboard => keyboard;

    public string KeyList
    {
        get => Keyboard.ToListString("|");
        set
        {
            if (value == null)
            {
                return;
            }
            int num = 0;
            string[] array = value.Split("|".ToCharArray(), NumberOfKeys);
            foreach (string value2 in array)
            {
                CommandKey commandKey = CommandKey.None;
                try
                {
                    commandKey = (CommandKey)Enum.Parse(typeof(CommandKey), value2);
                }
                catch
                {
                }
                keyboard[num++] = commandKey;
            }
        }
    }

    public string KeyNames
    {
        get
        {
            StringBuilder stringBuilder = new();
            CommandKey[] array = Keyboard;
            foreach (CommandKey commandKey in array)
            {
                if (commandKey != 0)
                {
                    if (stringBuilder.Length != 0)
                    {
                        stringBuilder.Append(CultureInfo.CurrentCulture.TextInfo.ListSeparator);
                        stringBuilder.Append(" ");
                    }
                    stringBuilder.Append(GetKeyName(commandKey));
                }
            }
            return stringBuilder.ToString();
        }
    }

    protected KeyboardCommand(Image image, string id, string group, string text, params CommandKey[] keys)
    {
        Image = image;
        Id = id;
        Group = TR.Load("Keyboard")[group, group];
        Text = TR.Load("Keyboard")[id, text];
        for (int i = 0; i < Math.Min(Keyboard.Length, keys.Length); i++)
        {
            keyboard[i] = keys[i];
        }
    }

    public KeyboardCommand(Image image, string id, string group, string text, Action<CommandKey> method, params CommandKey[] keys)
        : this(image, id, group, text, keys)
    {
        methodWithKey = method;
    }

    public KeyboardCommand(Image image, string id, string group, string text, Action method, params CommandKey[] keys)
        : this(image, id, group, text, keys)
    {
        this.method = method;
    }

    public KeyboardCommand(string id, string group, string text, Action method, params CommandKey[] keys)
        : this(null, id, group, text, method, keys)
    {
    }

    public KeyboardCommand(KeyboardCommand copy)
    {
        Image = copy.Image;
        Id = copy.Id;
        Group = copy.Group;
        Text = copy.Text;
        method = copy.method;
        methodWithKey = copy.methodWithKey;
        for (int i = 0; i < copy.Keyboard.Length; i++)
        {
            Keyboard[i] = copy.Keyboard[i];
        }
    }

    public void Invoke(CommandKey key)
    {
        method?.Invoke();
        methodWithKey?.Invoke(key);
    }

    public bool Handles(CommandKey key)
    {
        return keyboard.Any(k => k == key);
    }

    private static string GetEnglishName(CommandKey key)
    {
        return key switch
        {
            CommandKey.D0 => "0",
            CommandKey.D1 => "1",
            CommandKey.D2 => "2",
            CommandKey.D3 => "3",
            CommandKey.D4 => "4",
            CommandKey.D5 => "5",
            CommandKey.D6 => "6",
            CommandKey.D7 => "7",
            CommandKey.D8 => "8",
            CommandKey.D9 => "9",
            _ => key.ToString().PascalToSpaced(),
        };
    }

    public static string GetKeyName(CommandKey key)
    {
        CommandKey commandKey = key & (CommandKey)65535;
        TR tR = TR.Load("CommandKeys");
        StringBuilder stringBuilder = new();
        if (commandKey != 0)
        {
            if ((key & CommandKey.Ctrl) != 0)
            {
                stringBuilder.Append(tR["Ctrl", "Ctrl"]);
                stringBuilder.Append("+");
            }
            if ((key & CommandKey.Shift) != 0)
            {
                stringBuilder.Append(tR["Shift", "Shift"]);
                stringBuilder.Append("+");
            }
            if ((key & CommandKey.Alt) != 0)
            {
                stringBuilder.Append(tR["Alt", "Alt"]);
                stringBuilder.Append("+");
            }
        }
        stringBuilder.Append(tR[commandKey.ToString(), GetEnglishName(commandKey)]);
        return stringBuilder.ToString();
    }

    public static bool IsKeyValue(CommandKey key)
    {
        return (key & CommandKey.Modifiers) == 0;
    }
}
