using SPW.Admin.Api.Features.Circuit;

namespace SPW.Admin.Api.Features.Announcement;

[ExcludeFromCodeCoverage]
internal sealed class AnnouncementData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder) : IAnnouncementData
{
    public async Task<AnnouncementEntity> GetAnnouncementByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"announcement\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<AnnouncementEntity>(query, new { Id = id });
    }

    public async Task<IEnumerable<AnnouncementEntity>> GetAllAnnouncementsAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"announcement\"";
        return await connection.QueryAsync<AnnouncementEntity>(query, cancellationToken);
    }

    public async Task<int> CreateAnnoucnementAsync(AnnouncementEntity announcement, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""announcement"" (id, title, message, domain_id)
                      VALUES (@Id, @Title, @Message, @DomainId)";
        return await connection.ExecuteAsync(query, announcement);
    }

    public async Task<int> UpdateAnnoucementAsync(AnnouncementEntity announcement, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""announcement"" SET
            title = @Title,
            message = @Message,
            domain_id = @DomainId
            WHERE id = @Id";
        return await connection.ExecuteAsync(query, announcement);
    }

    public async Task<int> DeleteAnnouncementAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "DELETE FROM \"announcement\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}