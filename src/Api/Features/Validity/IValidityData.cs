namespace SPW.Admin.Api.Features.Validity;

internal interface IValidityData
{
    Task InsertAsync(ValidityEntity validityEntity, CancellationToken cancellationToken);

    Task UpdateAsync(ValidityEntity validityEntity, CancellationToken cancellationToken);

    Task DeleteAsync(ValidityEntity validityEntity, CancellationToken cancellationToken);

    Task<ValidityEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ValidityEntity>> GetAllAsync(CancellationToken cancellationToken);
}