namespace GameFellowship.Data
{
    public class GameInfo
    {
        public int GameID { get; init; } = -1;
        public string GameName { get; set; } = "Empty Name";
        public int Followers { get; set; } = 0;
        public string IconURI { get; set; } = "images/GameIcons/gametitle.jpg";
        public DateTime LastPostDate { get; set; } = DateTime.MinValue;

        public GameInfo() { }

        public GameInfo(int id, string name, int followers, DateTime? lastPost = null, string? icon = null)
        {
            GameID = id;
            GameName = name;
            Followers = followers;

            if (lastPost is not null && lastPost != DateTime.MinValue)
                LastPostDate = (DateTime)lastPost;

            if (icon is not null)
                IconURI = icon;
        }
    }
}