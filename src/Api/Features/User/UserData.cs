namespace SPW.Admin.Api.Features.User;

[ExcludeFromCodeCoverage]
internal sealed class UserData(NpgsqlDataSourceBuilder npgsqlDataSourceBuilder) : IUserData
{
    public async Task<UserEntity?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"user\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<UserEntity?>(query, new { Id = id });
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "SELECT * FROM \"user\"";
        return await connection.QueryAsync<UserEntity>(query, cancellationToken);
    }

    public async Task<int> CreateUserAsync(UserEntity user, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"INSERT INTO ""user"" (id, name, creation_date, email, phone_number, gender, birth_date, baptism_date, privilege, congregation_id)
                      VALUES (@Id, @Name, @CreationDate, @Email, @PhoneNumber, @Gender, @BirthDate, @BaptismDate, @Privilege, @CongregationId)";
        return await connection.ExecuteAsync(query, user);
    }

    public async Task<int> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = @"UPDATE ""user"" SET
            name = @Name,
            creation_date = @CreationDate,
            email = @Email,
            phone_number = @PhoneNumber,
            gender = @Gender,
            birth_date = @BirthDate,
            baptism_date = @BaptismDate,
            privilege = @Privilege,
            congregation_id = @CongregationId
            WHERE id = @Id";
        return await connection.ExecuteAsync(query, user);
    }

    public async Task<int> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        await using var npgsqlDataSource = npgsqlDataSourceBuilder.Build();
        using var connection = await npgsqlDataSource.OpenConnectionAsync(cancellationToken);
        var query = "DELETE FROM \"user\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}