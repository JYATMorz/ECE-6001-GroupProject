using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data
{
    public class PostModel
    {
        private readonly int resultCount = 5;
        private readonly GameService gameServiceModel;
        private readonly PostService postServiceModel;

        private string? _gameName;
        [PostGameValidator(10, "名")]
        public string? GameName
        {
            get => _gameName;
            set
            {
                _gameName = value;
                OnNameChangeAsync();
            }
        }
        public string[]? GameNameList { get; set; }

        [PostGameValidator(10, "类型")]
        public string? MatchType { get; set; }
        public string[]? MatchTypeList { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "队伍要求请少于等于5个捏")]
        [MinLength(1, ErrorMessage = "至少提1个队伍要求吧")]
        public string[] Requirements { get; set; } = Array.Empty<string>();

        [StringLength(50, ErrorMessage = "描述请精简地少于50个字")]
        public string? Description { get; set; }

        [Required]
        [PostPeopleValidator(2, 100)]
        public int TotalPeople { get; set; }
        [Required]
        [PostPeopleValidator(1, 100)]
        public int CurrentPeople { get; set; }

        [Required]
        public bool PlayNow { get; set; } = false;
        [PostDateValidator(30, true)]
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime StartTime { get; set; } = DateTime.Now;
        [PostDateValidator(30, false)]
        public DateTime EndDate { get; set; } = DateTime.Today;
        public DateTime EndTime { get; set; } = DateTime.Now;

        [Required]
        public bool AudioChat { get; set; }
        [StringLength(8, ErrorMessage = "语音平台名字请少于8个字")]
        public string? AudioPlatform { get; set; }
        public string[]? AudioPlatformList { get; set; }
        [StringLength(20, ErrorMessage = "链接/号码长度请少于20字符")]
        public string? AudioLink { get; set; }

        private async void OnNameChangeAsync()
        {
            GameNameList = await gameServiceModel.GetGameNamesAsync(resultCount, GameName);
            MatchTypeList = await postServiceModel.GetMatchTypesAsync(resultCount, GameName);
            AudioPlatformList = await postServiceModel.GetAudioPlatformsAsync(resultCount, GameName);
        }

        public PostModel(GameService gameService, PostService postService)
        {
            gameServiceModel = gameService;
            postServiceModel = postService;
        }
    }
}
