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

    public User Create(string full_name, string street, int zip,string email,  bool admin = false)
    {
        const string sql = $@"
INSERT INTO account.users (full_name, street, zip, email, admin)
VALUES (@full_name, @street, @zip,@email, @admin)
RETURNING
    order_id as {nameof(User.Id)},
    full_name as {nameof(User.full_name)},
    street as {nameof(User.Street)},
    zip as {nameof(User.Zip)},
    email as {nameof(User.Email)},
    admin as {nameof(User.IsAdmin)}
    ;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirst<User>(sql, new { full_name, street, zip, email, admin });
        }
    }

    public User? GetById(int id)
    {
        const string sql = $@"
SELECT
   order_id as {nameof(User.Id)},
    full_name as {nameof(User.full_name)},
    street as {nameof(User.Street)},
    zip as {nameof(User.Zip)},
    email as {nameof(User.Email)},
    admin as {nameof(User.IsAdmin)}
FROM account.users
WHERE order_id = @order_id;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirstOrDefault<User>(sql, new { id });
        }
    }

    public User GetAccountInfo()
    {
        const string sql = $@"
SELECT
   order_id as {nameof(User.Id)},
    full_name as {nameof(User.full_name)},
    street as {nameof(User.Street)},
    zip as {nameof(User.Zip)},
    email as {nameof(User.Email)},
    admin as {nameof(User.IsAdmin)}
FROM account.users
WHERE order_id = 2;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirstOrDefault<User>(sql);
        }
    }
    
}