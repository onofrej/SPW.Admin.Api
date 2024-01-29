using SPW.Admin.Api.Features.SpecialDate.DataAccess;
using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.SpecialDate.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<SpecialDateEntity>>>;