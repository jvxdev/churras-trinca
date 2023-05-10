using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChurrasTrinca.Data;
using ChurrasTrinca.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChurrasTrinca.Pages.Participantes
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly ChurrasTrinca.Data.AppDbContext _context;

        public EditModel(ChurrasTrinca.Data.AppDbContext context)
        {
            _context = context;
        }

        public Churrasco? Churrasco { get; set; } = default!;

        [BindProperty]
        public Participante Participante { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? churrasId)
        {
            if (id == null || _context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FirstOrDefaultAsync(m => m.Id == id);

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

            if (participante == null)
            {
                return NotFound();
            }

            Participante = participante;

            Churrasco = churrasco;

            ViewData["ChurrascoId"] = new SelectList(_context.Churrascos, "Id", "Nome");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var participantes = await _context.Participantes.Where(c => c.Id == Participante.Id).Include(x => x.Churrasco).ToListAsync();

            decimal valorChurrasOld = participantes.Select(p => p.ValorContribuicaoChurras).FirstOrDefault();

            decimal valorBebidaOld = participantes.Select(p => p.ValorContribuicaoBebidas).FirstOrDefault();

            _context.Attach(Participante).State = EntityState.Modified;

            var churrasco = await _context.Churrascos.FirstOrDefaultAsync(c => c.Id == Participante.ChurrascoId);

            try
            {
                decimal valorTotal = 0;

                if (participantes != null)
                {
                    //INCREMENTA O VALOR DE CONTRIB. DE CHURRAS E BEBIDAS DE ACORDO COM A QUANT. DE PARTICIPANTES DO CHURRASCO
                    foreach (var participante in participantes)
                        valorTotal = valorTotal += participante.ValorContribuicaoChurras + participante.ValorContribuicaoBebidas;
                }

                decimal valorTotalOld = valorChurrasOld + valorBebidaOld;

                churrasco.ValorTotalArrecadado -= valorTotalOld;

                if (Participante.ParticipanteConfirmado)
                    churrasco.ValorTotalArrecadado += valorTotal;
                else
                    churrasco.ValorTotalArrecadado -= valorTotal;

                await _context.SaveChangesAsync();

                TempData["Msg"] = "O participante foi editado com sucesso!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteExists(Participante.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Churrasco = churrasco;

            return Redirect($"../Churrascos/Edit?id={Churrasco.Id}");
        }

        private bool ParticipanteExists(int id)
        {
            return (_context.Participantes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
