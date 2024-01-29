namespace SPW.Admin.Api.Features.SpecialDate.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateRequest
{
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid CircuitId { get; set; }
}