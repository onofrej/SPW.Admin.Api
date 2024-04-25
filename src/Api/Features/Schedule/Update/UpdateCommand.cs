using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Time { get; set; } = default;
}