using System.ComponentModel.DataAnnotations;

namespace GameFellowship.Data
{
    public class PostGameValidatorAttribute : ValidationAttribute
    {
        private readonly int _nameLength;
        private readonly string _type;

        public PostGameValidatorAttribute(int length, string type)
        {
            _nameLength = length;
            _type = type;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || string.IsNullOrWhiteSpace((string)value))
            {
                return new ValidationResult($"游戏{_type}是必须的捏");
            }

            if (value.ToString().Length > _nameLength)
            {
                return new ValidationResult($"游戏{_type}请少于{_nameLength}个字符");
            }

            return ValidationResult.Success;
        }
    }

    public class PostPeopleValidatorAttribute : ValidationAttribute
    {
        private readonly int _minium;
        private readonly int _maximum;

        public PostPeopleValidatorAttribute(int min, int max)
        {
            _minium = min;
            _maximum = max;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || value is not int)
            {
                return new ValidationResult("人数不能为空");
            }
            
            if ((int)value < _minium)
            {
                return new ValidationResult($"人数不得少于{_minium}，请算上自己");
            }

            if ((int)value > _maximum)
            {
                return new ValidationResult($"人数请少于{_maximum}，已经塞不下了");
            }

            return ValidationResult.Success;
        }
    }

    public class PostDateValidatorAttribute : ValidationAttribute
    {
        private readonly DateTime _latestStartDate;
        private readonly int _intervalDays;
        private readonly bool _isStartDate;

        public PostDateValidatorAttribute(int intervalDays, bool isStartDate)
        {
            _intervalDays = intervalDays;
            _isStartDate = isStartDate;

            _latestStartDate = DateTime.Today + TimeSpan.FromDays(_intervalDays);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || value is not DateTime)
            {
                return new ValidationResult("请是个正常日期");
            }

            if ((DateTime)value < DateTime.Today)
            {
                return new ValidationResult("时间点不能设在过去");
            }

            if (_isStartDate && (DateTime)value > _latestStartDate)
            {
                return new ValidationResult($"请把开始时间设在{_intervalDays}天内");
            }

            return ValidationResult.Success;
        }
    }

    public class PostCompareValidatorAttribute : ValidationAttribute
    {
        private readonly string _smallerType;
        private readonly string _largerType;

        public PostCompareValidatorAttribute(string smaller, string larger)
        {
            _smallerType = smaller;
            _largerType = larger;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || !(bool)value)
            {
                return new ValidationResult($"{_smallerType}不应该大于{_largerType}捏");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }

    public class PostAcceptNullValidatorAttribute : ValidationAttribute
    {
        private readonly string _acceptNullName;

        public PostAcceptNullValidatorAttribute(string acceptNullName)
        {
            _acceptNullName = acceptNullName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null || !(bool)value)
            {
                return new ValidationResult($"当前选择下，{_acceptNullName}不可为空捏");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
