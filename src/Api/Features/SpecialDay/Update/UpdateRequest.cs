namespace SPW.Admin.Api.Features.SpecialDay.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid CircuitId { get; set; }
}