using System;
using System.ComponentModel;

using cYo.Common.ComponentModel;

namespace cYo.Projects.ComicRack.Viewer.Config;

[Serializable]
public class ExternalProgram : IComparable<ExternalProgram>, INamed, IPath, IOverride
{
    [DefaultValue("")]
    public string Name { get; set; }

    [DefaultValue("")]
    public string Path { get; set; }

    [DefaultValue("")]
    public string Arguments { get; set; }

    public string FullPath => $"{Path} {Arguments}";

    [DefaultValue(false)]
    public bool Override { get; set; }

    public ExternalProgram()
        : this(string.Empty, string.Empty)
    {
    }

    public ExternalProgram(string name, string path, string arguments = "", bool overrideViewer = false)
    {
        Name = name;
        Path = path;
        Arguments = arguments;
        Override = overrideViewer;
    }

    public int CompareTo(ExternalProgram other)
    {
        return string.Compare(Name, other.Name);
    }

    public override string ToString() => Name;
}
