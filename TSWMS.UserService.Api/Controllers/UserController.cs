#region Usings

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TSWMS.UserService.Api.Dto;
using TSWMS.UserService.Shared.Interfaces;
using TSWMS.UserService.Shared.Models;


#endregion

namespace TSWMS.UserService.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public UserController(IUserManager userService, IMapper mapper)
        {
            _userManager = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _userManager.GetUsersAsync();

                if (users == null || !users.Any())
                {
                    return NotFound("No users found.");
                }

                return Ok(_mapper.Map<List<UserDto>>(users));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<User>(createUserDto);

            var userId = await _userManager.CreateUserAsync(user);

            if (userId != Guid.Empty)
            {
                return Ok(new { Id = userId });
            }

            return BadRequest("User creation failed.");
        }
    }
}