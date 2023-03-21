namespace GameFellowship.Data
{
    public class GameInfoService
    {
        private GameInfo[] games = {
            new GameInfo (1, "Destiny 2", 114, new DateTime(2023,3,11,11,11,11)),
            new GameInfo (2, "Touhou Project", 514, new DateTime(2023,3,20,20,20,20), "images/GameIcons/75750856_p0.jpg"),
            new GameInfo (3, "Minecraft", 1919, new DateTime(2023,3,22,22,22,22), "images/GameIcons/75750856_p0.jpg"),
            new GameInfo (4, "APEX", 8100)
        };

        public Task<GameInfo[]> GetAllGameAsync()
        {
            return Task.FromResult(games);
        }

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

        public Task<string> GetGameIconAsync(string name)
        {
            var selectedGameIcon =
                from game in games
                where game.GameName.ToLower() == name.ToLower()
                select game.IconURI;

            if (selectedGameIcon.Count() == 0)
                return Task.FromResult("images/GameIcons/gametitle.jpg");

            return Task.FromResult(selectedGameIcon.First());
        }

        public Task<string[]> GetGameNameGroupAsync(int[] gameIDs)
        {
            var selectedGames =
                from game in games
                where gameIDs.Contains(game.GameID)
                select game.GameName;

            if (selectedGames.Count() == 0)
                return Task.FromResult(new string[] {});

            return Task.FromResult(selectedGames.ToArray());
        }
    }
}