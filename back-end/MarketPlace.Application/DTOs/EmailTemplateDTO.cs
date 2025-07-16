using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Entities;

namespace MarketPlace.Application.DTOs
{
    public class EmailTemplateDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Int16? NumberParameters { get; set; }
        public string? HtmlBody { get; set; }
        public string? Status { get; set; }
        public static implicit operator EmailTemplateDTO(EmailTemplate emailTemplate) => new EmailTemplateDTO
        {
            Id = emailTemplate.Id,
            Name = emailTemplate.Name,
            Description = emailTemplate.Description,
            NumberParameters = emailTemplate.NumberParameters,
            HtmlBody = emailTemplate.HtmlBody,
            Status = emailTemplate.Status
        };

        public static implicit operator EmailTemplate(EmailTemplateDTO dto) => new EmailTemplate
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            NumberParameters = dto.NumberParameters,
            HtmlBody = dto.HtmlBody,
            Status = dto.Status
        };
    }
}
