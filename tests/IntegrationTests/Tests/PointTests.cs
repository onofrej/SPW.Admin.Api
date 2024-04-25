using SPW.Admin.Api.Features.Domain;
using SPW.Admin.Api.Features.Point;
using SPW.Admin.Api.Features.Point.Create;
using SPW.Admin.Api.Features.Point.Update;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class PointTests : BaseIntegratedTest
{
    internal const string GetByIdQuery = "SELECT * FROM \"point\" WHERE id = @Id";
    internal const string InsertQuery = @"INSERT INTO ""point"" (id, name, number_of_publishers, address, image_url, google_maps_url, domain_id) VALUES (@Id, @Name, @NumberOfPublishers, @Address, @ImageUrl, @GoogleMapsUrl, @DomainId)";
    private const string RequestUri = "/points";
    private readonly MainFixture _mainFixture;

    private readonly DomainEntity _domainEntity = new Faker<DomainEntity>().StrictMode(true)
        .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
        .RuleFor(property => property.Id, setter => Guid.NewGuid())
        .Generate();

    public PointTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;

        _mainFixture.PostgreSqlFixture.CreateAsync(_domainEntity, DomainTests.InsertQuery, GetCancellationToken).Wait();
    }

    [Fact(DisplayName = "Request received is valid then point is created")]
    public async Task Request_received_is_valid_then_point_is_created()
    {
        //Arrange
        var request = new Faker<CreateRequest>().StrictMode(true)
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.NumberOfPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url())
           .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
           .Generate();

        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        //Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var pointEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<PointEntity>(GetByIdQuery, response!.Data, GetCancellationToken);

        pointEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and point exists then point is updated")]
    public async Task Request_received_is_valid_and_point_exists_then_point_is_updated()
    {
        //Arrange
        var entity = new Faker<PointEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.NumberOfPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url())
           .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
           .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
           .RuleFor(property => property.Id, setter => entity.Id)
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.NumberOfPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url())
           .Generate();

        //Act
        var rawResponse = await _mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newPointEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<PointEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        newPointEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and point exists then schedule is deleted")]
    public async Task Request_received_is_valid_and_point_exists_then_point_is_deleted()
    {
        //Arrange
        var entity = new Faker<PointEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.NumberOfPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url())
           .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
           .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var pointEntity = await _mainFixture.PostgreSqlFixture
            .GetByIdAsync<PointEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        pointEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Point id received is valid and point is returned")]
    public async Task Point_id_received_is_valid_and_point_is_returned()
    {
        //Arrange
        var entity = new Faker<PointEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.NumberOfPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url())
           .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
            .Generate();

        await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<PointEntity>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and points are returned")]
    public async Task Request_received_is_valid_and_points_are_returned()
    {
        //Arrange
        var numberOfItemsToCreate = 10;
        var entities = new Faker<PointEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.NumberOfPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url())
           .RuleFor(property => property.DomainId, setter => _domainEntity.Id)
           .Generate(numberOfItemsToCreate);

        foreach (var entity in entities)
        {
            await _mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);
        }

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<PointEntity>>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data!.Count().Should().BeGreaterThanOrEqualTo(entities.Count);
    }
}