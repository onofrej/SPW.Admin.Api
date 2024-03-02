using SPW.Admin.Api.Features.Validity.Create;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class ValidityTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/validities";
    private readonly Fixture _fixture;
    private readonly MainFixture _mainFixture;

    public ValidityTests(MainFixture mainFixture)
    {
        _fixture = new Fixture();
        _mainFixture = mainFixture;
    }

    [Fact(DisplayName = "Request received is valid then Validity is created")]
    public async Task Request_received_is_valid_then_circuit_is_created()
    {
        //Arrange
        var request = _fixture.Create<CreateRequest>();

        //Act
        var response = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request);

        //Assert
        request.Should().NotBeNull();
    }

    [Fact(DisplayName = "Request received is invalid then Validity is not created")]
    public async Task Request_received_is_valid_then_circuit_is_not_created()
    {
        //Arrange

        //Act

        //Assert
    }
}