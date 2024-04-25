using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Features.Circuit.Update;

namespace SPW.Admin.UnitTests.Features.Circuit.Update;

public class UpdateHandlerTest
{
    private readonly Mock<ICircuitData> _circuitDataMock;
    private readonly Mock<IValidator<UpdateCommand>> _validatorMock;
    private readonly UpdateHandler _handler;

    public UpdateHandlerTest()
    {
        _circuitDataMock = new Mock<ICircuitData>();
        _validatorMock = new Mock<IValidator<UpdateCommand>>();
        _handler = new UpdateHandler(_circuitDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithGuid()
    {
        // Arrange
        var request = new UpdateCommand();
        var expectedId = Guid.NewGuid();
        var validationResult = new ValidationResult();

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        _circuitDataMock.Setup(expression => expression.UpdateCircuitAsync(It.IsAny<CircuitEntity>(), It.IsAny<CancellationToken>()))
            .Callback<CircuitEntity, CancellationToken>((entity, _) => entity.Id = expectedId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data.Should().Be(expectedId);

        _circuitDataMock.Verify(expression => expression.UpdateCircuitAsync(It.IsAny<CircuitEntity>(),
                    It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange
        var request = new UpdateCommand();
        var validationErrorMessage = "Validation error message";
        var validationResult = new ValidationResult(new List<ValidationFailure> { new(default, validationErrorMessage, default) });

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(Errors.ReturnInvalidEntriesError(validationErrorMessage));

        _circuitDataMock.Verify(expression => expression.UpdateCircuitAsync(It.IsAny<CircuitEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}