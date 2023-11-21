using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class OrdreRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public OrdreRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Ordre> GetAllOrdre()
    {
        var sql = @"SELECT * FROM avatar ORDER BY ordre_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Ordre>(sql);
        }
    }

    public Ordre CreateOrdre(int user_id)
    {
        var sql = 
            @"INSERT INTO ordre (user_id) VALUES (@user_id) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Ordre>(sql, new { user_id});
        }
    }

    public Ordre UpdateOrdre(int ordre_id, int user_id)
    {
        var sql = @"Update ordre Set user_id = @user_id WHERE ordre_id = @ordre_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Ordre>(sql, new { ordre_id, user_id});
        }
    }

    public void DeleteOrdre(int ordre_id)
    {
        var sql = @"DELETE FROM ordre WHERE ordre_id = @ordre_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<Ordre>(sql, new { ordre_id});
        }
    }
}