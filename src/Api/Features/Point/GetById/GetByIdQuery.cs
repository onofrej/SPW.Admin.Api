using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Point.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<PointEntity>>;