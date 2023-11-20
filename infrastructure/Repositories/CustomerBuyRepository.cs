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
            @"SELECT * FROM customer_buy_id ORDER BY customer_buy_id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<CustomerBuy>(sql);
        }
    }

    public CustomerBuy CreateCustomerBuy(int order_id, int avatar_id)
    {
        var sql =
            @"INSERT INTO customer_by_id (order_id, avatar_id) VALUES (@order_id, @avatar_id) RETURNING *;";
    }
    
}