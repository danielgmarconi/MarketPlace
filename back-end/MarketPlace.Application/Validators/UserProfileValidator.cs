using System.Reflection;
using System.Text.RegularExpressions;
using FluentValidation;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Application.Validators
{
    public class UserProfileValidator : AbstractValidator<UserDTO>
    {
        public UserProfileValidator(IUserRepository userRepository)
        {
            RuleSet("Create", () => {
                RuleFor(x => x.FullName)
                    .NotNull().NotEmpty().WithMessage("FullName is required.")
                    .MinimumLength(7).WithMessage("The FullName mu  st be at least 7 characters long.")
                    .MaximumLength(150).WithMessage("The FullName must have a maximum of 150 characters.");
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Email is required.");
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage("Password is required.")
                    .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$")
                    .WithMessage("The Password must be between 8 and 20 characters long, including at least one uppercase letter, one lowercase letter, one number, and one special character.");
                RuleFor(x => x)
                    .Must(x => userRepository.Get(x.Email).Result == null ).WithMessage("Email already exists.");
            });
            RuleSet("Update", () => {
                RuleFor(x => x.FullName)
                    .MinimumLength(7).When(x => !string.IsNullOrEmpty(x.FullName)).WithMessage("The FullName must be at least 7 characters long.")
                    .MaximumLength(150).When(x => !string.IsNullOrEmpty(x.FullName)).WithMessage("The FullName must have a maximum of 150 characters.");
                RuleFor(x => x)
                    .Must(x => x == null || x.Id >0).WithMessage("Invalid Id.");
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("Email is required.")
                    .EmailAddress().WithMessage("Email is required.");
                 RuleFor(x => x)
                    .Must(x => ValidateEmailUpdate(userRepository, x)).WithMessage("Email is different from the one registered");
                RuleFor(x => x)
                    .Must(ValidatePasswordUpdate)
                    .WithMessage("The Password must be between 8 and 20 characters long, including at least one uppercase letter, one lowercase letter, one number, and one special character.");

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
                return Regex.IsMatch(model.Password, "^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,20}$");
            return true;
        }
    }
}
