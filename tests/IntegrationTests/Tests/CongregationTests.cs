using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Features.Congregation;
using SPW.Admin.Api.Features.Congregation.Create;
using SPW.Admin.Api.Features.Congregation.Update;
using SPW.Admin.Api.Features.Domain;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class CongregationTests : BaseIntegratedTest
{
    internal const string GetByIdQuery = "SELECT * FROM \"congregation\" WHERE id = @Id";
    internal const string InsertQuery = @"INSERT INTO ""congregation"" (id, name, number, circuit_id) VALUES (@Id, @Name, @Number, @CircuitId)";
    private const string RequestUri = "/congregations";
    private readonly MainFixture _mainFixture;

    private readonly DomainEntity _domainEntity;
    private readonly CircuitEntity _circuitEntity;

    public CongregationTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;

        _domainEntity = new Faker<DomainEntity>().StrictMode(true)
           .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .Generate();

        _circuitEntity = new Faker<CircuitEntity>().StrictMode(true)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        _mainFixture.PostgreSqlFixture.CreateAsync(_domainEntity, DomainTests.InsertQuery, GetCancellationToken).Wait();
        _mainFixture.PostgreSqlFixture.CreateAsync(_circuitEntity, CircuitTests.InsertQuery, GetCancellationToken).Wait();
    }

    [Fact(DisplayName = "Request received is valid then congregation is created")]
    public async Task Request_received_is_valid_then_congregation_is_created()
    {
        //Arrange
        var request = new Faker<CreateRequest>().StrictMode(true)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Number, setter => setter.Random.ReplaceNumbers("#####"))
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate();

        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        //Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var congregationEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<CongregationEntity>(GetByIdQuery, response!.Data, GetCancellationToken);

        congregationEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and congregation exists then congregation is updated")]
    public async Task Request_received_is_valid_and_congregation_exists_then_congregation_is_updated()
    {
        //Arrange
        var entity = new Faker<CongregationEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Number, setter => setter.Random.ReplaceNumbers("#####"))
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
            .RuleFor(property => property.Id, setter => entity.Id)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Number, setter => setter.Random.ReplaceNumbers("#####"))
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate();

        //Act
        var rawResponse = await _mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newCongregationEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<CongregationEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        newCongregationEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and congregation exists then congregation is deleted")]
    public async Task Request_received_is_valid_and_congregation_exists_then_congregation_is_deleted()
    {
        //Arrange
        var entity = new Faker<CongregationEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Number, setter => setter.Random.ReplaceNumbers("#####"))
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var congregationEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<CongregationEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        congregationEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Congregation id received is valid and congregation is returned")]
    public async Task Congregation_id_received_is_valid_and_congregation_is_returned()
    {
        //Arrange
        var entity = new Faker<CongregationEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Number, setter => setter.Random.ReplaceNumbers("#####"))
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<CongregationEntity>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and congregations are returned")]
    public async Task Request_received_is_valid_and_congregations_are_returned()
    {
        //Arrange
        var numberOfItemsToCreate = 10;
        var entities = new Faker<CongregationEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Number, setter => setter.Random.ReplaceNumbers("#####"))
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate(numberOfItemsToCreate);

        foreach (var entity in entities)
        {
            await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);
        }

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<CongregationEntity>>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data!.Count().Should().BeGreaterThanOrEqualTo(entities.Count);
    }
}