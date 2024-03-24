using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDay.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<SpecialDayEntity>>;