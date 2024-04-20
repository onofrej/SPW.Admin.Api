﻿using SPW.Admin.Api.Features.Circuit;
using SPW.Admin.Api.Features.Circuit.GetAll;

namespace SPW.Admin.UnitTests.Features.Circuit.GetAll;

public class GetAllHandlerTests
{
    private readonly Mock<ICircuitData> _circuitDataMock;
    private readonly GetAllHandler _handler;

    public GetAllHandlerTests()
    {
        _circuitDataMock = new Mock<ICircuitData>();
        _handler = new GetAllHandler(_circuitDataMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithAnnouncements()
    {
        // Arrange
        var request = new List<CircuitEntity>
        {
            new CircuitEntity {Id = Guid.NewGuid(), Name = "Circuit 1", DomainId  = Guid.NewGuid() },
            new CircuitEntity {Id = Guid.NewGuid(), Name = "Circuit 2", DomainId  = Guid.NewGuid() },
        };

        var getAllQuery = new GetAllQuery();
        var cancellationToken = CancellationToken.None;

        _circuitDataMock.Setup(c => c.GetAllCircuitsAsync(cancellationToken)).ReturnsAsync(request);

        // Act
        var result = await _handler.Handle(getAllQuery, cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();

        var circuitEntities = result.Data!.ToList();
        circuitEntities.Should().NotBeNull();
        circuitEntities.Should().HaveCount(request.Count);

        foreach (var expectedCircuits in request)
        {
            circuitEntities.Should().ContainEquivalentOf(expectedCircuits);
        }

        _circuitDataMock.Verify(c => c.GetAllCircuitsAsync(cancellationToken), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}