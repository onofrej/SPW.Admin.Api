using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.Delete;

[ExcludeFromCodeCoverage]
public sealed class DeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}