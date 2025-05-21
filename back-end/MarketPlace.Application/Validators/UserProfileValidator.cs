using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MarketPlace.Application.DTOs;

namespace MarketPlace.Application.Validators
{
    public class UserProfileValidator : AbstractValidator<UserDTO>
    {
        public UserProfileValidator() 
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter ao menos 3 caracteres.");
        }
    }
}
