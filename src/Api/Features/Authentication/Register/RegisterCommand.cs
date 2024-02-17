using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication.Register;

[ExcludeFromCodeCoverage]
public sealed class RegisterCommand : IRequest<Result<Guid>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}