using SPW.Admin.Api.Features.Holiday.Create;
using SPW.Admin.Api.Features.Holiday.DataAccess;
using SPW.Admin.Api.Features.Holiday.Update;
using SPW.Admin.Api.Features.Holiday.GetAll;
using SPW.Admin.Api.Shared.Models;
using SPW.Admin.IntegrationTests.Fixtures;

namespace SPW.Admin.IntegrationTests.Tests;

public class HolidayTests : BaseIntegratedTest, IClassFixture<MainFixture>
{
    private const string RequestUri = "/holidays";
    private const string HashKey = "id";

    private readonly MainFixture _mainFixture;
    private readonly DateTime _mockHolidayDate;

    public HolidayTests(MainFixture mainFixture)
    {
        _mainFixture = mainFixture;
        _mockHolidayDate = DateTime.Now.Date;
    }

    [Fact(DisplayName = "GetAll")]
    public async Task GetAll()
    {
        // Act
        var rawResponse = _mainFixture.HttpClient.GetFromJsonAsAsyncEnumerable<HolidayEntity>(RequestUri, GetCancellationToken);

        // Assert
        rawResponse.Should().NotBeNull();
        rawResponse.Should().Be(HttpStatusCode.OK);
    }
}

