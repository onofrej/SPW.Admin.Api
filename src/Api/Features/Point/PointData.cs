namespace SPW.Admin.Api.Features.Point;

[ExcludeFromCodeCoverage]
internal sealed class PointData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder) : IPointData
{
    public async Task<PointEntity?> GetPointByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"point\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<PointEntity>(query, new { Id = id });
    }

    public async Task<IEnumerable<PointEntity>> GetAllPointsAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"point\"";
        return await connection.QueryAsync<PointEntity>(query, cancellationToken);
    }

    public async Task<int> CreatePointAsync(PointEntity point, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""point"" (id, name, number_of_publishers, address, image_url, google_maps_url, domain_id)
                      VALUES (@Id, @Name, @NumberOfPublishers, @Address, @ImageUrl, @GoogleMapsUrl, @DomainId)";
        return await connection.ExecuteAsync(query, point);
    }

    public async Task<int> UpdatePointAsync(PointEntity point, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""point"" SET
            name = @Name,
            number_of_publishers = @NumberOfPublishers,
            address = @Address,
            image_url = @ImageUrl,
            google_maps_url = @GoogleMapsUrl
            WHERE id = @Id";
        return await connection.ExecuteAsync(query, point);
    }

    public async Task<int> DeletePointAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "DELETE FROM \"point\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}