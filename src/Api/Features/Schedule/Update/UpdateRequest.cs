namespace SPW.Admin.Api.Features.Schedule.Update;

[ExcludeFromCodeCoverage]
public sealed class UpdateRequest
{
    public Guid Id { get; set; }
    public string? Time { get; set; } = default;
}