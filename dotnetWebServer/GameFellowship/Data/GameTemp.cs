using GameFellowship.Data.FormModels;

namespace GameFellowship.Data;

public class GameTemp
{
    private static int _gameID;

    public int GameID { get; init; } = -1;
    public string GameName { get; set; } = "Empty Name";
    public int Followers { get; set; } = 0;
    public string IconURI { get; set; } = "images/GameIcons/gametitle.jpg";
    public DateTime LastPostDate { get; set; } = DateTime.MinValue;

    public GameTemp()
    {
        GameID = ++_gameID;
    }

    public GameTemp(GameModel model)
    {
        GameID = ++_gameID;

        GameName = model.GameName;
        Followers = model.Follow ? 1 : 0;
        IconURI = model.IconURI;
    }

    public GameTemp(string name, int followers, DateTime? lastPost = null, string? icon = null)
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