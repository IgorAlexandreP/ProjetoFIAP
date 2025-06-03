using Microsoft.AspNetCore.Mvc;
using ProjetoFiap.Application.Services;
using ProjetoFiap.Domain.Entities;

namespace ProjetoFiap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            try
            {
                _usuarioService.RegistrarUsuario(usuario);
                return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            var usuario = _usuarioService.ObterPorId(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }
    }
}