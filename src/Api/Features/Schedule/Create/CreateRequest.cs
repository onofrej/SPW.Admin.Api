namespace SPW.Admin.Api.Features.Schedule.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateRequest
{
    public Guid DomainId { get; set; }
    public string? Time { get; set; }
}