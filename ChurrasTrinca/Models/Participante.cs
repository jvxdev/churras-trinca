using System.ComponentModel.DataAnnotations.Schema;

namespace ChurrasTrinca.Models
{
    public class Participante
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public decimal ValorContribuicao { get; set; }

        public bool ParticipanteConfirmado { get; set; }

        [ForeignKey("Churrasco")]
        public int ChurrascoId { get; set; }

        public Churrasco? Churrasco { get; set; }
    }
}
