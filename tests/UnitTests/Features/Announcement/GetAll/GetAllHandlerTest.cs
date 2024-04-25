using SPW.Admin.Api.Features.Announcement;
using SPW.Admin.Api.Features.Announcement.GetAll;

namespace SPW.Admin.UnitTests.Features.Announcement.GetAll;

public class GetAllHandlerTest
{
    private readonly Mock<IAnnouncementData> _announcementDataMock;
    private readonly GetAllHandler _handler;

    public GetAllHandlerTest()
    {
        _announcementDataMock = new Mock<IAnnouncementData>();
        _handler = new GetAllHandler(_announcementDataMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithCircuits()
    {
        // Arrange
        var request = new List<AnnouncementEntity>
        {
            new AnnouncementEntity {Id = Guid.NewGuid(), Title = "Announcement Title", Message = "Announcement Message", DomainId = Guid.NewGuid() }
        };

        var getAllQuery = new GetAllQuery();
        var cancellationToken = CancellationToken.None;

        _announcementDataMock.Setup(c => c.GetAllAnnouncementsAsync(cancellationToken)).ReturnsAsync(request);

        // Act
        var result = await _handler.Handle(getAllQuery, cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();

        var announcementEntities = result.Data!.ToList();
        announcementEntities.Should().NotBeNull();
        announcementEntities.Should().HaveCount(request.Count);

        foreach (var expectedAnnouncement in request)
        {
            announcementEntities.Should().ContainEquivalentOf(expectedAnnouncement);
        }

        _announcementDataMock.Verify(c => c.GetAllAnnouncementsAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}