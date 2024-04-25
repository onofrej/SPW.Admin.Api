using SPW.Admin.Api.Features.Announcement;
using SPW.Admin.Api.Features.Announcement.Delete;

namespace SPW.Admin.UnitTests.Features.Announcement.Delete;

public class DeleteHandlerTest
{
    private readonly Mock<IAnnouncementData> _announcementDataMock;
    private readonly Mock<IValidator<DeleteCommand>> _validatorMock;
    private readonly DeleteHandler _handler;

    public DeleteHandlerTest()
    {
        _announcementDataMock = new Mock<IAnnouncementData>();
        _validatorMock = new Mock<IValidator<DeleteCommand>>();
        _handler = new DeleteHandler(_announcementDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithGuid()
    {
        // Arrange
        var request = new DeleteCommand
        {
            Id = Guid.NewGuid()
        };
        var expectedResultId = request.Id;
        var validationResult = new ValidationResult();

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        _announcementDataMock.Setup(expression => expression.GetAnnouncementByIdAsync(request.Id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new AnnouncementEntity { Id = request.Id });

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data.Should().Be(expectedResultId);

        _announcementDataMock.Verify(expression => expression.DeleteAnnouncementAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange
        var request = new DeleteCommand();
        var validationErrorMessage = "Validation error message";
        var validationResult = new ValidationResult(new List<ValidationFailure> { new(default, validationErrorMessage, default) });

        _validatorMock.Setup(expression => expression.Validate(request)).Returns(validationResult);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeTrue();
        result.Error.Should().BeEquivalentTo(Errors.ReturnInvalidEntriesError(validationErrorMessage));

        _announcementDataMock.Verify(expression => expression.DeleteAnnouncementAsync(Guid.Empty, It.IsAny<CancellationToken>()), Times.Never);
    }
}