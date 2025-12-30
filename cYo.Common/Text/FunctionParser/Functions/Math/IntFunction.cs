using System;

namespace cYo.Common.Text.FunctionParser.Functions.Math;

public record IntFunctionParameters(string intInText) : FunctionParameter;

[FunctionDefinition(name: "int")]
public class IntFunction(string name) : FunctionBase<IntFunctionParameters, int>(name)
{
    protected override Func<IntFunctionParameters, int> Function => param => string.IsNullOrWhiteSpace(param.intInText) ? -1 : Int32.TryParse(param.intInText, out int result) ? result : -1;
}
