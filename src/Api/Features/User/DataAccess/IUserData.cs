namespace SPW.Admin.Api.Features.User.DataAccess;

internal interface IUserData
{
    Task InsertAsync(UserEntity userEntity, CancellationToken cancellationToken);

    Task InsertSingleItemAsync(UserEntity userEntity, CancellationToken cancellationToken);
}