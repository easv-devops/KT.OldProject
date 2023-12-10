using Dapper;
using Npgsql;
using infrastructure.DataModels;

namespace infrastructure.Repositories;

public class OrderRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public OrderRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<OrderModel> GetAllOrder()
    {
        var sql = @"SELECT * FROM webshop.order ORDER BY order_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<OrderModel>(sql);
        }
    }

    public OrderModel CreateOrder(int user_id)
    {
        var sql =
            @"INSERT INTO webshop.order (user_id) VALUES (@user_id) RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<OrderModel>(sql, new { user_id });
        }
    }

    public void DeleteOrder(int order_id)
    {
        var sql = @"DELETE FROM webshop.order WHERE order_id = @order_id RETURNING *;";

        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<OrderModel>(sql, new { order_id });
        }
    }


    public void CreateCustomerBuy(OrderModel model)
    {
        
        using (var conn = _dataSource.OpenConnection())
        {

            var transaction = conn.BeginTransaction();

            var sql =
                @"INSERT INTO webshop.order (user_id) VALUES (@user_id) RETURNING *;";
            conn.QueryFirst<OrderModel>(sql, new { model.user_id });

            var sql2 =
                @"select * from webshop.order where user_id= (@user_id)  and order_id = ( SELECT MAX(order_id) FROM webshop.order);";

            var result = conn.QueryFirst<OrderModel>(sql2, new { model.user_id }, transaction);


            for (int i = 0; i < model.avatarArray.Length; i++)
            {

                var sql3 =
                    @"INSERT INTO webshop.customer_buy (order_id, avatar_id) VALUES (@order_id, @avatar_id) RETURNING *;";

                conn.QueryFirst(sql3, new { order_id = result.user_id, avatar_id = model.avatarArray[i].avatar_id }, transaction);
            }
            transaction.Commit();
            }

        }

    public OrderModel getLastOrderToEmail(int user_id)
    {
        var sql2 =
            @"select * from webshop.order where user_id= (@user_id)  and order_id = ( SELECT MAX(order_id) FROM webshop.order);";

         
        using (var conn = _dataSource.OpenConnection())
        {
            return   conn.QueryFirst<OrderModel>(sql2, new { user_id });
        }
    }
       
        
    }
