namespace SPW.Admin.Api.Features.Holiday.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateRequest
{
    public Guid DomainId { get; set; }
    public string? Name { get; set; }
    public DateTime Date { get; set; }
}