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
using VaccinationSystemApi.Repositories.Interfaces;

namespace VaccinationSystemApi.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;
        private readonly IVaccinationSystemRepository _vaccinationService;

        public LoginController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IVaccinationSystemRepository vaccinationService,
            RoleManager<IdentityRole> roleManager
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
                var isCreatedInPatientTable = _vaccinationService.CreatePatient(registerRequest, Guid.Parse(newUser.Id));

                bool creationSuccess = isCreated.Succeeded && isCreatedInPatientTable;

                if (creationSuccess)
                {
                    var result = await _userManager.AddToRoleAsync(newUser, "Patient");

                    var jwtToken = await GenerateJwtToken(newUser);

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
                    Jwt = jwtToken.Result,
                });
            }

            return BadRequest("Unrecognised data format");
        }

        [HttpGet("/user/logout/{userId}")]
        public void Logout(int userId)
        {
            // empty
        }

        private async Task<string> GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var claims = await GetAllValidClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        private async Task<List<Claim>> GetAllValidClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Getting the claims that we have assigned to the user
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            // Get the user role and add it to the claims
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(userRole);

                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));

                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    foreach (var roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim);
                    }
                }
            }

            return claims;
        }
    }
}
