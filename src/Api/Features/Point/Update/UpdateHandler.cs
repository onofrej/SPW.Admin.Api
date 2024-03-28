using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point.Update;

internal sealed class UpdateHandler : IRequestHandler<UpdateCommand, Result<Guid>>
{
    private readonly IPointData _pointData;
    private readonly IValidator<UpdateCommand> _validator;

    public UpdateHandler(IPointData pointData, IValidator<UpdateCommand> validator)
    {
        _pointData = pointData;
        _validator = validator;
    }

    public async Task<Result<Guid>> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<Guid>(Guid.Empty,
                Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var entity = new PointEntity
        {
            Id = request.Id,
            Name = request.Name,
            NumberOfPublishers = request.NumberOfPublishers,
            Address = request.Address,
            ImageUrl = request.ImageUrl,
            GoogleMapsUrl = request.GoogleMapsUrl,
        };

        await _pointData.UpdatePointAsync(entity, cancellationToken);

        return new Result<Guid>(entity.Id);
    }
}