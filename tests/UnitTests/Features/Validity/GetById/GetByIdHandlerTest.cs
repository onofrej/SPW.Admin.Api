using SPW.Admin.Api.Features.Validity;
using SPW.Admin.Api.Features.Validity.GetById;

namespace SPW.Admin.UnitTests.Features.Validity.GetById;

public class GetByIdHandlerTest
{
    private readonly Mock<IValidityData> _validityDataMock;
    private readonly Mock<IValidator<GetByIdQuery>> _validatorMock;
    private readonly GetByIdHandler _handler;

    public GetByIdHandlerTest()
    {
        _validityDataMock = new Mock<IValidityData>();
        _validatorMock = new Mock<IValidator<GetByIdQuery>>();
        _handler = new GetByIdHandler(_validityDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithValidity()
    {
        // Arrange
        var request = new ValidityEntity
        {
            Id = Guid.NewGuid(),
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(15),
            Status = true,
            DomainId = Guid.NewGuid(),
        };
        var expectedResultId = request.Id;
        var cancellationToken = CancellationToken.None;

        _validatorMock.Setup(v => v.Validate(It.IsAny<GetByIdQuery>())).Returns(new ValidationResult());

        _validityDataMock.Setup(c => c.GetValidityByIdAsync(request.Id, cancellationToken)).ReturnsAsync(request);

        //Act
        var handler = new GetByIdHandler(_validityDataMock.Object, _validatorMock.Object);

        var result = await handler.Handle(new GetByIdQuery(request.Id), cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data!.Id.Should().Be(expectedResultId);

        _validityDataMock.Verify(expression => expression.GetValidityByIdAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}