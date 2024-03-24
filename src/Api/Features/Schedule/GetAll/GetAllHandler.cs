using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler : IRequestHandler<GetAllQuery, Result<IEnumerable<ScheduleEntity>>>
{
    private readonly IScheduleData _scheduleData;

    public GetAllHandler(IScheduleData scheduleData)
    {
        _scheduleData = scheduleData;
    }

    public async Task<Result<IEnumerable<ScheduleEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<ScheduleEntity>>(await _scheduleData.GetAllAsync(cancellationToken));
    }
}