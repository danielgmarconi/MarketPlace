using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Application.Validators
{
    public class UserProfileValidator : AbstractValidator<UserDTO>
    {
        public UserProfileValidator(IUserRepository userRepository)
        {
            //RuleFor(x => x.Id)
            //    .Must(id=> ValidateId(userRepository, id)).WithMessage("Id does not exist.");
            //RuleFor(x => x)
            //    .Must(x => (x.Id!=null && x.Email!=null && !userRepository.Get(x.Id.Value).Result.FirstOrDefault().Email.Equals(x.Email)).WithMessage("Email already exists11.");
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName is required.")
                .MinimumLength(7).WithMessage("The FullName must be at least 7 characters long.")
                .MaximumLength(150).WithMessage("The FullName must have a maximum of 150 characters.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is required.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is required.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$")
                .WithMessage("The Password must be between 8 and 20 characters long, including at least one uppercase letter, one lowercase letter, one number, and one special character.");
            //RuleFor(x => x)
            //    .Must(x=> x.Id==null && userRepository.Get(x.Email).Result.Count == 0).WithMessage("Email already exists.");
        }
        //private bool ValidateId(IUserRepository userRepository, int? id)
        //{
        //    if (id != null)
        //        return userRepository.Get(id.Value).Result.Count > 0;
        //    return true;
        //}
    }
}
