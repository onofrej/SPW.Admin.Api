namespace SPW.Admin.Api.Features.Users.DataAccess;

internal interface IUserData
{
    Task InsertAsync(UserEntity userEntity, CancellationToken cancellationToken);
}