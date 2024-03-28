using SPW.Admin.Api.Features.Domain;
using SPW.Admin.Api.Features.Domain.Create;
using SPW.Admin.Api.Features.Domain.Update;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class DomainTests(MainFixture mainFixture) : BaseIntegratedTest
{
    internal const string GetByIdQuery = "SELECT * FROM \"domain\" WHERE id = @Id";
    internal const string InsertQuery = @"INSERT INTO ""domain"" (id, name) VALUES (@Id, @Name)";
    private const string RequestUri = "/domains";

    [Fact(DisplayName = "Request received is valid then domain is created")]
    public async Task Request_received_is_valid_then_domain_is_created()
    {
        //Arrange
        var request = new Faker<CreateRequest>().StrictMode(true)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .Generate();

        var rawResponse = await mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

        //Act
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

        var domainEntity = await mainFixture.PostgreSqlFixture
            .GetByIdAsync<DomainEntity>(GetByIdQuery, response!.Data, GetCancellationToken);

        domainEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and domain exists then domain is updated")]
    public async Task Request_received_is_valid_and_domain_exists_then_domain_is_updated()
    {
        //Arrange
        var entity = new Faker<DomainEntity>().StrictMode(true)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .Generate();

        await mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        var request = new Faker<UpdateRequest>().StrictMode(true)
            .RuleFor(property => property.Id, setter => entity.Id)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .Generate();

        //Act
        var rawResponse = await mainFixture.HttpClient.PutAsJsonAsync(RequestUri, request, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var newDomainEntity = await mainFixture.PostgreSqlFixture
            .GetByIdAsync<DomainEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        newDomainEntity.Should().BeEquivalentTo(request);
    }

    [Fact(DisplayName = "Request received is valid and domain exists then domain is deleted")]
    public async Task Request_received_is_valid_and_domain_exists_then_domain_is_deleted()
    {
        //Arrange
        var entity = new Faker<DomainEntity>().StrictMode(true)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .Generate();

        await mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await mainFixture.HttpClient.DeleteAsync($"{RequestUri}/{entity.Id}");

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var domainEntity = await mainFixture.PostgreSqlFixture
            .GetByIdAsync<DomainEntity>(GetByIdQuery, entity.Id, GetCancellationToken);

        domainEntity.Should().BeNull();
    }

    [Fact(DisplayName = "Domain id received is valid and domain is returned")]
    public async Task Domain_id_received_is_valid_and_domain_is_returned()
    {
        //Arrange
        var entity = new Faker<DomainEntity>().StrictMode(true)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .Generate();

        await mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);

        //Act
        var rawResponse = await mainFixture.HttpClient.GetAsync($"{RequestUri}/{entity.Id}", GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<DomainEntity>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        entity.Should().BeEquivalentTo(response!.Data);
    }

    [Fact(DisplayName = "Request received is valid and domains are returned")]
    public async Task Request_received_is_valid_and_domains_are_returned()
    {
        //Arrange
        var numberOfItemsToCreate = 10;
        var entities = new Faker<DomainEntity>().StrictMode(true)
            .RuleFor(property => property.Name, setter => setter.Company.CompanyName())
            .RuleFor(property => property.Id, setter => Guid.NewGuid())
            .Generate(numberOfItemsToCreate);

        foreach (var entity in entities)
        {
            await mainFixture.PostgreSqlFixture.CreateAsync(entity, InsertQuery, GetCancellationToken);
        }

        //Act
        var rawResponse = await mainFixture.HttpClient.GetAsync(RequestUri, GetCancellationToken);
        var response = await rawResponse.Content.ReadFromJsonAsync<Response<IEnumerable<DomainEntity>>>(GetCancellationToken);

        //Assert
        rawResponse.Should().NotBeNull();
        rawResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        response!.Data!.Count().Should().BeGreaterThanOrEqualTo(entities.Count);
    }
}