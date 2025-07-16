using MarketPlace.Domain.Entities;

namespace MarketPlace.Domain.Interfaces
{
    public  interface IEmailTemplateRepository
    {
        Task<EmailTemplate> Get(int id);
        Task<List<EmailTemplate>> Get(EmailTemplate model);
        Task<EmailTemplate> Create(EmailTemplate model);
        Task Update(EmailTemplate model);
        Task Remove(int id);
    }
}
