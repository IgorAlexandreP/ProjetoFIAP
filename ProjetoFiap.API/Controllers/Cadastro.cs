using Microsoft.AspNetCore.Mvc;
using ProjetoFiap.Application.Services;
using ProjetoFiap.Domain.Entities;
using System;

namespace ProjetoFiap.API.Controllers
{
    public class CadastroController : Controller
    {
        private readonly UsuarioService _usuarioService;
        public CadastroController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string nome, string email, string senha, string role)
        {
            try
            {
                var usuario = new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nome = nome,
                    Email = email,
                    Senha = senha,
                    Role = role
                };

                _usuarioService.RegistrarUsuario(usuario);

                var salvo = _usuarioService.ObterPorId(usuario.Id);
                if (salvo != null)
                    ViewBag.Mensagem = $"Cadastrado com sucesso! ID: {salvo.Id}";
            }
            catch (ArgumentException ex)
            {
                ViewBag.Erro = ex.Message;
            }

            return View();
        }
    }
}