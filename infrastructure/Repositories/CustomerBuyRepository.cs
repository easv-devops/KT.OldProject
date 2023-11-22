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
            @"SELECT * FROM account.customer_buy ORDER BY id;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<CustomerBuy>(sql);
        }
    }

    public CustomerBuy CreateCustomerBuy(int order_id, int avatar_id)
    {
        var sql =
            @"INSERT INTO account.customer_buy (order_id, avatar_id) VALUES (@order_id, @avatar_id) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<CustomerBuy>(sql, new { order_id, avatar_id });
        }
    }
    
    public void DeleteCustomerBuy(int customer_buy_id)
    {
        var sql = @"DELETE FROM account.customer_buy WHERE account.customer_buy.id = @customer_buy_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<CustomerBuy>(sql, new {customer_buy_id});
        }
    }
    
}