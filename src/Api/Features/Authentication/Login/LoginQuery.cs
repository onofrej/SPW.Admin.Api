using SPW.Admin.Api.Features.Authentication.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication.Login;

public record LoginQuery(string Email, string Password) : IRequest<Result<AuthenticationEntity>>;