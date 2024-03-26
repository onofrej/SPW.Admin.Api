namespace SPW.Admin.Api.Features.Announcement;

[ExcludeFromCodeCoverage]
internal sealed class AnnouncementEntity
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Message { get; set; }
}