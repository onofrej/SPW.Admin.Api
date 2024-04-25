using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point.GetById;

[ExcludeFromCodeCoverage]
internal sealed class GetByIdHandler : IRequestHandler<GetByIdQuery, Result<PointEntity>>
{
    private readonly IPointData _pointData;
    private readonly IValidator<GetByIdQuery> _validator;

    public GetByIdHandler(IPointData pointData, IValidator<GetByIdQuery> validator)
    {
        _pointData = pointData;
        _validator = validator;
    }

    public async Task<Result<PointEntity>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return new Result<PointEntity>(default, Errors.ReturnInvalidEntriesError(validationResult.ToString()));
        }

        var pointEntity = await _pointData.GetPointByIdAsync(request.Id, cancellationToken);

        if (pointEntity is null)
        {
            return new Result<PointEntity>(default, Errors.ReturnPointNotFoundError());
        }

        return new Result<PointEntity>(pointEntity);
    }
}