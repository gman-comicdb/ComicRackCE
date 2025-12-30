using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace cYo.Common.Drawing;

public class ColorSchema : IXmlSerializable
{
    private readonly Dictionary<string, Color> table = new();

    private string name;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public Color this[string name]
    {
        get => !table.TryGetValue(name, out var value) ? Color.Empty : value;
        set => table[name] = value;
    }

    public ColorSchema(string name)
    {
        this.name = name;
    }

    public ColorSchema()
        : this(string.Empty)
    {
    }

    public XmlSchema GetSchema()
    {
        return null;
    }

    public void ReadXml(XmlReader reader)
    {
        ColorConverter colorConverter = new();
        name = reader.GetAttribute("name");
        reader.ReadStartElement();
        while (reader.IsStartElement())
        {
            string key = reader.Name;
            string text = reader.ReadElementContentAsString();
            try
            {
                table[key] = (Color)colorConverter.ConvertFromInvariantString(text);
            }
            catch (Exception)
            {
                table[key] = Color.Red;
            }
        }
        reader.ReadEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        ColorConverter colorConverter = new();
        if (!string.IsNullOrEmpty(name))
        {
            writer.WriteAttributeString("name", name);
        }
        foreach (string key in table.Keys)
        {
            writer.WriteElementString(key, colorConverter.ConvertToInvariantString(table[key]));
        }
    }
}
