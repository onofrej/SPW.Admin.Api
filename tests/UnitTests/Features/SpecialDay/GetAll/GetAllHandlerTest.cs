using SPW.Admin.Api.Features.SpecialDay;
using SPW.Admin.Api.Features.SpecialDay.GetAll;

namespace SPW.Admin.UnitTests.Features.SpecialDay.GetAll;

public class GetAllHandlerTest
{
    private readonly Mock<ISpecialDayData> _specialDayDataMock;
    private readonly GetAllHandler _handler;

    public GetAllHandlerTest()
    {
        _specialDayDataMock = new Mock<ISpecialDayData>();
        _handler = new GetAllHandler(_specialDayDataMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithSpecialDays()
    {
        // Arrange
        var request = new List<SpecialDayEntity>
        {
            new SpecialDayEntity {Id = Guid.NewGuid(), Name = "Special Day 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2), CircuitId = Guid.NewGuid()},
            new SpecialDayEntity {Id = Guid.NewGuid(), Name = "Special Day 2", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2), CircuitId = Guid.NewGuid()},
        };

        var getAllQuery = new GetAllQuery();
        var cancellationToken = CancellationToken.None;

        _specialDayDataMock.Setup(c => c.GetAllSpecialDaysAsync(cancellationToken)).ReturnsAsync(request);

        // Act
        var result = await _handler.Handle(getAllQuery, cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();

        var specialDayEntities = result.Data!.ToList();
        specialDayEntities.Should().NotBeNull();
        specialDayEntities.Should().HaveCount(request.Count);

        foreach (var expectedSpecialDays in request)
        {
            specialDayEntities.Should().ContainEquivalentOf(expectedSpecialDays);
        }

        _specialDayDataMock.Verify(c => c.GetAllSpecialDaysAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}