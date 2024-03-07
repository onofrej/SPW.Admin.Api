namespace SPW.Admin.IntegrationTests;

public abstract class BaseIntegratedTest
{
    protected static CancellationToken GetCancellationToken
    {
        get
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(600_000);
            return cancellationTokenSource.Token;
        }
    }
}