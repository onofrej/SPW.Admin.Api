namespace SPW.Admin.Api.DataAccess.User;

internal interface IUserData
{
    Task InsertAsync(UserEntity userEntity, CancellationToken cancellationToken);
}