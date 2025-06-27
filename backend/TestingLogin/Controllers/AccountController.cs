using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestingLogin.Models;

namespace TestingLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpGet("me")]
        [Authorize] // ✅ Requires valid token

        public async Task<IActionResult> GetUserProfile()
        {
            // DEBUG: Show claims
            var claims = User.Claims.ToDictionary(c => c.Type, c => c.Value);
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(claims));


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                 User.FindFirstValue(JwtRegisteredClaimNames.Sub);


            if (userId == null)
                return Unauthorized("User ID not found in token.");

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound("User not found");

            return Ok(new
            {
                id = user.Id,
                username = user.UserName,
                email = user.Email
            });
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            if (string.IsNullOrEmpty(model.Email) || !new EmailAddressAttribute().IsValid(model.Email))
            {
                return BadRequest(new { message = "Invalid email address format." });
            }

            // Validate the password requirements (you can adjust these as needed)
            if (string.IsNullOrEmpty(model.Password) || model.Password.Length < 6)
            {
                return BadRequest(new { message = "Password must be at least 6 characters long." });
            }
            var checking =await _userManager.FindByEmailAsync(model.Email);
            if (checking.Email == user.Email)
            {
                return BadRequest(new { message = "This email is already exists." });
            }
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Generate token immediately after registration (same as login)
                var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id), // <- this is the user's Id

        };

                // Add any default roles if needed
                // await _userManager.AddToRoleAsync(user, "User");

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                        SecurityAlgorithms.HmacSha256)
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    token = tokenString,
                    data = new
                    {
                        username = user.UserName,
                        email = user.Email,
                    }
                });
            }

            return BadRequest(result.Errors);

        }
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] Login model)
        //{
        //    var user = await _userManager.FindByEmailAsync(model.Email) ?? await _userManager.FindByNameAsync(model.Username);
        //    if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        var userRoles = await _userManager.GetRolesAsync(user);
        //        var authClaims = new List<Claim>
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub,user.UserName!),
        //            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
        //            new Claim(ClaimTypes.NameIdentifier, user.Id), // <- this is the user's Id

        //        };
        //        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
        //        var token = new JwtSecurityToken(
        //            issuer: _configuration["Jwt:Issuer"],
        //            expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
        //            claims: authClaims,
        //            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
        //            SecurityAlgorithms.HmacSha256
        //            )
        //            );
        //        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        //        // 🎯 Return token and user info
        //        return Ok(new
        //        {
        //            token = tokenString,
        //            data = new
        //            {
        //                username = user.UserName,
        //                email = user.Email,
        //            }

        //        });
        //        // return Ok(new {Token=new JwtSecurityTokenHandler().WriteToken(token)});
        //    }
        //    return Unauthorized();
        //}
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            // First, check by Email
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest(new { message = "This email is not registered." });
            }

            // Now check the password
            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return BadRequest(new { message = "Incorrect password. Please try again." });
            }

            // If valid, generate token
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.NameIdentifier, user.Id),
    };

            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                    SecurityAlgorithms.HmacSha256
                )
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                data = new
                {
                    username = user.UserName,
                    email = user.Email,
                }
            });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? Ok() : BadRequest(result.Errors);
        }
    }
}
