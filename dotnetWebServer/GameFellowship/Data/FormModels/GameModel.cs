namespace GameFellowship.Data.FormModels;

public class GameModel
{
	public string GameName { get; init; } = string.Empty;
	public string IconURI { get; set; } = string.Empty;
	public bool Follow { get; set; } = true;

	public GameModel(string name)
	{
		GameName = name;
	}
}