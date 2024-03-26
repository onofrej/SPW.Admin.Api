namespace SPW.Admin.Api.Features.User;

internal interface IUserData
{
    Task<int> CreateUserAsync(PointEntity user, CancellationToken cancellationToken);

    Task<int> DeleteUserAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<PointEntity>> GetAllUsersAsync(CancellationToken cancellationToken);

    Task<PointEntity?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<int> UpdateUserAsync(PointEntity user, CancellationToken cancellationToken);
}