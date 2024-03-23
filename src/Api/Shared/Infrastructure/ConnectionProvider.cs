using System.Data;
using Npgsql;

namespace SPW.Admin.Api.Shared.Infrastructure;

[ExcludeFromCodeCoverage]
internal sealed class ConnectionProvider : IConnectionProvider
{
    private const string SqlServerConfiguration = "PostgreSQL:ConnectionString";
    private readonly string _connectionString;

    public ConnectionProvider(IConfiguration configuration)
    {
        _connectionString = configuration.GetSection(SqlServerConfiguration).Value!;

        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new ArgumentException(SqlServerConfiguration);
        }
    }

    public async Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken)
    {
        var npgsqlConnection = new NpgsqlConnection(_connectionString);
        await npgsqlConnection.OpenAsync(cancellationToken);
        return npgsqlConnection;
    }
}