using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDay.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<SpecialDayEntity>>>
{
    private readonly ISpecialDayData _specialDayData;

    public GetAllHandler(ISpecialDayData specialDayData)
    {
        _specialDayData = specialDayData;
    }

    public async Task<Result<IEnumerable<SpecialDayEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<SpecialDayEntity>>(await _specialDayData.GetAllAsync(cancellationToken));
    }
}