namespace GameFellowship.Data.FormModels;

public class ConversationModel
{
	public DateTime SendTime { get; set; } = DateTime.Now;
	public string Context { get; set; } = string.Empty;
	public int PostId { get; set; } = -1;
	public int CreatorId { get; set; } = -1;

	public ConversationModel() { }

	public ConversationModel(DateTime sendTime, string context, int postId, int creatorId)
	{
		SendTime = sendTime;
		Context = context;
		PostId = postId;
		CreatorId = creatorId;
	}
}