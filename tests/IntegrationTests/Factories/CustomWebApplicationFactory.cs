namespace SPW.Admin.IntegrationTests.Factories;

internal sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTests");

        return base.CreateHost(builder);
    }
}