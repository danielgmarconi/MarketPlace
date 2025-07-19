using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Application.Common;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Entities;

namespace MarketPlace.Application.Interfaces
{
    public interface IEmailTemplateService
    {
        Task<MethodResponse> Get(int id);
        Task<MethodResponse> Get(EmailTemplateDTO model);
        Task<MethodResponse> Create(EmailTemplateDTO model);
        Task<MethodResponse> Update(EmailTemplateDTO model);
        Task<MethodResponse> Remove(int id);
    }
}
