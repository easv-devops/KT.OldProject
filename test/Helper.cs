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
            Console.WriteLine(e.Message);
            Console.WriteLine(e.InnerException);
            Console.WriteLine(e.StackTrace);
            throw new Exception("Didnt get connection to database");
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
            catch (Exception)
            {
                throw new Exception("Failed to rebuild the schema in database");
            }
        }
    }

    public static string RebuildScript = @"
DROP SCHEMA IF EXISTS testingSchema CASCADE;
CREATE SCHEMA testingSchema;
CREATE TABLE IF NOT EXISTS testingSchema.testingDatabase
(
    id         SERIAL NOT NULL,
    full_name  VARCHAR(50)  NOT NULL,
    street     VARCHAR(50)   not NULL,
    zip        integer   not NULL,
    email      VARCHAR(50)  NOT NULL,
    admin      BOOLEAN      NOT NULL
);
";
}