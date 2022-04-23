using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VaccinationSystemApi.Configuration;
using VaccinationSystemApi.Dtos.Login;
using VaccinationSystemApi.Repositories;

namespace VaccinationSystemApi.Controllers
{
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly VaccinationSystemRepository _vaccinationService;

        public DefaultController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            VaccinationSystemRepository vaccinationService
            )
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _vaccinationService = vaccinationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if (ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByEmailAsync(registerRequest.Mail);

                if (existingUser != null)
                {
                    return BadRequest("Unrecognised data format");
                }

                var newUser = new IdentityUser() { Email = registerRequest.Mail, UserName = registerRequest.PESEL };
                var isCreated = await _userManager.CreateAsync(newUser, registerRequest.Password);
                var isCreatedInPatientTable = _vaccinationService.CreatePatient(registerRequest);

                bool creationSuccess = isCreated.Succeeded && isCreatedInPatientTable;

                if (creationSuccess) //password required bunch of stuff: alpha, upper, digit, nonalphanumeric
                {
                    var jwtToken = GenerateJwtToken(newUser);

                    return Ok(jwtToken);
                }
                else
                {
                    return BadRequest("Unrecognised data format");
                }
            }

            return BadRequest("Unrecognised data format");
        }

        [HttpPost("signin")]
        public async Task<ActionResult<SignInResponse>> SignIn(SignInRequest signInRequest)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(signInRequest.Mail);

                if (existingUser == null)
                {
                    return BadRequest("Unrecognised data format");
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, signInRequest.Password);

                if (!isCorrect)
                {
                    return BadRequest("Unrecognised data format");
                }

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new SignInResponse()
                {
                    UserId = existingUser.Id,
                    UserType = "patient", // for now hardcoded patient,
                    Jwt = jwtToken,
                });
            }

            return BadRequest("Unrecognised data format");
        }

        [HttpGet("/user/logout/{userId}")]
        public void Logout(int userId)
        {
            // empty
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
