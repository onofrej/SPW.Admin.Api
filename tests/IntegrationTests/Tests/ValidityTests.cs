using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class ValidityTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/validities";
    private readonly MainFixture _mainFixture;

    public ValidityTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
    }
}