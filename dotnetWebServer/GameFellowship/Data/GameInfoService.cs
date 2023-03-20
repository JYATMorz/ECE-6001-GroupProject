namespace GameFellowship.Data
{
    public class GameInfoService
    {
        private GameInfo[] games = {
            new GameInfo (1, "Destiny 2", 114),
            new GameInfo (2, "Touhou Project", 514, "images/GameIcons/75750856_p0.jpg"),
            new GameInfo (3, "Minecraft", 1919, "images/GameIcons/75750856_p0.jpg"),
            new GameInfo (4, "APEX", 8100)
        };

        public Task<GameInfo> GetGameAsync(int id)
        {
            var selectedGame =
                from game in games
                where game.GameID == id
                select game;

            if (selectedGame.Count() == 0)
                return Task.FromResult(new GameInfo());

            return Task.FromResult(selectedGame.First());
        }

        public Task<GameInfo> GetGameAsync(string name)
        {
            var selectedGame =
                from game in games
                where game.GameName.ToLower() == name.ToLower()
                select game;

            if (selectedGame.Count() == 0)
                return Task.FromResult(new GameInfo());

            return Task.FromResult(selectedGame.First());
        }
    }
}