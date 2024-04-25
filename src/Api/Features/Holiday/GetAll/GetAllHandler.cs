using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<HolidayEntity>>>
{
    private readonly IHolidayData _holidayData;

    public GetAllHandler(IHolidayData holidayData)
    {
        _holidayData = holidayData;
    }

    public async Task<Result<IEnumerable<HolidayEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<HolidayEntity>>(await _holidayData.GetAllHolidayAsync(cancellationToken));
    }
}