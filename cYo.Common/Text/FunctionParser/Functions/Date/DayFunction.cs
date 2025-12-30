using System;

namespace cYo.Common.Text.FunctionParser.Functions.Date;

public record DayFunctionParameters(string dateInText) : FunctionParameter;

[FunctionDefinition(name: "day")]
public class DayFunction(string name) : FunctionBase<DayFunctionParameters, string>(name)
{

    protected override Func<DayFunctionParameters, string> Function => param =>
    {
        return string.IsNullOrEmpty(param.dateInText)
            ? string.Empty
            : DateTime.TryParse(param.dateInText, out DateTime result)
            ? result.Day.ToString("D4")
            : throw new ArgumentException("Can't parse date");
    };
}
