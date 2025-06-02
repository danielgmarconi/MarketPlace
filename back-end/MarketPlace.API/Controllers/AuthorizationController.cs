using MarketPlace.Application.DTOs;
using MarketPlace.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthorizationController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("Authentication")]
        [AllowAnonymous]
        public async Task<IActionResult> Authentication([FromBody] AuthenticationDTO authenticationDTO)
        {
            var result = await _userService.Authentication(authenticationDTO);
            return StatusCode(result.StatusCode, result);
        }
    }
}
