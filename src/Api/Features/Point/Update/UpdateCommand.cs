using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point.Update;

public class UpdateCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public int QuantityPublishers { get; set; } = 0;
    public string? Address { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public string? GoogleMapsUrl { get; set; } = string.Empty;
    public Guid DomainId { get; set; } = default;
}