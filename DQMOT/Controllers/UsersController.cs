using DQMOT.Models;
using DQMOT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DQMOT.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUsersService _usersService;
        
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(
            [FromBody] UserRegisterModel userRegisterModel,
            CancellationToken cancellationToken = default)
        {
            var possibleUser = await _usersService.GetUserByUsernameAsync(userRegisterModel.Username, cancellationToken);
            if (possibleUser != null)
            {
                return BadRequest("User with this username already exists");
            }
            
            await  _usersService.CreateUserAsync(userRegisterModel, cancellationToken);
            return Ok();
        }
    }
}
