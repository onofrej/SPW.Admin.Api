using SPW.Admin.Api.Features.Schedule.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Schedule.GetById
{
    public record GetByIdQuery(Guid Id) : IRequest<Result<ScheduleEntity>>;
}