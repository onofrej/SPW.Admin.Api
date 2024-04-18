using SPW.Admin.Api.Features.Congregation;
using SPW.Admin.Api.Features.Congregation.Create;

namespace SPW.Admin.UnitTests.Features.Congregation.Create;

public class CreateHandlerTest
{
    private readonly Mock<ICongregationData> _congregationDataMock;
    private readonly Mock<IValidator<CreateCommand>> _validatorMock;
    private readonly CreateHandler _handler;

    public CreateHandlerTest()
    {
        _congregationDataMock = new Mock<ICongregationData>();
        _validatorMock = new Mock<IValidator<CreateCommand>>();
        _handler = new CreateHandler(_congregationDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithGuid()
    {
        // Arrange
        var request = new CreateCommand();
        var expectedId = Guid.NewGuid();
        var validationResult = new ValidationResult();

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        _congregationDataMock.Setup(expression => expression.CreateCongregationAsync(It.IsAny<CongregationEntity>(), It.IsAny<CancellationToken>()))
            .Callback<CongregationEntity, CancellationToken>((entity, _) => entity.Id = expectedId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data.Should().Be(expectedId);

        _congregationDataMock.Verify(expression => expression.CreateCongregationAsync(It.IsAny<CongregationEntity>(),
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

        _congregationDataMock.Verify(expression => expression.CreateCongregationAsync(It.IsAny<CongregationEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}