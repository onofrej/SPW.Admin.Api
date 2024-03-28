using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public Guid CircuitId { get; set; } = default;
}