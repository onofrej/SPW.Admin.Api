using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<CongregationEntity>>;