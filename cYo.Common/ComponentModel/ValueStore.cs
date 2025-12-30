using System;

namespace cYo.Common.ComponentModel;

public class ValueStore<T> : IValueStore<T>
{
    private Action<T> setCall;

    private Func<T> getCall;

    public ValueStore(Action<T> setCall, Func<T> getCall)
    {
        this.setCall = setCall;
        this.getCall = getCall;
    }

    public T GetValue()
    {
        return getCall == null ? throw new NotImplementedException() : getCall();
    }

    public void SetValue(T value)
    {
        if (setCall == null)
        {
            throw new NotImplementedException();
        }
        setCall(value);
    }
}
