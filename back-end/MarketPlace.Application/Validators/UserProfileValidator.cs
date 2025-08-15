using System.Reflection;
using System.Text.RegularExpressions;
using FluentValidation;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Application.Validators
{
    public class UserProfileValidator : AbstractValidator<UserDTO>
    {
        public UserProfileValidator(IUserRepository userRepository, IMessageLocalizer messageLocalizer)
        {
            RuleSet("Create", () => {
                RuleFor(x => x.FullName)
                    .Must(x => !string.IsNullOrEmpty(x)).WithMessage(messageLocalizer.Get("Is-Required", "FullName"))
                    .MinimumLength(7).WithMessage(messageLocalizer.Get("Min-Value", "FullName", "7"))
                    .MaximumLength(150).WithMessage(messageLocalizer.Get("Min-Value", "FullName", "150"));
                RuleFor(x => x.Email)
                    .Must(x => !string.IsNullOrEmpty(x)).WithMessage(messageLocalizer.Get("Is-Required", "Email"))
                    .EmailAddress().WithMessage(messageLocalizer.Get("Invalid", "Email"));
                RuleFor(x => x.Password)
                    .Must(x => !string.IsNullOrEmpty(x)).WithMessage(messageLocalizer.Get("Is-Required", "Password"))
                    .Matches(RegexValidator.Password)
                    .WithMessage(messageLocalizer["Invalid-Password"]);
                RuleFor(x => x)
                    .Must(x => userRepository.Get(x.Email).Result == null).WithMessage(messageLocalizer["Email-Exists"]);
            });
            RuleSet("Update", () => {
                RuleFor(x => x)
                    .Must(x => x == null || x.Id < 1).WithMessage(messageLocalizer.Get("Invalid", "Id"));
                RuleFor(x => x.FullName)
                    .Must(x => !string.IsNullOrEmpty(x)).WithMessage(messageLocalizer.Get("Is-Required", "FullName"))
                    .MinimumLength(7).WithMessage(messageLocalizer.Get("Min-Value", "FullName", "7"))
                    .MaximumLength(150).WithMessage(messageLocalizer.Get("Min-Value", "FullName", "150"));
                RuleFor(x => x.Email)
                    .Must(x => !string.IsNullOrEmpty(x)).WithMessage(messageLocalizer.Get("Is-Required", "Email"))
                    .EmailAddress().WithMessage(messageLocalizer.Get("Invalid", "Email"));
                RuleFor(x => x)
                   .Must(x => ValidateEmailUpdate(userRepository, x)).WithMessage(messageLocalizer["Email-Different-Registered"]);
                RuleFor(x => x)
                    .Must(x => ValidatePasswordUpdate(x))
                    .WithMessage(messageLocalizer["Invalid-Password"]);
            });
            RuleSet("ChangePassword", () => {
                RuleFor(x => x)
                    .Must(x => x.Id != null || !string.IsNullOrWhiteSpace(x.UserGuid))
                    .WithMessage(messageLocalizer["Enter-Id-UserGuid"]);
                RuleFor(x => x.UserGuid)
                    .Must(x => ValidateGuid(x))
                    .WithMessage(messageLocalizer.Get("Invalid", "UserGuid"));
                RuleFor(x => x.Password)
                    .Must(x => string.IsNullOrWhiteSpace(x)).WithMessage(messageLocalizer.Get("Is-Required", "Password"))
                    .Matches(RegexValidator.Password)
                    .WithMessage(messageLocalizer["Invalid-Password"]);

            });
        }
        private bool ValidateEmailUpdate(IUserRepository userRepository, UserDTO model)
        {
            if (model.Id != null && model.Email != null)
            {
                var result = userRepository.Get(model.Id.Value).Result;
                if (result != null && !result.Email.Equals(model.Email))
                    return false;
            }
            return true;
        }
        private bool ValidatePasswordUpdate(UserDTO model)
        {
            if (model.Password != null)
                return Regex.IsMatch(model.Password, RegexValidator.Password);
            return true;
        }
        private bool ValidateGuid(string? guid)
        { 
            if(guid != null)
                return Regex.IsMatch(guid, RegexValidator.Guid);
            return true;
        }
    }
}
