namespace SPW.Admin.Api.Features.Holiday.DataAccess;

internal interface IHolidayData
{
    Task InsertAsync(HolidayEntity holidayEntity, CancellationToken cancellationToken);

    Task UpdateAsync(HolidayEntity holidayEntity, CancellationToken cancellationToken);

    Task DeleteAsync(HolidayEntity holidayEntity, CancellationToken cancellationToken);

    Task<HolidayEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<HolidayEntity>> GetAllAsync(CancellationToken cancellationToken);
}