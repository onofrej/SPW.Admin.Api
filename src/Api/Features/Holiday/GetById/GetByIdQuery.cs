using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday.GetById
{
    public record GetByIdQuery(Guid Id) : IRequest<Result<HolidayEntity>>;
}