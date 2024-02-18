using Dapper;
using Npgsql;

namespace Tests;

public static class Helper
{
    public static readonly NpgsqlDataSource DataSource;
    //TEST
    public static readonly string ApiBaseUrl = "http://localhost:5000/";

    static Helper()
    {
        var rawConnectionString =
            "postgres://wvgldqtr:gIgEzwsDlIrYu1XRlg76RLRAVoBH6s1f@cornelius.db.elephantsql.com/wvgldqtr"; //Environment.GetEnvironmentVariable("pgconn");
        try
        {
            var uri = new Uri(rawConnectionString);
            var properlyFormattedConnectionString = string.Format(
                "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=false;",
                uri.Host, 
                uri.AbsolutePath.Trim('/'),
                uri.UserInfo.Split(':')[0],
                uri.UserInfo.Split(':')[1],
                uri.Port > 0 ? uri.Port : 5432);
            DataSource =
                new NpgsqlDataSourceBuilder(properlyFormattedConnectionString).Build();
            DataSource.OpenConnection().Close();
        }
        catch (Exception e)
        {
            throw new Exception("Didnt get connection to database", e);
        }
    }

    public static void TriggerRebuild()
    {
        using (var conn = DataSource.OpenConnection())
        {
            try
            {
                conn.Execute(RebuildScript);
            }
            catch (Exception e)
            {
                throw new Exception("Failed to rebuild the schema in database", e);
            }
        }
    }

    public static string RebuildScript = @"

DROP SCHEMA IF EXISTS webshop CASCADE;
CREATE SCHEMA IF NOT EXISTS webshop;
DROP TABLE IF EXISTS webshop.password_hash;
DROP TABLE IF EXISTS webshop.customer_buy;
DROP TABLE IF EXISTS webshop.avatar;
DROP TABLE IF EXISTS webshop.order;
DROP TABLE IF EXISTS webshop.users;

create table webshop.users
(
    user_id         SERIAL PRIMARY KEY,
    full_name  VARCHAR(50)  NOT NULL,
    email      VARCHAR(50)  NOT NULL UNIQUE,
    admin      VARCHAR(20)      NOT NULL DEFAULT 'Non-admin'
);

create table webshop.avatar
(
    avatar_id     SERIAL PRIMARY KEY,
    avatar_name  VARCHAR(50)  NOT NULL,
    avatar_price        integer   not NULL,
    information VARCHAR(300),
    deleted bool not null DEFAULT FALSE
);

create table webshop.order
(
    order_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES webshop.users (user_id)
);

create table webshop.customer_buy
(
    customer_buy_id SERIAL PRIMARY KEY,
    order_id INTEGER NOT NUll,
    avatar_id INTEGER NOT NUll,
    FOREIGN KEY (order_id) REFERENCES webshop.order (order_id),
    FOREIGN KEY (avatar_id) REFERENCES webshop.avatar (avatar_id)
);

create table webshop.password_hash
(
    user_id   integer      NOT NULL,
    hash      VARCHAR(350) NOT NULL,
    salt      VARCHAR(180) NOT NULL,
    FOREIGN KEY (user_id) REFERENCES webshop.users (user_id)
);
";
}