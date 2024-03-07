using SPW.Admin.IntegrationTests.Factories;
using SPW.Admin.IntegrationTests.Fixtures.DynamoDb;

namespace SPW.Admin.IntegrationTests.Fixtures;

public sealed class MainFixture : IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly DynamoDbFixture _dynamoDbFixture;
    private readonly CustomWebApplicationFactory _customWebApplicationFactory;
    private readonly HttpClient _httpClient;

    public MainFixture()
    {
        _configuration = new ConfigurationBuilder()
          .SetBasePath(AppContext.BaseDirectory)
          .AddJsonFile("integrationtests-settings.json", optional: false)
          .Build();

        InitializeEnvironmentVariables();

        _dynamoDbFixture = new DynamoDbFixture(_configuration);

        _customWebApplicationFactory = new CustomWebApplicationFactory();
        _httpClient = _customWebApplicationFactory.CreateClient();
    }

    internal HttpClient HttpClient => _httpClient;

    internal DynamoDbFixture DynamoDbFixture => _dynamoDbFixture;

    public void Dispose()
    {
        _dynamoDbFixture.Dispose();

        GC.SuppressFinalize(this);
    }

    private void InitializeEnvironmentVariables()
    {
        Environment.SetEnvironmentVariable("HTTP_PROXY", "");
        Environment.SetEnvironmentVariable("HTTPS_PROXY", "");
        Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", "test");
        Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", "test");
    }
}