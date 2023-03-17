namespace GameFellowship.Data
{
    public class GameInfo
    {
        public string GameName { get; set; } = "Empty Name";
        public int Followers { get; set; } = 0;
        public string IconURI { get; set; } = "images/gametitle.jpg";

        public GameInfo() { }
        
        public GameInfo(string name, int followers, string icon)
        {
            GameName = name;
            Followers = followers;
            IconURI = icon;
        }
    }
}