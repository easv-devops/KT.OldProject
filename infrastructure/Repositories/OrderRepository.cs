using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class OrderRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public OrderRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Order> GetAllOrder()
    {
        var sql = @"SELECT * FROM account.order ORDER BY id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Order>(sql);
        }
    }

    public Order CreateOrder(int user_id)
    {
        var sql = 
            @"INSERT INTO account.order (user_id) VALUES (@user_id) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Order>(sql, new { user_id});
        }
    }
    
    public void DeleteOrder(int id)
    {
        var sql = @"DELETE FROM account.order WHERE id = @id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<Order>(sql, new { id});
        }
    }
}