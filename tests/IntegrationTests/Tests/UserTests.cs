using SPW.Admin.Api.Features.User.Create;
using SPW.Admin.Api.Features.User.DataAccess;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class UserTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/users";
    private const string HashKey = "id";
    private readonly Fixture _fixture;
    private readonly MainFixture _mainFixture;
    private readonly DateTime _birthDate;
    private readonly DateTime _baptismDate;

    public UserTests(MainFixture mainFixture)
    {
        _fixture = new Fixture();
        _mainFixture = mainFixture;
        _birthDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        _baptismDate = new DateTime(DateTime.Now.Year + 10, DateTime.Now.Month, DateTime.Now.Day);
    }

    [Fact(DisplayName = "Request received is valid then user is created")]
    public async Task Request_received_is_valid_then_user_is_created()
    {
        //Arrange
        var request = _fixture.Build<CreateRequest>()
            .With(propertyPicker => propertyPicker.Email, "johndoe@gmail.com")
            .With(propertyPicker => propertyPicker.PhoneNumber, "12345678912")
            .With(propertyPicker => propertyPicker.BirthDate, _birthDate)
            .With(propertyPicker => propertyPicker.BaptismDate, _baptismDate)
            .Create();

        //Act
        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>();

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var userEntity = await _mainFixture.DynamoDbFixture
            .ReadAsync<UserEntity>(HashKey, response!.Data.ToString());

        userEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is invalid then user is not created")]
    public async Task Request_received_is_invalid_then_user_is_not_created()
    {
        //Arrange

        //Act

        //Assert
    }
}