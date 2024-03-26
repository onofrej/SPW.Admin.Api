namespace SPW.Admin.Api.Features.Circuit;

internal interface ICircuitData
{
    Task InsertAsync(CircuitEntity circuitEntity, CancellationToken cancellationToken);

    Task UpdateAsync(CircuitEntity circuitEntity, CancellationToken cancellationToken);

    Task DeleteAsync(CircuitEntity circuitEntity, CancellationToken cancellationToken);

    Task<CircuitEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<CircuitEntity>> GetAllAsync(CancellationToken cancellationToken);
}