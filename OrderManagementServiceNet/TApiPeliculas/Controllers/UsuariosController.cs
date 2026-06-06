using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using OrderManagementService.Application.Dtos;
using OrderManagementService.Application.Interfaces;
using OrderManagementService.Core.Entities;

namespace OrderManagementService.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UsersController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var secretKey = _config.GetValue<string>("ApiSettings:Secreta")!;
            var response = await _userService.LoginAsync(dto, secretKey);

            if (response.User == null || string.IsNullOrEmpty(response.Token))
            {
                var apiResponse = new ApiResponse { StatusCode = HttpStatusCode.BadRequest, IsSuccess = false };
                apiResponse.ErrorMessages.Add("Invalid username or password");
                return BadRequest(apiResponse);
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.RegisterAsync(dto);
            if (user == null)
            {
                var apiResponse = new ApiResponse { StatusCode = HttpStatusCode.BadRequest, IsSuccess = false };
                apiResponse.ErrorMessages.Add("Registration failed. Username may already exist.");
                return BadRequest(apiResponse);
            }

            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }
    }
}
