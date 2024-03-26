namespace SPW.Admin.Api.Features.Circuit;

internal sealed class CircuitData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder) : ICircuitData
{
    public async Task<CircuitEntity> GetCircuitByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"circuit\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<CircuitEntity>(query, new { Id = id });
    }

    public async Task<IEnumerable<CircuitEntity>> GetAllCircuitsAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"circuit\"";
        return await connection.QueryAsync<CircuitEntity>(query, cancellationToken);
    }

    public async Task<int> CreateCircuitAsync(CircuitEntity circuit, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""circuit"" (id, name, domain_id)
                      VALUES (@Id, @Name, @DomainId)";
        return await connection.ExecuteAsync(query, circuit);
    }

    public async Task<int> UpdateCircuitAsync(CircuitEntity circuit, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""circuit"" SET
            name = @Name,
            domain_id = @DomainId
            WHERE id = @Id";
        return await connection.ExecuteAsync(query, circuit);
    }

    public async Task<int> DeleteCircuitAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "DELETE FROM \"circuit\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}