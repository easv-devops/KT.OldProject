using infrastructure.DataModels;

namespace service;

public class SessionData
{
    public required int User_Id { get; init; }
    public required bool IsAdmin { get; init; }

    public static SessionData FromUser(User user)
    {
        return new SessionData { User_Id = user.user_id, IsAdmin = user.IsAdmin };
    }

    public static SessionData FromDictionary(Dictionary<string, object> dict)
    {
        return new SessionData { User_Id = (int)dict[Keys.User_Id], IsAdmin = (bool)dict[Keys.IsAdmin] };
    }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object> { { Keys.User_Id, User_Id }, { Keys.IsAdmin, IsAdmin } };
    }

    public static class Keys
    {
        public const string User_Id = "u";
        public const string IsAdmin = "a";
    }
}