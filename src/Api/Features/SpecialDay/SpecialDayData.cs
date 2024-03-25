namespace SPW.Admin.Api.Features.SpecialDay;

[ExcludeFromCodeCoverage]
internal sealed class SpecialDayData : ISpecialDayData
{
    private readonly NpgsqlDataSourceBuilder _npgsqlDataSourceBuilder;

    public SpecialDayData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder)
    {
        _npgsqlDataSourceBuilder = npgsqlDataSourceBuilder;
    }

    public async Task<SpecialDayEntity> GetSpecialDayByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"special_day\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<SpecialDayEntity>(query, new { Id = id });
    }

    public async Task<IEnumerable<SpecialDayEntity>> GetAllSpecialDaysAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"special_day\"";
        return await connection.QueryAsync<SpecialDayEntity>(query, cancellationToken);
    }

    public async Task<int> CreateSpecialDayAsync(SpecialDayEntity specialDay, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""special_day"" (id, name, start_date, end_date, circuit_id)
                      VALUES (@Id, @Name, @StartDate, @EndDate, @CircuitId)";
        return await connection.ExecuteAsync(query, specialDay);
    }

    public async Task<int> UpdateSpecialDayAsync(SpecialDayEntity specialDay, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""special_day"" SET
            name = @Name,
            start_date = @StartDate,
            end_date = @EndDate,
            circuit_id = @CircuitId
            WHERE id = @Id";
        return await connection.ExecuteAsync(query, specialDay);
    }

    public async Task<int> DeleteSpecialDayAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = _npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "DELETE FROM \"special_day\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}