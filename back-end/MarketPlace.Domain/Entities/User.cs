﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Validation;

namespace MarketPlace.Domain.Entities
{
    public sealed class User : Entity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Status { get; set; }
        public bool? IsBlocked { get; set; }

        public User() { }
        public User(string fullName,
                    string email,
                    string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Id = null;
            Validation();
        }
        public void Update(string fullName,
                           string email,
                           string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            CreationDate = null;
            ModificationDate = null;
            Validation();
        }
        public void Validation()
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(FullName), "FullName is required.");
            DomainExceptionValidation.When(FullName.Length < 7 , "FullName cannot be less than 7 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Email), "Email is required.");
            DomainExceptionValidation.When(Email.Length < 10, "Email cannot be less than 10 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Password), "FullName is required.");
            DomainExceptionValidation.When(Password.Length < 7, "FullName cannot be less than 7 characters");
        }

    }
}
