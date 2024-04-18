using SPW.Admin.Api.Features.Announcement;
using SPW.Admin.Api.Features.Announcement.GetById;
using SPW.Admin.Api.Features.Circuit;

namespace SPW.Admin.UnitTests.Features.Announcement.GetById;

public class GetByIdHandlerTest
{
    private readonly Mock<IAnnouncementData> _announcementDataMock;
    private readonly Mock<IValidator<GetByIdQuery>> _validatorMock;
    private readonly GetByIdHandler _handler;

    public GetByIdHandlerTest()
    {
        _announcementDataMock = new Mock<IAnnouncementData>();
        _validatorMock = new Mock<IValidator<GetByIdQuery>>();
        _handler = new GetByIdHandler(_announcementDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithAnnouncement()
    {
        // Arrange
        var request = new AnnouncementEntity
        {
            Id = Guid.NewGuid(),
            Title = "Announcement Title",
            Message = "Announcement Message",
            DomainId = Guid.NewGuid()
        };
        var expectedResultId = request.Id;
        var cancellationToken = CancellationToken.None;

        _validatorMock.Setup(v => v.Validate(It.IsAny<GetByIdQuery>())).Returns(new ValidationResult());

        _announcementDataMock.Setup(c => c.GetAnnouncementByIdAsync(request.Id, cancellationToken)).ReturnsAsync(request);

        //Act
        var handler = new GetByIdHandler(_announcementDataMock.Object, _validatorMock.Object);

        var result = await handler.Handle(new GetByIdQuery(request.Id), cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data!.Id.Should().Be(expectedResultId);

        _announcementDataMock.Verify(expression => expression.GetAnnouncementByIdAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}