using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication.Register;

[ExcludeFromCodeCoverage]
public sealed class AuthenticationCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}