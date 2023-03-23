namespace GameFellowship.Data
{
    public class PostModel
    {
        private static int _postID;

        public int PostID { get; init; } = -1;
        public int CreatorID { get; init; } = -1;
        public int[] CurrentUserIDs { get; set; } = { };

        public DateTime LastUpdate { get; init; }

        public string GameName { get; set; } = "Empty Game Name";
        public string MatchType { get; set; } = "Empty Game Type";

        public string[] Requirements { get; set; } = { };
        public string Description { get; set; } = string.Empty;

        public int TotalPeople { get; set; } = 0;
        public int CurrentPeople { get; set; } = 0;

        public bool PlayNow { get; set; } = true;
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MaxValue;

        public bool AudioChat { get; set; } = false;
        public string AudioPlatform { get; set; } = "No Platform";
        public string AudioLink { get; set; } = string.Empty;

        public Conversation[] Conversations { get; set; } = { };

        public PostModel()
        {
            PostID = ++_postID;
        }

        public PostModel(DateTime update, string game, string match, string[] requirements,
            string? description, int total, int creator, int[] userIDs, DateTime? start = null, DateTime? end = null,
            string? platform = null, string? link = null, Conversation[]? conversations = null)
        {
            PostID = ++_postID;
            LastUpdate = update;
            GameName = game;
            MatchType = match;
            Requirements = requirements;
            TotalPeople = total;
            CurrentPeople = userIDs.Length;
            CreatorID = creator;
            CurrentUserIDs = userIDs;

            if (description is not null) Description = description;

            if (start is null || end is null)
            {
                PlayNow = true;
            }
            else
            {
                PlayNow = false;
                StartDate = (DateTime)start;
                EndDate = (DateTime)end;
            }

            if (platform is null || link is null)
            {
                AudioChat = false;
            }
            else
            {
                AudioChat = true;
                AudioPlatform = platform;
                AudioLink = link;
            }

            if (conversations is not null)
            {
                Conversations = conversations;
            }
        }

    }
}