using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace cYo.Common.Threading;

public class BackgroundRunner : Component
{
    private readonly ManualResetEvent exitEvent = new(initialState: false);

    private readonly ManualResetEvent runEvent = new(initialState: false);

    private readonly ManualResetEvent intervalEvent = new(initialState: false);

    private Thread thread;

    private volatile bool enabled;

    [DefaultValue(null)]
    public ISynchronizeInvoke Synchronize { get; set; }

    [DefaultValue(0)]
    public int Interval { get; set; }

    [DefaultValue(false)]
    public bool Enabled
    {
        get => enabled;
        set
        {
            if (value == enabled)
            {
                return;
            }
            enabled = value;
            if (enabled)
            {
                thread ??= ThreadUtility.RunInBackground("BackgroundRunner Thread", BackgroundMethod);
                runEvent.Set();
                intervalEvent.Reset();
            }
            else
            {
                runEvent.Reset();
                intervalEvent.Set();
            }
        }
    }

    public event EventHandler Tick;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            runEvent.Reset();
            exitEvent.Set();
            intervalEvent.Set();
            Thread thread = this.thread;
            if (thread != null && !thread.Join(5000))
            {
                thread.Abort();
                thread.Join();
            }
            runEvent.Dispose();
            exitEvent.Dispose();
        }
        base.Dispose(disposing);
    }

    protected virtual void OnTick()
    {
        Tick?.Invoke(this, EventArgs.Empty);
    }

    public void Start()
    {
        Enabled = true;
    }

    public void Stop()
    {
        Enabled = false;
    }

    private void BackgroundMethod()
    {
        ManualResetEvent[] waitHandles =
        [
            exitEvent,
            runEvent
        ];
        while (WaitHandle.WaitAny(waitHandles) != 0)
        {
            if (Synchronize == null)
            {
                InvokeTick();
            }
            else
            {
                try
                {
                    Synchronize.Invoke(new MethodInvoker(InvokeTick), null);
                }
                catch (InvalidOperationException)
                {
                }
            }
            intervalEvent.WaitOne(Interval);
        }
    }

    private void InvokeTick()
    {
        if (enabled)
        {
            OnTick();
        }
    }
}
