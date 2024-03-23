using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class SpecialDayTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/specialdays";
    private readonly MainFixture _mainFixture;

    public SpecialDayTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
    }
}