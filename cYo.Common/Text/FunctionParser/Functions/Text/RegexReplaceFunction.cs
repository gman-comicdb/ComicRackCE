using System;
using System.Text.RegularExpressions;

namespace cYo.Common.Text.FunctionParser.Functions.Text;

public record RegexReplaceFunctionParameters(string inputText, string pattern, string replacement) : FunctionParameter;

[FunctionDefinition("RegexReplace")]
public class RegexReplaceFunction(string name) : FunctionBase<RegexReplaceFunctionParameters, string>(name)
{
    protected override Func<RegexReplaceFunctionParameters, string> Function => param => Regex.Replace(param.inputText, param.pattern, param.replacement, RegexOptions.IgnoreCase);
}
