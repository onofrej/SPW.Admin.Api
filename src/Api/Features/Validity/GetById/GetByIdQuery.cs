using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.GetById;

public record GetByIdQuery(Guid Id) : IRequest<Result<ValidityEntity>>;