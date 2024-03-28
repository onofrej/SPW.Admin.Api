namespace SPW.Admin.Api.Features.Schedule;

internal interface IScheduleData
{
    Task<int> CreateScheduleAsync(ScheduleEntity entity, CancellationToken cancellationToken);

    Task<int> DeleteScheduleAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ScheduleEntity>> GetAllSchedulesAsync(CancellationToken cancellationToken);

    Task<ScheduleEntity?> GetScheduleByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<int> UpdateScheduleAsync(ScheduleEntity entity, CancellationToken cancellationToken);
}