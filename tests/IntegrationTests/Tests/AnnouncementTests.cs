using SPW.Admin.Api.Features.Announcement;
using SPW.Admin.Api.Features.Announcement.Create;
using SPW.Admin.Api.Features.Announcement.Update;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class AnnouncementTests : BaseIntegratedTest
{
    private const string RequestUri = "/announcements";
    private const string HashKey = "id";
    private readonly MainFixture _mainFixture;

    public AnnouncementTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
    }

    [Fact(DisplayName = "Request received is valid then announcement is created")]
    public async Task Request_received_is_valid_then_announcement_is_created()
    {
        ////Arrange
        //var request = new Faker<CreateRequest>().StrictMode(true)
        //   .RuleFor(property => property.Title, setter => string.Join(" ", setter.Lorem.Words(3)))
        //   .RuleFor(property => property.Message, setter => string.Join(" ", setter.Lorem.Words(3)))
        //   .Generate();

        //var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        ////Act
        //var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        ////Assert
        //rawResponse.Should().NotBeNull();
        //rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        //var announcementEntity = await _mainFixture.DynamoDbFixture
        //    .ReadAsync<AnnouncementEntity>(HashKey, response!.Data.ToString());

        //announcementEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and announcement exists then announcement is updated")]
    public async Task Request_received_is_valid_and_announcement_exists_then_announcement_is_updated()
    {
        ////Arrange
        //var faker = new Faker<AnnouncementEntity>().StrictMode(true)
        //   .RuleFor(property => property.Id, setter => Guid.NewGuid())
        //   .RuleFor(property => property.Title, setter => string.Join(" ", setter.Lorem.Words(3)))
        //   .RuleFor(property => property.Message, setter => string.Join(" ", setter.Lorem.Words(3)));

        //var entity = faker.Generate();

        //await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        //var request = new Faker<UpdateRequest>().StrictMode(true)
        //    .RuleFor(property => property.Id, setter => entity.Id)
        //    .RuleFor(property => property.Title, setter => string.Join(" ", setter.Lorem.Words(3)))
        //    .RuleFor(property => property.Message, setter => string.Join(" ", setter.Lorem.Words(3)))
        //    .Generate();

        ////Act
        //var rawResponse = await _mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        //var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        ////Assert
        //rawResponse.Should().NotBeNull();
        //rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        //var newAnnouncementEntity = await _mainFixture.DynamoDbFixture
        //    .ReadAsync<AnnouncementEntity>(HashKey, response!.Data.ToString());

        //newAnnouncementEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and announcement exists then announcement is deleted")]
    public async Task Request_received_is_valid_and_announcement_exists_then_announcement_is_deleted()
    {
        ////Arrange
        //var entity = new Faker<AnnouncementEntity>().StrictMode(true)
        //   .RuleFor(property => property.Id, setter => Guid.NewGuid())
        //   .RuleFor(property => property.Title, setter => string.Join(" ", setter.Lorem.Words(3)))
        //   .RuleFor(property => property.Message, setter => string.Join(" ", setter.Lorem.Words(3)))
        //   .Generate();

        //await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        ////Act
        //var rawResponse = await _mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        ////Assert
        //rawResponse.Should().NotBeNull();
        //rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        //var announcementEntity = await _mainFixture.DynamoDbFixture
        //    .ReadAsync<AnnouncementEntity>(HashKey, entity.ToString());

        //announcementEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Announcement id received is valid and announcement is returned")]
    public async Task Announcement_id_received_is_valid_and_announcement_is_returned()
    {
        ////Arrange
        //var entity = new Faker<AnnouncementEntity>().StrictMode(true)
        //   .RuleFor(property => property.Id, setter => Guid.NewGuid())
        //   .RuleFor(property => property.Title, setter => string.Join(" ", setter.Lorem.Words(3)))
        //   .RuleFor(property => property.Message, setter => string.Join(" ", setter.Lorem.Words(3)))
        //   .Generate();

        //await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        ////Act
        //var rawResponse = await _mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        //var response = await rawResponse.Content.ReadFromJsonAsync<Response<AnnouncementEntity>>(GetCancellationToken);

        ////Assert
        //rawResponse.Should().NotBeNull();
        //rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        //entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and announcement are returned")]
    public async Task Request_received_is_valid_and_announcement_are_returned()
    {
        ////Arrange
        //var entity = new Faker<AnnouncementEntity>().StrictMode(true)
        //   .RuleFor(property => property.Id, setter => Guid.NewGuid())
        //   .RuleFor(property => property.Title, setter => string.Join(" ", setter.Lorem.Words(3)))
        //   .RuleFor(property => property.Message, setter => string.Join(" ", setter.Lorem.Words(3)))
        //   .Generate();

        //await _mainFixture.DynamoDbFixture.TruncateTableAsync(GetCancellationToken);

        //await _mainFixture.DynamoDbFixture.InsertAsync(entity, GetCancellationToken);

        ////Act
        //var rawResponse = await _mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        //var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<AnnouncementEntity>>>(GetCancellationToken);

        ////Assert
        //rawResponse.Should().NotBeNull();
        //rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        //response!.Data.Should().BeEquivalentTo(new List<AnnouncementEntity> { entity });
    }
}