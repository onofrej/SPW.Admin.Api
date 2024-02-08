using SPW.Admin.Api.Features.Holiday.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Holiday.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<HolidayEntity>>>;