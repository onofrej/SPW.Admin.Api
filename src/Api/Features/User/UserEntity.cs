namespace SPW.Admin.Api.Features.User;

[ExcludeFromCodeCoverage]
internal sealed class UserEntity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public DateTime CreationDate { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Gender { get; set; }

    public DateTime BirthDate { get; set; }

    public DateTime BaptismDate { get; set; }

    public string? Privilege { get; set; }
}