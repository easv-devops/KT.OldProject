using Dapper;
using Npgsql;

namespace Tests;

public static class Helper
{
    public static readonly NpgsqlDataSource DataSource;

    public static readonly string ApiBaseUrl = "http://localhost:5500/";

    static Helper()
    {
        var rawConnectionString = Environment.GetEnvironmentVariable("pgconn");
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
DROP SCHEMA IF EXISTS account CASCADE;
CREATE SCHEMA IF NOT EXISTS account;
create table account.users
(
    id         SERIAL PRIMARY KEY,
    full_name  VARCHAR(50)  NOT NULL,
    street     VARCHAR(50)   not NULL,
    zip        integer   not NULL,
    email      VARCHAR(50)  NOT NULL UNIQUE,
    admin      BOOLEAN      NOT NULL CHECK (admin IN (FALSE, TRUE)) DEFAULT FALSE
);

create table account.avatar
(
    avatar_id     SERIAL PRIMARY KEY,
    avatar_name  VARCHAR(50)  NOT NULL,
    price        integer   not NULL
    );

create table account.order
(
    id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL,
    FOREIGN KEY (user_id) REFERENCES account.users (id)
);

create table account.customer_buy
(
    id SERIAL PRIMARY KEY,
    order_id INTEGER NOT NUll,
    avatar_id INTEGER NOT NULL,
    FOREIGN KEY (order_id) REFERENCES account.order (id),
    FOREIGN KEY (avatar_id) REFERENCES account.avatar (avatar_id)
);

create table account.password_hash
(
    user_id   integer      NOT NULL,
    hash      VARCHAR(350) NOT NULL,
    salt      VARCHAR(180) NOT NULL,
    algorithm VARCHAR(12)  NOT NULL,
    FOREIGN KEY (user_id) REFERENCES account.users (id)
);
";
}