namespace SPW.Admin.Api.Features.Authentication.Register;

[ExcludeFromCodeCoverage]
public sealed class AuthenticationRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}