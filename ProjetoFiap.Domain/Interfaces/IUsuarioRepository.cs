using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetoFiap.Domain.Entities;

namespace ProjetoFiap.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario ObterPorEmail(string email);
        void Adicionar(Usuario usuario);
        Usuario ObterPorId(Guid id);
    }
}
