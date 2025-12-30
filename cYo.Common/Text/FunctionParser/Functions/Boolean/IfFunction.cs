using System;

namespace cYo.Common.Text.FunctionParser.Functions.Boolean;

public record IfFunctionParameters(string condition, string ifTrue, string ifFalse) : FunctionParametersEval(condition);

[FunctionDefinition("if")]
public class IfFunction(string name) : FunctionBase<IfFunctionParameters, string>(name)
{
    protected override Func<IfFunctionParameters, string> Function => param =>
        //if condition doesn't resolve to a true or false, return an empty string
        param.BoolEval is null ? string.Empty : param.BoolEval == true ? param.ifTrue : param.ifFalse;
}
