using System.Threading;

namespace SPW.Admin.IntegrationTests.Fixtures.PostgreSql;

internal sealed class PostgreSqlFixture : IDisposable
{
    private const string PathReference = "IntegrationTests";
    private readonly string _connectionString;

    public PostgreSqlFixture(IConfiguration _configuration)
    {
        string rootPath = GetProjectRootPath();
        string databaseScriptPath = string.Concat(rootPath, "/infra/terraform/rds/scripts/database-script.sql");

        _connectionString = _configuration.GetSection("PostgreSQL:ConnectionString").Value!;

        ArgumentException.ThrowIfNullOrEmpty(_connectionString);

        CreateDatabase(databaseScriptPath);
    }

    private static string GetProjectRootPath()
    {
        string currentDirectory = AppDomain.CurrentDomain.BaseDirectory
            [..AppDomain.CurrentDomain.BaseDirectory.IndexOf("IntegrationTests")];

        string projectRootPath = Path.Combine(currentDirectory, "..");

        return Path.GetFullPath(projectRootPath);
    }

    private void CreateDatabase(string databaseScriptPath)
    {
        var databaseScript = File.ReadAllText(databaseScriptPath);

        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var command = new NpgsqlCommand(databaseScript, connection);
        command.ExecuteNonQuery();
    }

    public async Task<T?> GetByIdAsync<T>(string query, Guid id, CancellationToken cancellationToken)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        return await connection.QueryFirstOrDefaultAsync<T?>(query, new { Id = id });
    }

    public async Task<int> CreateAsync<T>(T entity, string query, CancellationToken cancellationToken)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        return await connection.ExecuteAsync(query, entity);
    }

    internal async Task TruncateTableAsync(string tableName, CancellationToken cancellationToken)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await connection.ExecuteAsync($"TRUNCATE TABLE {tableName};");
    }

    public void Dispose()
    {
    }
}