using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChurrasTrinca.Models
{
    public class Participante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [StringLength(100, ErrorMessage = "O {0} deve ter entre {2} a {1} caracteres.", MinimumLength = 3)]
        [Display(Name = ("Descrição"))]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Valor estima. de churras é obrigatório!")]
        [Display(Name = "Valor de contrib. de churras")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal ValorContribuicaoChurras { get; set; }

        [Required(ErrorMessage = "O campo Valor de contri. de bebidas é obrigatório!")]
        [Display(Name = "Valor de contrib. de bebidas")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal ValorContribuicaoBebidas { get; set; }

        [Display(Name = "O participante pagou?")]
        public bool ParticipantePagou { get; set; }

        [ForeignKey("Churrasco")]
        public int ChurrascoId { get; set; }

        public Churrasco? Churrasco { get; set; }

        public decimal GetContribuicaoSugerida()
        {
            int quantidadeEstimativaPessoas = Churrasco.EstimativaPessoas;
            int quantidadeParticipantesPagConfirmado = Churrasco.Participantes.Where(p => p.ParticipantePagou).Count();
            
            return quantidadeEstimativaPessoas - quantidadeParticipantesPagConfirmado;
        }
    }
}
