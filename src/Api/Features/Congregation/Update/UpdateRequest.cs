namespace SPW.Admin.Api.Features.Congregation.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Number { get; set; }
    public Guid CircuitId { get; set; }
}