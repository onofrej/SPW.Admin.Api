namespace SPW.Admin.Api.Features.Authentication.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateRequest
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}