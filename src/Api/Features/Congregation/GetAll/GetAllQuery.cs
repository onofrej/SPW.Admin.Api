using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Congregation.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<CongregationEntity>>>;