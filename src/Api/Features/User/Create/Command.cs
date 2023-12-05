using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Create;

[ExcludeFromCodeCoverage]
public sealed class Command : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
}