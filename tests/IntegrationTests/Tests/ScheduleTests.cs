using SPW.Admin.Api.Features.Domain;
using SPW.Admin.Api.Features.Schedule;
using SPW.Admin.Api.Features.Schedule.Create;
using SPW.Admin.Api.Features.Schedule.Update;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class ScheduleTests(MainFixture mainFixture) : BaseIntegratedTest
{
    private const string GetByIdQuery = "SELECT * FROM \"schedule\" WHERE id = @Id";
    private const string InsertQuery = @"INSERT INTO ""schedule"" (id, ""time"") VALUES (@Id, @Time)";
    private const string RequestUri = "/schedules";
    private const int HourToAdd = 3;

    private readonly DomainEntity _domainEntity = new Faker<DomainEntity>().StrictMode(true)
        .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
        .RuleFor(property => property.Id, setter => Guid.NewGuid())
        .Generate();

    [Fact(DisplayName = "Request received is valid then schedule is created")]
    public async Task Request_received_is_valid_then_schedule_is_created()
    {
        //Arrange
        await mainFixture.PostgreSqlFixture.CreateAsync(_domainEntity, DomainTests.InsertQuery, GetCancellationToken);

        var request = new Faker<CreateRequest>().StrictMode(true)
            .RuleFor(property => property.Time, setter => $"{DateTime.Now:HH:mm}-{DateTime.Now.AddHours(HourToAdd):HH:mm}")
            .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        var rawResponse = await mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        //Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var scheduleEntity = await mainFixture.PostgreSqlFixture
            .GetByIdAsync<ScheduleEntity>(GetByIdQuery, response!.Data, GetCancellationToken);

        scheduleEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and schedule exists then schedule is updated")]
    public async Task Request_received_is_valid_and_schedule_exists_then_schedule_is_updated()
    {
        //Arrange
        var entity = new Faker<ScheduleEntity>().StrictMode(true)
            .RuleFor(property => property.Time, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .Generate();

        await mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
            .RuleFor(property => property.Id, setter => entity.Id)
            .RuleFor(property => property.Time, setter => setter.Company.CompanyName())
            .Generate();

        //Act
        var rawResponse = await mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newScheduleEntity = await mainFixture.PostgreSqlFixture
            .GetByIdAsync<ScheduleEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        newScheduleEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and schedule exists then schedule is deleted")]
    public async Task Request_received_is_valid_and_schedule_exists_then_schedule_is_deleted()
    {
        //Arrange
        var entity = new Faker<ScheduleEntity>().StrictMode(true)
            .RuleFor(property => property.Time, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .Generate();

        await mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var scheduleEntity = await mainFixture.PostgreSqlFixture
            .GetByIdAsync<ScheduleEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        scheduleEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Schedule id received is valid and schedule is returned")]
    public async Task Schedule_id_received_is_valid_and_schedule_is_returned()
    {
        //Arrange
        var entity = new Faker<ScheduleEntity>().StrictMode(true)
            .RuleFor(property => property.Time, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .Generate();

        await mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<ScheduleEntity>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and schedules are returned")]
    public async Task Request_received_is_valid_and_schedules_are_returned()
    {
        //Arrange
        var numberOfItemsToCreate = 10;
        var entities = new Faker<ScheduleEntity>().StrictMode(true)
            .RuleFor(property => property.Time, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .Generate(numberOfItemsToCreate);

        foreach (var entity in entities)
        {
            await mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);
        }

        //Act
        var rawResponse = await mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<ScheduleEntity>>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data!.Count().Should().BeGreaterThanOrEqualTo(entities.Count);
    }
}