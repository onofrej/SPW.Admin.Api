namespace SPW.Admin.Api.Users.DataAccess;

internal interface IUserData
{
    Task InsertAsync(UserEntity userEntity, CancellationToken cancellationToken);
}