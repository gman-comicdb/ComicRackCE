using System;
using System.Text.RegularExpressions;

namespace cYo.Common.Text.FunctionParser.Functions.Text;

public record RegexMatchFunctionParameters(string inputText, string pattern) : FunctionParameter;

[FunctionDefinition("RegexMatch")]
public class RegexMatchFunction(string name) : FunctionBase<RegexMatchFunctionParameters, bool>(name)
{
    protected override Func<RegexMatchFunctionParameters, bool> Function => param => Regex.IsMatch(param.inputText, param.pattern, RegexOptions.IgnoreCase);
}
