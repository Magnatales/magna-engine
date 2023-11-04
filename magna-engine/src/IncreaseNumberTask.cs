using Log;

namespace HelloWorld;

public class IncreaseNumberTask : IDisposable
{
    public int Number { get; private set; }
    private CancellationTokenSource cts;

    public IncreaseNumberTask()
    {
        cts = new CancellationTokenSource();
        Do();
    }
    private async void Do()
    {
        while (!cts.Token.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(1), cts.Token);
                Number += 1;
                Logger.Debug(Number.ToString());
            }
            catch (OperationCanceledException e)
            {
                Logger.Debug(e.ToString());
                break;
            }   
            
        }
    }

    public void Dispose()
    {
        Logger.Debug("Dispose");
        cts.Cancel();
        cts.Dispose();
    }
}