using DataAccessLayer.SqlServerAdapter;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Infra.Data.Repositories;

public class EmailTemplateRepository: IEmailTemplateRepository
{
    private readonly ISQLServerAdapter _sqlServerAdapter;
    public EmailTemplateRepository(ISQLServerAdapter sqlServerAdapter)
    {
        _sqlServerAdapter = sqlServerAdapter;
    }
    public async Task<EmailTemplate> Create(EmailTemplate model)
    {
        _sqlServerAdapter.Open();
        var id = await _sqlServerAdapter.ExecuteScalarAsync("spEmailTemplatesCreate", model);
        return await Get(int.Parse(id.ToString()));
    }
    public async Task Remove(int id)
    {
        _sqlServerAdapter.Open();
        await _sqlServerAdapter.ExecuteNonQueryAsync("spEmailTemplatesRemove", new User() { Id = id });
    }
    public async Task<EmailTemplate> Get(int id)
    {
        var result = await Get(new EmailTemplate() { Id = id });
        return result.FirstOrDefault();
    }
    public async Task<List<EmailTemplate>> Get(EmailTemplate model)
    {
        _sqlServerAdapter.Open();
        return await _sqlServerAdapter.ExecuteReaderAsync<EmailTemplate>("spEmailTemplatesSelect", model);
    }
    public async Task Update(EmailTemplate model)
    {
        _sqlServerAdapter.Open();
        await _sqlServerAdapter.ExecuteNonQueryAsync("spEmailTemplatesUpdate", model);
    }
}
