using GameFellowship.Data.Database;

namespace GameFellowship.Data.DtoModel;

public readonly record struct ConversationDto
{
    public string CreatorName { get; init; }
    public string CreatorIconUri { get; init; }
    public DateTime SendTime { get; init; }
    public string Context { get; init; }

    public ConversationDto(Conversation conversation, User? creator = null)
    {
        CreatorName = creator?.Name ?? conversation.Creator.Name;
        CreatorIconUri = creator?.IconURI ?? conversation.Creator.IconURI;
        SendTime = conversation.SendTime.ToLocalTime();
        Context = conversation.Context;
    }
}
