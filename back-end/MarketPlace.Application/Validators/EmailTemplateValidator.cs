using System.Text.RegularExpressions;
using FluentValidation;
using MarketPlace.Application.DTOs;
using MarketPlace.Application.Services;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Application.Validators;

public class EmailTemplateValidator : AbstractValidator<EmailTemplateDTO>
{
    public EmailTemplateValidator(IEmailTemplateRepository emailTemplateRepository, IMessageLocalizer messageLocalizer)
    {
        RuleSet("Create", () => {
            RuleFor(x => x.Name)
                .Must(x => !string.IsNullOrEmpty(x)).WithMessage(messageLocalizer.Get("Is-Required", "Name"))
                .MinimumLength(7).WithMessage("The Name must be at least 7 characters long.")
                .MaximumLength(100).WithMessage("The Name must have a maximum of 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => !string.IsNullOrWhiteSpace(x.Description)).WithMessage("The Description must have a maximum of 500 characters.");
            RuleFor(x => x.NumberParameters)
                .NotNull().WithMessage("NumberParameters is required.")
                .NotEmpty().WithMessage("NumberParameters is required.")
                .GreaterThan((Int16)0).WithMessage("NumberParameters must be greater than zero.");
            RuleFor(x => x.HtmlBody)
                .NotNull().WithMessage("HtmlBody is required.")
                .NotEmpty().WithMessage("HtmlBody is required.")
                .Must(x => ValidateHtml(x)).WithMessage("invalid HtmlBody.");
        });
        RuleSet("Update", () => {
            RuleFor(x => x.Id)
                .NotNull().WithMessage(messageLocalizer.Get("Is-Required", "Id"));
            RuleFor(x => x)
                .Must(x => ValidateIdUpdate(emailTemplateRepository, x)).WithMessage("Id does not exist.");
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(7).WithMessage("The Name must be at least 7 characters long.")
                .MaximumLength(100).WithMessage("The Name must have a maximum of 100 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(500).When(x => !string.IsNullOrWhiteSpace(x.Description)).WithMessage("The Description must have a maximum of 500 characters.");
            RuleFor(x => x.NumberParameters)
                .NotNull().WithMessage("NumberParameters is required.")
                .NotEmpty().WithMessage("NumberParameters is required.")
                .GreaterThan((Int16)0).WithMessage("NumberParameters must be greater than zero.");
            RuleFor(x => x.HtmlBody)
                .NotNull().WithMessage("HtmlBody is required.")
                .NotEmpty().WithMessage("HtmlBody is required.")
                .Must(x => ValidateHtml(x)).WithMessage("invalid HtmlBody.");
            //RuleFor(x => x.Status)
            //    .NotNull().WithMessage("Status is required.")
            //    .NotEmpty().WithMessage("Status is required.")
            //    .MaximumLength(1).WithMessage("The Status must have a maximum of 1 characters.");
        });
    }
    private bool ValidateIdUpdate(IEmailTemplateRepository emailTemplateRepository, EmailTemplate model)
    {
        if (model.Id != null)
        {
            var result = emailTemplateRepository.Get(model.Id.Value).Result;
            if (result == null)
                return false;
        }
        return true;
    }
    private bool ValidateHtml(string html)
    {
        if (html != null)
        {
            string pattern = @"^(\s*<([a-zA-Z][a-zA-Z0-9]*)\b[^>]*>(.*?)<\/\2>\s*)+$";
            return Regex.IsMatch(html, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
        }
        return true;
    }
}
