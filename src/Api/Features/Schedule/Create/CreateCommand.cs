using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public string? Time { get; set; } = default;
}