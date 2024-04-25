namespace SPW.Admin.Api.Features.SpecialDay;

internal interface ISpecialDayData
{
    Task<int> CreateSpecialDayAsync(SpecialDayEntity specialDay, CancellationToken cancellationToken);

    Task<int> DeleteSpecialDayAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<SpecialDayEntity>> GetAllSpecialDaysAsync(CancellationToken cancellationToken);

    Task<SpecialDayEntity> GetSpecialDayByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<int> UpdateSpecialDayAsync(SpecialDayEntity specialDay, CancellationToken cancellationToken);
}