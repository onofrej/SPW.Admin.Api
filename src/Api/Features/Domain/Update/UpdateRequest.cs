namespace SPW.Admin.Api.Features.Domain.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}