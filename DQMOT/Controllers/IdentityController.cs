using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DQMOT.Models;
using DQMOT.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DQMOT.Controllers.Identity
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ICipherService _cipherService;
        private readonly IConfiguration _configuration;
        
        public IdentityController(
            IUsersService usersService,
            ICipherService cipherService,
            IConfiguration configuration)
        {
            _usersService = usersService;
            _cipherService = cipherService;
            _configuration = configuration;
        }
        
        [HttpPost("token")]
        public async Task<IActionResult> GetToken(
            [FromBody] UserLoginModel userLoginModel,
            CancellationToken cancellationToken = default)
        {
            var user = await _usersService.GetUserByUsernameAndPasswordAsync(userLoginModel, cancellationToken);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = _configuration["JwtSettings:Key"];
            
            if (key == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "JWT key not found");
            }
            
            var claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new(ClaimTypes.Name, user.Username)
            };

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    SecurityAlgorithms.HmacSha256Signature)
            });
            
            return Ok(tokenHandler.WriteToken(token));
        }
    }
}
