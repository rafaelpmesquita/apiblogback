using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Business;

namespace api.Controllers
{
    [Route("v1/auth")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UsuarioService _usuarioService;

        public AuthController(IConfiguration configuration, UsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
            {
                return BadRequest("Dados inválidos");
            }

            // naive auth: find by email and compare hashed password via service layer
            // NOTE: original model hashes on set; here we re-use hashing approach
            var usuarios = _usuarioService.ListarTodos();
            foreach (var u in usuarios)
            {
                // Usuario.senha getter returns string of hash; we compare with new hash of input
                var inputHash = request.Senha.Trim().GetHashCode().ToString();
                if (string.Equals(u.Email, request.Email.Trim(), StringComparison.OrdinalIgnoreCase)
                    && string.Equals(u.senha, inputHash, StringComparison.Ordinal))
                {
                    var token = GenerateJwt(u.Email);
                    return Ok(new { token });
                }
            }

            return Unauthorized();
        }

        private string GenerateJwt(string email)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"]));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}


