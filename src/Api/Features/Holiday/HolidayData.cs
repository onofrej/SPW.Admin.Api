namespace SPW.Admin.Api.Features.Holiday;

[ExcludeFromCodeCoverage]
internal sealed class HolidayData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder) : IHolidayData
{
    public async Task<int> CreateHolidayAsync(HolidayEntity entity, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""holiday"" (id, name, date, domain_id) VALUES (@Id, @Name, @Date, @DomainId)";
        return await connection.ExecuteAsync(query, entity);
    }

    public async Task<int> DeleteHolidayAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"DELETE FROM ""holiday"" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<IEnumerable<HolidayEntity>> GetAllHolidayAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"SELECT * FROM ""holiday""";
        return await connection.QueryAsync<HolidayEntity>(query, cancellationToken);
    }

    public async Task<HolidayEntity> GetHolidayByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"SELECT * FROM ""holiday"" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<HolidayEntity?>(query, new { Id = id });
    }

    public async Task<int> UpdateHolidayAsync(HolidayEntity entity, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""holiday"" SET name = @Name, date = @Date WHERE id = @Id";
        return await connection.ExecuteAsync(query, entity);
    }
}