namespace SPW.Admin.UnitTests.Features.User;

public class CreateHandlerTests
{
    //[Fact]
    //public async Task Handle_ValidCommand_ReturnsResultWithGuid()
    //{
    //    // Arrange
    //    var userDataMock = new Mock<IUserData>();
    //    var validatorMock = new Mock<IValidator<CreateCommand>>();

    //    validatorMock.Setup(v => v.Validate(It.IsAny<CreateCommand>()))
    //        .Returns(new ValidationResult());

    //    var handler = new CreateHandler(userDataMock.Object, validatorMock.Object);
    //    var createCommand = new CreateCommand
    //    {
    //        // Set valid properties for the test
    //    };

    //    // Act
    //    var result = await handler.Handle(createCommand, CancellationToken.None);

    //    // Assert
    //    result.Should().NotBeNull();

    //    userDataMock.Verify(u => u.InsertAsync(It.IsAny<UserEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    //}

    //[Fact]
    //public async Task Handle_InvalidCommand_ReturnsResultWithError()
    //{
    //    // Arrange
    //    var userDataMock = new Mock<IUserData>();
    //    var validatorMock = new Mock<IValidator<CreateCommand>>();

    //    var validationErrors = new ValidationResult();
    //    validationErrors.Errors.Add(new ValidationFailure("PropertyName", "Error message"));

    //    validatorMock.Setup(v => v.Validate(It.IsAny<CreateCommand>()))
    //        .Returns(validationErrors);

    //    var handler = new CreateHandler(userDataMock.Object, validatorMock.Object);
    //    var createCommand = new CreateCommand
    //    {
    //        // Set invalid properties for the test
    //    };

    //    // Act
    //    var result = await handler.Handle(createCommand, CancellationToken.None);

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.IsSuccess.Should().BeFalse();
    //    result.Value.Should().Be(Guid.Empty);
    //    result.Errors.Should().Contain("Error message"); // Adjust this based on your actual error handling logic
    //}
}