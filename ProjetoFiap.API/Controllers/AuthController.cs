using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjetoFiap.Application.Services;
using ProjetoFiap.Domain.Entities;

namespace ProjetoFiap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public AuthController(UsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _usuarioService.ObterPorEmail(request.Email);
            if (user == null || user.Senha != request.Password)
                return Unauthorized("Credenciais inválidas.");

            var jwtSettings = _configuration.GetSection("JwtSettings");

            var secretKey = jwtSettings.GetValue<string>("SecretKey");
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new InvalidOperationException("JWT SecretKey não configurada em appsettings.json");

            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");
            var expiryMinutes = jwtSettings.GetValue<int>("ExpiryMinutes");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds);

            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
