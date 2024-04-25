using SPW.Admin.Api.Features.SpecialDay;
using SPW.Admin.Api.Features.SpecialDay.GetById;

namespace SPW.Admin.UnitTests.Features.SpecialDay.GetById;

public class GetByIdHandlerTest
{
    private readonly Mock<ISpecialDayData> _specialDayDataMock;
    private readonly Mock<IValidator<GetByIdQuery>> _validatorMock;
    private readonly GetByIdHandler _handler;

    public GetByIdHandlerTest()
    {
        _specialDayDataMock = new Mock<ISpecialDayData>();
        _validatorMock = new Mock<IValidator<GetByIdQuery>>();
        _handler = new GetByIdHandler(_specialDayDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithSpecialDay()
    {
        // Arrange
        var request = new SpecialDayEntity
        {
            Id = Guid.NewGuid(),
            Name = "Special Day 1",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(2),
            CircuitId = Guid.NewGuid()
        };
        var expectedResultId = request.Id;
        var cancellationToken = CancellationToken.None;

        _validatorMock.Setup(v => v.Validate(It.IsAny<GetByIdQuery>())).Returns(new ValidationResult());

        _specialDayDataMock.Setup(c => c.GetSpecialDayByIdAsync(request.Id, cancellationToken)).ReturnsAsync(request);

        //Act
        var handler = new GetByIdHandler(_specialDayDataMock.Object, _validatorMock.Object);

        var result = await handler.Handle(new GetByIdQuery(request.Id), cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data!.Id.Should().Be(expectedResultId);

        _specialDayDataMock.Verify(expression => expression.GetSpecialDayByIdAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}