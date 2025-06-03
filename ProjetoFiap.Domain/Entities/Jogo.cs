using System;

namespace ProjetoFiap.Domain.Entities
{
    public class Jogo
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
    }
}

