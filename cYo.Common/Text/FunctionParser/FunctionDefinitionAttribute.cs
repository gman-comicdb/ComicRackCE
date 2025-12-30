using System;

namespace cYo.Common.Text.FunctionParser;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class FunctionDefinitionAttribute : Attribute
{
    public string Name { get; set; }

    public FunctionDefinitionAttribute(string name)
    {
        Name = name;
    }
}
