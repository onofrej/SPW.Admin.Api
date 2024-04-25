﻿namespace SPW.Admin.Api.Features.Holiday;

[ExcludeFromCodeCoverage]
internal sealed class HolidayEntity
{
    public Guid DomainId { get; set; }
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public DateTime Date { get; set; }
}