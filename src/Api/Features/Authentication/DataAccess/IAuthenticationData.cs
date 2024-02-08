namespace SPW.Admin.Api.Features.Authentication.DataAccess;

internal interface IAuthenticationData
{
    Task InsertAsync(AuthenticationEntity authenticationEntity, CancellationToken cancellationToken);
}