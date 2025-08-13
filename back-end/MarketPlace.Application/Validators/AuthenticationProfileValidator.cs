using FluentValidation;
using MarketPlace.Application.DTOs;

namespace MarketPlace.Application.Validators
{
    public class AuthenticationProfileValidator : AbstractValidator<AuthenticationDTO>
    {
        public AuthenticationProfileValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is required.");
            RuleFor(x => x.Password)
                .NotNull().NotEmpty().WithMessage("Password is required.");

        }
    }
}
