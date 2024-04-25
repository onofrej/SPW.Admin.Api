using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Features.Circuit.Create;

namespace SPW.Admin.UnitTests.Features.Circuit.Create;

public class CreateHandlerTest
{
    private readonly Mock<ICircuitData> _circuitDataMock;
    private readonly Mock<IValidator<CreateCommand>> _validatorMock;
    private readonly CreateHandler _handler;

    public CreateHandlerTest()
    {
        _circuitDataMock = new Mock<ICircuitData>();
        _validatorMock = new Mock<IValidator<CreateCommand>>();
        _handler = new CreateHandler(_circuitDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithGuid()
    {
        // Arrange
        var request = new CreateCommand();
        var expectedId = Guid.NewGuid();
        var validationResult = new ValidationResult();

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        _circuitDataMock.Setup(expression => expression.CreateCircuitAsync(It.IsAny<CircuitEntity>(), It.IsAny<CancellationToken>()))
            .Callback<CircuitEntity, CancellationToken>((entity, _) => entity.Id = expectedId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data.Should().Be(expectedId);

        _circuitDataMock.Verify(expression => expression.CreateCircuitAsync(It.IsAny<CircuitEntity>(),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange
        var request = new CreateCommand();
        var validationErrorMessage = "Validation error message";
        var validationResult = new ValidationResult(new List<ValidationFailure> { new(default, validationErrorMessage, default) });

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(Errors.ReturnInvalidEntriesError(validationErrorMessage));

        _circuitDataMock.Verify(expression => expression.CreateCircuitAsync(It.IsAny<CircuitEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}