namespace SPW.Admin.Api.Features.User.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime BaptismDate { get; set; }
    public string? Privilege { get; set; }
}