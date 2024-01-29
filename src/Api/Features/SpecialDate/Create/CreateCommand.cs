using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public Guid CircuitId { get; set; } = Guid.Empty;
}