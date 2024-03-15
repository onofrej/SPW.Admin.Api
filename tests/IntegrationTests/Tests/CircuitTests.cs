using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class CircuitTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/circuits";
    private readonly MainFixture _mainFixture;

    public CircuitTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
    }
}