using System;

namespace cYo.Common.Text.FunctionParser.Functions.Date;

public record DateFunctionParameters(string dateInText, string format) : FunctionParameter;

[FunctionDefinition("date")]
public class DateFormatFunction(string name) : FunctionBase<DateFunctionParameters, string>(name)
{
    protected override Func<DateFunctionParameters, string> Function => param =>
    {
        return string.IsNullOrEmpty(param.dateInText)
            ? string.Empty
            : DateTime.TryParse(param.dateInText, out DateTime result)
            ? result.ToString(param.format)
            : throw new ArgumentException("Can't parse date");
    };
}
