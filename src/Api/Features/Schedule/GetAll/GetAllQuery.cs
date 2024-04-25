using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<ScheduleEntity>>>;