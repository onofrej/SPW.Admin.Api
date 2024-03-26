using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement.Update;

[ExcludeFromCodeCoverage]
public class UpdateCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public string? Message { get; set; } = string.Empty;
    public Guid DomainId { get; set; }
}