using SPW.Admin.IntegrationTests.Common;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

[Collection(TestCollection.CollectionDefinition)]
public class ValidityTests : BaseIntegratedTest
{
    private const string RequestUri = "/validities";
    private const string HashKey = "id";
    private const int DaysToAddAtDateValues = 10;
    private const bool DefaultValidityStatus = true;
    private readonly MainFixture _mainFixture;
    private readonly DateTime _startDate;
    private readonly DateTime _endDate;

    public ValidityTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
        _startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        _endDate = new DateTime(DateTime.Now.Year + DaysToAddAtDateValues, DateTime.Now.Month, DateTime.Now.Day);
    }

    //[Fact(DisplayName = "Request received is valid then validity is created")]
    //public async Task Request_received_is_valid_then_validity_is_created()
    //{
    //    //Arrange
    //    var request = new Faker<CreateRequest>().StrictMode(true)
    //       .RuleFor(property => property.StartDate, setter => _startDate)
    //       .RuleFor(property => property.EndDate, setter => _endDate)
    //       .RuleFor(property => property.Status, setter => DefaultValidityStatus)
    //       .Generate();

    //    var rawResponse = await _mainFixture.HttpClient.PostAsJsonAsync(RequestUri, request, GetCancellationToken);

    //    //Act
    //    var response = await rawResponse.Content.ReadFromJsonAsync<Response<Guid>>(GetCancellationToken);

    //    //Assert
    //    rawResponse.Should().NotBeNull();
    //    rawResponse.StatusCode.Should().Be(HttpStatusCode.Created);

    //    var validityEntity = await _mainFixture.DynamoDbFixture
    //        .ReadAsync<ValidityEntity>(HashKey, response!.Data.ToString());

    //    validityEntity.Should().BeEquivalentTo(request);
    //}
}