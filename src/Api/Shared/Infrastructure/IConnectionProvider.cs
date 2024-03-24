using System.Data;

namespace SPW.Admin.Api.Shared.Infrastructure;

internal interface IConnectionProvider
{
    Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken);
}