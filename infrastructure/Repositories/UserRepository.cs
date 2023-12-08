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
    
    public UserModel Create(RegisterModel model)
    {
        const string sql = @"
INSERT INTO webshop.users (full_name, email)
VALUES (@name, @mail)
RETURNING *;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirst<UserModel>(sql, new { name = model.full_name,  mail = model.email });
        }
    }
    
    public UserModel? GetById(int id)
    {
        const string sql = @"
SELECT *
FROM webshop.users
WHERE user_id = @id;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirstOrDefault<UserModel>(sql, new { id });
        }
    }
}