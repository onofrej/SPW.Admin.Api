namespace SPW.Admin.Api.Features.Congregation;

[ExcludeFromCodeCoverage]
internal sealed class CongregationEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public Guid CircuitId { get; set; }
}