using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<PointEntity>>>;