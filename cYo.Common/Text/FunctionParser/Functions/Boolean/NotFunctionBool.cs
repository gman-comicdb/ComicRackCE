using System;
using System.Linq;
using System.Reflection;

using DynamicExpresso;

namespace cYo.Common.Text.FunctionParser.Functions.Boolean;

public record NotFunctionParameter(string value) : FunctionParametersEval(value);

[FunctionDefinition("not")]
public class NotFunction(string name) : FunctionBase<NotFunctionParameter, bool>(name)
{
    protected override Func<NotFunctionParameter, bool> Function => param => (bool)!param.BoolEval;
}
