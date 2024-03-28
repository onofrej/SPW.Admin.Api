using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<CongregationEntity>>>
{
    private readonly ICongregationData _congregationData;

    public GetAllHandler(ICongregationData congregationData)
    {
        _congregationData = congregationData;
    }

    public async Task<Result<IEnumerable<CongregationEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<CongregationEntity>>(await _congregationData.GetAllCongregationsAsync(cancellationToken));
    }
}