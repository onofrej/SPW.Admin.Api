using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDay.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<SpecialDayEntity>>>;