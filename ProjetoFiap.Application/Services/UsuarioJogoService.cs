using System;
using System.Collections.Generic;
using ProjetoFiap.Domain.Entities;
using ProjetoFiap.Domain.Interfaces;

namespace ProjetoFiap.Application.Services
{
    public class UsuarioJogoService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IJogoRepository _jogoRepo;
        private readonly IUsuarioJogoRepository _usuarioJogoRepo;

        public UsuarioJogoService(
            IUsuarioRepository usuarioRepo,
            IJogoRepository jogoRepo,
            IUsuarioJogoRepository usuarioJogoRepo)
        {
            _usuarioRepo = usuarioRepo;
            _jogoRepo = jogoRepo;
            _usuarioJogoRepo = usuarioJogoRepo;
        }

        public void ComprarJogo(Guid usuarioId, Guid jogoId)
        {
            var usuario = _usuarioRepo.ObterPorId(usuarioId);
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado.");

            var jogo = _jogoRepo.ObterPorId(jogoId);
            if (jogo == null)
                throw new ArgumentException("Jogo não encontrado.");

            if (_usuarioJogoRepo.ExisteAssociacao(usuarioId, jogoId))
                throw new InvalidOperationException("Usuário já possui este jogo.");

            var usuarioJogo = new UsuarioJogo
            {
                Id = Guid.NewGuid(),
                UsuarioId = usuarioId,
                JogoId = jogoId
            };
            _usuarioJogoRepo.Adicionar(usuarioJogo);
        }

        public IEnumerable<Jogo> ListarJogosDoUsuario(Guid usuarioId)
        {
            var listaAssoc = _usuarioJogoRepo.ObterPorUsuarioId(usuarioId);
            var jogos = new List<Jogo>();
            foreach (var assoc in listaAssoc)
            {
                var jogo = _jogoRepo.ObterPorId(assoc.JogoId);
                if (jogo != null)
                    jogos.Add(jogo);
            }
            return jogos;
        }
    }
}
