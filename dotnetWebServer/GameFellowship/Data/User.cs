using GameFellowship.Data.FormModels;

namespace GameFellowship.Data;

public class User
{
    private static int _userID;

    public int UserID { get; init; } = -1;

    // FIXME: public string Password { get; init; } = "password";

    public List<int> LikedGameIDs { get; set; } = new List<int>();
    public List<int> CreatedPostIDs { get; set; } = new List<int>();
    public List<int> JoinedPostIDs { get; set; } = new List<int>();
    public List<int> FriendIDs { get; set; } = new List<int>();

    public string UserName { get; private set; } = "Anonymous";
    public string UserEmail { get; private set; } = string.Empty;
    public string UserIconURI { get; private set; } = "images/UserIcons/50913860_p9.jpg";

    public User()
    {
        UserID = ++_userID;
    }

    public User(UserModel model)
    {
        UserID = ++_userID;

        UserName = model.UserName;
        // FIXME: User Password
        UserEmail = model.UserEmail;
        UserIconURI = model.UserIconURI;
    }

    public User(string name, string? email = null, string? icon = null, int[]? games = null, int[]? createdPosts = null, int[]? joinedPosts = null, int[]? friends = null)
    {
        UserID = ++_userID;
        UserName = name;

        if (email is not null && email != string.Empty)
            UserEmail = email;

        if (icon is not null && icon != string.Empty)
            UserIconURI = icon;

        if (games is not null)
            LikedGameIDs = games.ToList();

        if (createdPosts is not null)
            CreatedPostIDs = createdPosts.ToList();

        if (joinedPosts is not null)
            JoinedPostIDs = joinedPosts.ToList();

        if (friends is not null)
            FriendIDs = friends.ToList();
    }
}