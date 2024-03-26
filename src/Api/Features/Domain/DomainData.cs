namespace SPW.Admin.Api.Features.Domain;

[ExcludeFromCodeCoverage]
internal sealed class DomainData : IDomainData
{
    private readonly NpgsqlDataSourceBuilder _npgsqlDataSourceBuilder;

    public DomainData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder)
    {
        _npgsqlDataSourceBuilder = npgsqlDataSourceBuilder;
    }

    public async Task<DomainEntity?> GetDomainByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"domain\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<DomainEntity>(query, new { Id = id });
    }

    public async Task<IEnumerable<DomainEntity>> GetAllDomainsAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"domain\"";
        return await connection.QueryAsync<DomainEntity>(query, cancellationToken);
    }

    public async Task<int> CreateDomainAsync(DomainEntity domain, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""domain"" (id, name)
                      VALUES (@Id, @Name)";
        return await connection.ExecuteAsync(query, domain);
    }

    public async Task<int> UpdateDomainAsync(DomainEntity domain, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""domain"" SET
            name = @Name
            WHERE id = @Id";
        return await connection.ExecuteAsync(query, domain);
    }

    public async Task<int> DeleteDomainAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "DELETE FROM \"domain\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}