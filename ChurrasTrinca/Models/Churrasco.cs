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

        [Required(ErrorMessage = "O campo Valor estima. de churrasco é obrigatório!")]
        [Display(Name = "Valor estima. de churras")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal ValorEstimadoChurrasco { get; set; }

        [Required(ErrorMessage = "O campo Valor estima. de bebidas é obrigatório!")]
        [Display(Name = "Valor estima. de bebidas")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DataType(DataType.Currency)]
        public decimal ValorEstimadoBebida { get; set; }

        [Required(ErrorMessage = "O campo Estima. de pessoas é obrigatório!")]
        [Display(Name = "Estimativa de pessoas")]
        public int EstimativaPessoas { get; set; }

        [Required(ErrorMessage = "O campo Data é obrigatório!")]
        [DisplayFormat(DataFormatString = "{0:dd/MM}")]
        public DateTime? Data { get; set; }

        private ICollection<Participante> _participantes;

        public ICollection<Participante> Participantes
        {
            get
            {
                return _participantes;
            }
            set
            {
                _participantes = value;

                GetContribuicaoTotal();
            }
        }

        public decimal ValorContribuicaoChurras { get; set; }

        public decimal ValorContribuicaoBebidas { get; set; }

        public decimal ValorContribuicaoTotal
        {
            get
            {
                return ValorContribuicaoChurras + ValorContribuicaoBebidas;
            }
        }

        public decimal ValorContribuicaoRestante
        {
            get
            {
                return ValorEstimadoChurrasco + ValorEstimadoBebida - ValorContribuicaoTotal;
            }
        }

        private void GetContribuicaoTotal() 
        {
            foreach (var participante in Participantes) 
            {
                ValorContribuicaoChurras += participante.ValorContribuicaoChurras;

                ValorContribuicaoBebidas += participante.ValorContribuicaoBebidas;
            }
        }
    }
}
