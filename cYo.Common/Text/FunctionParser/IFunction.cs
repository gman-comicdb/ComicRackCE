namespace cYo.Common.Text.FunctionParser;

public interface IFunction
{
    string Name { get; }
    void SetParameters(params object[] args);
    object Result { get; }
    string ResultAsText { get; }
}
