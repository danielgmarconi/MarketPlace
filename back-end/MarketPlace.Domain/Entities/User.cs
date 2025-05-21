using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Validation;

namespace MarketPlace.Domain.Entities
{
    public sealed class User : Entity
    {
        public string? FullName { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public string? Status { get; private set; }
        public bool? IsBlocked { get; private set; }

        public User() { }
        public User(int id) 
        {
            DomainExceptionValidation.When(id <= 0, "Invalid Id value.");
            Id = id;
        }
        public User(string fullName,
                    string email,
                    string password)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            Validation();
        }
        public void Update(int id,
                           string fullName,
                           string email,
                           string password,
                           string status,
                           bool IsBlocked)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Password = password;
            Status = status;
            
            DomainExceptionValidation.When(id <= 0, "Invalid Id value.");
            Validation();
        }
        public void Validation()
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(FullName), "Invalid FullName value.");
            DomainExceptionValidation.When(FullName.Length < 7 , "FullName cannot be less than 7 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Email), "Invalid Email value.");
            DomainExceptionValidation.When(Email.Length < 10, "Email cannot be less than 10 characters");
            DomainExceptionValidation.When(Password.Length < 7, "FullName cannot be less than 7 characters");
        }

    }
}
