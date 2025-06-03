using ProjetoFiap.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ProjetoFiap.Domain.Interfaces
{
    public interface IUsuarioJogoRepository
    {
        void Adicionar(UsuarioJogo usuarioJogo);
        IEnumerable<UsuarioJogo> ObterPorUsuarioId(Guid usuarioId);
        bool ExisteAssociacao(Guid usuarioId, Guid jogoId);
    }
}
