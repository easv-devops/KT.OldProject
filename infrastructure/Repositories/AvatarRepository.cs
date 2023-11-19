using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class AvatarRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public AvatarRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    
    public IEnumerable<Avatar> GetAllAvatars()
    {
        var sql = @"SELECT * FROM avatar ORDER BY avatar_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Avatar>(sql);
        }
    }
    
    
    public Avatar CreateAvatar(string avatar_name, int price)
    {
        var sql =
            @" INSERT INTO avatar (avatar_name, price) VALUES (@avatar_name, @price) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Avatar>(sql, new { avatar_name, price });
        }
    }
    
    public Avatar UpdateAvatar(int avatar_id, string avatar_name, int price)
    {
        var sql =
            @"UPDATE avatar SET avatar_name = @avatar_name, price = @price RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Avatar>(sql, new { avatar_id, avatar_name, price});
        }
    }

    
    public void DeleteAvatar(int avatar_id)
    {
        var sql = @"DELETE FROM avatar WHERE avatar_id = @avatar_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<Avatar>(sql, new { avatar_id });
        }
    }
    
    
    
}