using ProjetoFiap.Domain.Entities;
using ProjetoFiap.Domain.Interfaces;

namespace ProjetoFiap.Application.Services
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        public void AdicionarJogo(Jogo jogo)
        {
            _repository.Adicionar(jogo);
        }

        public IEnumerable<Jogo> ListarTodos()
        {
            return _repository.ObterTodos();
        }

        public Jogo ObterPorId(Guid id)
        {
            return _repository.ObterPorId(id);
        }
    }
}
