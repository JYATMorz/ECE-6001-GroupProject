namespace GameFellowship.Data
{
    public class GameService : IGameService
    {
        private List<Game> games = new() {
            new Game ("Destiny 2", 114, new DateTime(2023,3,11,11,11,11)),
            new Game ("Touhou Project", 514, new DateTime(2023,3,20,20,20,20), "images/GameIcons/75750856_p0.jpg"),
            new Game ("Minecraft", 1919, new DateTime(2023,3,22,22,22,22), "images/GameIcons/75750856_p0.jpg"),
            new Game ("APEX", 8100),
            new Game ("Destiny 1", 11, new DateTime(2023,3,11,11,11,11), "images/GameIcons/75750856_p0.jpg"),
            new Game ("MineCity", 2020, new DateTime(2023,3,22,22,22,22)),
            new Game ("CS:GO", 77777),
            new Game ("CS 2", 888, new DateTime(2023,3,23,23,23,23), "images/UserIcons/50913860_p9.jpg")
        };

        public Task<bool> CreateNewGameAsync(GameModel model)
        {
            Game newGame = new(model);
            games.Add(newGame);

            // TODO: IF everything goes well
            return Task.FromResult(true);
        }

        public Task<bool> UpdateNewLatestPostDate(string name)
        {
            var selectedGame =
                from game in games
                where game.GameName.ToLower() == name.ToLower()
                select game;

            if (!selectedGame.Any()) return Task.FromResult(false);

            selectedGame.First().LastPostDate = DateTime.Now;
            // TODO: Save it back to database

            return Task.FromResult(true);
        }

        public Task<Game[]> GetAllGameAsync()
        {
            return Task.FromResult(games.ToArray());
        }

        public Task<Game> GetGameAsync(int id)
        {
            var selectedGame =
                from game in games
                where game.GameID == id
                select game;

            if (!selectedGame.Any())
                return Task.FromResult(new Game());

            return Task.FromResult(selectedGame.First());
        }

        public Task<Game> GetGameAsync(string name)
        {
            var selectedGame =
                from game in games
                where game.GameName.ToLower() == name.ToLower()
                select game;

            if (!selectedGame.Any())
                return Task.FromResult(new Game());

            return Task.FromResult(selectedGame.First());
        }

        public Task<string> GetGameIconAsync(string name)
        {
            var selectedGameIcon =
                from game in games
                where game.GameName.ToLower() == name.ToLower()
                select game.IconURI;

            if (!selectedGameIcon.Any())
                return Task.FromResult("images/GameIcons/gametitle.jpg");

            return Task.FromResult(selectedGameIcon.First());
        }

        public Task<IEnumerable<string>> GetGameNamesAsync(IEnumerable<int> gameIDs)
        {
            var selectedGames =
                from game in games
                where gameIDs.Contains(game.GameID)
                select game.GameName;

            return Task.FromResult(selectedGames);
        }

        public Task<IEnumerable<string>> GetGameNamesAsync(int count, string? prefix = null)
        {
            IEnumerable<string> selectedGames;

            if (string.IsNullOrWhiteSpace(prefix))
            {
                selectedGames = (
                    from game in games
                    orderby game.Followers descending
                    select game.GameName
                ).Take(count);
            }
            else
            {
                selectedGames = (
                    from game in games
                    where game.GameName.Contains(prefix)
                    orderby game.Followers descending
                    select game.GameName
                ).Take(count);
            }

            return Task.FromResult(selectedGames);
        }

        public Task<bool> HasGameNameAsync(string name)
        {
            var anyGameName = (
                from game in games
                where game.GameName.ToLower() == name.ToLower()
                select game.GameName
            );

            return Task.FromResult(anyGameName.Any());
        }
    }
}