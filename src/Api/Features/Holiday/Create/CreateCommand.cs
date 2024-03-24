using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday.Create;

[ExcludeFromCodeCoverage]
public sealed class CreateCommand : IRequest<Result<Guid>>
{
    public string? Name { get; set; } = default;
    public DateTime Date { get; set; } = default;
}