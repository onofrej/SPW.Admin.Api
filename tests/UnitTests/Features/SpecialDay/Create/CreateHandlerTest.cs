﻿using SPW.Admin.Api.Features.SpecialDay;
using SPW.Admin.Api.Features.SpecialDay.Create;

namespace SPW.Admin.UnitTests.Features.SpecialDay.Create;

public class CreateHandlerTest
{
    private readonly Mock<ISpecialDayData> _specialDayDataMock;
    private readonly Mock<IValidator<CreateCommand>> _validatorMock;
    private readonly CreateHandler _handler;

    public CreateHandlerTest()
    {
        _specialDayDataMock = new Mock<ISpecialDayData>();
        _validatorMock = new Mock<IValidator<CreateCommand>>();
        _handler = new CreateHandler(_specialDayDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithGuid()
    {
        // Arrange
        var request = new CreateCommand();
        var expectedId = Guid.NewGuid();
        var validationResult = new ValidationResult();

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        _specialDayDataMock.Setup(expression => expression.CreateSpecialDayAsync(It.IsAny<SpecialDayEntity>(), It.IsAny<CancellationToken>()))
            .Callback<SpecialDayEntity, CancellationToken>((entity, _) => entity.Id = expectedId);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data.Should().Be(expectedId);

        _specialDayDataMock.Verify(expression => expression.CreateSpecialDayAsync(It.IsAny<SpecialDayEntity>(),
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

        _specialDayDataMock.Verify(expression => expression.CreateSpecialDayAsync(It.IsAny<SpecialDayEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}