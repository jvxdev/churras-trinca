using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChurrasTrinca.Data;
using ChurrasTrinca.Models;
using Microsoft.AspNetCore.Authorization;

namespace ChurrasTrinca.Pages.Participantes
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ChurrasTrinca.Data.AppDbContext _context;

        public DeleteModel(ChurrasTrinca.Data.AppDbContext context)
        {
            _context = context;
        }

        public Churrasco? Churrasco { get; set; }

        [BindProperty]
        public Participante Participante { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? churrasId)
        {
            if (id == null || _context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FirstOrDefaultAsync(m => m.Id == id);

            var churrasco = await _context.Churrascos.FirstOrDefaultAsync(c => c.Id == churrasId);

            if (participante == null)
            {
                return NotFound();
            }
            else 
            {   
                Participante = participante;

                Churrasco = churrasco;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Participantes == null)
            {
                return NotFound();
            }

            var participante = await _context.Participantes.FindAsync(id);

            if (participante != null)
            {
                Participante = participante;

                var participanteList = await _context.Participantes.Where(c => c.Id == id).ToListAsync();

                //CALCULA O VALOR DE CONTRIB. EM CHURRAS E BEBIDAS
                var valorTotal = participante.ValorContribuicaoChurras + participante.ValorContribuicaoBebidas;

                var churrasco = await _context.Churrascos.FirstOrDefaultAsync(c => c.Id == Participante.ChurrascoId);

                //ARMAZENA O VALOR TOTAL DA CONTRIBUIÇÃO SUBTRAINDO DO VALOR TOTAL QUE JÁ ESTAVA ARMAZENADO
                churrasco.ValorTotalArrecadado = churrasco.ValorTotalArrecadado - valorTotal;

                _context.Participantes.Remove(Participante);

                await _context.SaveChangesAsync();
            }

            TempData["Msg"] = "O participante foi removido com sucesso!";

            return Redirect($"../Churrascos/Edit?id={Participante.ChurrascoId}");
        }
    }
}
