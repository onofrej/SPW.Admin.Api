namespace SPW.Admin.Api.Features.Holiday.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = default;
    public DateTime Date { get; set; } = default;
}