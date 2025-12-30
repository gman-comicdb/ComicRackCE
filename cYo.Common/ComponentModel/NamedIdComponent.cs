using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace cYo.Common.ComponentModel;

[Serializable]
public class NamedIdComponent : IdComponent
{
    private string name;

    [DefaultValue(null)]
    [XmlAttribute]
    public string Name
    {
        get => name;
        set
        {
            if (!(name == value))
            {
                name = value;
                OnNameChanged();
            }
        }
    }

    [field: NonSerialized]
    public event EventHandler NameChanged;

    protected virtual void OnNameChanged()
    {
        NameChanged?.Invoke(this, EventArgs.Empty);
    }
}
