using SPW.Admin.Api.Features.Congregation;
using SPW.Admin.Api.Features.Congregation.GetAll;

namespace SPW.Admin.UnitTests.Features.Congregation.GetAll;

public class GetAllHandlerTest
{
    private readonly Mock<ICongregationData> _congregationDataMock;
    private readonly GetAllHandler _handler;

    public GetAllHandlerTest()
    {
        _congregationDataMock = new Mock<ICongregationData>();
        _handler = new GetAllHandler(_congregationDataMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithCongregations()
    {
        // Arrange
        var request = new List<CongregationEntity>
        {
            new CongregationEntity {Id = Guid.NewGuid(), Name = "Congregation 1", Number = "0001", CircuitId = Guid.NewGuid()},
            new CongregationEntity {Id = Guid.NewGuid(), Name = "Congregation 2", Number = "0002", CircuitId = Guid.NewGuid()},
        };

        var getAllQuery = new GetAllQuery();
        var cancellationToken = CancellationToken.None;

        _congregationDataMock.Setup(c => c.GetAllCongregationsAsync(cancellationToken)).ReturnsAsync(request);

        // Act
        var result = await _handler.Handle(getAllQuery, cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();

        var congregationEntities = result.Data!.ToList();
        congregationEntities.Should().NotBeNull();
        congregationEntities.Should().HaveCount(request.Count);

        foreach (var expectedCongregations in request)
        {
            congregationEntities.Should().ContainEquivalentOf(expectedCongregations);
        }

        _congregationDataMock.Verify(c => c.GetAllCongregationsAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}