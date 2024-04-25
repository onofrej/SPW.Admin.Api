namespace SPW.Admin.Api.Features.Schedule;

[ExcludeFromCodeCoverage]
internal sealed class ScheduleEntity
{
    public Guid DomainId { get; set; }
    public Guid Id { get; set; }
    public string? Time { get; set; }
}