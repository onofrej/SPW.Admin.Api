using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Features.Circuit.GetById;

namespace SPW.Admin.UnitTests.Features.Circuit.GetById;

public class GetByIdHandlerTest
{
    private readonly Mock<ICircuitData> _circuitDataMock;
    private readonly Mock<IValidator<GetByIdQuery>> _validatorMock;
    private readonly GetByIdHandler _handler;

    public GetByIdHandlerTest()
    {
        _circuitDataMock = new Mock<ICircuitData>();
        _validatorMock = new Mock<IValidator<GetByIdQuery>>();
        _handler = new GetByIdHandler(_circuitDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithCircuit()
    {
        // Arrange
        var request = new CircuitEntity
        {
            Id = Guid.NewGuid(),
            Name = "Circuit1",
            DomainId = Guid.NewGuid()
        };
        var expectedResultId = request.Id;
        var cancellationToken = CancellationToken.None;

        _validatorMock.Setup(v => v.Validate(It.IsAny<GetByIdQuery>())).Returns(new ValidationResult());

        _circuitDataMock.Setup(c => c.GetCircuitByIdAsync(request.Id, cancellationToken)).ReturnsAsync(request);

        var handler = new GetByIdHandler(_circuitDataMock.Object, _validatorMock.Object);

        var result = await handler.Handle(new GetByIdQuery(request.Id), cancellationToken);

        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data!.Id.Should().Be(expectedResultId);

        _circuitDataMock.Verify(expression => expression.GetCircuitByIdAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}