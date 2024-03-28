using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class UserTests(MainFixture mainFixture) : BaseIntegratedTest
{
    private const string RequestUri = "/users";
    private const string HashKey = "id";
    private const int DaysToAddAtDateValues = 10;
    private readonly DateTime _birthDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    private readonly DateTime _baptismDate = new(DateTime.Now.Year + DaysToAddAtDateValues, DateTime.Now.Month, DateTime.Now.Day);
    private readonly DateTime _creationDate = new(DateTime.Now.Year + DaysToAddAtDateValues, DateTime.Now.Month, DateTime.Now.Day);

    [Fact(DisplayName = "Request received is valid then user is created")]
    public async Task Request_received_is_valid_then_user_is_created()
    {
        ////Arrange
        //var request = new Faker<CreateRequest>().StrictMode(true)
        //    //.RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
        //    //.RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
        //    //.RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
        //    //.RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
        //    //.RuleFor(property => property.BirthDate, setter => _birthDate)
        //    //.RuleFor(property => property.BaptismDate, setter => _baptismDate)
        //    //.RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
        //    .Generate();

        //var rawResponse = await mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        ////Act
        //var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        ////Assert
        //rawResponse.Should().NotBeNull();
        //rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var userEntity = await mainFixture.DynamoDbFixture
            .ReadAsync<UserEntity>(HashKey, response!.Data.ToString());

        //userEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and user exists then user is updated")]
    public async Task Request_received_is_valid_and_user_exists_then_user_is_updated()
    {
        //Arrange
        var faker = new Faker<UserEntity>().StrictMode(true);
        //.RuleFor(property => property.Id, setter => Guid.NewGuid())
        //.RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
        //.RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
        //.RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
        //.RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
        //.RuleFor(property => property.CreationDate, setter => _creationDate)
        //.RuleFor(property => property.BirthDate, setter => _birthDate)
        //.RuleFor(property => property.BaptismDate, setter => _baptismDate)
        //.RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }));

        //var entity = faker.Generate();

        //await mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        //var request = new Faker<UpdateRequest>().StrictMode(true)
        //    //.RuleFor(property => property.Id, setter => entity.Id)
        //    //.RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
        //    //.RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
        //    //.RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
        //    //.RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
        //    //.RuleFor(property => property.BirthDate, setter => _birthDate.AddDays(DaysToAddAtDateValues))
        //    //.RuleFor(property => property.BaptismDate, setter => _baptismDate.AddDays(DaysToAddAtDateValues))
        //    //.RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
        //    .Generate();

        ////Act
        //var rawResponse = await mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        //var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        ////Assert
        //rawResponse.Should().NotBeNull();
        //rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newUserEntity = await mainFixture.DynamoDbFixture
            .ReadAsync<UserEntity>(HashKey, response!.Data.ToString());

        //newUserEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and user exists then user is deleted")]
    public async Task Request_received_is_valid_and_user_exists_then_user_is_deleted()
    {
        //Arrange
        var entity = new Faker<UserEntity>().StrictMode(true)
           //.RuleFor(property => property.Id, setter => Guid.NewGuid())
           //.RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
           //.RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
           //.RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
           //.RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
           //.RuleFor(property => property.CreationDate, setter => _creationDate)
           //.RuleFor(property => property.BirthDate, setter => _birthDate)
           //.RuleFor(property => property.BaptismDate, setter => _baptismDate)
           //.RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
           .Generate();

        //await mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        ////Act
        //var rawResponse = await mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        ////Assert
        //rawResponse.Should().NotBeNull();
        //rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var userEntity = await mainFixture.DynamoDbFixture
            .ReadAsync<UserEntity>(HashKey, entity.ToString());

        //userEntity.Should().BeNull();
    }

    [Fact(DisplayName = "User id received is valid and user is returned")]
    public async Task User_id_received_is_valid_and_user_is_returned()
    {
        //Arrange
        var entity = new Faker<UserEntity>().StrictMode(true)
           //.RuleFor(property => property.Id, setter => Guid.NewGuid())
           //.RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
           //.RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
           //.RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
           //.RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
           //.RuleFor(property => property.CreationDate, setter => _creationDate)
           //.RuleFor(property => property.BirthDate, setter => _birthDate)
           //.RuleFor(property => property.BaptismDate, setter => _baptismDate)
           //.RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
           .Generate();

        //await mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        //Act
        var rawResponse = await mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<UserEntity>>(GetCancellationToken);

        ////Assert
        //rawResponse.Should().NotBeNull();
        //rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        //entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and users are returned")]
    public async Task Request_received_is_valid_and_users_are_returned()
    {
        //Arrange
        var entity = new Faker<UserEntity>().StrictMode(true)
           //.RuleFor(property => property.Id, setter => Guid.NewGuid())
           //.RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
           //.RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
           //.RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
           //.RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
           //.RuleFor(property => property.CreationDate, setter => _creationDate)
           //.RuleFor(property => property.BirthDate, setter => _birthDate)
           //.RuleFor(property => property.BaptismDate, setter => _baptismDate)
           //.RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
           .Generate();

        //await mainFixture.DynamoDbFixture.TruncateTableAsync(GetCancellationToken);

        //await mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        //Act
        var rawResponse = await mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<UserEntity>>>(GetCancellationToken);

        ////Assert
        //rawResponse.Should().NotBeNull();
        //rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data.Should().BeEquivalentTo(new List<UserEntity> { entity });
    }
}