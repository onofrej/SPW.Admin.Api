using SPW.Admin.Api.Features.Point.Create;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class PointTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/points";
    private readonly Fixture _fixture;
    private readonly MainFixture _mainFixture;

    public PointTests(MainFixture mainFixture)
    {
        _fixture = new Fixture();
        _mainFixture = mainFixture;
    }

    [Fact(DisplayName = "Request received is valid then point is created")]
    public async Task Request_received_is_valid_then_point_is_created()
    {
        //Arrange
        var request = _fixture.Create<CreateRequest>();

        //Act
        var response = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request);

        //Assert
        response.Should().NotBeNull();
    }

    [Fact(DisplayName = "Request received is invalid then point is not created")]
    public async Task Request_received_is_invalid_then_point_is_not_created()
    {
        //Arrange

        //Act

        //Assert
    }
}