namespace OverPaw.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using OverPaw.Configuration;
    using OverPaw.InputModels.Account;
    using OverPaw.Models;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly string jwtSettings;

        public AccountController(IOptions<JwtSettings> jwtSettings)
        {
            this.jwtSettings = jwtSettings.Value.Secret;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserInputModel loginUser)
        {
            var user = new LoginUserModel();

            // Data Base validation
            if (loginUser.Password != "1234")
            {
                user.Status = 0;
                return Ok(user);
            }

            // Authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtSettings);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(1),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, loginUser.Email),
                    new Claim(ClaimTypes.Role, "User"),
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string jwt = tokenHandler.WriteToken(token);

            user.Status = 1;
            user.Token = jwt;

            return Ok(user);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            return Ok(1234);
        }
    }
}