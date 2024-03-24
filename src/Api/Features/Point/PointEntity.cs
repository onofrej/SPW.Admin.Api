namespace SPW.Admin.Api.Features.Point;

[ExcludeFromCodeCoverage]
internal sealed class PointEntity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int NumberOfPublishers { get; set; }

    public string? Address { get; set; }

    public string? ImageUrl { get; set; }

    public string? GoogleMapsUrl { get; set; }
}