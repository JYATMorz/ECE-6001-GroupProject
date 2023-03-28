namespace GameFellowship.Data
{
    public class GameModel
    {
        public string GameName { get; set; } = string.Empty;
        public string IconURI { get; set; } = string.Empty;
        public bool Follow { get; set; } = true;
        public bool CreatingGame { get; set; } = false;

        public GameModel(string name)
        {
            GameName = name;
        }
    }
}