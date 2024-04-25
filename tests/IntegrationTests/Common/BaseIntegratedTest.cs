namespace SPW.Admin.IntegrationTests.Common;

public abstract class BaseIntegratedTest
{
    protected static CancellationToken GetCancellationToken
    {
        get
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(60_000);
            return cancellationTokenSource.Token;
        }
    }
}