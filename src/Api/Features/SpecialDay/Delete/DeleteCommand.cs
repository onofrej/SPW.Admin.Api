using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDay.Delete;

[ExcludeFromCodeCoverage]
public class DeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}