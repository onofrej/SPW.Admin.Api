using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<PointEntity>>>
{
    private readonly IPointData _pointData;

    public GetAllHandler(IPointData pointData)

    {
        _pointData = pointData;
    }

    public async Task<Result<IEnumerable<PointEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<PointEntity>>(await _pointData.GetAllAsync(cancellationToken));
    }
}