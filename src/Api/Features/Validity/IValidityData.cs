namespace SPW.Admin.Api.Features.Validity;

internal interface IValidityData
{
    Task<int> CreateValidityAsync(ValidityEntity validity, CancellationToken cancellationToken);

    Task<int> DeleteValidityAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<ValidityEntity>> GetAllValiditiesAsync(CancellationToken cancellationToken);

    Task<ValidityEntity> GetValidityByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<int> UpdateValidityAsync(ValidityEntity validity, CancellationToken cancellationToken);
}