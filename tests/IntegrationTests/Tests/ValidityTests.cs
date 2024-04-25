using SPW.Admin.Api.Features.Domain;
using SPW.Admin.Api.Features.Validity;
using SPW.Admin.Api.Features.Validity.Create;
using SPW.Admin.Api.Features.Validity.Update;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class ValidityTests : BaseIntegratedTest
{
    internal const string GetByIdQuery = "SELECT * FROM \"validity\" WHERE id = @Id";

    internal const string InsertQuery = @"INSERT INTO ""validity"" (id, start_date, end_date, status, domain_id)
                      VALUES (@Id, @StartDate, @EndDate, @Status, @DomainId)";

    private const int DaysToAddAtDateValues = 30;
    private readonly DateTime _startDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    private readonly DateTime _endDate = new(DateTime.Now.Year + DaysToAddAtDateValues, DateTime.Now.Month, DateTime.Now.Day);
    private const bool DefaultValidityStatus = true;
    private const string RequestUri = "/validities";
    private readonly MainFixture _mainFixture;

    private readonly DomainEntity _domainEntity;

    public ValidityTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;

        _domainEntity = new Faker<DomainEntity>().StrictMode(true)
       .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
       .RuleFor(property => property.Id, setter => Guid.NewGuid())
       .Generate();

        _mainFixture.PostgreSqlFixture.CreateAsync(_domainEntity, DomainTests.InsertQuery, GetCancellationToken).Wait();
    }

    [Fact(DisplayName = "Received is valid then validity is created")]
    public async Task Request_received_is_valid_then_validity_is_created()
    {
        //Arrange
        var request = new Faker<CreateRequest>().StrictMode(true)
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.Status, setter => DefaultValidityStatus)
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        //Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var validityEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<ValidityEntity>(GetByIdQuery, response!.Data, GetCancellationToken);

        validityEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and validity exists then validity is updated")]
    public async Task Request_received_is_valid_and_validity_exists_then_validity_is_updated()
    {
        //Arrange
        var entity = new Faker<ValidityEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.Status, setter => DefaultValidityStatus)
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
            .RuleFor(property => property.Id, setter => entity.Id)
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.Status, setter => false)
           .Generate();

        //Act
        var rawResponse = await _mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newValidityEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<ValidityEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        newValidityEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and validity exists then validity is deleted")]
    public async Task Request_received_is_valid_and_validity_exists_then_validity_is_deleted()
    {
        //Arrange
        var entity = new Faker<ValidityEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.Status, setter => DefaultValidityStatus)
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var validityEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<ValidityEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        validityEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Validity id received is valid and validity is returned")]
    public async Task Validity_id_received_is_valid_and_validity_is_returned()
    {
        //Arrange
        var entity = new Faker<ValidityEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.Status, setter => DefaultValidityStatus)
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<ValidityEntity>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and validities are returned")]
    public async Task Request_received_is_valid_and_validities_are_returned()
    {
        //Arrange
        var numberOfItemsToCreate = 10;
        var entities = new Faker<ValidityEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.StartDate, setter => _startDate)
            .RuleFor(property => property.EndDate, setter => _endDate)
            .RuleFor(property => property.Status, setter => DefaultValidityStatus)
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate(numberOfItemsToCreate);

        foreach (var entity in entities)
        {
            await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);
        }

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<ValidityEntity>>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data!.Count().Should().BeGreaterThanOrEqualTo(entities.Count);
    }
}