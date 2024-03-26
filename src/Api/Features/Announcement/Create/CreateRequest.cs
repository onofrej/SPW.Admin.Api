namespace SPW.Admin.Api.Features.Announcement.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateRequest
{
    public string? Title { get; set; }
    public string? Message { get; set; }

    public Guid DomainId { get; set; }
}