using SPW.Admin.Api.Features.SpecialDay;
using SPW.Admin.Api.Features.Validity;
using SPW.Admin.Api.Features.Validity.GetAll;

namespace SPW.Admin.UnitTests.Features.Validity.GetAll;

public class GetAllHandlerTest
{
    private readonly Mock<IValidityData> _validityDayDataMock;
    private readonly GetAllHandler _handler;

    public GetAllHandlerTest()
    {
        _validityDayDataMock = new Mock<IValidityData>();
        _handler = new GetAllHandler(_validityDayDataMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithValidities()
    {
        // Arrange
        var request = new List<ValidityEntity>
        {
            new ValidityEntity {Id = Guid.NewGuid(), StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(15), Status = true, DomainId = Guid.NewGuid()},
            new ValidityEntity {Id = Guid.NewGuid(), StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(15), Status = false, DomainId = Guid.NewGuid()}
        };

        var getAllQuery = new GetAllQuery();
        var cancellationToken = CancellationToken.None;

        _validityDayDataMock.Setup(c => c.GetAllValiditiesAsync(cancellationToken)).ReturnsAsync(request);

        // Act
        var result = await _handler.Handle(getAllQuery, cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();

        var validityEntities = result.Data!.ToList();
        validityEntities.Should().NotBeNull();
        validityEntities.Should().HaveCount(request.Count);

        foreach (var expectedValidities in request)
        {
            validityEntities.Should().ContainEquivalentOf(expectedValidities);
        }

        _validityDayDataMock.Verify(c => c.GetAllValiditiesAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}