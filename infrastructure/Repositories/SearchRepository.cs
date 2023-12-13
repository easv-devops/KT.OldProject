using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class SearchRepository
{
    private NpgsqlDataSource _dataSource;

    /*
     * Constructor that initializes the repository with a specific data source.
     */
    public SearchRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource; 
    }

    /*
     * Searches for AvatarModel entities based on a provided search query.
     */
    public IEnumerable<AvatarModel> SearchAvatar(string searchQuery)
    {
        var sql = $@"SELECT * FROM webshop.avatar WHERE avatar_name ILIKE '%{searchQuery}%' and deleted=false;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<AvatarModel>(sql, new { searchQuery });
        }
    }
}
