using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Domain.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<DomainEntity>>;