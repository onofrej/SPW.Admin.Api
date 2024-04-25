using SPW.Admin.Api.Features.Point;
using SPW.Admin.Api.Features.Point.GetById;

namespace SPW.Admin.UnitTests.Features.Point.GetById;

public class GetByIdHandlerTest
{
    private readonly Mock<IPointData> _pointDataMock;
    private readonly Mock<IValidator<GetByIdQuery>> _validatorMock;
    private readonly GetByIdHandler _handler;

    public GetByIdHandlerTest()
    {
        _pointDataMock = new Mock<IPointData>();
        _validatorMock = new Mock<IValidator<GetByIdQuery>>();
        _handler = new GetByIdHandler(_pointDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithPoint()
    {
        // Arrange
        var request = new PointEntity
        {
            Id = Guid.NewGuid(),
            Name = "Point 1",
            NumberOfPublishers = 2,
            Address = "Point 1 Address",
            ImageUrl = "Point1Image",
            GoogleMapsUrl = "Point1GoogleMapsUrl",
            DomainId = Guid.NewGuid()
        };
        var expectedResultId = request.Id;
        var cancellationToken = CancellationToken.None;

        _validatorMock.Setup(v => v.Validate(It.IsAny<GetByIdQuery>())).Returns(new ValidationResult());

        _pointDataMock.Setup(c => c.GetPointByIdAsync(request.Id, cancellationToken)).ReturnsAsync(request);

        //Act
        var handler = new GetByIdHandler(_pointDataMock.Object, _validatorMock.Object);

        var result = await handler.Handle(new GetByIdQuery(request.Id), cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data!.Id.Should().Be(expectedResultId);

        _pointDataMock.Verify(expression => expression.GetPointByIdAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}