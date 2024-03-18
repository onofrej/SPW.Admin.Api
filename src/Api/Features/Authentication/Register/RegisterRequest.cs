namespace SPW.Admin.Api.Features.Authentication.Register;

[ExcludeFromCodeCoverage]
public sealed class RegisterRequest
{
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
}