namespace SPW.Admin.Api.Features.Circuit;

[ExcludeFromCodeCoverage]
internal sealed class CircuitEntity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public Guid DomainId { get; set; }
}