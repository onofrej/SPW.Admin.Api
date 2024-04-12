namespace SPW.Admin.Api.Features.Holiday;

internal interface IHolidayData
{
    Task<int> CreateHolidayAsync(HolidayEntity entity, CancellationToken cancellationToken);

    Task<int> DeleteHolidayAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<HolidayEntity>> GetAllHolidayAsync(CancellationToken cancellationToken);

    Task<HolidayEntity> GetHolidayByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<int> UpdateHolidayAsync(HolidayEntity entity, CancellationToken cancellationToken);
}