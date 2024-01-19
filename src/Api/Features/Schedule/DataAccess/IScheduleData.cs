namespace SPW.Admin.Api.Features.Schedule.DataAccess;

internal interface IScheduleData
{
    Task InsertAsync(ScheduleEntity scheduleEntity, CancellationToken cancellationToken);

    Task UpdateAsync(ScheduleEntity scheduleEntity, CancellationToken cancellationToken);

    Task DeleteAsync(ScheduleEntity scheduleEntity, CancellationToken cancellationToken);

    Task<ScheduleEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ScheduleEntity>> GetAllAsync(CancellationToken cancellationToken);
}