using System;

namespace cYo.Common.Text.FunctionParser.Functions.Date;

public record YearFunctionParameters(string dateInText) : FunctionParameter;

[FunctionDefinition("year")]
public class YearFunction(string name) : FunctionBase<YearFunctionParameters, string>(name)
{

    protected override Func<YearFunctionParameters, string> Function => param =>
    {
        return string.IsNullOrEmpty(param.dateInText)
            ? string.Empty
            : DateTime.TryParse(param.dateInText, out DateTime result)
            ? result.Year.ToString()
            : throw new ArgumentException("Can't parse date");
    };
}
