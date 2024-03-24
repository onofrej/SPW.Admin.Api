namespace SPW.Admin.Api.Features.User.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateRequest
{
    public DateTime BaptismDate { get; set; } = default;
    public DateTime BirthDate { get; set; } = default;
    public Guid CongregationId { get; set; } = default;
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Privilege { get; set; } = string.Empty;
}