using SPW.Admin.Api.Features.Point.Create;
using SPW.Admin.Api.Features.Point.DataAccess;
using SPW.Admin.Api.Features.Point.Update;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class PointTests : BaseIntegratedTest
{
    private const string RequestUri = "/points";
    private const string HashKey = "id";
    private readonly MainFixture _mainFixture;

    public PointTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
    }

    [Fact(DisplayName = "Request received is valid then point is created")]
    public async Task Request_received_is_valid_then_point_is_created()
    {
        //Arrange
        var request = new Faker<CreateRequest>().StrictMode(true)
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.QuantityPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url())
           .Generate();

        var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        //Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var pointEntity = await _mainFixture.DynamoDbFixture
            .ReadAsync<PointEntity>(HashKey, response!.Data.ToString());

        pointEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and point exists then point is updated")]
    public async Task Request_received_is_valid_and_point_exists_then_point_is_updated()
    {
        //Arrange
        var faker = new Faker<PointEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.QuantityPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url());

        var entity = faker.Generate();

        await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
            .RuleFor(property => property.Id, setter => entity.Id)
            .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
            .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
            .RuleFor(property => property.QuantityPublishers, setter => setter.Random.Number(3, 3))
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

        var newPointEntity = await _mainFixture.DynamoDbFixture
            .ReadAsync<PointEntity>(HashKey, response!.Data.ToString());

        newPointEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and point exists then point is deleted")]
    public async Task Request_received_is_valid_and_point_exists_then_point_is_deleted()
    {
        //Arrange
        var entity = new Faker<PointEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.QuantityPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url())
           .Generate();

        await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var pointEntity = await _mainFixture.DynamoDbFixture
            .ReadAsync<PointEntity>(HashKey, entity.ToString());

        pointEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Point id received is valid and point is returned")]
    public async Task Point_id_received_is_valid_and_point_is_returned()
    {
        //Arrange
        var entity = new Faker<PointEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.QuantityPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url())
           .Generate();

        await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<PointEntity>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and point are returned")]
    public async Task Request_received_is_valid_and_point_are_returned()
    {
        //Arrange
        var entity = new Faker<PointEntity>().StrictMode(true)
           .RuleFor(property => property.Id, setter => Guid.NewGuid())
           .RuleFor(property => property.Name, setter => string.Join(" ", setter.Lorem.Words(3)))
           .RuleFor(property => property.QuantityPublishers, setter => setter.Random.Number(3, 3))
           .RuleFor(property => property.Address, setter => setter.Address.StreetAddress())
           .RuleFor(property => property.ImageUrl, setter => setter.Image.PicsumUrl())
           .RuleFor(property => property.GoogleMapsUrl, setter => setter.Internet.Url()).Generate();

        await _mainFixture.DynamoDbFixture.TruncateTableAsync(GetCancellationToken);

        await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        //Act
        var rawResponse = await _mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<PointEntity>>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data.Should().BeEquivalentTo(new List<PointEntity> { entity });
    }
}