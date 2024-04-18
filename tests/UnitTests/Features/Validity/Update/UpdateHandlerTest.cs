using SPW.Admin.Api.Features.Validity;
using SPW.Admin.Api.Features.Validity.Update;

namespace SPW.Admin.UnitTests.Features.Validity.Update;

public class UpdateHandlerTest
{
    private readonly Mock<IValidityData> _validityDataMock;
    private readonly Mock<IValidator<UpdateCommand>> _validatorMock;
    private readonly UpdateHandler _handler;

    public UpdateHandlerTest()
    {
        _validityDataMock = new Mock<IValidityData>();
        _validatorMock = new Mock<IValidator<UpdateCommand>>();
        _handler = new UpdateHandler(_validityDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithGuid()
    {
        // Arrange
        var request = new UpdateCommand();
        var expectedId = Guid.NewGuid();
        var validationResult = new ValidationResult();

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        _validityDataMock.Setup(expression => expression.UpdateValidityAsync(It.IsAny<ValidityEntity>(), It.IsAny<CancellationToken>()))
            .Callback<ValidityEntity, CancellationToken>((entity, _) => entity.Id = expectedId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data.Should().Be(expectedId);

        _validityDataMock.Verify(expression => expression.UpdateValidityAsync(It.IsAny<ValidityEntity>(),
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

        _validityDataMock.Verify(expression => expression.UpdateValidityAsync(It.IsAny<ValidityEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}