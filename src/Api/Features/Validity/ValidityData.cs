namespace SPW.Admin.Api.Features.Validity;

[ExcludeFromCodeCoverage]
internal sealed class ValidityData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder) : IValidityData
{
    public async Task<ValidityEntity> GetValidityByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"validity\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<ValidityEntity?>(query, new { Id = id });
    }

    public async Task<IEnumerable<ValidityEntity>> GetAllValiditiesAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"validity\"";
        return await connection.QueryAsync<ValidityEntity>(query, cancellationToken);
    }

    public async Task<int> CreateValidityAsync(ValidityEntity validity, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""validity"" (id, start_date, end_date, status, domain_id)
                      VALUES (@Id, @StartDate, @EndDate, @Status, @DomainId)";
        return await connection.ExecuteAsync(query, validity);
    }

    public async Task<int> UpdateValidityAsync(ValidityEntity validity, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""validity"" SET
            start_date = @StartDate,
            end_date = @EndDate,
            status = @Status,
            domain_id = @DomainId
            WHERE id = @Id";
        return await connection.ExecuteAsync(query, validity);
    }

    public async Task<int> DeleteValidityAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "DELETE FROM \"validity\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}