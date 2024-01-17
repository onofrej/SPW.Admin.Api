using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit.Delete;

[ExcludeFromCodeCoverage]
public sealed class DeleteCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}