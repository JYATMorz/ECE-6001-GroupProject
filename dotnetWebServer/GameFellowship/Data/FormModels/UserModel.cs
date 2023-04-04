using GameFellowship.Data.Services;

namespace GameFellowship.Data.FormModels;

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
	[UserExistValidator("ÓÃ»§Ãû")]
	public bool UserNameExisted { get; private set; } = false;

	private string _userEmail = string.Empty;
	private string _passwordRepeat = string.Empty;

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
	[UserExistValidator("ÓÊÏä")]
	public bool UserEmailExisted { get; private set; } = false;

	[UserPasswordValidator(6, 20)]
	public string UserPassword { get; set; } = string.Empty;
	public bool PasswordModified { get; set; } = false;
	public string PasswordRepeat
	{
		get => _passwordRepeat;
		set
		{
			_passwordRepeat = value;
			if (!PasswordModified) PasswordModified = true;
		}
	}
	[UserPasswordCheckValidator]
	public bool ValidPassword => UserPassword == PasswordRepeat;

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