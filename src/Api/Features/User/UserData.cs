namespace SPW.Admin.Api.Features.User;

[ExcludeFromCodeCoverage]
internal sealed class UserData : IUserData
{
    private readonly string _connectionString;

    public UserData(IConfiguration configuration)
    {
        _connectionString = configuration.GetSection("PostgreSQL:ConnectionString").Value!;
    }

    public async Task<UserEntity> GetUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var query = "SELECT * FROM \"user\" WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<UserEntity>(query, new { Id = id });
    }

    public async Task<IEnumerable<UserEntity>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var query = "SELECT * FROM \"user\"";
        return await connection.QueryAsync<UserEntity>(query, cancellationToken);
    }

    public async Task<int> CreateUserAsync(UserEntity user, CancellationToken cancellationToken)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var query = @"INSERT INTO ""user"" (id, name, creation_date, email, phonenumber, gender, birthdate, baptismdate, privilege)
                      VALUES (@Id, @Name, @CreationDate, @Email, @PhoneNumber, @Gender, @BirthDate, @BaptismDate, @Privilege)";
        return await connection.ExecuteAsync(query, user);
    }

    public async Task<int> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var query = @"UPDATE ""user"" SET
                      name = @Name,
                      creation_date = @CreationDate,
                      email = @Email,
                      phonenumber = @PhoneNumber,
                      gender = @Gender,
                      birthdate = @BirthDate,
                      baptismdate = @BaptismDate,
                      privilege = @Privilege
                      WHERE id = @Id";
        return await connection.ExecuteAsync(query, user);
    }

    public async Task<int> DeleteUserAsync(Guid id, CancellationToken cancellationToken)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var query = "DELETE FROM \"user\" WHERE id = @Id";
        return await connection.ExecuteAsync(query, new { Id = id });
    }
}