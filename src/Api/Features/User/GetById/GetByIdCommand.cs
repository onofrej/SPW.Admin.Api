using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Read;

[ExcludeFromCodeCoverage]
public sealed class ReadCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; } = default;
    public DateTime BaptismDate { get; set; } = default;
    public string Privilege { get; set; } = string.Empty;
}