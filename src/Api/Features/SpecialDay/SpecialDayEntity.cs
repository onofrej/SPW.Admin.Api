namespace SPW.Admin.Api.Features.SpecialDay;

[ExcludeFromCodeCoverage]
internal sealed class SpecialDayEntity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Guid CircuitId { get; set; }
}