using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.Create;

public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public DateTime StartDate { get; set; } = default;
    public DateTime EndDate { get; set; } = default;
    public bool Status { get; set; } = true;
    public Guid DomainId { get; set; } = default;
}