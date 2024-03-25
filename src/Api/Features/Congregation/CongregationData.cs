namespace SPW.Admin.Api.Features.Congregation;

internal sealed class CongregationData : ICongregationData
{
    private readonly NpgsqlDataSourceBuilder _npgsqlDataSourceBuilder;

    public CongregationData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder)
    {
        _npgsqlDataSourceBuilder = npgsqlDataSourceBuilder;
    }

    public async Task<CongregationEntity> GetCongregationByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"congregation\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<CongregationEntity>(query, new { Id = id });
    }

    public async Task<IEnumerable<CongregationEntity>> GetAllCongregationsAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"congregation\"";
        return await connection.QueryAsync<CongregationEntity>(query, cancellationToken);
    }

    public async Task<int> CreateCongregationAsync(CongregationEntity congregation, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""congregation"" (id, name, number, circuit_id)
                      VALUES (@Id, @Name, @Number, @CircuitId)";
        return await connection.ExecuteAsync(query, congregation);
    }

    public async Task<int> UpdateCongregationAsync(CongregationEntity congregation, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""congregation"" SET
            name = @Name,
            number = @Number,
            circuit_id = @CircuitId
            WHERE id = @Id";
        return await connection.ExecuteAsync(query, congregation);
    }

    public async Task<int> DeleteCongregationAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "DELETE FROM \"congregation\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}