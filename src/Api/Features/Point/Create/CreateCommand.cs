using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public int NumberOfPublishers { get; set; } = 0;
    public string Address { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string GoogleMapsUrl { get; set; } = string.Empty;
    public Guid DomainId { get; set; } = default;
}