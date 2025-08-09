using FluentValidation;
using MarketPlace.Application.Common;
using MarketPlace.Application.DTOs;
using MarketPlace.Application.Interfaces;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Interfaces;

namespace MarketPlace.Application.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IValidator<EmailTemplateDTO> _validator;
        public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository,
                                    IValidator<EmailTemplateDTO> validator)
        {
            _emailTemplateRepository = emailTemplateRepository;
            _validator = validator;

        }
        public async Task<MethodResponse> Create(EmailTemplateDTO model)
        {
            var result = new MethodResponse();
            if (model == null)
            {
                result.Update(400, "Bad Request");
                return result;
            }
            try
            {
                var validatorResult = await _validator.ValidateAsync(model, options => options.IncludeRuleSets("Create"));
                if (!validatorResult.IsValid)
                {
                    result.Update(500, 1, "Invalid data", validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
                    return result;
                }
                model = await _emailTemplateRepository.Create(model);
                result.Update(true, 201, "Created successfully", model);
            }
            catch (Exception e)
            {
                result.Update(500, 500, "Error", e.Message);
            }
            return result;
        }
        public async Task<MethodResponse> Get(int id)
        {
            var result = new MethodResponse();
            try
            {
                if (id <= 0)
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                var emailTemplate = await _emailTemplateRepository.Get(id);
                result.Update(true, 200, "Successfully executed", emailTemplate == null ? null : (EmailTemplateDTO)emailTemplate);
            }
            catch (Exception e)
            {
                result.Update(500, 500, "Error", e.Message);
            }
            return result;
        }
        public async Task<MethodResponse> Get(EmailTemplateDTO model)
        {
            var result = new MethodResponse();
            try
            {
                if (model == null)
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                var list = (await _emailTemplateRepository.Get(model)).Select(x => (EmailTemplateDTO)x).ToList();
                result.Update(true, 200, "Successfully executed", list);
            }
            catch (Exception e)
            {
                result.Update(500, 500,"Error", e.Message);
            }
            return result;
        }
        public async Task<MethodResponse> Remove(int id)
        {
            var result = new MethodResponse();
            if (id <= 0)
            {
                result.Update(400, "Bad Request");
                return result;
            }
            try
            {
                await _emailTemplateRepository.Remove(id);
                result.Update(true, 201, "Created successfully");
            }
            catch (Exception e)
            {
                result.Update(500, 500,"Error", e.Message);
            }
            return result;
        }
        public async Task<MethodResponse> Update(EmailTemplateDTO model)
        {
            var result = new MethodResponse();
            if (model == null)
            {
                result.Update(400, "Bad Request");
                return result;
            }
            try
            {
                var validatorResult = await _validator.ValidateAsync(model, options => options.IncludeRuleSets("Update"));
                if (!validatorResult.IsValid)
                {
                    result.Update(500, 1, "Invalid data", validatorResult.Errors.Select(e => e.ErrorMessage).ToList());
                    return result;
                }
                var entity = await _emailTemplateRepository.Get(model.Id.Value);
                entity.Update(model.Name ?? entity.Name,
                              model.Description ?? entity.Description,
                              model.NumberParameters ?? entity.NumberParameters,
                              model.HtmlBody ?? entity.HtmlBody,
                              model.Status ?? entity.Status);
                await _emailTemplateRepository.Update(entity);

                result.Update(true, 200, "Created successfully", (EmailTemplateDTO)entity);
            }
            catch (Exception e)
            {
                result.Update(500, 500,"Error", e.Message);
            }
            return result;
        }
        public async Task<MethodResponse> MailAssembler(MailAssemblerDTO model)
        {
            var result = new MethodResponse();
            try
            {
                if (model == null)
                {
                    result.Update(400, "Bad Request");
                    return result;
                }
                result.Update(true, 200, "Successfully executed",  await MailAssemblerCreate(model));
            }
            catch (Exception e)
            {
                result.Update(500, 500, "Error", e.Message);
            }
            return result;
        }
        public async Task<string> MailAssemblerCreate(MailAssemblerDTO model)
        {
            if (model.templateName == null)
                throw new Exception("TemplateName is required.");
            if (model.parmList == null || model.parmList.Length.Equals(0))
                throw new Exception("parmList is required.");            
            var list = await _emailTemplateRepository.Get(new EmailTemplate() { Name = model.templateName });
            if(list == null || list.Count.Equals(0))
                throw new Exception("EmailTemplate not found.");
            var item = list.FirstOrDefault();
            if (model.parmList.Length != item.NumberParameters.Value)
                throw new Exception("different number of parameters");
            for (int a = 0; a < item.NumberParameters; a++)
                item.HtmlBody = item.HtmlBody.Replace($"{{{a+1}}}", model.parmList[a]);
            return item.HtmlBody;
        }
    }
}
