using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChurrasTrinca.Models
{
    public class Participante
    {
        public int Id { get; set; }

        public string? Nome { get; set; }

        [Display(Name = "Valor de contrib. de churras")]
        public decimal ValorContribuicaoChurras { get; set; }

        [Display(Name = "Valor de contrib. de bebibas")]
        public decimal ValorContribuicaoBebidas { get; set; }

        [Display(Name = "Participante está cofirmado?")]
        public bool ParticipanteConfirmado { get; set; }

        [ForeignKey("Churrasco")]
        [Display(Name = "Churrasco")]
        public int ChurrascoId { get; set; }

        public decimal ValorContribuicaoTotal => ValorContribuicaoChurras + ValorContribuicaoBebidas;

        public Churrasco? Churrasco { get; set; }

        public decimal GetContribuicaoSugerida()
        {
            int quantidadeEstimativaPessoas = Churrasco.EstimativaPessoas;
            int quantidadeParticipantesPagConfirmado = Churrasco.Participantes.Where(p => p.ParticipanteConfirmado).Count();
            
            return quantidadeEstimativaPessoas - quantidadeParticipantesPagConfirmado;
        }
    }
}
