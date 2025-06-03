using ProjetoFiap.Domain.Entities;
using ProjetoFiap.Domain.Interfaces;
using ProjetoFiap.Infrastructure.Data;
using System;
using System.Linq;

namespace ProjetoFiap.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public Usuario ObterPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public Usuario ObterPorId(Guid id)
        {
            return _context.Usuarios.Find(id);
        }
    }
}