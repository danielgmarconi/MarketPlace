using MarketPlace.Application.DTOs;
using MarketPlace.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IEmailTemplateService _emailTemplateService;
        public EmailTemplateController(IEmailTemplateService emailTemplateService)
        {
            _emailTemplateService = emailTemplateService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] EmailTemplateDTO dto)
        {
            var result = await _emailTemplateService.Get(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _emailTemplateService.Get(id);
            return StatusCode(200, result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmailTemplateDTO dto)
        {
            var result = await _emailTemplateService.Create(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmailTemplateDTO dto)
        {
            var result = await _emailTemplateService.Update(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _emailTemplateService.Remove(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
