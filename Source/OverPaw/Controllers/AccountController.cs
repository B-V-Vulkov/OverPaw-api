namespace OverPaw.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Services.Contracts;
    using RequestModels.Account;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequestModel requestModel)
        {
            var registerResult = await this.accountService.RegisterUserAsync(requestModel);

            return Ok(registerResult);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequestModel requestModel)
        {
            var loginResult = await this.accountService.LoginUserAsync(requestModel);

            return Ok(loginResult);
        }
    }
}
