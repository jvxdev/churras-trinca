using System.ComponentModel.DataAnnotations.Schema;

namespace ChurrasTrinca.Models
{
    public class Participante
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        public decimal ValorContribuicaoChurras { get; set; }

        public decimal ValorContribuicaoBebidas { get; set; }

        public bool ParticipantePagou { get; set; }

        [ForeignKey("Churrasco")]
        public int ChurrascoId { get; set; }

        public Churrasco? Churrasco { get; set; }
    }
}
