using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.GetAll;

[ExcludeFromCodeCoverage]
internal sealed class GetAllHandler(IScheduleData scheduleData) :
    IRequestHandler<GetAllQuery, Result<IEnumerable<ScheduleEntity>>>
{
    public async Task<Result<IEnumerable<ScheduleEntity>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return new Result<IEnumerable<ScheduleEntity>>(await scheduleData.GetAllSchedulesAsync(cancellationToken));
    }
}