using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = default;
    public string? Date { get; set; } = default;
}