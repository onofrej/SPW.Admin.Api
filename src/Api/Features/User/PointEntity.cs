namespace SPW.Admin.Api.Features.User;

[ExcludeFromCodeCoverage]
internal sealed class PointEntity
{
    public DateTime BaptismDate { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid CongregationId { get; set; }
    public Guid Id { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Privilege { get; set; }
}