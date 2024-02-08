using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Authentication.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}