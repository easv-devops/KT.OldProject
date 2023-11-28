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
        var sql = @"SELECT * FROM account.order ORDER BY order_id;";

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
            return conn.QueryFirst<Order>(sql, new { user_id });
        }
    }

    public void DeleteOrder(int order_id)
    {
        var sql = @"DELETE FROM account.order WHERE order_id = @order_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<Order>(sql, new { order_id });
        }
    }

 
    public User GetAnUser(int order_id)
    {
        var sql = @"SELECT * 
        FROM account.users
        INNER JOIN account.order on account.order.user_id=account.users.user_id 
        where account.order.user_id=@order_id";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new { order_id });
        }
    }
    
    
    
    
    
}