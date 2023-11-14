using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class UserRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public UserRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public User Create(string fullName, string street, int zip,string email,  bool admin = false)
    {
        const string sql = $@"
INSERT INTO users (full_name, street,zip,email,  admin)
VALUES (@fullName, @street, @zip,@email,  @admin)
RETURNING
    id as {nameof(User.Id)},
    full_name as {nameof(User.FullName)},
    street as {nameof(User.Street)},
    zip as {nameof(User.Zip)},
    email as {nameof(User.Email)},
    admin as {nameof(User.IsAdmin)}
    ;
";
        using var connection = _dataSource.OpenConnection();
        return connection.QueryFirst<User>(sql, new { fullName, street, zip, email,admin });
    }

    public User? GetById(int id)
    {
        const string sql = $@"
SELECT
   id as {nameof(User.Id)},
    full_name as {nameof(User.FullName)},
    street as {nameof(User.Street)},
    zip as {nameof(User.Zip)},
    email as {nameof(User.Email)},
    admin as {nameof(User.IsAdmin)}
FROM users
WHERE id = @id;
";
        using var connection = _dataSource.OpenConnection();
        return connection.QueryFirstOrDefault<User>(sql, new { id });
    }
    
}