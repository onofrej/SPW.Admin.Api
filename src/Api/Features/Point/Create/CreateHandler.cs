using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point.Create;

internal sealed class CreateHandler : IRequestHandler<CreateCommand, Result<Guid>>
{
    private readonly IPointData _pointData;
    private readonly IValidator<CreateCommand> _validator;

    public CreateHandler(IPointData pointData, IValidator<CreateCommand> validator)
    {
        _pointData = pointData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var ValidationResult = _validator.Validate(request);

        if (!ValidationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(ValidationResult.ToString()));
        }

        var entity = new PointEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            NumberOfPublishers = request.NumberOfPublishers,
            Address = request.Address,
            ImageUrl = request.ImageUrl,
            GoogleMapsUrl = request.GoogleMapsUrl,
            DomainId = request.DomainId
        };

        await _pointData.CreatePointAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}