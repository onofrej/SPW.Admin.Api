namespace SPW.Admin.Api.Features.Circuit;

internal interface ICircuitData
{
    Task<int> CreateCircuitAsync(CircuitEntity circuit, CancellationToken cancellationToken);

    Task<int> DeleteCircuitAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<CircuitEntity>> GetAllCircuitsAsync(CancellationToken cancellationToken);

    Task<CircuitEntity> GetCircuitByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<int> UpdateCircuitAsync(CircuitEntity circuit, CancellationToken cancellationToken);
}