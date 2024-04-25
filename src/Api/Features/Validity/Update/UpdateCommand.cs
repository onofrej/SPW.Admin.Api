using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; } = default;
    public DateTime EndDate { get; set; } = default;
    public bool Status { get; set; } = true;
}