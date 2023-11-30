using Dapper;
using Npgsql;
using infrastructure.DataModels;

namespace infrastructure.Repositories;

public class UserRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public UserRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public User Create(RegisterCommandModel model)
    {
        const string sql = $@"
INSERT INTO account.users (full_name, street, zip, email, admin)
VALUES (@full_name, @street, @zip,@email, @admin)
RETURNING
    user_id as {nameof(User.user_id)},
    full_name as {nameof(User.full_name)},
    street as {nameof(User.Street)},
    zip as {nameof(User.Zip)},
    email as {nameof(User.Email)},
    admin as {nameof(User.IsAdmin)}
    ;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirst<User>(sql, new { full_name = model.full_name, street = model.Street, zip = model.Zip, email = model.Email, admin = false });
        }
    }

    public User? GetById(int id)
    {
        const string sql = $@"
SELECT
   user_id as {nameof(User.user_id)},
    full_name as {nameof(User.full_name)},
    street as {nameof(User.Street)},
    zip as {nameof(User.Zip)},
    email as {nameof(User.Email)},
    admin as {nameof(User.IsAdmin)}
FROM account.users
WHERE user_id = @customer_buy_id;
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
   user_id as {nameof(User.user_id)},
    full_name as {nameof(User.full_name)},
    street as {nameof(User.Street)},
    zip as {nameof(User.Zip)},
    email as {nameof(User.Email)},
    admin as {nameof(User.IsAdmin)}
FROM account.users
WHERE user_id = 2;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirstOrDefault<User>(sql);
        }
    }
}