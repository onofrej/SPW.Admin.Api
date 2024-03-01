using SPW.Admin.Api.Features.User.Create;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class UserTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/users";
    private readonly Fixture _fixture;
    private readonly MainFixture _mainFixture;

    public UserTests(MainFixture mainFixture)
    {
        _fixture = new Fixture();
        _mainFixture = mainFixture;
    }

    [Fact(DisplayName = "Request received is valid then user is created")]
    public async Task Request_received_is_valid_then_user_is_created()
    {
        //Arrange
        var request = _fixture.Create<CreateRequest>();

        //Act
        var response = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request);

        //Assert
        response.Should().NotBeNull();
    }

    [Fact(DisplayName = "Request received is invalid then user is not created")]
    public async Task Request_received_is_invalid_then_user_is_not_created()
    {
        //Arrange

        //Act

        //Assert
    }
}