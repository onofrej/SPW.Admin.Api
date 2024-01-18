using SPW.Admin.Api.Features.Point.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<PointEntity>>>;