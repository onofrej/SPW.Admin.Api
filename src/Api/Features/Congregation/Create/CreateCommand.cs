using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public Guid CircuitId { get; set; } = default;
}