using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Validity.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<ValidityEntity>>>;