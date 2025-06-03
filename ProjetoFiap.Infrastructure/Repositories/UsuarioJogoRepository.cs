// ProjetoFiap.Infrastructure/Repositories/UsuarioJogoRepository.cs

using ProjetoFiap.Domain.Entities;
using ProjetoFiap.Domain.Interfaces;
using ProjetoFiap.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoFiap.Infrastructure.Repositories
{
    public class UsuarioJogoRepository : IUsuarioJogoRepository
    {
        private readonly AppDbContext _context;

        public UsuarioJogoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Adicionar(UsuarioJogo usuarioJogo)
        {
            _context.Set<UsuarioJogo>().Add(usuarioJogo);
            _context.SaveChanges();
        }

        public IEnumerable<UsuarioJogo> ObterPorUsuarioId(Guid usuarioId)
        {
            return _context.Set<UsuarioJogo>()
                           .Where(uj => uj.UsuarioId == usuarioId)
                           .ToList();
        }

        public bool ExisteAssociacao(Guid usuarioId, Guid jogoId)
        {
            return _context.Set<UsuarioJogo>()
                           .Any(uj => uj.UsuarioId == usuarioId && uj.JogoId == jogoId);
        }
    }
}
