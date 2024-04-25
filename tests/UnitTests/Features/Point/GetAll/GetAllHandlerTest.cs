using SPW.Admin.Api.Features.Point;
using SPW.Admin.Api.Features.Point.GetAll;

namespace SPW.Admin.UnitTests.Features.Point.GetAll;

public class GetAllHandlerTest
{
    private readonly Mock<IPointData> _pointDataMock;
    private readonly GetAllHandler _handler;

    public GetAllHandlerTest()
    {
        _pointDataMock = new Mock<IPointData>();
        _handler = new GetAllHandler(_pointDataMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithPoints()
    {
        // Arrange
        var request = new List<PointEntity>
        {
            new PointEntity {Id = Guid.NewGuid(), Name = "Point 1", NumberOfPublishers = 2, Address = "Point 1 Address", ImageUrl = "Point1Image",
            GoogleMapsUrl = "Point1GoogleMapsUrl", DomainId = Guid.NewGuid() },
            new PointEntity {Id = Guid.NewGuid(), Name = "Point 2", NumberOfPublishers = 2, Address = "Point 2 Address", ImageUrl = "Point2Image",
            GoogleMapsUrl = "Point2GoogleMapsUrl", DomainId = Guid.NewGuid() }
        };

        var getAllQuery = new GetAllQuery();
        var cancellationToken = CancellationToken.None;

        _pointDataMock.Setup(c => c.GetAllPointsAsync(cancellationToken)).ReturnsAsync(request);

        // Act
        var result = await _handler.Handle(getAllQuery, cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();

        var pointsEntities = result.Data!.ToList();
        pointsEntities.Should().NotBeNull();
        pointsEntities.Should().HaveCount(request.Count);

        foreach (var expectedPoints in request)
        {
            pointsEntities.Should().ContainEquivalentOf(expectedPoints);
        }

        _pointDataMock.Verify(c => c.GetAllPointsAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}