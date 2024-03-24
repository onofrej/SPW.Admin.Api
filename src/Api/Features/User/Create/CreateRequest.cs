namespace SPW.Admin.Api.Features.User.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateRequest
{
    public DateTime BaptismDate { get; set; }
    public DateTime BirthDate { get; set; }
    public Guid CongregationId { get; set; }
    public string? Email { get; set; }
    public string? Gender { get; set; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Privilege { get; set; }
}