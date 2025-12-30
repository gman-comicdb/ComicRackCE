using System;

namespace cYo.Common.Text.FunctionParser.Functions.Math;

public record DoubleFunctionParameters(string doubleInText) : FunctionParameter;

[FunctionDefinition(name: "double")]
public class DoubleFunction(string name) : FunctionBase<DoubleFunctionParameters, double>(name)
{
    protected override Func<DoubleFunctionParameters, double> Function => param =>
    {
        return string.IsNullOrWhiteSpace(param.doubleInText)
            ? -1.0d
            : Double.TryParse(param.doubleInText, out double result) ? result : -1.0d;
    };
}
