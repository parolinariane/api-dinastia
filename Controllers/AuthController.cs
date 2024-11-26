using Microsoft.AspNetCore.Mvc;
using SistemaCadastroLogin.Data;
using SistemaCadastroLogin.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaCadastroLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Rota de Cadastro
        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastro([FromBody] Usuario usuario)
        {
            try
            {
                if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
                {
                    return BadRequest("Email já cadastrado");
                }

                // Se o email do admin for fornecido, defina como 'Admin', senão, 'Viewer'
                if (usuario.Email == "admin@admin.com") // Exemplo de login de administrador
                {
                    usuario.Role = "Admin";
                }
                else
                {
                    usuario.Role = "Viewer"; // Padrão para todos os outros usuários
                }

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Usuário cadastrado com sucesso" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar usuário");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        // Rota de Login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            var usuarioEncontrado = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == usuario.Email && u.Password == usuario.Password);

            if (usuarioEncontrado == null)
            {
                return Unauthorized("Credenciais inválidas");
            }

            // Retornar as informações de login, incluindo a Role (Admin ou Viewer)
            return Ok(new 
            { 
                message = "Login bem-sucedido",
                role = usuarioEncontrado.Role // Enviar o papel do usuário
            });
        }

        // Rota para obter o nome do usuário pelo email
        [HttpGet("nome")]
        public async Task<IActionResult> ObterNomeUsuario([FromQuery] string email)
        {
            try
            {
                // Procurar o usuário pelo email no banco de dados
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                // Retornar apenas o nome do usuário
                return Ok(new { nome = usuario.Name });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter nome do usuário");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        // Rota para obter a Role do usuário pelo email
        [HttpGet("role")]
        public async Task<IActionResult> ObterRoleUsuario([FromQuery] string email)
        {
            try
            {
                // Procurar o usuário pelo email no banco de dados
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

                if (usuario == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                // Retornar apenas a role do usuário
                return Ok(new { role = usuario.Role });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a role do usuário");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        
    }
}
