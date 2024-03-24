using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public DateTime BaptismDate { get; set; } = default;
    public DateTime BirthDate { get; set; } = default;
    public Guid CongregationId { get; set; } = default;
    public string Email { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Privilege { get; set; } = string.Empty;
}