using ProjetoFiap.Domain.Entities;
using ProjetoFiap.Domain.Interfaces;
using ProjetoFiap.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoFiap.Infrastructure.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly AppDbContext _context;

        public JogoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Jogo jogo)
        {
            _context.Set<Jogo>().Add(jogo);
            _context.SaveChanges();
        }

        public IEnumerable<Jogo> ObterTodos()
        {
            return _context.Set<Jogo>().ToList();
        }

        public Jogo ObterPorId(Guid id)
        {
            return _context.Set<Jogo>().Find(id);
        }
    }
}