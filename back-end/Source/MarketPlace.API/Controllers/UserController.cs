using MarketPlace.API.Common;
using MarketPlace.Application.Common;
using MarketPlace.Application.DTOs;
using MarketPlace.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.GetUsers();
            return StatusCode(result.StatusCode, result.StatusCode == 200 ? result.Response : result.Message);
        }
        [HttpGet("{id:int}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound("Users not found");
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO userDto)
        {
            if (userDto == null)
                return BadRequest("Invalid Data");
            await _userService.Create(userDto);
            return new CreatedAtRouteResult("GetUser", new { id = userDto.Id }, userDto);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDTO userDto)
        {
            if (userDto == null)
                return BadRequest("Invalid Data");
            await _userService.Update(userDto);
            return Ok(userDto);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null)
                return NotFound("Users not found");
            await _userService.Remove(id);
            return Ok(user);
        }
    }
}
