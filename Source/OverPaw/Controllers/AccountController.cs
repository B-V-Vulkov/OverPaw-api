namespace OverPaw.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using OverPaw.RequestModels.Account;
    using OverPaw.Services.Contracts;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly string jwtSettings;
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequestModel requestModel)
        {
            var resulr = await this.accountService.RegisterUserAsync(requestModel);

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequestModel requestModel)
        {
            var loginResult = await this.accountService.LoginUserAsync(requestModel);

            return Ok(loginResult);
        }













        //[HttpPost("login")]
        //public async Task<IActionResult> Login(LoginRequestModel requestModel)
        //{
        //    var user = new LoginUserModel();

        //    // Data Base validation
        //    if (loginUser.Password != "1234")
        //    {
        //        user.Status = 0;
        //        return Ok(user);
        //    }

        //    // Authentication successful so generate jwt token
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(this.jwtSettings);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Expires = DateTime.UtcNow.AddDays(1),
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Email, loginUser.Email),
        //            new Claim(ClaimTypes.Role, "User"),
        //        }),
        //        SigningCredentials = new SigningCredentials(
        //            new SymmetricSecurityKey(key),
        //            SecurityAlgorithms.HmacSha256Signature
        //        )
        //    };

        //    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        //    string jwt = tokenHandler.WriteToken(token);

        //    user.Status = 1;
        //    user.Token = jwt;

        //    return Ok(user);
        //}





    }
}