using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProjetoFiap.Application.Services;
using ProjetoFiap.Domain.Entities;
using System;

namespace ProjetoFiap.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class JogosController : ControllerBase
    {
        private readonly JogoService _jogoService;

        public JogosController(JogoService jogoService)
        {
            _jogoService = jogoService;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Adicionar([FromBody] Jogo jogo)
        {
            jogo.Id = Guid.NewGuid();
            _jogoService.AdicionarJogo(jogo);
            return CreatedAtAction(nameof(ObterPorId), new { id = jogo.Id }, jogo);
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var jogos = _jogoService.ListarTodos();
            return Ok(jogos);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            var jogo = _jogoService.ObterPorId(id);
            if (jogo == null) return NotFound();
            return Ok(jogo);
        }
    }
}