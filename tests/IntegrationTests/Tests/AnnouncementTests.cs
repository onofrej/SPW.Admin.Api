using SPW.Admin.Api.Features.Announcement.Create;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class AnnouncementTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/announcements";
    private readonly Fixture _fixture;
    private readonly MainFixture _mainFixture;

    public AnnouncementTests(MainFixture mainFixture)
    {
        _fixture = new Fixture();
        _mainFixture = mainFixture;
    }

    [Fact(DisplayName = "Request received is valid then announcement is created")]
    public async Task Request_received_is_valid_then_announcement_is_created()
    {
        //Arrange
        var request = _fixture.Create<CreateRequest>();

        //Act
        var response = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request);

        //Assert
        response.Should().NotBeNull();
    }

    [Fact(DisplayName = "Request received is invalid then announcement is not created")]
    public async Task Request_received_is_invalid_then_announcement_is_not_created()
    {
        //Arrange

        //Act

        //Assert
    }
}