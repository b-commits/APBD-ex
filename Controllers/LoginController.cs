using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public IConfiguration Configuration;
        private IStudentsDbService _service;
        public LoginController(IConfiguration configuration, IStudentsDbService service)
        {
            Configuration = configuration;
            _service = service;
        }
        [HttpPost]
        public IActionResult Login(LoginRequestDTO request)
        {
            if (!_service.AuthorizeStudent(request.Login, request.Password))
            {
                return StatusCode(403);
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, request.Login)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                issuer: "School",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
                );

            var RefreshToken = Guid.NewGuid();
            _service.SetRefreshToken(RefreshToken.ToString(), request.Login);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = RefreshToken
            });
        }

        
        [HttpPost("refresh-token/{refreshToken}")]
        public IActionResult RefreshToken(LoginRequestDTO request, string refreshToken)
        {
            if (_service.CheckRefreshToken(refreshToken, request.Login))
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, request.Login)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken
                (
                    issuer: "School",
                    audience: "Students",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds
                );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            return StatusCode(401);
        }




    }
}