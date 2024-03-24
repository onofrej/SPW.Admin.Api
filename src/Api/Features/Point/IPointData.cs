namespace SPW.Admin.Api.Features.Point;

internal interface IPointData
{
    Task InsertAsync(PointEntity pointEntity, CancellationToken cancellationToken);

    Task UpdateAsync(PointEntity pointEntity, CancellationToken cancellationToken);

    Task DeleteAsync(PointEntity pointEntity, CancellationToken cancellationToken);

    Task<PointEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<PointEntity>> GetAllAsync(CancellationToken cancellationToken);
}