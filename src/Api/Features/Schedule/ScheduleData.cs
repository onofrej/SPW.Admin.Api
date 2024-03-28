namespace SPW.Admin.Api.Features.Schedule;

[ExcludeFromCodeCoverage]
internal sealed class ScheduleData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder) : IScheduleData
{
    public async Task<ScheduleEntity?> GetScheduleByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"schedule\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<ScheduleEntity?>(query, new { Id = id });
    }

    public async Task<IEnumerable<ScheduleEntity>> GetAllSchedulesAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"schedule\"";
        return await connection.QueryAsync<ScheduleEntity>(query, cancellationToken);
    }

    public async Task<int> CreateScheduleAsync(ScheduleEntity entity, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""schedule"" (id, ""time"", domain_id) VALUES (@Id, @Time, @DomainId)";
        return await connection.ExecuteAsync(query, entity);
    }

    public async Task<int> UpdateScheduleAsync(ScheduleEntity entity, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""schedule"" SET ""time"" = @Time WHERE id = @Id";
        return await connection.ExecuteAsync(query, entity);
    }

    public async Task<int> DeleteScheduleAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "DELETE FROM \"schedule\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}