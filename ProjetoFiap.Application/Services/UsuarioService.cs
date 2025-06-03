using ProjetoFiap.Domain.Entities;
using ProjetoFiap.Domain.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjetoFiap.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public void RegistrarUsuario(Usuario usuario)
        {
            if (!EmailEhValido(usuario.Email))
                throw new ArgumentException("E-mail inválido.");

            if (!SenhaEhSegura(usuario.Senha))
                throw new ArgumentException("Senha fraca. Deve ter ao menos 8 caracteres com letra, número e caractere especial.");

            _repository.Adicionar(usuario);
        }

        public Usuario ObterPorId(Guid id)
        {
            return _repository.ObterPorId(id);
        }

        public Usuario ObterPorEmail(string email)
        {
            return _repository.ObterPorEmail(email);
        }

        private bool EmailEhValido(string email)
        {
            return Regex.IsMatch(email, "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");
        }

        private bool SenhaEhSegura(string senha)
        {
            return senha.Length >= 8 &&
                   senha.Any(char.IsLetter) &&
                   senha.Any(char.IsDigit) &&
                   senha.Any(c => !char.IsLetterOrDigit(c));
        }
    }
}