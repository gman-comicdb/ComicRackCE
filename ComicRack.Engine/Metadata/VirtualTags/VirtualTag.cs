using System.ComponentModel;
using System.Xml.Serialization;

namespace cYo.Projects.ComicRack.Engine;

public class VirtualTag : IVirtualTag
{
    public VirtualTag()
    {
    }

    public VirtualTag(int id, string name, string description, string captionFormat, bool isEnabled = false, bool isDefault = false)
    {
        ID = id;
        Name = name;
        Description = description;
        CaptionFormat = captionFormat;
        IsEnabled = isEnabled;
        IsDefault = isDefault;
    }

    [DefaultValue(0)]
    public int ID { get; set; }

    [DefaultValue("")]
    public string Name { get; set; }

    [DefaultValue("")]
    public string Description { get; set; }

    [DefaultValue("")]
    public string CaptionFormat { get; set; }

    [DefaultValue(false)]
    public bool IsEnabled { get; set; }

    [XmlIgnore]
    [DefaultValue(false)]
    public bool IsDefault { get; set; }

    [XmlIgnore]
    public string PropertyName => $"VirtualTag{ID:00}";

    [XmlIgnore]
    public string DisplayMember => $"{ID:00}: {Name}{(IsEnabled ? " (Enabled)" : "")}";

    public override bool Equals(object obj)
    {
        return obj is VirtualTag vtag ? Equals(vtag) : false;
    }

    public bool Equals(VirtualTag vtag)
    {
        return vtag != null
            ? vtag.ID == ID && vtag.Name == Name && vtag.Description == Description
                && vtag.CaptionFormat == CaptionFormat && vtag.IsEnabled == IsEnabled && vtag.IsDefault == IsDefault
            : false;
    }

    public override int GetHashCode()
    {
        if (this is null)
            return 0;

        int hash = 17;
        hash = hash * 23 + ID.GetHashCode();
        hash = hash * 23 + Name?.GetHashCode() ?? 0;
        hash = hash * 23 + Description?.GetHashCode() ?? 0;
        hash = hash * 23 + CaptionFormat?.GetHashCode() ?? 0;
        hash = hash * 23 + IsEnabled.GetHashCode();
        hash = hash * 23 + IsDefault.GetHashCode();
        return hash;
    }
}


