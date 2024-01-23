using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.Create;

public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    public bool Status { get; set; } = true;
}