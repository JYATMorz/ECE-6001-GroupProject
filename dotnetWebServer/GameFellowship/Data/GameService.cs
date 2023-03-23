namespace GameFellowship.Data
{
    public class GameService
    {
        private GameModel[] games = {
            new GameModel ("Destiny 2", 114, new DateTime(2023,3,11,11,11,11)),
            new GameModel ("Touhou Project", 514, new DateTime(2023,3,20,20,20,20), "images/GameIcons/75750856_p0.jpg"),
            new GameModel ("Minecraft", 1919, new DateTime(2023,3,22,22,22,22), "images/GameIcons/75750856_p0.jpg"),
            new GameModel ("APEX", 8100),
            new GameModel ("Destiny 1", 11, new DateTime(2023,3,11,11,11,11), "images/GameIcons/75750856_p0.jpg"),
            new GameModel ("MineCity", 2020, new DateTime(2023,3,22,22,22,22)),
            new GameModel ("CS:GO", 77777),
            new GameModel ("CS 2", 888, new DateTime(2023,3,23,23,23,23), "images/UserIcons/50913860_p9.jpg")
        };

        public Task<GameModel[]> GetAllGameAsync()
        {
            return Task.FromResult(games);
        }

        public Task<GameModel> GetGameAsync(int id)
        {
            var selectedGame =
                from game in games
                where game.GameID == id
                select game;

            if (selectedGame.Count() == 0)
                return Task.FromResult(new GameModel());

            return Task.FromResult(selectedGame.First());
        }

        public Task<GameModel> GetGameAsync(string name)
        {
            var selectedGame =
                from game in games
                where game.GameName.ToLower() == name.ToLower()
                select game;

            if (selectedGame.Count() == 0)
                return Task.FromResult(new GameModel());

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
                return Task.FromResult(new string[] { });

            return Task.FromResult(selectedGames.ToArray());
        }

        public Task<string[]> GetGameNameTopAsync(int num, string? prefix = null)
        {
            IEnumerable<string> selectedGames;

            if (string.IsNullOrWhiteSpace(prefix))
            {
                selectedGames = (
                    from game in games
                    orderby game.Followers descending
                    select game.GameName
                ).Take(num);
            }
            else
            {
                selectedGames = (
                    from game in games
                    where game.GameName.Contains(prefix)
                    orderby game.Followers descending
                    select game.GameName
                ).Take(num);
            }

            return Task.FromResult(selectedGames.ToArray());
        }
    }
}