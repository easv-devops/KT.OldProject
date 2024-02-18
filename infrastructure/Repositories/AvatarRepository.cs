using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class AvatarRepository
{
     private readonly NpgsqlDataSource _dataSource;

    public AvatarRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    /*
     * Gets all avatars from the database
     */
    public IEnumerable<AvatarModel> GetAllAvatars()
    {
        var sql = @"SELECT * FROM webshop.avatar where deleted=false ORDER BY avatar_id;";
        Console.WriteLine(sql);
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<AvatarModel>(sql);
        }
    }
    
    public IEnumerable<AvatarModel> GetAllDeletedAvatars()
    {
        var sql = @"SELECT * FROM webshop.avatar where deleted=true ORDER BY avatar_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<AvatarModel>(sql);
        }
    }
    
    /*
     * Creates a new avatar in the database.
     */
    public AvatarModel CreateAvatar(AvatarModel avatar)
    {
        return null;
        /**   var sql =
               @" INSERT INTO webshop.avatar (avatar_name, avatar_price, information,deleted) VALUES (@avatar_name, @avatar_price, @information, false) RETURNING *;";

           using (var conn = _dataSource.OpenConnection())
           {
               return conn.QueryFirst<AvatarModel>(sql, new { avatar_name=avatar.avatar_name, avatar_price=avatar.avatar_price, information=avatar.information});
           }*/
    }
    
    /*
     * Updates an existing avatar in the database. 
     */
    public AvatarModel UpdateAvatar(int avatar_id, AvatarModel avatar)
    {
        var sql =
            @"UPDATE webshop.avatar SET avatar_name = @avatar_name, avatar_price = @avatar_price, information = @information where avatar_id = @avatar_id
RETURNING *";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<AvatarModel>(sql, new { avatar_id, avatar_name=avatar.avatar_name, avatar_price=avatar.avatar_price,information=avatar.information});
        }
    }

    /*
     * Deletes an avatar from the database.
     */
    public void DeleteAvatar(int avatar_id)
    {
        
        var sql =
            @"UPDATE webshop.avatar SET deleted = true where avatar_id = @avatar_id
RETURNING *"; 
       
        using (var conn = _dataSource.OpenConnection())
        {
            conn.QueryFirst<AvatarModel>(sql, new { avatar_id });
        }
    }

    public void enableAvatar(int avatar_id)
    {
        var sql = @"UPDATE webshop.avatar SET deleted = false WHERE avatar_id = @avatar_id RETURNING*;";

        using (var conn = _dataSource.OpenConnection())
        { 
            conn.QueryFirst<AvatarModel>(sql, new { avatar_id });
        }
    }
    
    /*
     * Checks if an avatar with the given name exists in the database. 
     */
    public AvatarModel CheckIfNameExist(string avatar_name)    {

        var sql = $@"SELECT * FROM webshop.avatar WHERE avatar_name = @avatar_name;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<AvatarModel>(sql, new { avatar_name });
        }
    }
    
    /*
     * Gets the information for an avatar with the given id.
     */
    public string GetAvatarInformation(int avatar_id)
    {
        var sql = @"SELECT information FROM webshop.avatar where deleted = false AND avatar_id = @avatar_id;";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<string>(sql, new { avatar_id });
        }
    }
}