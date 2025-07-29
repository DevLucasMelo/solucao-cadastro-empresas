using BCrypt.Net;
using CadastroEmpresasApp.Data;
using CadastroEmpresasApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CadastroEmpresasApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuariosController> _logger; 

        public UsuariosController(ApplicationDbContext context, IConfiguration configuration, ILogger<UsuariosController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioRegisterDto dto)
        {
            try
            {
                if (await _context.Usuarios.AnyAsync(u => u.usu_email == dto.Email))
                    return BadRequest("Email já cadastrado.");

                var usuario = new Usuario
                {
                    usu_nome = dto.Nome,
                    usu_email = dto.Email,
                    usu_senha_hash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok("Usuário registrado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o registro de usuário.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
        {
            try
            {
                var usuario = await _context.Usuarios.SingleOrDefaultAsync(u => u.usu_email == dto.Email);
                if (usuario == null)
                {
                    return Unauthorized("Email ou senha inválidos.");
                }

                bool senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.usu_senha_hash);
                if (!senhaValida)
                {
                    return Unauthorized("Email ou senha inválidos.");
                }

                var secret = _configuration["JwtSecret"];
                if (string.IsNullOrEmpty(secret))
                {
                    return StatusCode(500, "Chave JWT não configurada.");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.usu_id.ToString()),
                        new Claim(ClaimTypes.Email, usuario.usu_email),
                        new Claim(ClaimTypes.Name, usuario.usu_nome)
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { Token = tokenString });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o login.");
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }

        public class UsuarioRegisterDto
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        public class UsuarioLoginDto
        {
            public string Email { get; set; }
            public string Senha { get; set; }
        }
    }
}
