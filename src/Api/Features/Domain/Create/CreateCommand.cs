using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Domain.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
}