namespace SPW.Admin.Api.Features.Holiday.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateRequest
{
    public DateTime Date { get; set; } = default;
    public Guid DomainId { get; set; }
    public Guid Id { get; set; }
    public string? Name { get; set; } = default;
}