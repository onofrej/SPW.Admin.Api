using SPW.Admin.Api.Features.Circuit.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Circuit.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<CircuitEntity>>>;