﻿using SPW.Admin.Api.Shared.Models;

namespace SPW.Admin.Api.Features.Domain.GetAll;

public record GetAllQuery() : IRequest<Result<IEnumerable<DomainEntity>>>;