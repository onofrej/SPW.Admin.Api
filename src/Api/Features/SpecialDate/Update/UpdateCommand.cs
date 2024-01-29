using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public Guid CircuitId { get; set; } = Guid.Empty;
}