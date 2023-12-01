namespace SPW.Admin.Api.Features.User;

internal sealed record Command(string Name) : IRequest<Guid>;