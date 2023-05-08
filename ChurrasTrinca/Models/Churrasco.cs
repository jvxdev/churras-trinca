using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChurrasTrinca.Models
{
    public class Churrasco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        [StringLength(100, ErrorMessage = "O {0} deve ter entre {2} a {1} caracteres.", MinimumLength = 3)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório!")]
        [StringLength(255, ErrorMessage = "A {0} deve ter entre {2} a {1} caracteres.", MinimumLength = 3)]
        [Display(Name = ("Descrição"))]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "O campo Valor sugerido (R$) é obrigatório!")]
        [Display(Name = ("Valor sugerido (R$)"))]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal ValorSugerido { get; set; }

        [Required(ErrorMessage = "O campo Data é obrigatório!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM}")]
        public DateTime? Data { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal? ValorArrecadado { get; set; }

        public int? TotalParticipantes { get; set; }

        public virtual IList<Participante>? Participantes { get; set; }
    }
}
