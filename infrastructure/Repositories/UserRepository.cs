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
    /*
     * Create a new user in the database.
     */
    public UserModel Create(RegisterModel model)
    {
        const string sql = @"
INSERT INTO webshop.users (full_name, email)
VALUES (@name, @mail)
RETURNING *;
";
        using (var connection = _dataSource.OpenConnection())
        {
            return connection.QueryFirst<UserModel>(sql, new { name = model.full_name,  mail = model.email.ToLower() });
        }
    }

    public UserModel Update(int user_id, UserModel userModel)
    {
        var sql = @"UPDATE webshop.users SET full_name = @full_name, email = @email WHERE user_id = @user_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<UserModel>(sql,new { user_id, full_name = userModel.full_name, email = userModel.email });
        }
    }
    
    /*
     * Retrieve a user by ID from the database.
     */
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