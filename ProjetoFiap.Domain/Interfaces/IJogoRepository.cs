using ProjetoFiap.Domain.Entities;

namespace ProjetoFiap.Domain.Interfaces
{
    public interface IJogoRepository
    {
        void Adicionar(Jogo jogo);
        IEnumerable<Jogo> ObterTodos();
        Jogo ObterPorId(Guid id);
    }
}
