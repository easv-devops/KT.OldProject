using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class PasswordHashRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public PasswordHashRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public PasswordHash GetByEmail(string email)
    {
        const string sql = $@"
SELECT 
    user_id as {nameof(PasswordHash.UserId)},
    hash as {nameof(PasswordHash.Hash)},
    salt as {nameof(PasswordHash.Salt)},
    algorithm as {nameof(PasswordHash.Algorithm)}
FROM account.password_hash
JOIN account.users ON account.password_hash.user_id = account.users.user_id
WHERE email = @email;
";
        using var connection = _dataSource.OpenConnection();
        return connection.QuerySingle<PasswordHash>(sql, new { email });
    }
    
    public void Create(int userId, string hash, string salt, string algorithm)
    {
        const string sql = $@"
INSERT INTO account.password_hash (user_id, hash, salt, algorithm)
VALUES (@userId, @hash, @salt, @algorithm)
";
        using var connection = _dataSource.OpenConnection();
        connection.Execute(sql, new { userId, hash, salt, algorithm });
    }
    
}

