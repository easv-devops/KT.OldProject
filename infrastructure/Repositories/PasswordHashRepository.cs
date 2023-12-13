using Dapper;
using Npgsql;
using infrastructure.DataModels;

namespace infrastructure.Repositories;

public class PasswordHashRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public PasswordHashRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    /*
     * Creates a new password hash in the database. 
     */
    public void Create(int id, string hash, string salt)
    {
        const string sql = $@"INSERT INTO webshop.password_hash (user_id, hash, salt) VALUES (@id, @hash, @salt)";
        using var connection = _dataSource.OpenConnection();
        connection.Execute(sql, new {id, hash, salt  });
    }
    
    /*
     * Gets the password hash for the given email address.
     */
    public PasswordHashModel GetByEmail(string mail)
    {
        const string sql = $@"
    SELECT 
    password_hash.user_id as {nameof(PasswordHashModel.user_id)},
    hash as {nameof(PasswordHashModel.hash)},
    salt as {nameof(PasswordHashModel.salt)}
    FROM webshop.password_hash
    JOIN webshop.users ON webshop.password_hash.user_id = webshop.users.user_id
    WHERE email = lower(@mail);
    ";
        using var connection = _dataSource.OpenConnection();
        return connection.QuerySingle<PasswordHashModel>(sql, new { mail });
    }
}

