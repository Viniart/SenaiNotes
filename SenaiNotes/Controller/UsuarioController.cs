using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.DTO;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.Services;

namespace SenaiNotes.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_usuarioRepository.ListarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            return Ok(_usuarioRepository.ObterPorId(id));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto login)
        {
            var usuario = _usuarioRepository.BuscarPorEmailSenha(login.Email, login.Senha);

            if (usuario == null)
            {
                return Unauthorized("Email ou Senha invalidos!");
            }

            var tokenService = new TokenService();

            var token = tokenService.GenerateToken(usuario.EmailUsuario);

            return Ok(new
            {
                token,
                usuario
            });
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(Usuario usuario)
        {
            _usuarioRepository.CadastrarUsuario(usuario);

            return Created();
        }
    }
}
