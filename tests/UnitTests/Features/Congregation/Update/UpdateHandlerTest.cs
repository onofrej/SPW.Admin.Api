using SPW.Admin.Api.Features.Congregation;
using SPW.Admin.Api.Features.Congregation.Update;

namespace SPW.Admin.UnitTests.Features.Congregation.Update;

public class UpdateHandlerTest
{
    private readonly Mock<ICongregationData> _congregationDataMock;
    private readonly Mock<IValidator<UpdateCommand>> _validatorMock;
    private readonly UpdateHandler _handler;

    public UpdateHandlerTest()
    {
        _congregationDataMock = new Mock<ICongregationData>();
        _validatorMock = new Mock<IValidator<UpdateCommand>>();
        _handler = new UpdateHandler(_congregationDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithGuid()
    {
        // Arrange
        var request = new UpdateCommand();
        var expectedId = Guid.NewGuid();
        var validationResult = new ValidationResult();

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        _congregationDataMock.Setup(expression => expression.UpdateCongregationAsync(It.IsAny<CongregationEntity>(), It.IsAny<CancellationToken>()))
            .Callback<CongregationEntity, CancellationToken>((entity, _) => entity.Id = expectedId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data.Should().Be(expectedId);

        _congregationDataMock.Verify(expression => expression.UpdateCongregationAsync(It.IsAny<CongregationEntity>(),
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

        _congregationDataMock.Verify(expression => expression.UpdateCongregationAsync(It.IsAny<CongregationEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}