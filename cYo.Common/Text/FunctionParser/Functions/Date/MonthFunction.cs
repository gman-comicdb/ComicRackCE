using System;

namespace cYo.Common.Text.FunctionParser.Functions.Date;

public record MonthFunctionParameters(string dateInText) : FunctionParameter;

[FunctionDefinition("month")]
public class MonthFunction(string name) : FunctionBase<MonthFunctionParameters, string>(name)
{

    protected override Func<MonthFunctionParameters, string> Function => param =>
    {
        return string.IsNullOrEmpty(param.dateInText)
            ? string.Empty
            : DateTime.TryParse(param.dateInText, out DateTime result)
            ? result.Month.ToString("D4")
            : throw new ArgumentException("Can't parse date");
    };
}
