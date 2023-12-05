using SPW.Admin.Api.Shared;

namespace SPW.Admin.Api.Features.User.Create;

[ExcludeFromCodeCoverage]
public sealed class Command : IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; } = default;
    public DateTime BaptismDate { get; set; } = default;
    public string Privilege { get; set; } = string.Empty;
}