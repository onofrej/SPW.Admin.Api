namespace SPW.Admin.Api.Features.SpecialDay.DataAccess;

internal interface ISpecialDayData
{
    Task InsertAsync(SpecialDayEntity specialDateEntity, CancellationToken cancellationToken);

    Task UpdateAsync(SpecialDayEntity specialDateEntity, CancellationToken cancellationToken);

    Task DeleteAsync(SpecialDayEntity specialDateEntity, CancellationToken cancellationToken);

    Task<SpecialDayEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<SpecialDayEntity>> GetAllAsync(CancellationToken cancellationToken);
}