using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjetoFiap.Application.Services;
using System.Security.Claims;

namespace ProjetoFiap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioJogoController : ControllerBase
    {
        private readonly UsuarioJogoService _usuarioJogoService;

        public UsuarioJogoController(UsuarioJogoService usuarioJogoService)
        {
            _usuarioJogoService = usuarioJogoService;
        }

        [HttpPost("{usuarioId:guid}/jogos/{jogoId:guid}")]
        public IActionResult Comprar(Guid usuarioId, Guid jogoId)
        {
            try
            {
                var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (claimUserId == null || Guid.Parse(claimUserId) != usuarioId)
                {
                    return Forbid();
                }

                _usuarioJogoService.ComprarJogo(usuarioId, jogoId);
                return Ok(new { Message = "Jogo comprado com sucesso." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{usuarioId:guid}/jogos")]
        public IActionResult ListarBiblioteca(Guid usuarioId)
        {
            var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claimUserId == null || Guid.Parse(claimUserId) != usuarioId)
            {
                return Forbid();
            }

            var lista = _usuarioJogoService.ListarJogosDoUsuario(usuarioId);
            return Ok(lista);
        }
    }
}
