namespace SPW.Admin.Api.Features.User;

internal interface IUserData
{
    Task<int> CreateUserAsync(UserEntity user, CancellationToken cancellationToken);

    Task<int> DeleteUserAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<UserEntity>> GetAllUsersAsync(CancellationToken cancellationToken);

    Task<UserEntity?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<int> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken);
}