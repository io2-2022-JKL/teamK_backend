using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using VaccinationSystemApi.Dtos.Login;

namespace VaccinationSystemApi.Controllers
{
    [ApiController]
    public class DefaultController : ControllerBase
    {

        [HttpPost("register")]
        public void Register(RegisterRequest registerRequest)
        {

        }

        [HttpPost("signin")]
        public void SignIn(SignInRequest signInRequest)
        {

        }

        [HttpGet("/user/logout/{userId}")]
        public void Logout(int userId)
        {

        }
    }
}
