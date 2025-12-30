using System;

namespace cYo.Projects.ComicRack.Engine.IO.Provider.XmlInfo;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class XmlInfoFileAttribute : Attribute
{
    public string XmlInfoFile { get; set; }

    public int Order { get; set; }

    public XmlInfoFileAttribute(string xmlInfoFile, int order)
    {
        XmlInfoFile = xmlInfoFile;
        Order = order;
    }
}
