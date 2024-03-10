using SPW.Admin.Api.Features.User.Create;
using SPW.Admin.Api.Features.User.DataAccess;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Fixtures;
using Bogus;

namespace SPW.Admin.IntegrationTests.Tests;

public class UserTests : IClassFixture<MainFixture>
{
    private const string RequestUri = "/users";
    private const string HashKey = "id";
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
        var faker = new Faker<CreateRequest>().StrictMode(true)
               .RuleFor(user => user.Name, fakerData => fakerData.Name.FullName(Bogus.DataSets.Name.Gender.Male))
               .RuleFor(user => user.Email, fakerData => fakerData.Internet.Email(fakerData.Person.FirstName.ToLower()))
               .RuleFor(user => user.PhoneNumber, fakerData => fakerData.Random.ReplaceNumbers("###########"))
               .RuleFor(user => user.Gender, fakerData => fakerData.PickRandom(new string[] { "male", "female" }))
               .RuleFor(user => user.BirthDate, fakerData => fakerData.Date.Recent(3650).ToUniversalTime())
               .RuleFor(user => user.BaptismDate, fakerData => fakerData.Date.Recent(350).ToUniversalTime())
               .RuleFor(user => user.Privilege, fakerData => fakerData.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }));

        //Act
        var entity = faker.Generate();
        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, entity);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>();

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var userEntity = await _mainFixture.DynamoDbFixture
            .ReadAsync<UserEntity>(HashKey, response!.Data.ToString());

        userEntity.Should().BeEquivalentTo(entity);
    }

    [Fact(DisplayName = "Request received is invalid then user is not created")]
    public async Task Request_received_is_invalid_then_user_is_not_created()
    {
        //Arrange

        //Act

        //Assert
    }
}