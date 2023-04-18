using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GameFellowship.Data.FormModel;

public class UserNameValidatorAttribute : ValidationAttribute
{
	private readonly int _nameLength;
	// valid if {number, character, '.', '@', '-'}
	private readonly string _nameInvalidPattern = @"[^\w\.@-]";

	public UserNameValidatorAttribute(int nameLength)
	{
		_nameLength = nameLength;
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		if (value is null || string.IsNullOrWhiteSpace((string)value))
		{
			return new ValidationResult($"名字是必填的捏");
		}
		string userName = (string)value;

		if (userName.Length > _nameLength)
		{
			return new ValidationResult($"用户名请少于{_nameLength}个字符");
		}

		try
		{
			if (Regex.IsMatch(userName, _nameInvalidPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500)))
			{
				return new ValidationResult("无效的用户名");
			}
		}
		catch (RegexMatchTimeoutException)
		{
			return new ValidationResult("你觉得为什么用户名解析超时了捏(*^_^*)");
		}

		return ValidationResult.Success;
	}
}

public class UserEmailValidatorAttribute : ValidationAttribute
{
	private readonly string _emailDomainPattern = @"(@)(.+)$";
	private readonly string _emailValidPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		if (value is not null)
		{
			string email = (string)value;

			if (email.Length == 0)
			{
				return ValidationResult.Success;
			}
			else if (string.IsNullOrWhiteSpace(email))
			{
				return new ValidationResult("邮箱不可以是空格捏");
			}

			// https://learn.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
			try
			{
				// Normalize the domain
				email = Regex.Replace(email, _emailDomainPattern, DomainMapper,
									  RegexOptions.None, TimeSpan.FromMilliseconds(200));

				// Examines the domain part of the email and normalizes it.
				string DomainMapper(Match match)
				{
					// Use IdnMapping class to convert Unicode domain names.
					var idn = new IdnMapping();
					// Pull out and process domain name (throws ArgumentException on invalid)
					string domainName = idn.GetAscii(match.Groups[2].Value);

					return match.Groups[1].Value + domainName;
				}
			}
			catch (RegexMatchTimeoutException)
			{
				return new ValidationResult("你觉得为什么邮箱地址解析超时了捏(*^_^*)");
			}
			catch (ArgumentException)
			{
				return new ValidationResult("无效的邮箱地址");
			}

			try
			{
				if (!Regex.IsMatch(email, _emailValidPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
				{
					return new ValidationResult("无效的邮箱地址");
				}
			}
			catch (RegexMatchTimeoutException)
			{
				return new ValidationResult("你觉得为什么邮箱地址解析超时了捏(*^_^*)");
			}
		}

		// email can be null
		return ValidationResult.Success;
	}
}

public class UserPasswordValidatorAttribute : ValidationAttribute
{
	private readonly int _passwordMin;
	private readonly int _passwordMax;
	// valid if {number, character, '!', '?', '@', '-'}
	private readonly string _passwordInvalidPattern = @"[^\w\!?@-]";

	public UserPasswordValidatorAttribute(int passwordMin, int passwordMax)
	{
		_passwordMin = passwordMin;
		_passwordMax = passwordMax;
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		if (value is null || string.IsNullOrWhiteSpace((string)value))
		{
			return new ValidationResult($"密码是必填的捏");
		}
		string password = (string)value;

		if (password.Length < _passwordMin)
		{
			return new ValidationResult($"密码不得短于{_passwordMin}字符");
		}
		if (password.Length > _passwordMax)
		{
			return new ValidationResult($"密码不得长于{_passwordMax}字符");
		}

		try
		{
			if (Regex.IsMatch(password, _passwordInvalidPattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500)))
			{
				return new ValidationResult("密码不得包含非法字符");
			}
		}
		catch (RegexMatchTimeoutException)
		{
			return new ValidationResult("你觉得为什么密码解析超时了捏(*^_^*)");
		}

		return ValidationResult.Success;
	}
}

public class UserExistValidatorAttribute : ValidationAttribute
{
	private readonly string _existType;

	public UserExistValidatorAttribute(string type)
	{
		_existType = type;
	}

	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		if (value is not null && !(bool)value)
		{
			return ValidationResult.Success;
		}
		else
		{
			return new ValidationResult($"有没有可能当前{_existType}已经有人用了😊");
		}
	}
}

public class UserValidCheckValidatorAttribute : ValidationAttribute
{
	protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
	{
		if (value is not null && (bool)value)
		{
			return ValidationResult.Success;
		}
		else
		{
			return new ValidationResult("内容不符合要求");
		}
	}
}