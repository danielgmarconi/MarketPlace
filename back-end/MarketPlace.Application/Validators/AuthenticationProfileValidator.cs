using FluentValidation;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Application.Validators
{
    public class AuthenticationProfileValidator : AbstractValidator<AuthenticationDTO>
    {
        public AuthenticationProfileValidator(IMessageLocalizer messageLocalizer)
        {
            RuleFor(x => x.Email)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage(messageLocalizer.Get("Is-Required", "Email"))
                .EmailAddress().WithMessage(messageLocalizer.Get("Invalid", "Email"));
            RuleFor(x => x.Password)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage(messageLocalizer.Get("Is-Required", "Password")); 

        }
    }
}
