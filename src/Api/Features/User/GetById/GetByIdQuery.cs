using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.User.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<PointEntity>>;