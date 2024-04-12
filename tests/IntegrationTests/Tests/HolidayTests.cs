﻿using SPW.Admin.Api.Features.Holiday;
using SPW.Admin.Api.Features.Holiday.Create;
using SPW.Admin.Api.Features.Holiday.Update;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;
namespace SPW.Admin.IntegrationTests.Tests;

public class HolidayTests : BaseIntegratedTest, IClassFixture<MainFixture>
{
    private const string RequestUri = "/holidays";
    private const string GetByIdQuery = @"SELECT * FROM ""holiday"" WHERE id = @Id";
    private const string InsertQuery = @"INSERT INTO ""holiday"" (id, name, date) VALUES (@Id, @Name, @Date)";

    private readonly MainFixture _mainFixture;
    private readonly DateTime _mockHolidayDate;
    private readonly string _mockHolidayName = "Mock name";

    public HolidayTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
        _mockHolidayDate = DateTime.Now.Date;
    }

    [Fact(DisplayName = "Request received is valid then holiday is created")]
    public async Task Request_received_is_valid_then_holiday_is_created()
    {
        // Arrange
        var request = new Faker<CreateRequest>().StrictMode(true)
            .RuleFor(property => property.Name, setter => _mockHolidayName)
            .RuleFor(property => property.Date, setter => _mockHolidayDate)
            .Generate();

        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        // Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        // Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var holidayEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<HolidayEntity>(GetByIdQuery, response!.Data, GetCancellationToken);

        holidayEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and holiday exists then holiday is updated")]
    public async Task Request_received_is_valid_and_holiday_exists_then_holiday_is_updated()
    {
        // Arrange
        Random rng = new Random();
        int days = rng.Next(1, 32);

        var entity = new Faker<HolidayEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => _mockHolidayName)
            .RuleFor(property => property.Date, setter => _mockHolidayDate)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
            .RuleFor(property => property.Id, setter => entity.Id)
            .RuleFor(property => property.Name, setter => "Carnival or something, I don't know")
            .RuleFor(property => property.Date, setter => entity.Date.AddDays(days))
            .Generate();

        // Act
        var rawResponse = await _mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        // Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newHolidayEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<HolidayEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        newHolidayEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and holiday exists then holiday is deleted")]
    public async Task Request_received_is_valid_and_holiday_exists_then_holiday_is_deleted()
    {
        // Arrange
        var entity = new Faker<HolidayEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => _mockHolidayName)
            .RuleFor(property => property.Date, setter => _mockHolidayDate)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        // Act
        var rawResponse = await _mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        // Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var holidayEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<HolidayEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        holidayEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Holiday id received is valid then holiday is returned")]
    public async Task Holiday_id_received_is_valid_then_holiday_is_returned()
    {
        // Arrange
        var entity = new Faker<HolidayEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => _mockHolidayName)
            .RuleFor(property => property.Date, setter => _mockHolidayDate)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        // Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}");
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<HolidayEntity>>(GetCancellationToken);

        // Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid then holidays are returned")]
    public async Task Request_received_is_valid_then_holidays_are_returned()
    {
        // Arrange
        int numberOfItemsToCreate = 10;
        var entities = new Faker<HolidayEntity>().StrictMode(true)
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .RuleFor(property => property.Name, setter => _mockHolidayName)
            .RuleFor(property => property.Date, setter => _mockHolidayDate)
            .Generate(numberOfItemsToCreate);

        foreach (var entity in entities)
        {
            await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);
        }

        // Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<HolidayEntity>>>(GetCancellationToken);

        // Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data!.Count().Should().BeGreaterThanOrEqualTo(entities.Count);
    }
}