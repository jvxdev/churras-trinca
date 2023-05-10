using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChurrasTrinca.Models
{
    public class Participante
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [StringLength(100, ErrorMessage = "O {0} deve ter entre {2} a {1} caracteres.", MinimumLength = 3)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Valor de contrib. de churras é obrigatório!")]
        [Display(Name = "Valor de contrib. de churras")]
        public decimal ValorContribuicaoChurras { get; set; }

        [Required(ErrorMessage = "O campo Valor de contrib. de bebidas é obrigatório!")]
        [Display(Name = "Valor de contrib. de bebibas")]
        public decimal ValorContribuicaoBebidas { get; set; }

        [Display(Name = "Participante está cofirmado?")]
        public bool ParticipanteConfirmado { get; set; }

        [ForeignKey("Churrasco")]
        [Display(Name = "Churrasco")]
        public int ChurrascoId { get; set; }

        public Churrasco? Churrasco { get; set; }
    }
}
