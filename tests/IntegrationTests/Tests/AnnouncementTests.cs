using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class AnnouncementTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/announcements";
    private readonly MainFixture _mainFixture;

    public AnnouncementTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
    }
}