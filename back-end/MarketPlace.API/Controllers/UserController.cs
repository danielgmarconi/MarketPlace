using MarketPlace.Application.DTOs;
using MarketPlace.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserDTO dto)
        {
            var result = await _userService.Get(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _userService.Get(id);
            return StatusCode(200, result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO dto)
        {
            var result = await _userService.Create(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDTO dto)
        {
            var result = await _userService.Update(dto);
            return StatusCode(result.StatusCode, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Remove(id);
            return StatusCode(result.StatusCode, result);
        }
    }
}
