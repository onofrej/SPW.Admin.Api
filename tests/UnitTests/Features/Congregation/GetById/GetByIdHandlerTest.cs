using SPW.Admin.Api.Features.Congregation;
using SPW.Admin.Api.Features.Congregation.GetById;

namespace SPW.Admin.UnitTests.Features.Congregation.GetById;

public class GetByIdHandlerTest
{
    private readonly Mock<ICongregationData> _congregationDataMock;
    private readonly Mock<IValidator<GetByIdQuery>> _validatorMock;
    private readonly GetByIdHandler _handler;

    public GetByIdHandlerTest()
    {
        _congregationDataMock = new Mock<ICongregationData>();
        _validatorMock = new Mock<IValidator<GetByIdQuery>>();
        _handler = new GetByIdHandler(_congregationDataMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ReturnResultWithCongregation()
    {
        // Arrange
        var request = new CongregationEntity
        {
            Id = Guid.NewGuid(),
            Name = "Congregation 1",
            Number = "0001",
            CircuitId = Guid.NewGuid(),
        };
        var expectedResultId = request.Id;
        var cancellationToken = CancellationToken.None;

        _validatorMock.Setup(v => v.Validate(It.IsAny<GetByIdQuery>())).Returns(new ValidationResult());

        _congregationDataMock.Setup(c => c.GetCongregationByIdAsync(request.Id, cancellationToken)).ReturnsAsync(request);

        //Act
        var handler = new GetByIdHandler(_congregationDataMock.Object, _validatorMock.Object);

        var result = await handler.Handle(new GetByIdQuery(request.Id), cancellationToken);

        //Assert
        result.Should().NotBeNull();
        result.HasFailed.Should().BeFalse();
        result.Data!.Id.Should().Be(expectedResultId);

        _congregationDataMock.Verify(expression => expression.GetCongregationByIdAsync(request.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidRequest_ReturnsResultWithError()
    {
        // Arrange

        // Act

        // Assert
    }
}