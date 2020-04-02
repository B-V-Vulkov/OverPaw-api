using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using OverPaw.Services.Contracts;

namespace OverPaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly IAccountService userService;

        public TestController(IAccountService userService)
        {
            this.userService = userService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            return Ok(this.User.Identity.Name);
        }
    }
}
