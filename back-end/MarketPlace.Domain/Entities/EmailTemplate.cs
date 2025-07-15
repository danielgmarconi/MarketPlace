using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MarketPlace.Domain.Validation;

namespace MarketPlace.Domain.Entities
{
    public class EmailTemplate
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Int16? NumberParameters { get; set; }
        public string? HtmlBody { get; set; }
        public string? Status { get; set; }
        public EmailTemplate() { }
        public EmailTemplate(string? name,
                             string? description,
                             Int16? numberParameters,
                             string? htmlBody,
                             string? status)
        {
            Name = name;
            Description = description;
            NumberParameters = numberParameters;
            HtmlBody = htmlBody;
            Status = status;
        }
        public void Update(string? name,
                           string? description,
                           Int16? numberParameters,
                           string? htmlBody,
                           string? status)
        {
            Name = name;
            Description = description;
            NumberParameters = numberParameters;
            HtmlBody = htmlBody;
            Status = status;
            Validation();
        }
        public void Validation()
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(Name), "Name is required.");
            DomainExceptionValidation.When(Name.Length < 7, "Name cannot be less than 7 characters");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Description), "Description is required.");
            DomainExceptionValidation.When(Description.Length < 7, "Description cannot be less than 7 characters");
            DomainExceptionValidation.When(NumberParameters == null, "NumberParameters is required.");
            DomainExceptionValidation.When(NumberParameters != null && NumberParameters.Value  == 0, "NumberParameters must be greater than 0");
            DomainExceptionValidation.When(string.IsNullOrEmpty(HtmlBody), "HtmlBody is required.");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Status), "Status is required.");
        }
    }
}
