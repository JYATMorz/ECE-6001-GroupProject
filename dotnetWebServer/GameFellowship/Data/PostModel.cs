using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data
{
    public class PostModel
    {
        private readonly int _resultCount;
        private readonly IGameService _gameServiceModel;

        private string _gameName = string.Empty;
        [PostGameValidator(10, "名")]
        public string GameName
        {
            get => _gameName;
            set
            {
                _gameName = value;
                OnNameChangeAsync();
            }
        }
        public IEnumerable<string>? GameNameList { get; set; }

        [PostGameValidator(10, "类型")]
        public string MatchType { get; set; } = string.Empty;
        public string[]? MatchTypeList { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "队伍要求请少于等于5个捏")]
        [MinLength(1, ErrorMessage = "至少提1个队伍要求吧")]
        public string[] Requirements { get; set; } = Array.Empty<string>();

        [StringLength(50, ErrorMessage = "描述请精简地少于50个字")]
        public string Description { get; set; } = string.Empty;

        [PostPeopleValidator(2, 100)]
        public int TotalPeople { get; set; } = 2;
        [PostPeopleValidator(1, 100)]
        public int CurrentPeople { get; set; } = 1;
        [PostCompareValidator("当前人数","需要人数")]
        public bool ValidPeople => CurrentPeople <= TotalPeople;

        [Required]
        public bool PlayNow { get; set; } = true;
        [PostDateValidator(30, true)]
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime StartTime { get; set; } = DateTime.Now;
        [PostDateValidator(30, false)]
        public DateTime EndDate { get; set; } = DateTime.Today;
        public DateTime EndTime { get; set; } = DateTime.Now;
        [PostCompareValidator("开始日期", "结束日期")]
        public bool ValidDate => StartDate < EndDate ||
                                 (StartDate == EndDate && StartTime <= EndTime);

        [Required]
        public bool AudioChat { get; set; }
        [StringLength(8, ErrorMessage = "语音平台名字请少于8个字")]
        public string AudioPlatform { get; set; } = string.Empty;
        [PostAcceptNullValidator("语音平台")]
        public bool ValidAudioPlatform => !AudioChat || !string.IsNullOrWhiteSpace(AudioPlatform);
        public string[]? AudioPlatformList { get; set; }
        [StringLength(20, ErrorMessage = "链接/号码长度请少于20字符")]
        public string AudioLink { get; set; } = string.Empty;
        [PostAcceptNullValidator("语音地址")]
        public bool ValidAudioLink => !AudioChat || !string.IsNullOrWhiteSpace(AudioLink);

        private async void OnNameChangeAsync()
        {
            GameNameList = await _gameServiceModel.GetGameNamesAsync(_resultCount, GameName);
        }

        public PostModel(int count, IGameService gameService)
        {
            _resultCount = count;
            _gameServiceModel = gameService;
        }
    }
}
