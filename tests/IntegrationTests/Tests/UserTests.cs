using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Features.Congregation;
using SPW.Admin.Api.Features.Domain;
using SPW.Admin.Api.Features.User;
using SPW.Admin.Api.Features.User.Create;
using SPW.Admin.Api.Features.User.Update;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class UserTests : BaseIntegratedTest
{
    internal const string GetByIdQuery = "SELECT * FROM \"user\" WHERE id = @Id";

    internal const string InsertQuery = @"INSERT INTO ""user"" (id, name, creation_date, email, phone_number, gender, birth_date, baptism_date, privilege, congregation_id)
             VALUES (@Id, @Name, @CreationDate, @Email, @PhoneNumber, @Gender, @BirthDate, @BaptismDate, @Privilege, @CongregationId)";

    private const int DaysToAddAtDateValues = 10;
    private readonly DateTime _birthDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    private readonly DateTime _baptismDate = new(DateTime.Now.Year + DaysToAddAtDateValues, DateTime.Now.Month, DateTime.Now.Day);
    private readonly DateTime _creationDate = new(DateTime.Now.Year + DaysToAddAtDateValues, DateTime.Now.Month, DateTime.Now.Day);
    private const string RequestUri = "/users";
    private readonly MainFixture _mainFixture;

    private readonly DomainEntity _domainEntity;
    private readonly CircuitEntity _circuitEntity;
    private readonly CongregationEntity _congregationEntity;

    public UserTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;

        _domainEntity = new Faker<DomainEntity>().StrictMode(true)
       .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
       .RuleFor(property => property.Id, setter => Guid.NewGuid())
       .Generate();

        _circuitEntity = new Faker<CircuitEntity>().StrictMode(true)
        .RuleFor(property => property.Id, setter => Guid.NewGuid())
        .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
        .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
        .Generate();

        _congregationEntity = new Faker<CongregationEntity>().StrictMode(true)
        .RuleFor(property => property.Id, setter => Guid.NewGuid())
        .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
        .RuleFor(property => property.Number, setter => setter.Random.ReplaceNumbers("#####"))
        .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
        .Generate();

        _mainFixture.PostgreSqlFixture.CreateAsync(_domainEntity, DomainTests.InsertQuery, GetCancellationToken).Wait();
        _mainFixture.PostgreSqlFixture.CreateAsync(_circuitEntity, CircuitTests.InsertQuery, GetCancellationToken).Wait();
        _mainFixture.PostgreSqlFixture.CreateAsync(_congregationEntity, CongregationTests.InsertQuery, GetCancellationToken).Wait();
    }

    [Fact(DisplayName = "Request received is valid then user is created")]
    public async Task Request_received_is_valid_then_user_is_created()
    {
        //Arrange
        var request = new Faker<CreateRequest>().StrictMode(true)
            .RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
            .RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
            .RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
            .RuleFor(property => property.BirthDate, setter => _birthDate)
            .RuleFor(property => property.BaptismDate, setter => _baptismDate)
            .RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
            .RuleFor(property => property.CongregationId, setter => _congregationEntity.Id)
            .Generate();

        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        //Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var congregationEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<UserEntity>(GetByIdQuery, response!.Data, GetCancellationToken);

        congregationEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and user exists then user is updated")]
    public async Task Request_received_is_valid_and_user_exists_then_user_is_updated()
    {
        //Arrange
        var entity = new Faker<UserEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
            .RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
            .RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
            .RuleFor(property => property.CreationDate, setter => _creationDate)
            .RuleFor(property => property.BirthDate, setter => _birthDate)
            .RuleFor(property => property.BaptismDate, setter => _baptismDate)
            .RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
            .RuleFor(property => property.CongregationId, setter => _congregationEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
            .RuleFor(property => property.Id, setter => entity.Id)
            .RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
            .RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
            .RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
            .RuleFor(property => property.BirthDate, setter => _birthDate)
            .RuleFor(property => property.BaptismDate, setter => _baptismDate)
            .RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
            .RuleFor(property => property.CongregationId, setter => _congregationEntity.Id)
           .Generate();

        //Act
        var rawResponse = await _mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newCongregationEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<UserEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        newCongregationEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and user exists then user is deleted")]
    public async Task Request_received_is_valid_and_user_exists_then_user_is_deleted()
    {
        //Arrange
        var entity = new Faker<UserEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
            .RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
            .RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
            .RuleFor(property => property.CreationDate, setter => _creationDate)
            .RuleFor(property => property.BirthDate, setter => _birthDate)
            .RuleFor(property => property.BaptismDate, setter => _baptismDate)
            .RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
            .RuleFor(property => property.CongregationId, setter => _congregationEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var userEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<UserEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        userEntity.Should().BeNull();
    }

    [Fact(DisplayName = "User id received is valid and user is returned")]
    public async Task User_id_received_is_valid_and_user_is_returned()
    {
        //Arrange
        var entity = new Faker<UserEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
            .RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
            .RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
            .RuleFor(property => property.CreationDate, setter => _creationDate)
            .RuleFor(property => property.BirthDate, setter => _birthDate)
            .RuleFor(property => property.BaptismDate, setter => _baptismDate)
            .RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
            .RuleFor(property => property.CongregationId, setter => _congregationEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<UserEntity>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and users are returned")]
    public async Task Request_received_is_valid_and_users_are_returned()
    {
        //Arrange
        var numberOfItemsToCreate = 10;
        var entities = new Faker<UserEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Name.FullName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(property => property.Email, setter => setter.Internet.Email(setter.Person.FirstName.ToLower()))
            .RuleFor(property => property.PhoneNumber, setter => setter.Random.ReplaceNumbers("###########"))
            .RuleFor(property => property.CreationDate, setter => _creationDate)
            .RuleFor(property => property.Gender, setter => setter.PickRandom(new string[] { "male", "female" }))
            .RuleFor(property => property.BirthDate, setter => _birthDate)
            .RuleFor(property => property.BaptismDate, setter => _baptismDate)
            .RuleFor(property => property.Privilege, setter => setter.PickRandom(new string[] { "Elder", "Pioneer", "Ministerial Servant" }))
            .RuleFor(property => property.CongregationId, setter => _congregationEntity.Id)
            .Generate(numberOfItemsToCreate);

        foreach (var entity in entities)
        {
            await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);
        }

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<UserEntity>>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data!.Count().Should().BeGreaterThanOrEqualTo(entities.Count);
    }
}