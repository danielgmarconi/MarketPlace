using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MarketPlace.Application.DTOs;

namespace MarketPlace.Application.Validators
{
    public class AuthenticationProfileValidator : AbstractValidator<AuthenticationDTO>
    {
    }
}
