using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate.Delete;

[ExcludeFromCodeCoverage]
public class DeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}