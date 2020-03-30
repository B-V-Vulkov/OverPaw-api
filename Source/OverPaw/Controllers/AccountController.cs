namespace OverPaw.Controllers
{
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using OverPaw.Configuration;
    using OverPaw.InputModels.Account;
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
            // Data Base validation
            if (loginUser.UserName != "Bobi")
            {
                return Unauthorized();
            }

            // Authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.jwtSettings);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, loginUser.UserName),
                    new Claim(ClaimTypes.NameIdentifier, "secretPasss1234")
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenHandler.WriteToken(token);

            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            return Ok(1234);
        }
    }
}