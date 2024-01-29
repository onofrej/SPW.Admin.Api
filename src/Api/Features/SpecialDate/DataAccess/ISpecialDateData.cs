namespace SPW.Admin.Api.Features.SpecialDate.DataAccess;

internal interface ISpecialDateData
{
    Task InsertAsync(SpecialDateEntity specialDateEntity, CancellationToken cancellationToken);

    Task UpdateAsync(SpecialDateEntity specialDateEntity, CancellationToken cancellationToken);

    Task DeleteAsync(SpecialDateEntity specialDateEntity, CancellationToken cancellationToken);

    Task<SpecialDateEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<SpecialDateEntity>> GetAllAsync(CancellationToken cancellationToken);
}