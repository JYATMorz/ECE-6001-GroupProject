namespace GameFellowship.Data
{
    public class UserModel
    {
        private readonly IUserService userService;

        private string _userName = string.Empty;
		[UserNameValidator(12)]
		public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnUserNameChange();
			}
        }
		[UserExistValidator("�û���")]
		public bool UserNameExisted { get; private set; } = false;

		private string _userEmail = string.Empty;
		[UserEmailValidator]
		public string UserEmail
		{
			get => _userEmail;
			set
			{
				_userEmail = value;
				OnUserEmailChange();
			}
		}
		[UserExistValidator("����")]
		public bool UserEmailExisted { get; private set; } = false;

		[UserPasswordValidator(6, 20)]
		public string Password { get; set; } = string.Empty;
		// public bool ValidPassword => !string.IsNullOrWhiteSpace(Password);

        public string UserIconURI { get; set; } = string.Empty;

		public UserModel(IUserService service)
        {
			userService = service;
        }

		private async void OnUserNameChange()
		{
            if (!string.IsNullOrWhiteSpace(UserName))
			{
                UserNameExisted = await userService.HasUserAsync(UserName);
            }
			else
			{
				UserNameExisted = false;
			}
		}

		private async void OnUserEmailChange()
		{
			if (!string.IsNullOrWhiteSpace(UserEmail))
            {
                UserEmailExisted = await userService.HasEmailAsync(UserEmail);
            }
			else
			{
				UserEmailExisted = false;
			}
		}
	}
}