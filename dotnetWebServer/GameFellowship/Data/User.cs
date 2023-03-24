namespace GameFellowship.Data
{
    public class User
    {
        private static int _userID;

        public int UserID { get; init; } = -1;

        // FIXME: public string Password { get; init; } = "password";

        public int[] GameIDs { get; set; } = { };
        public int[] CreatedPostIDs { get; set; } = { };
        public int[] JoinedPostIDs { get; set; } = { };
        public int[] FriendIDs { get; set; } = { };

        public string UserName { get; set; } = "Undefined Name";
        public string UserEmail { get; set; } = string.Empty;
        public string UserIconURI { get; set; } = "images/UserIcons/50913860_p9.jpg";

        public User()
        {
            UserID = ++_userID;
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
                GameIDs = games;

            if (createdPosts is not null)
                CreatedPostIDs = createdPosts;

            if (joinedPosts is not null)
                JoinedPostIDs = joinedPosts;

            if (friends is not null)
                FriendIDs = friends;
        }
    }
}