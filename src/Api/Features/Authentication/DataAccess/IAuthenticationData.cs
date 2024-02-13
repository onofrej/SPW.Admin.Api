namespace SPW.Admin.Api.Features.Authentication.DataAccess;

internal interface IAuthenticationData
{
    Task RegisterAsync(AuthenticationEntity authenticationEntity, CancellationToken cancellationToken);
}