namespace SPW.Admin.Api.Features.Point;

internal interface IPointData
{
    Task<int> CreatePointAsync(PointEntity point, CancellationToken cancellationToken);

    Task<int> DeletePointAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<PointEntity>> GetAllPointsAsync(CancellationToken cancellationToken);

    Task<PointEntity?> GetPointByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<int> UpdatePointAsync(PointEntity point, CancellationToken cancellationToken);
}