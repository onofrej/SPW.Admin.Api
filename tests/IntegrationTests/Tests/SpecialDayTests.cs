using SPW.Admin.Api.Features.SpecialDay.Create;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class SpecialDayTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/specialdays";
    private readonly Fixture _fixture;
    private readonly MainFixture _mainFixture;

    public SpecialDayTests(MainFixture mainFixture)
    {
        _fixture = new Fixture();
        _mainFixture = mainFixture;
    }

    [Fact(DisplayName = "Request received is valid then special day is created")]
    public async Task Request_received_is_valid_then_special_day_is_created()
    {
        //Arrange
        var request = _fixture.Create<CreateRequest>();

        //Act
        var response = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request);

        //Assert
        response.Should().NotBeNull();
    }

    [Fact(DisplayName = "Request received is invalid then special day is not created")]
    public async Task Request_received_is_valid_then_special_day_is_not_created()
    {
        //Arrange

        //Act

        //Assert
    }
}