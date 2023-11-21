using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class CustomerBuyRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public CustomerBuyRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<CustomerBuy> GetAllCustomerBuy()
    {
        var sql =
            @"SELECT * FROM customer_buy ORDER BY id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<CustomerBuy>(sql);
        }
    }

    public CustomerBuy CreateCustomerBuy(int order_id, int avatar_id)
    {
        var sql =
            @"INSERT INTO customer_buy (order_id, avatar_id) VALUES (@order_id, @avatar_id) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<CustomerBuy>(sql, new { order_id, avatar_id });
        }
    }

    public CustomerBuy UpdateCustomerBuy(int id, int order_id, int avatar_id)
    {
        var sql =
            @"UPDATE customer_buy SET order_id = @order_id, avatar_id = @avatar_id WHERE id = @customer_buy_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<CustomerBuy>(sql, new {id, order_id, avatar_id});
        }
    }

    public void DeleteCustomerBuy(int customer_buy_id)
    {
        var sql = @"DELETE FROM customer_buy WHERE customer_buy.id = @customer_buy_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<CustomerBuy>(sql, new {customer_buy_id});
        }
    }
    
}