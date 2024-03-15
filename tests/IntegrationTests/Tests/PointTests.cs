using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class PointTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/points";
    private readonly MainFixture _mainFixture;

    public PointTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
    }
}