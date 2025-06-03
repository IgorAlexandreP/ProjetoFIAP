using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjetoFiap.Application.Services;
using System.Security.Claims;

namespace ProjetoFiap.API.Controllers
{
    [Authorize]
    public class MeusJogosUIController : Controller
    {
        private readonly UsuarioJogoService _usuarioJogoService;

        public MeusJogosUIController(UsuarioJogoService usuarioJogoService)
        {
            _usuarioJogoService = usuarioJogoService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claimUserId == null)
                return Forbid();

            var usuarioId = Guid.Parse(claimUserId);
            var jogos = _usuarioJogoService.ListarJogosDoUsuario(usuarioId);
            return View(jogos);
        }
    }
}
