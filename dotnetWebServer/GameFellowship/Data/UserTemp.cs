using GameFellowship.Data.FormModels;

namespace GameFellowship.Data;

public class UserTemp
{
    private static int _userID;

    public int UserID { get; init; } = -1;

    // FIXME: public string Password { get; init; } = "password";

    public HashSet<int> LikedGameIDs { get; set; } = new();
    public HashSet<int> CreatedPostIDs { get; set; } = new();
    public HashSet<int> JoinedPostIDs { get; set; } = new();
    public HashSet<int> FriendIDs { get; set; } = new();

    public string UserName { get; private set; } = "Anonymous";
    public string UserEmail { get; private set; } = string.Empty;
    public string UserIconURI { get; private set; } = "images/UserIcons/50913860_p9.jpg";

    public UserTemp()
    {
        UserID = ++_userID;
    }

    public UserTemp(UserModel model)
    {
        UserID = ++_userID;

        UserName = model.UserName;
        // FIXME: User Password
        UserEmail = model.UserEmail;
        UserIconURI = model.UserIconURI;
    }

    public UserTemp(string name, string? email = null, string? icon = null, int[]? games = null, int[]? createdPosts = null, int[]? joinedPosts = null, int[]? friends = null)
    {
        UserID = ++_userID;
        UserName = name;

        if (email is not null && email != string.Empty)
            UserEmail = email;

        if (icon is not null && icon != string.Empty)
            UserIconURI = icon;

        if (games is not null)
            LikedGameIDs = games.ToHashSet();

        if (createdPosts is not null)
            CreatedPostIDs = createdPosts.ToHashSet();

        if (joinedPosts is not null)
            JoinedPostIDs = joinedPosts.ToHashSet();

        if (friends is not null)
            FriendIDs = friends.ToHashSet();
    }
}