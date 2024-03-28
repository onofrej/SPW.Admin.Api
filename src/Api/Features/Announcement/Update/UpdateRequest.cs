namespace SPW.Admin.Api.Features.Announcement.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Message { get; set; }
}