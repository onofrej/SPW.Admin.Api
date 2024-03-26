using Npgsql;

namespace SPW.Admin.IntegrationTests.Fixtures.PostgreSql;

internal sealed class PostgreSqlFixture : IDisposable
{
    private const string ApplicationName = "SPW.Admin.Api";

    public PostgreSqlFixture(IConfiguration _configuration)
    {
        string rootPath = AppDomain.CurrentDomain.BaseDirectory;
        string filePath = rootPath[..(rootPath.IndexOf(ApplicationName) + ApplicationName.Length)];
        string databaseScriptPath = string.Concat(filePath, "\\infra\\terraform\\scripts\\database-script.sql");

        CreateDatabase(_configuration, databaseScriptPath);
    }

    private static void CreateDatabase(IConfiguration configuration, string databaseScriptPath)
    {
        var connectionString = configuration.GetSection("PostgreSQL:ConnectionString").Value;
        var databaseScript = File.ReadAllText(databaseScriptPath);

        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();

        using var command = new NpgsqlCommand(databaseScript, connection);
        command.ExecuteNonQuery();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}