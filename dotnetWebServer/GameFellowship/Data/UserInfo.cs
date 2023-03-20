namespace GameFellowship.Data
{
    public class UserInfo
    {
        public int UserID { get; init; } = -1;

        // FIXME: public string Password { get; init; } = "password";

        public int[] GameIDs { get; set; } = { };
        public int[] CreatedPostIDs { get; set; } = { };
        public int[] JoinedPostIDs { get; set; } = { };
        public int[] FriendIDs { get; set; } = { };

        public string UserName { get; set; } = "Undefined Name";
        public string UserEmail { get; set; } = string.Empty;
        public string UserIconURI { get; set; } = "images/UserIcons/50913860_p9.jpg";
    }
}