namespace SPW.Admin.Api.Features.Domain;

[ExcludeFromCodeCoverage]
internal sealed class DomainData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder) : IDomainData
{
    public async Task<DomainEntity?> GetDomainByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"domain\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<DomainEntity?>(query, new { Id = id });
    }

    public async Task<IEnumerable<DomainEntity>> GetAllDomainsAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"domain\"";
        return await connection.QueryAsync<DomainEntity>(query, cancellationToken);
    }

    public async Task<int> CreateDomainAsync(DomainEntity entity, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""domain"" (id, name) VALUES (@Id, @Name)";
        return await connection.ExecuteAsync(query, entity);
    }

    public async Task<int> UpdateDomainAsync(DomainEntity entity, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""domain"" SET name = @Name WHERE id = @Id";
        return await connection.ExecuteAsync(query, entity);
    }

    public async Task<int> DeleteDomainAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "DELETE FROM \"domain\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}