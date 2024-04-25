using SPW.Admin.Api.Features.Validity;
using SPW.Admin.Api.Features.Validity.Create;

namespace SPW.Admin.UnitTests.Features.Validity.Create;

public class CreasteHandlerTest
{
    private readonly Mock<IValidityData> _validityDataMock;
    private readonly Mock<IValidator<CreateCommand>> _validatorMock;
    private readonly CreateHandler _handler;

    public CreasteHandlerTest()
    {
        _validityDataMock = new Mock<IValidityData>();
        _validatorMock = new Mock<IValidator<CreateCommand>>();
        _handler = new CreateHandler(_validityDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithGuid()
    {
        // Arrange
        var request = new CreateCommand();
        var expectedId = Guid.NewGuid();
        var validationResult = new ValidationResult();

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        _validityDataMock.Setup(expression => expression.CreateValidityAsync(It.IsAny<ValidityEntity>(), It.IsAny<CancellationToken>()))
            .Callback<ValidityEntity, CancellationToken>((entity, _) => entity.Id = expectedId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data.Should().Be(expectedId);

        _validityDataMock.Verify(expression => expression.CreateValidityAsync(It.IsAny<ValidityEntity>(),
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

        _validityDataMock.Verify(expression => expression.CreateValidityAsync(It.IsAny<ValidityEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}