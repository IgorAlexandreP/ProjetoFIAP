using System;

namespace ProjetoFiap.Domain.Entities
{
    public class UsuarioJogo
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid JogoId { get; set; }
    }
}
