namespace SPW.Admin.Api.Features.Congregation.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateRequest
{
    public string? Name { get; set; }
    public string? Number { get; set; }
    public Guid CircuitId { get; set; }
}