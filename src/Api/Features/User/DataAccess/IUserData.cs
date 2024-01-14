namespace SPW.Admin.Api.Features.User.DataAccess;

internal interface IUserData
{
    Task InsertAsync(UserEntity userEntity, CancellationToken cancellationToken);

    Task UpdateAsync(UserEntity userEntity, CancellationToken cancellationToken);

    Task DeleteAsync(UserEntity userEntity, CancellationToken cancellationToken);

    Task<UserEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<UserEntity>> GetAllAsync(CancellationToken cancellationToken);
}