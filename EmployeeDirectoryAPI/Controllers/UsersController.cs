﻿using EmployeeDirectoryAPI.Models;
using EmployeeDirectoryAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static EmployeeDirectoryAPI.Services.UserService;

namespace EmployeeDirectoryAPI.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userSevice)
        {
            _userService = userSevice;
        }

        [HttpGet("Users")]
        public async Task<ActionResult<User>> Get()
        {
            var result = await _userService.GetAsync();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("Users/Register")]

        public async Task<ActionResult<User>> Register([FromBody] LoginDTO login)
        {
            if (login.email == null)
            {
                return BadRequest(new { message = "Email is required" });
            }

            if (login.password == null)
            {
                return BadRequest(new { message = "Password is required" });
            }

            if (await _userService.IsEmailTaken(login.email))
            {
                return BadRequest(new { message = "Email is already taken" });
            }

            var result = await _userService.Register(login);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(new { message = "User Registered Succesfully" });


        }

        [HttpPost("Users/login")]

        public async Task<ActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await _userService.Login(login);

            if (user == null)
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString())
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }
    }
}
