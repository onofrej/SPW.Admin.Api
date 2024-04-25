namespace SPW.Admin.Api.Features.Domain;

internal interface IDomainData
{
    Task<int> CreateDomainAsync(DomainEntity entity, CancellationToken cancellationToken);

    Task<int> DeleteDomainAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<DomainEntity>> GetAllDomainsAsync(CancellationToken cancellationToken);

    Task<DomainEntity?> GetDomainByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<int> UpdateDomainAsync(DomainEntity entity, CancellationToken cancellationToken);
}