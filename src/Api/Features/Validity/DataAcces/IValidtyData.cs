namespace SPW.Admin.Api.Features.Validity.DataAcces;

internal interface IValidtyData
{
    Task InsertAsync(ValidityEntity validityEntity, CancellationToken cancellationToken);

    Task UpdateAsync(ValidityEntity validityEntity, CancellationToken cancellationToken);

    Task DeleteAsync(ValidityEntity validityEntity, CancellationToken cancellationToken);

    Task<ValidityEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ValidityEntity>> GetAllAsync(CancellationToken cancellationToken);
}