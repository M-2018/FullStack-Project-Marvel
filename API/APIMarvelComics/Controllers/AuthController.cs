using APIMarvelComics.Data;
using APIMarvelComics.DTOs;
using APIMarvelComics.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Cryptography;

namespace APIMarvelComics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequestDTO request)
        {
            var usuarioExistente = _context.Usuarios.FirstOrDefault(u => u.Email == request.Email);

            if (usuarioExistente != null)
                return BadRequest("El usuario ya existe");

            var usuario = new Usuario
            {
                Email = request.Email,
                NombreCompleto = request.NombreCompleto,
                NumeroIdentificacion = request.NumeroIdentificacion,
                Password = _passwordHasher.HashPassword(null, request.Password)  
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return Ok("Usuario registrado con éxito");
        }

        [HttpPost("login")]
        public IActionResult Login(AuthRequestDTO request)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == request.Email);

            if (usuario == null)
                return Unauthorized("Usuario o contraseña incorrectos");

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, request.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                return Unauthorized("Usuario o contraseña incorrectos");

            var token = GenerateJwtToken(usuario);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                Usuario = new UsuarioDTO
                {
                    NombreCompleto = usuario.NombreCompleto,
                    Email = usuario.Email
                }
            });
        }
 
        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Email, usuario.Email),
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [ApiController]
        [Route("api/[controller]")]
        public class DebugController : ControllerBase
        {
            [HttpGet("claims")]
            [Authorize]
            public IActionResult GetClaims()
            {
                var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
                return Ok(claims);
            }
        }

        [HttpPost("validate-token")]
        public IActionResult ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(token, validationParameters, out _);
                return Ok("Token is valid");
            }
            catch (Exception ex)
            {
                return BadRequest($"Token validation failed: {ex.Message}");
            }
        }

        //Hasheo a MD5 (ts + privateKey + publicKey)
        [HttpPost("generate-md5-hash")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult GenerateMd5Hash(string ts, string privateKey, string publicKey)
        {
            if (string.IsNullOrEmpty(ts) || string.IsNullOrEmpty(privateKey) || string.IsNullOrEmpty(publicKey))
            {
                return BadRequest("Todos los parámetros son necesarios: ts, privateKey y publicKey.");
            }

            try
            {
                
                string concatenated = ts + privateKey + publicKey;

                
                using (MD5 md5 = MD5.Create())
                {
                    byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(concatenated));
                    string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                    return Ok(hash); 
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al generar el hash MD5: {ex.Message}");
            }
        }



    }
}
