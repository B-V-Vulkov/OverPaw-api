﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using OverPaw.Models;
using OverPaw.Services.Contracts;

namespace OverPaw.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly IUserService userService;

        public TestController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var user = new TestUser()
            {
                Name = "Pesho",
            };

            var test = await userService.GetAll();
            return Ok(test);
        }
    }
}
