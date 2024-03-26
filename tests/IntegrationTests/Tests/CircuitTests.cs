using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Features.Circuit.Create;
using SPW.Admin.Api.Features.Circuit.Update;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class CircuitTests : BaseIntegratedTest
{
    private const string RequestUri = "/circuits";
    private const string HashKey = "id";
    private readonly MainFixture _mainFixture;

    public CircuitTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
    }

    [Fact(DisplayName = "Request received is valid then circuit is created")]
    public async Task Request_received_is_valid_then_circuit_is_created()
    {
        //Arrange
        var request = new Faker<CreateRequest>().StrictMode(true)
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .Generate();

        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        //Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var circuitEntity = await _mainFixture.DynamoDbFixture
            .ReadAsync<CircuitEntity>(HashKey, response!.Data.ToString());

        circuitEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and circuit exists then circuit is updated")]
    public async Task Request_received_is_valid_and_circuit_exists_then_circuit_is_updated()
    {
        //Arrange
        var faker = new Faker<CircuitEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)));

        var entity = faker.Generate();

        await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
            .RuleFor(property => property.Id, setter => entity.Id)
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
            .Generate();

        //Act
        var rawResponse = await _mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newCircuitEntity = await _mainFixture.DynamoDbFixture
            .ReadAsync<CircuitEntity>(HashKey, response!.Data.ToString());

        newCircuitEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and circuit exists then circuit is deleted")]
    public async Task Request_received_is_valid_and_circuit_exists_then_circuit_is_deleted()
    {
        //Arrange
        var entity = new Faker<CircuitEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .Generate();

        await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var circuitEntity = await _mainFixture.DynamoDbFixture
            .ReadAsync<CircuitEntity>(HashKey, entity.ToString());

        circuitEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Circuit id received is valid and circuit is returned")]
    public async Task Circuit_id_received_is_valid_and_circuit_is_returned()
    {
        //Arrange
        var entity = new Faker<CircuitEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .Generate();

        await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<CircuitEntity>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and circuit are returned")]
    public async Task Request_received_is_valid_and_circuit_are_returned()
    {
        //Arrange
        var entity = new Faker<CircuitEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .Generate();

        await _mainFixture.DynamoDbFixture.TruncateTableAsync(GetCancellationToken);

        await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<CircuitEntity>>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data.Should().BeEquivalentTo(new List<CircuitEntity> { entity });
    }
}