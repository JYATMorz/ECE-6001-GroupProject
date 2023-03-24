namespace GameFellowship.Data
{
    public class Game
    {
        private static int _gameID;

        public int GameID { get; init; } = -1;
        public string GameName { get; set; } = "Empty Name";
        public int Followers { get; set; } = 0;
        public string IconURI { get; set; } = "images/GameIcons/gametitle.jpg";
        public DateTime LastPostDate { get; set; } = DateTime.MinValue;

        public Game()
        {
            GameID = ++_gameID;
        }

        public Game(string name, int followers, DateTime? lastPost = null, string? icon = null)
        {
            GameID = ++_gameID;
            GameName = name;
            Followers = followers;

            if (lastPost is not null && lastPost != DateTime.MinValue)
                LastPostDate = (DateTime)lastPost;

            if (icon is not null)
                IconURI = icon;
        }
    }
}