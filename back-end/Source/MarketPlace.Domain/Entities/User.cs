using MarketPlace.Domain.Validation;

namespace MarketPlace.Domain.Entities
{
    public sealed class User : Entity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool IsAdmin { get; private set; }
        public bool IsActive { get; private set; }
        public User(string name,
                    string email,
                    string password,
                    bool isadmin,
                    bool isactive)
        {
            ValidateDomain(name, email, password, isactive);
            IsAdmin = isadmin;
            DateCreate = DateTime.Now;
        }
        private void Update(int id,
                            string name,
                            string password,
                            bool isactive)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            IdUserUpdate = id;
            DateUpdate = DateTime.Now;
            ValidateDomain(name, Email, password, isactive);

            
        }

        private void ValidateDomain(string name,
                                    string email,
                                    string password,
                                    bool isactive)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                                           "Invalid name. Name is required");
            DomainExceptionValidation.When(!(name.Length >= 3 && name.Length<=200),
                                           "Invalid name, must be greater than or equal to 3 and less than 200 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(email),
                                           "Invalid email. Name is required");
            DomainExceptionValidation.When(!(email.Length >= 10 && email.Length <= 250),
                                            "Invalid email, must be greater than or equal to 10 and less than 250 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(password),
                                           "Invalid password. Name is required");
            Name = name;
            Email = email;
            Password = password;
            IsActive = isactive;
        }

    }
}
