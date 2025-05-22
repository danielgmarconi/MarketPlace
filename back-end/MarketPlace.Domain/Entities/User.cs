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
            DomainExceptionValidation.When(id <= 0, "Email is required.");
            Id = id;
        }
        public User(string email) 
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Email is required.");
            Email = email;
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
        public void PasswordUpdate(string password)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(password), "FullName is required.");
            DomainExceptionValidation.When(password.Length < 7, "FullName cannot be less than 7 characters");
            Password = password;
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
