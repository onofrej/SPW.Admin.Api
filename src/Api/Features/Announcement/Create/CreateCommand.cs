using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Announcement.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public string? Title { get; set; } = string.Empty;
    public string? Message { get; set; } = string.Empty;
    public Guid DomainId { get; set; } = default;
}