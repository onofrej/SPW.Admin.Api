using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Features.Circuit.Create;
using SPW.Admin.Api.Features.Circuit.Update;
using SPW.Admin.Api.Features.Domain;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class CircuitTests : BaseIntegratedTest
{
    internal const string GetByIdQuery = "SELECT * FROM \"circuit\" WHERE id = @Id";
    internal const string InsertQuery = @"INSERT INTO ""circuit"" (id, name, domain_id) VALUES (@Id, @Name, @DomainId)";
    private const string RequestUri = "/circuits";
    private readonly MainFixture _mainFixture;

    private readonly DomainEntity _domainEntity = new Faker<DomainEntity>().StrictMode(true)
        .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
        .RuleFor(property => property.Id, setter => Guid.NewGuid())
        .Generate();

    public CircuitTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;

        _mainFixture.PostgreSqlFixture.CreateAsync(_domainEntity, DomainTests.InsertQuery, GetCancellationToken).Wait();
    }

    [Fact(DisplayName = "Request received is valid then circuit is created")]
    public async Task Request_received_is_valid_then_circuit_is_created()
    {
        //Arrange
        var request = new Faker<CreateRequest>().StrictMode(true)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        //Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var circuitEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<CircuitEntity>(GetByIdQuery, response!.Data, GetCancellationToken);

        circuitEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and circuit exists then circuit is updated")]
    public async Task Request_received_is_valid_and_circuit_exists_then_circuit_is_updated()
    {
        //Arrange
        var entity = new Faker<CircuitEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
            .RuleFor(property => property.Id, setter => entity.Id)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .Generate();

        //Act
        var rawResponse = await _mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newCircuitEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<CircuitEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        newCircuitEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and circuit exists then circuit is deleted")]
    public async Task Request_received_is_valid_and_circuit_exists_then_circuit_is_deleted()
    {
        //Arrange
        var entity = new Faker<CircuitEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var circuitEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<CircuitEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        circuitEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Circuit id received is valid and circuit is returned")]
    public async Task Circuit_id_received_is_valid_and_circuit_is_returned()
    {
        //Arrange
        var entity = new Faker<CircuitEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<CircuitEntity>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and circuits are returned")]
    public async Task Request_received_is_valid_and_circuits_are_returned()
    {
        //Arrange
        var numberOfItemsToCreate = 10;
        var entities = new Faker<CircuitEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate(numberOfItemsToCreate);

        foreach (var entity in entities)
        {
            await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);
        }

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<CircuitEntity>>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data!.Count().Should().BeGreaterThanOrEqualTo(entities.Count);
    }
}