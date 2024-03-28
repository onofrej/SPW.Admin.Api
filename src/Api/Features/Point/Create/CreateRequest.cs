namespace SPW.Admin.Api.Features.Point.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateRequest
{
    public string? Name { get; set; }

    public int NumberOfPublishers { get; set; }

    public string? Address { get; set; }

    public string? ImageUrl { get; set; }

    public string? GoogleMapsUrl { get; set; }

    public Guid DomainId { get; set; }
}