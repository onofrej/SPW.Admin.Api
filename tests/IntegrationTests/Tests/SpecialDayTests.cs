using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Features.Domain;
using SPW.Admin.Api.Features.SpecialDay;
using SPW.Admin.Api.Features.SpecialDay.Create;
using SPW.Admin.Api.Features.SpecialDay.Update;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class SpecialDayTests : BaseIntegratedTest
{
    internal const string GetByIdQuery = "SELECT * FROM \"special_day\" WHERE id = @Id";

    internal const string InsertQuery = @"INSERT INTO ""special_day"" (id, name, start_date, end_date, circuit_id)
                      VALUES (@Id, @Name, @StartDate, @EndDate, @CircuitId)";

    private const int DaysToAddAtDateValues = 3;
    private readonly DateTime _startDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    private readonly DateTime _endDate = new(DateTime.Now.Year + DaysToAddAtDateValues, DateTime.Now.Month, DateTime.Now.Day);
    private const string RequestUri = "/specialdays";
    private readonly MainFixture _mainFixture;

    private readonly DomainEntity _domainEntity;
    private readonly CircuitEntity _circuitEntity;

    public SpecialDayTests(MainFixture mainFixture)
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

        _mainFixture.PostgreSqlFixture.CreateAsync(_domainEntity, DomainTests.InsertQuery, GetCancellationToken).Wait();
        _mainFixture.PostgreSqlFixture.CreateAsync(_circuitEntity, CircuitTests.InsertQuery, GetCancellationToken).Wait();
    }

    [Fact(DisplayName = "Received is valid then special day is created")]
    public async Task Request_received_is_valid_then_special_day_is_created()
    {
        //Arrange
        var request = new Faker<CreateRequest>().StrictMode(true)
            .RuleFor(property => property.Name, setter => setter.Random.Words(5))
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate();

        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        //Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var specialDayEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<SpecialDayEntity>(GetByIdQuery, response!.Data, GetCancellationToken);

        specialDayEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and special day exists then special day is updated")]
    public async Task Request_received_is_valid_and_special_day_exists_then_special_day_is_updated()
    {
        //Arrange
        var entity = new Faker<SpecialDayEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Random.Words(5))
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
            .RuleFor(property => property.Id, setter => entity.Id)
            .RuleFor(property => property.Name, setter => setter.Random.Words(5))
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
           .Generate();

        //Act
        var rawResponse = await _mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newSpecialDayEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<SpecialDayEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        newSpecialDayEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and special day exists then special day is deleted")]
    public async Task Request_received_is_valid_and_special_day_exists_then_special_day_is_deleted()
    {
        //Arrange
        var entity = new Faker<SpecialDayEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Random.Words(5))
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var specialDayEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<SpecialDayEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        specialDayEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Special day id received is valid and special day is returned")]
    public async Task Special_day_id_received_is_valid_and_special_day_is_returned()
    {
        //Arrange
        var entity = new Faker<SpecialDayEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Random.Words(5))
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<SpecialDayEntity>>(GetCancellationToken);

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
        var entities = new Faker<SpecialDayEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => setter.Random.Words(5))
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.CircuitId, setter => _circuitEntity.Id)
            .Generate(numberOfItemsToCreate);

        foreach (var entity in entities)
        {
            await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);
        }

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<SpecialDayEntity>>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data!.Count().Should().BeGreaterThanOrEqualTo(entities.Count);
    }
}