using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChurrasTrinca.Data;
using ChurrasTrinca.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ChurrasTrinca.Pages.Participantes
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public Participante Participante { get; set; }

        public Churrasco? Churrasco { get; set; }

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet(int churrasId)
        {
            ViewData["ChurrascoId"] = new SelectList(_context.Churrascos.Where(c => c.Id == churrasId), "Id", "Nome");

            var churrasco = await _context.Churrascos.FirstOrDefaultAsync(m => m.Id == churrasId);

            //CALCULA VALOR ESTIMADO DE CHURRAS + BEBIDAS
            var valorEstimadoTotal = churrasco.ValorEstimadoChurrasco + churrasco.ValorEstimadoBebida;

            //PEGA QUANT. PARTICIPANTES QUE JÁ FORAM CONFIRMADOS
            var participantesConfirmados = _context.Participantes.Where(p => p.ChurrascoId == churrasId && p.ParticipanteConfirmado).Count();

            //PEGA A QUANT. ESTIMADA DE PESSOAS
            var estimativaParticipantesTotal = churrasco.EstimativaPessoas + participantesConfirmados;

            //CALCULO O VALOR SUGERIDO DIVIDINDO O VALOR ESTIMADO TOTAL PELA ESTIMATIVA DE PESSOAS +
            //OS PARTICIPANTES JÁ CONFIRMADOS, DIVIDE POR DOIS E ARREDONDA VALOR PARA 2 CASAS DECIMAIS
            var valorSugerido = Math.Round(valorEstimadoTotal / estimativaParticipantesTotal / 2, 2);

            ViewData["ValorSugerido"] = valorSugerido;

            Churrasco = churrasco;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Participantes == null || Participante == null)
                return Page();

            _context.Participantes.Add(Participante);

            TempData["Msg"] = "O participante foi convidado com sucesso!";

            //PEGA O ID DO CHURRASCO DO PARTICIPANTE QUE ACABOU DE SER REGISTRADO
            var churrasId = Participante.ChurrascoId;

            var churrasco = await _context.Churrascos.FirstOrDefaultAsync(c => c.Id == churrasId);

            //CALCULA O VALOR DE CONTRIB. EM CHURRAS E BEBIDAS
            var valorTotal = Participante.ValorContribuicaoChurras + Participante.ValorContribuicaoBebidas;

            //ACRESCENTA AO CHURRASCO O VALOR TOTAL DA CONTRIBUIÇÃO DO PARTICIPANTE (CHURRAS + BEBIDAS)
            if (Participante.ParticipanteConfirmado)
                churrasco.ValorTotalArrecadado += valorTotal;

            await _context.SaveChangesAsync();

            ViewData["ChurrasId"] = churrasId;

            return Redirect($"../Churrascos/Edit?id={churrasId}");
        }
    }
}
