using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<CircuitEntity>>;